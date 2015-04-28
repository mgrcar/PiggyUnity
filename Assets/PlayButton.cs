using UnityEngine;

public class PlayButton : MonoBehaviour 
{
	void Start() 
    {
        gameObject.GetComponent<PixelPerfectButton>().OnButtonPressed += new PixelPerfectButton.OnButtonPressedHandler(delegate() {
            gameObject.SetActive(false);
            Common.FindGameObject("Score").SetActive(true);
        });
	}
}
