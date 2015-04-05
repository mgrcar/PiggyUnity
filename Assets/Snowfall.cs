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
            prop.Angle = Rnd.NextDouble() * Common.AngleRange + 1.57079632679 - Common.AngleRange / 2.0;
            prop.Speed = Rnd.NextDouble() * 0.2 + 0.2;
        }
	}

    void FixedUpdate()
    {
        foreach (GameObject snowflake in GameObject.FindGameObjectsWithTag("Snowflake"))
        {
            SnowflakeProperties prop = snowflake.GetComponent<SnowflakeProperties>();
            PixelPerfectPosition pos = snowflake.GetComponent<PixelPerfectPosition>();
            pos.X = (float)((double)pos.X + prop.Speed * Math.Cos(prop.Angle));
            pos.Y = (float)((double)pos.Y - prop.Speed * Math.Sin(prop.Angle));
            prop.Angle += (Rnd.NextDouble() * 2.0 - 1.0) * Common.AngleChange;
            prop.Angle = Math.Max(Common.AngleMin, Math.Min(Common.AngleMax, prop.Angle));
            double hbh = (double)Common.BackgroundHeight / 2.0 + 0.5;
            double bw = Common.BackgroundWidth;
            if (pos.Y < -hbh) 
            { 
                pos.Y = (float)hbh;
                pos.X = (float)(Rnd.NextDouble() * bw - bw / 2.0);
            }
        }
    }
}
