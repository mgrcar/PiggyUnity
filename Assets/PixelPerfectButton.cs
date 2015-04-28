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

    public Sprite buttonDownSprite
        = null;
    private Sprite buttonUpSprite
        = null;
    
    private bool buttonDown 
        = false;

    public delegate void OnButtonPressedHandler();
    public event OnButtonPressedHandler OnButtonPressed
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
        buttonDownSprite = Common.CreateSpriteFrom(buttonDownSprite.texture);
		gameObject.AddComponent<BoxCollider2D>(); 
		buttonUpSprite = renderer.sprite;
    }

	private void OnMouseDown() 
	{
		gameObject.GetComponent<SpriteRenderer>().sprite = buttonDownSprite;
        buttonDown = true;
	}

	private void OnMouseUp() 
	{
        if (buttonDown)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = buttonUpSprite;
            buttonDown = false;
            if (OnButtonPressed != null) { OnButtonPressed(); }
        }
	}

    private void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = buttonUpSprite;
        buttonDown = false;
    }
}
