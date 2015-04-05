using UnityEngine;

public class PixelPerfectSprite : MonoBehaviour 
{
    void Start() 
    {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = Common.CreateSpriteFrom(renderer.sprite.texture);
        gameObject.transform.position = Common.SnapTo(
            gameObject.transform.position.x, 
            gameObject.transform.position.y, 
            gameObject.transform.position.z, 
            Common.Snap.Subpixel
        );
    }
}
