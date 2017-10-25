using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class GroundUnitInputManager : MonoBehaviour {
	public bool hasMoved = false;
	bool isSelecting = false;
	Vector3 mousePosition1;
	List<SelectableUnitComponent> selectedObjects;

	public GameObject selectionCirclePrefab;
	
	// Update is called once per frame
	void Update () {

		// If we press the left mouse button, begin selection and remember the location of the mouse
		if( Input.GetMouseButtonDown( 0 ) )
		{
			isSelecting = true;
			mousePosition1 = Input.mousePosition;

			foreach( var selectableObject in FindObjectsOfType<SelectableUnitComponent>() )
			{
				if( selectableObject.selectionCircle != null )
				{
					Destroy( selectableObject.selectionCircle.gameObject );
					selectableObject.selectionCircle = null;
				}
			}
		}
		// If we let go of the left mouse button, end selection
		if( Input.GetMouseButtonUp( 0 ) )
		{
			selectedObjects = new List<SelectableUnitComponent>();
			foreach( var selectableObject in FindObjectsOfType<SelectableUnitComponent>() )
			{
				if( IsWithinSelectionBounds( selectableObject.gameObject ) )
				{
					selectedObjects.Add( selectableObject );
				}
			}

			var sb = new StringBuilder();
			sb.AppendLine( string.Format( "Selecting [{0}] Units", selectedObjects.Count ) );
			foreach( var selectedObject in selectedObjects )
				sb.AppendLine( "-> " + selectedObject.gameObject.name );
			Debug.Log( sb.ToString() );

			isSelecting = false;
		}

		// Highlight all objects within the selection box
		if( isSelecting )
		{
			foreach( var selectableObject in FindObjectsOfType<SelectableUnitComponent>() )
			{
				if( IsWithinSelectionBounds( selectableObject.gameObject ) )
				{
					if( selectableObject.selectionCircle == null )
					{
						selectableObject.selectionCircle = Instantiate( selectionCirclePrefab );
						selectableObject.selectionCircle.transform.SetParent( selectableObject.transform, false );
						selectableObject.selectionCircle.transform.eulerAngles = new Vector3( 90, 0, 0 );
					}
				}
				else
				{
					if( selectableObject.selectionCircle != null )
					{
						Destroy( selectableObject.selectionCircle.gameObject );
						selectableObject.selectionCircle = null;
					}
				}
			}
		}

		//Commands the selected units to move to the given position
		if (Input.GetMouseButtonDown (1)) {
			//Iterates over all possible movable units
			foreach (var selectableObject in selectedObjects) {
				selectableObject.GetComponent<GroundUnitController> ().moveUnit ();
				hasMoved = true;
			}
			selectedObjects = new List<SelectableUnitComponent> ();
		}
	}

	void OnGUI(){
		if (isSelecting) {
//			//Transparent box, green borders
//			UnitSelectionTools.DrawScreenRectBorder (new Rect (32, 32, 256, 128), 2, Color.green);
//			//Slight opaque box, light blue borders
//			UnitSelectionTools.DrawScreenRect (new Rect (320, 32, 256, 128), new Color (0.8f, 0.8f, 0.95f, 0.25f));
//			UnitSelectionTools.DrawScreenRectBorder (new Rect (320, 32, 256, 128), 2, new Color (0.8f, 0.8f, 0.95f));
			var rect = UnitSelectionTools.GetScreenRect(mousePosition1, Input.mousePosition);
			UnitSelectionTools.DrawScreenRect (rect, new Color (0.8f, 0.8f, 0.95f, 0.25f));
			UnitSelectionTools.DrawScreenRectBorder (rect, 2, new Color (0.8f, 0.8f, 0.95f));
		}
	}

	//Determines whether a given unit is within the selection rectangle
	public bool IsWithinSelectionBounds(GameObject gameObject){
		if (!isSelecting)
			return false;

		var camera = Camera.main;
		var viewportBounds = UnitSelectionTools.GetViewportBounds (camera, mousePosition1, Input.mousePosition);

		return viewportBounds.Contains (camera.WorldToViewportPoint (gameObject.transform.position));
	}
}
