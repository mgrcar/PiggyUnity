using System;
using UnityEngine;

public class Snowfall : MonoBehaviour 
{
    private System.Random Rnd 
        = new System.Random();

	void Start() 
    {
        Sprite snowflake1 = Common.CreateSpriteFrom((Texture2D)Resources.Load("snowflake1"));
        Sprite snowflake2 = Common.CreateSpriteFrom((Texture2D)Resources.Load("snowflake2"));
        
        for (var i = 0; i < Common.NumSnowflakes; i++) 
        {
            double bw = Common.BackgroundWidth;
            double bh = Common.BackgroundHeight;
            float x = (float)(Rnd.NextDouble() * bw - bw / 2.0);
            float y = (float)(Rnd.NextDouble() * bh - bh / 2.0);
            GameObject snowflake = (GameObject)UnityEngine.Object.Instantiate(GameObject.Find("Snowflake"));
            snowflake.tag = "Snowflake";
            snowflake.GetComponent<PixelPerfectPosition>().SetPosition(x, y);
            SpriteRenderer renderer = snowflake.GetComponent<SpriteRenderer>();
            renderer.sprite = Math.Round(Rnd.NextDouble()) == 0 ? snowflake1 : snowflake2;
            SnowflakeProperties prop = snowflake.GetComponent<SnowflakeProperties>();
            prop.Angle = Rnd.NextDouble() * Common.AngleRange + 1.5708 - Common.AngleRange / 2.0;
            prop.Speed = Rnd.NextDouble() * 0.2 + 0.2;
        }
	}

    void FixedUpdate()
    {
        //for (i = 0; i < flakes.children.length; i++) {
        //    flake = flakes.children[i];
        //    flake.$x = flake.$x + flake.$inc * Math.cos(flake.$angle);
        //    flake.$y = flake.$y + flake.$inc * Math.sin(flake.$angle);
        //    flake.x = Math.round(flake.$x);
        //    flake.y = Math.round(flake.$y);
        //    flake.$angle += (Math.random() * 2 - 1) * angleChange;
        //    flake.$angle = Math.max(angleMin, Math.min(angleMax, flake.$angle));
        //    if (flake.y > ch + scale(5)) { 
        //        flake.y = flake.$y = -scale(5);
        //        flake.$x = Math.random() * cw;
        //        flake.x = Math.round(flake.$x);
        //        flake.$angle = Math.random() * angleRange + 1.5708 - angleRange / 2;
        //    }
        //}
        foreach (GameObject snowflake in GameObject.FindGameObjectsWithTag("Snowflake"))
        {
            SnowflakeProperties prop = snowflake.GetComponent<SnowflakeProperties>();
            PixelPerfectPosition pos = snowflake.GetComponent<PixelPerfectPosition>();
            pos.X = (float)((double)pos.X + prop.Speed * Math.Cos(prop.Angle));
            pos.Y = (float)((double)pos.Y - prop.Speed * Math.Sin(prop.Angle));
            prop.Angle += (Rnd.NextDouble() * 2.0 - 1.0) * Common.AngleChange;
            prop.Angle = Math.Max(Common.AngleMin, Math.Min(Common.AngleMax, prop.Angle));
            // ...
        }
    }
}
