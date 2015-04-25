using UnityEngine;

public class PixelPerfectButton : MonoBehaviour
{
    private enum EventType
    { 
        ButtonDown,
        ButtonUp,
        TouchDown,
        TouchUp
    }

    public Sprite ButtonDownSprite
        = null;

    private void Start()
    {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = Common.CreateSpriteFrom(renderer.sprite.texture);
        gameObject.transform.position = Common.SnapTo(
            gameObject.transform.position.x,
            gameObject.transform.position.y,
            gameObject.transform.position.z,
            Common.Snap.Subpixel
        );
        ButtonDownSprite = Common.CreateSpriteFrom(ButtonDownSprite.texture);
    }

    private void TouchOrClick(EventType eventType, float x, float y)
    {
        float X = x / (float)Common.Scale;
        float Y = y / (float)Common.Scale;
        Debug.Log(eventType.ToString() + " @ " + X + "," + Y);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                TouchOrClick(EventType.TouchDown, touch.position.x, touch.position.y);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                TouchOrClick(EventType.TouchUp, touch.position.x, touch.position.y);
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            TouchOrClick(EventType.ButtonDown, Input.mousePosition.x, Input.mousePosition.y);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            TouchOrClick(EventType.ButtonUp, Input.mousePosition.x, Input.mousePosition.y);
        }
    }
}
