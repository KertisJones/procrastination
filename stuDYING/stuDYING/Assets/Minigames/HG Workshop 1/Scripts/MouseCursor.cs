using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    private void Update()
    {
        transform.position = WorldPosition;
        Cursor.visible = false;
    }

    public static Vector2 WorldPosition { get { return Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.back * Camera.main.transform.position.z); } }
}
