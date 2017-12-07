using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorObjectEvents : PointerInputModule {

	public List <GameObject> cursorObjects;
	private EventSystem system;
	private PointerEventData pointer;
	public Vector2 myPos;
	public RaycastResult result;
	public RaycastResult UIray;
	public RaycastResult GOray;
	bool p1A;
	bool p2A;


	public void Start(){
		system = EventSystem.current;

	}

	public void Update(){
		if (ControlManager.P1A) {
			p1A = true;
		}
		if (ControlManager.P2A) {
			p2A = true;
		}

	}

	public override void Process(){

		for (int i = 0; i < cursorObjects.Count; i++) {
			GameObject cursorObject = cursorObjects[i];
			GetPointerData (i, out pointer, true);
			pointer.position = cursorObject.transform.position;
			system.RaycastAll (pointer, this.m_RaycastResultCache);
			result.Clear ();

			for (int x = 0; x < m_RaycastResultCache.Count; x++) {
				if (m_RaycastResultCache[x].gameObject.GetComponent<Button> () != null){
					result = m_RaycastResultCache[x];
				}
			}
			pointer.pointerCurrentRaycast = result;
			base.HandlePointerExitAndEnter (pointer, result.gameObject);
			pointer.clickCount = 0;
	
			if ((i == 0 && p1A) 
				|| (i == 1 && p2A)) {
				pointer.pressPosition = cursorObject.transform.position;
				pointer.clickTime = Time.unscaledTime;
				pointer.pointerPressRaycast = result;

				pointer.clickCount = 1;
				pointer.eligibleForClick = true;

				if (result.gameObject != null) {
					pointer.selectedObject = result.gameObject;
					pointer.pointerPress = ExecuteEvents.ExecuteHierarchy (result.gameObject, pointer, ExecuteEvents.submitHandler);
					pointer.rawPointerPress = result.gameObject;
				} else {
					pointer.selectedObject = null;
					pointer.pointerPress = null;
					pointer.rawPointerPress = null;
				}
				p1A = false;
				p2A = false;
			} else {
				pointer.clickCount = 0;
				pointer.eligibleForClick = false;
				pointer.pointerPress = null;
				pointer.rawPointerPress = null;
			}
		}
	}
	
}
