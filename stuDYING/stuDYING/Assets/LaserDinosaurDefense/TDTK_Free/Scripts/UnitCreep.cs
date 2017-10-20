using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using TDTK;

namespace TDTK {

	public class UnitCreep : Unit {
		
		public delegate void DestinationHandler(UnitCreep unit);
		public static event DestinationHandler onDestinationE;
		
		public bool flying=false;
		
		public int waveID=0;
		public int lifeCost=1;
		public int scoreValue=1;
		
		public int lifeValue=0;
		public List<int> valueRscMin=new List<int>();
		public List<int> valueRscMax=new List<int>();
		public int valueEnergyGain=0;
		
		//public float flightHeight=1.5f;
		//private Vector3 flightHeightOffset;
		private Vector3 pathDynamicOffset;
		public Vector3 GetPathDynamicOffset(){ return pathDynamicOffset; }
		
		public override void Awake() {
			SetSubClass(this);
			
			base.Awake();
			
			//if(flying) flightHeightOffset=new Vector3(0, flightHeight, 0);
			
			if(thisObj.GetComponent<Collider>()==null){
				thisObj.AddComponent<SphereCollider>();
			}
		}

		public override void Start() {
			base.Start();

			hit = this.gameObject;
		}
		
		//parent unit is for unit which is spawned from destroyed unit
		public void Init(PathTD p, int ID, int wID){
			//for navigation using navmesh, not in use at all, require navmesh carving (unity-pro only) to work properly
			if(true){ InitNavMesh(p, ID, wID); return; }
			
			Init();
			
			path=p;
			instanceID=ID;
			waveID=wID;
			
			float dynamicX=Random.Range(-path.dynamicOffset, path.dynamicOffset);
			float dynamicZ=Random.Range(-path.dynamicOffset, path.dynamicOffset);
			pathDynamicOffset=new Vector3(dynamicX, 0, dynamicZ);
			thisT.position+=pathDynamicOffset;
			
			waypointID=1;
			waypointList=path.GetWaypointList();
			
			distFromDestination=CalculateDistFromDestination();
		}
		
		
		
		
		public override void Update() {
			base.Update();
		}
		
		
		
		public float rotateSpd=10;
		public float moveSpeed=3;
		
		public PathTD path;
		public List<Vector3> waypointList=new List<Vector3>();
		public int waypointID=1;
		
		
		public override void FixedUpdate(){
			base.FixedUpdate();

			//ADDED
			if (targetAcquired) {
				timer -= Time.deltaTime;
				enemyAttack ();
				if (hit.layer == 8)
					agent.SetDestination (agent.gameObject.transform.position);
			}

			if(!stunned && !dead){
				if(MoveToPoint(waypointList[waypointID])){
					waypointID+=1;
					if(waypointID>=path.GetPathWPCount()){
						ReachDestination();
					}
				}
			}
		}
		
		//function call to rotate and move toward a pecific point, return true when the point is reached
		public bool MoveToPoint(Vector3 point){
			//this is for dynamic waypoint, each unit creep have it's own offset pos
			//point+=dynamicOffset;
			point+=pathDynamicOffset;//+flightHeightOffset;
			
			float dist=Vector3.Distance(point, thisT.position);
			
			//if the unit have reached the point specified
			//~ if(dist<0.15f) return true;
			if(dist<0.005f) return true;
			
			//rotate towards destination
			if(moveSpeed>0){
				Quaternion wantedRot=Quaternion.LookRotation(point-thisT.position);
				thisT.rotation=Quaternion.Slerp(thisT.rotation, wantedRot, rotateSpd*Time.deltaTime);
			}
			
			//move, with speed take distance into accrount so the unit wont over shoot
			Vector3 dir=(point-thisT.position).normalized;
			//if(slowModifier!=1) Debug.Log("slow mod:"+slowModifier+"   "+currentMoveSpd * Time.deltaTime * slowModifier);
			thisT.Translate(dir*Mathf.Min(dist, moveSpeed * slowMultiplier * Time.fixedDeltaTime), Space.World);
			
			distFromDestination-=(moveSpeed * slowMultiplier * Time.fixedDeltaTime);
			
			return false;
		}
		
		
		
		void ReachDestination(){
			if(path.loop){
				if(onDestinationE!=null) onDestinationE(this);
				waypointID=path.GetLoopPoint();
				waypointList=path.GetWaypointList();
				return;
			}
			
			dead=true;
			
			if(onDestinationE!=null) onDestinationE(this);
			
			float delay=0;
			if(aniInstance!=null){ delay=aniInstance.PlayDestination(); }
			
			StartCoroutine(_ReachDestination(delay));
		}
		IEnumerator _ReachDestination(float duration){
			yield return new WaitForSeconds(duration);
			ObjectPoolManager.Unspawn(thisObj);
		}
		
		
		public float CreepDestroyed(){
			List<int> rscGain=new List<int>();
			for(int i=0; i<valueRscMin.Count; i++){
				rscGain.Add(Random.Range(valueRscMin[i], valueRscMax[i]));
			}
			ResourceManager.GainResource(rscGain);
			
			if(aniInstance!=null){ return aniInstance.PlayDead(); }
			
			return 0;
		}
		
		
		private UnitCreepAnimation aniInstance;
		public void SetAnimationComponent(UnitCreepAnimation ani){ aniInstance=ani; }
		public void Hit(){ if(aniInstance!=null) aniInstance.PlayHit(); }
		
		
		public float GetMoveSpeed(){ return moveSpeed * slowMultiplier; }
		
		public float distFromDestination=0;
		public float _GetDistFromDestination(){ return distFromDestination; }
		public float CalculateDistFromDestination(){
			float dist=Vector3.Distance(thisT.position, waypointList[waypointID]);
			for(int i=waypointID+1; i<waypointList.Count; i++){
				dist+=Vector3.Distance(waypointList[i-1], waypointList[i]);
			}
			dist+=path.GetPathDistance(waypointID+1);
			
			return dist;
		}
		
		

		//for unity-navmesh based navigation, not in used (reason: requird navmesh carving - pro only feature)
		private UnityEngine.AI.NavMeshAgent agent;
		private Vector3 tgtPos;
		public void InitNavMesh(PathTD p, int ID, int wID){
			Init();
			
			path=p;
			instanceID=ID;
			waveID=wID;
			
			tgtPos=path.wpList[path.wpList.Count-1].position;
			agent = thisObj.GetComponent<UnityEngine.AI.NavMeshAgent>();
			agent.SetDestination(tgtPos);
			
			StartCoroutine(CheckDestination());
		}
		IEnumerator CheckDestination(){
			while(!stunned && !dead){
				tgtPos.y=thisT.position.y;
				if(Vector3.Distance(tgtPos, thisT.position)<0.5f) ReachDestination();
				yield return null;
			}
		}


		//Added functionality
		public int damage;
		public float reloadTime;
		float timer = 0f;

		bool targetAcquired = false;
		GameObject hit;
		//NavMeshAgent agent;


		void OnTriggerEnter(Collider other){
			if (!targetAcquired || other.gameObject.layer == 8) {
				if (other.gameObject.layer == 9) { //Portal
					Debug.Log ("ROAAAAAARRR!");
					Destroy (other.gameObject);
				} else if (other.gameObject.layer == 29) { //Tower
					Debug.Log ("Dino Dan Roars at " + other.gameObject.name + "!");
					targetAcquired = true;
					//target = other.gameObject.GetComponent<HealthManager>();
					hit = other.gameObject;
					//Destroy (other.gameObject);
				} else if (other.gameObject.layer == 8) { //Ground Unit
					Debug.Log ("Dino Dan Roars at " + other.gameObject.name + "!");
					targetAcquired = true;
					hit = other.gameObject;
					//target = other.gameObject.GetComponent<HealthManager>();
					//Destroy (other.gameObject);
				}
			}
		}

		void OnTriggerExit(Collider other){
			if (other.gameObject == hit) {
				hit = this.gameObject;
				targetAcquired = false;
				timer = 0f;
			}
		}

		void enemyAttack(){
			if (timer <= 0) {
				Debug.Log ("Dino Dan attacks for " + damage + " damage!");
				timer = reloadTime;
				bool targetKilled = false;
				if (hit.layer == 29) {
					this.GetComponent<AudioSource> ().Play ();
					targetKilled = hit.GetComponent<UnitTower> ().takeDamage (damage);
				} else if (hit.layer == 8) {
					this.GetComponent<AudioSource> ().Play ();
					targetKilled = hit.GetComponent<HealthManager> ().takeDamage (damage);
				}if (targetKilled)
					targetAcquired = false;
			}
		}
	}
}
