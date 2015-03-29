using UnityEngine;

public class PixelPerfectCamera : MonoBehaviour 
{
	void Start() 
    {
        gameObject.GetComponent<Camera>().orthographicSize = (float)Screen.height / 2f;
	}
}
