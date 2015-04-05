using System;
using UnityEngine;

public class Snowfall : MonoBehaviour 
{
	void Start() 
    {
        Sprite snowflake1 = Common.CreateSpriteFrom((Texture2D)Resources.Load("snowflake1"));
        Sprite snowflake2 = Common.CreateSpriteFrom((Texture2D)Resources.Load("snowflake2"));
        
        System.Random rnd = new System.Random();

        for (var i = 0; i < Common.NumSnowflakes; i++) 
        {
            double bw = Common.BackgroundWidth;
            double bh = Common.BackgroundHeight;
            float x = (float)(rnd.NextDouble() * bw - bw / 2.0);
            float y = (float)(rnd.NextDouble() * bh - bh / 2.0);
            GameObject snowflake = (GameObject)UnityEngine.Object.Instantiate(GameObject.Find("Snowflake"));
            snowflake.GetComponent<PixelPerfectPosition>().SetPosition(x, y);
            SpriteRenderer renderer = snowflake.GetComponent<SpriteRenderer>();
            renderer.sprite = Math.Round(rnd.NextDouble()) == 0 ? snowflake1 : snowflake2;
            //sprite.$angle = Math.random() * angleRange + 1.5708 - angleRange / 2;
            //sprite.$inc = Math.random() * scale(0.2) + scale(0.2); 
            //sprite.$x = x;
            //sprite.$y = y;
        }
	}
}
