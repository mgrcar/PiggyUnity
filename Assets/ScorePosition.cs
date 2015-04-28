using UnityEngine;

public class ScorePosition : MonoBehaviour 
{
	void Start()
    {
        gameObject.GetComponent<PixelPerfectPosition>().SetPosition(
            Common.ScreenWidth / 2f, 
            Common.ScreenHeight / 2f
        );
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
	}
}
