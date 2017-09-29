using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class FlexibleDraggableObject : MonoBehaviour
{
    public GameObject Target;
    private EventTrigger _eventTrigger;

    public RectTransform canvasRect;

    void Start ()
    {
        _eventTrigger = GetComponent<EventTrigger>();
        _eventTrigger.AddEventTrigger(OnDrag, EventTriggerType.Drag);
    }

    private void Update()
    {
        Target.transform.position = ClampToWindow(Target.transform.position);
    } 

    void OnDrag(BaseEventData data)
    {
        PointerEventData ped = (PointerEventData)data;
        //Target.transform.Translate(ClampToWindow(ped));
        Target.transform.Translate(ped.delta);
    }

    Vector2 ClampToWindow(Vector2 pos)
    {
        Vector2 rawPos = pos;

        float clampedX = pos.x;
        float clampedY = pos.y;
        if (canvasRect.position.x < 0)
        {
            clampedX = 0f;
        }
        if (canvasRect.position.y < 0)
        {
            clampedY = 0f;
        }
        if (canvasRect.position.x + this.GetComponent<RectTransform>().rect.width > GetComponent<RectTransform>().root.GetComponentInParent<RectTransform>().rect.width)
        {
            clampedX = GetComponent<RectTransform>().root.GetComponentInParent<RectTransform>().rect.width - this.GetComponent<RectTransform>().rect.width;
        }
        if (canvasRect.position.y + this.GetComponent<RectTransform>().rect.height > GetComponent<RectTransform>().root.GetComponentInParent<RectTransform>().rect.height)
        {
            clampedY = GetComponent<RectTransform>().root.GetComponentInParent<RectTransform>().rect.height - this.GetComponent<RectTransform>().rect.height;
        }


        //clampedX = Mathf.Clamp(rawPointerPosition.x, canvasCorners[0].x, canvasCorners[2].x);
        //clampedY = Mathf.Clamp(rawPointerPosition.y, canvasCorners[0].y, canvasCorners[2].y);

        Vector2 newPos = new Vector2(clampedX, clampedY);
        return newPos;
    }


}