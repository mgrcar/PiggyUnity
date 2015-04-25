using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour 
{
    private IEnumerator Game()
    {
        Debug.Log("Before Waiting");
        yield return new WaitForSeconds(2);
        Debug.Log("After Waiting 2 Seconds");
        yield return new WaitForSeconds(2);
        Debug.Log("After Waiting 4 Seconds");
    }

    private void Start() 
    {
	    StartCoroutine(Game());
	}
}
