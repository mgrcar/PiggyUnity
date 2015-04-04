using System;
using UnityEngine;

public class Snowfall : MonoBehaviour 
{
	void Start() 
    {
        Sprite snowflake1 = Common.CreateSpriteFrom((Texture2D)Resources.Load("snowflake1"));
        Sprite snowflake2 = Common.CreateSpriteFrom((Texture2D)Resources.Load("snowflake2"));
        
        System.Random rnd = new System.Random();

        for (var i = 0; i < Common.NumFlakes; i++) 
        {
            float x = (float)(rnd.NextDouble() * (double)Screen.width - (double)Screen.width / 2.0);
            float y = (float)(rnd.NextDouble() * (double)Screen.height - (double)Screen.height / 2.0);
            GameObject snowflake = (GameObject)UnityEngine.Object.Instantiate(GameObject.Find("Snowflake"), new Vector3(x, y, -1), new Quaternion());
            SpriteRenderer renderer = snowflake.GetComponent<SpriteRenderer>();
            renderer.sprite = Math.Round(rnd.NextDouble()) == 0 ? snowflake1 : snowflake2;
            //sprite.$angle = Math.random() * angleRange + 1.5708 - angleRange / 2;
            //sprite.$inc = Math.random() * scale(0.2) + scale(0.2); 
            //sprite.$x = x;
            //sprite.$y = y;
        }
	}
}
