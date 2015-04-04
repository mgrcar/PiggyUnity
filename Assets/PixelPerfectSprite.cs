using UnityEngine;

public class PixelPerfectSprite : MonoBehaviour 
{
    private void Start() 
    {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = Common.CreateSpriteFrom(renderer.sprite.texture);
        gameObject.transform.position = new Vector3(
            (float)Common.Scale * gameObject.transform.position.x,
            (float)Common.Scale * gameObject.transform.position.y,
            gameObject.transform.position.z
        );
    }
}
