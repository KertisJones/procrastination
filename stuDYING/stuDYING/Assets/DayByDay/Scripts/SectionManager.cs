using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionManager : MonoBehaviour {

	public GameObject tree;
	public GameObject coal;
	public GameObject bush;
	public GameObject background;
	public Transform player;
	public Quaternion rot;

	//for checkpoints, these are the next x position the player needs to reach for either sides to spawn new background/resources
	public float next_left_checkpoint;
	public float next_right_checkpoint;

	//for adjustments, take length of one background times 1.5)
	public float left_adjustment;
	public float right_adjustment;

	public float background_length;

	//this determines how my possible resources are per new sections, and how far they should be away from each other
	public float num_resources;
	public float left_increments;
	public float right_increments;

	void Awake(){
		DontDestroyOnLoad (this);
	}

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		Vector2 player_pos = new Vector2(player.transform.position.x, player.transform.position.y);

		//if player goes too far left, game will spawn new background to ensure player stays in bounds while also spawning resources (randomly)
		if (player_pos.x <= next_left_checkpoint) {
			Vector2 left_background_spawn;
			left_background_spawn = new Vector2(player_pos.x + left_adjustment, 6f);
				
			Instantiate (background, left_background_spawn, rot);
			next_left_checkpoint -= background_length;

			for (int i = 1; i < num_resources; i++) {
                Debug.Log("in the loop");
				int rand_num = Random.Range (1, 6);
				float changing_increment = i * left_increments;

				Vector2 next_left_spawn;
				next_left_spawn = new Vector2 (next_left_checkpoint + changing_increment, 3f);

				if (rand_num == 1) {
					Instantiate (tree, new Vector2(next_left_spawn.x,
						(next_left_spawn.y + 1.75f)), rot);
				}
				if (rand_num == 2) {
					Instantiate (coal, new Vector2(next_left_spawn.x,
						(next_left_spawn.y + 1.75f)), rot);
                }
				if (rand_num == 3) {
                    Instantiate (bush, next_left_spawn, rot);
                }
            }
		}

		//if player goes too far right, game will spawn new background to ensure player stays in bounds
		if (player_pos.x >= next_right_checkpoint) {
			Vector2 right_background_spawn;
			right_background_spawn = new Vector2(player_pos.x + right_adjustment, 6f);

			Instantiate (background, right_background_spawn, rot);
			next_right_checkpoint += background_length;

			for (int i = 1; i <= num_resources; i++) {
				int rand_num = Random.Range (1, 6);
				float changing_increment = i * right_increments;

				Vector2 next_right_spawn;
				next_right_spawn = new Vector2 (next_right_checkpoint + changing_increment, 3f);

				if (rand_num == 1) {
					Instantiate (tree, new Vector2(next_right_spawn.x,
						next_right_spawn.y + 1.75f), rot);
                }
				if (rand_num == 2) {
					Instantiate (coal, new Vector2(next_right_spawn.x,
						next_right_spawn.y + 0.7f), rot);
                }
				if (rand_num == 3) {
					Instantiate (bush, next_right_spawn, rot);
                }

            }
		}
	}
}
