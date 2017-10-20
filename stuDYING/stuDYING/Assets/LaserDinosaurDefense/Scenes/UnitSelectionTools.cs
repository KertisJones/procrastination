using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitSelectionTools{

	static Texture2D _whiteTexture;
	public static Texture2D WhiteTexture{
		get{
			if (_whiteTexture == null) {
				_whiteTexture = new Texture2D (1, 1);
				_whiteTexture.SetPixel (0, 0, Color.white);
				_whiteTexture.Apply ();
			}

			return _whiteTexture;
		}
	}

	//Draws a selection rectangle
	public static void DrawScreenRect(Rect rect, Color color){
		GUI.color = color;
		GUI.DrawTexture (rect, WhiteTexture);
		GUI.color = Color.white;
	}

	//Draws the borders for a selection rectangle
	public static void DrawScreenRectBorder(Rect rect, float thickness, Color color){
		//Top
		UnitSelectionTools.DrawScreenRect (new Rect (rect.xMin, rect.yMin, rect.width, thickness), color);
		//Left
		UnitSelectionTools.DrawScreenRect (new Rect (rect.xMin, rect.yMin, thickness, rect.height), color);
		//Right
		UnitSelectionTools.DrawScreenRect (new Rect (rect.xMax -thickness, rect.yMin, thickness, rect.height), color);
		//Bottom
		UnitSelectionTools.DrawScreenRect (new Rect (rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
	}

	//Creates a selection rectangle between two given vector3s
	public static Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2){
		
		//Move origin from bottom left to top left to account for inconsistency with origin position in Unity and Rect structure
		screenPosition1.y = Screen.height - screenPosition1.y;
		screenPosition2.y = Screen.height - screenPosition2.y;

		//Calculate corners of selection rectangle
		var topLeft = Vector3.Min(screenPosition1, screenPosition2);
		var bottomRight = Vector3.Max (screenPosition1, screenPosition2);

		//Creates selection rectangle
		return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
	}

	//Gets the bounds of the selection rectangle in relation to the camera
	public static Bounds GetViewportBounds(Camera camera, Vector3 screenPosition1, Vector3 screenPosition2){
		var v1 = Camera.main.ScreenToViewportPoint (screenPosition1);
		var v2 = Camera.main.ScreenToViewportPoint (screenPosition2);
		var min = Vector3.Min (v1, v2);
		var max = Vector3.Max (v1, v2);
		min.z = camera.nearClipPlane;
		max.z = camera.farClipPlane;

		var bounds = new Bounds ();
		bounds.SetMinMax (min, max);
		return bounds;
	}
}
