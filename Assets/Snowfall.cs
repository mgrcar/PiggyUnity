using UnityEngine;

using Rnd = UnityEngine.Random;

public class Snowfall : MonoBehaviour 
{
	void Start() 
    {
        Sprite snowflake1 = Common.CreateSpriteFrom((Texture2D)Resources.Load("snowflake1"));
        Sprite snowflake2 = Common.CreateSpriteFrom((Texture2D)Resources.Load("snowflake2"));
        
        for (var i = 0; i < Common.NumSnowflakes; i++) 
        {
            float bw = Common.ScreenWidth;
            float bh = Common.ScreenHeight;
            float x = Rnd.value * bw - bw / 2f;
            float y = Rnd.value * bh - bh / 2f;
            GameObject snowflake = (GameObject)UnityEngine.Object.Instantiate(GameObject.Find("Snowflake"));
            snowflake.tag = "Snowflake";
            snowflake.GetComponent<PixelPerfectPosition>().SetPosition(x, y);
            SpriteRenderer renderer = snowflake.GetComponent<SpriteRenderer>();
            renderer.sprite = Mathf.Round(Rnd.value) == 0 ? snowflake1 : snowflake2;
            SnowflakeProperties prop = snowflake.GetComponent<SnowflakeProperties>();
            prop.Angle = Rnd.value * Common.AngleRange + 1.57079632679f - Common.AngleRange / 2f;
            prop.Speed = Rnd.value * 0.2f + 0.2f;
            if (prop.Speed < 0.3) { snowflake.transform.position = snowflake.transform.position + new Vector3(0, 0, 3); }
        }
	}

    void FixedUpdate()
    {
        foreach (GameObject snowflake in GameObject.FindGameObjectsWithTag("Snowflake"))
        {
            SnowflakeProperties prop = snowflake.GetComponent<SnowflakeProperties>();
            PixelPerfectPosition pos = snowflake.GetComponent<PixelPerfectPosition>();
            pos.X = pos.X + prop.Speed * Mathf.Cos(prop.Angle);
            pos.Y = pos.Y - prop.Speed * Mathf.Sin(prop.Angle);
            prop.Angle += (Rnd.value * 2f - 1f) * Common.AngleChange;
            prop.Angle = Mathf.Max(Common.AngleMin, Mathf.Min(Common.AngleMax, prop.Angle));
            float hbh = Common.ScreenHeight / 2f + 5f;
            float bw = Common.ScreenWidth;
            if (pos.Y < -hbh) 
            { 
                pos.Y = hbh;
                pos.X = Rnd.value * bw - bw / 2f;
            }
        }
    }
}
