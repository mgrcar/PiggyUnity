using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Common 
{
    public enum Snap
    {
        BigPixel,
        SmallPixel,
        Subpixel
    }

    static Common()
    {
        int s1 = (int)Mathf.Floor((float)Screen.width / (float)BackgroundWidth);
        int s2 = (int)Mathf.Ceil((float)Screen.width / (float)BackgroundWidth);
        Scale = Mathf.Abs(s1 * BackgroundWidth - Screen.width) > Mathf.Abs(s2 * BackgroundWidth - Screen.width) ? s2 : s1;
    }

    public static readonly int Scale;

    public static readonly int BackgroundWidth 
        = 160;
    public static readonly int BackgroundHeight
        = 240;

    public static readonly int NumSnowflakes
        = 500;
    public static readonly float AngleRange
        = 0.2f;
    public static readonly float AngleChange
        = 0.02f;
    public static readonly float AngleMax
        = AngleRange + 1.57079632679f - AngleRange / 2f;
    public static readonly float AngleMin
        = 1.57079632679f - AngleRange / 2f;

    public static Sprite CreateSpriteFrom(Texture2D srcTexture)
    {
        int w = srcTexture.width;
        int h = srcTexture.height;
        if (w % 2 != 0) { Debug.Log("ERROR: Pixel perfect sprite's width must be even!"); }
        if (h % 2 != 0) { Debug.Log("ERROR: Pixel perfect sprite's height must be even!"); }
        Color[] originalPixels = srcTexture.GetPixels();
        List<Color> pixels = new List<Color>(w * Scale * h * Scale);
        List<Color> row = new List<Color>(w * Scale);
        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                row.AddRange(Enumerable.Repeat(originalPixels[w * y + x], Scale));
            }
            for (int r = 0; r < Scale; r++)
            {
                pixels.AddRange(row);
            }
            row.Clear();
        }
        Texture2D texture = new Texture2D(w * Scale, h * Scale, srcTexture.format, mipmap: false);
        texture.wrapMode = srcTexture.wrapMode;
        texture.SetPixels(pixels.ToArray());
        texture.Apply();
        return Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f),
            1
        );
    }

    public static Vector3 SnapTo(float x, float y, float z, Snap snapTo)
    {
        switch (snapTo)
        {
            case Snap.BigPixel:
                return new Vector3(
                    Mathf.Round(x) * (float)Common.Scale,
                    Mathf.Round(y) * (float)Common.Scale,
                    z
                );
            case Snap.SmallPixel:
                return new Vector3(
                    Mathf.Round(x * (float)Common.Scale),
                    Mathf.Round(y * (float)Common.Scale),
                    z
                );
            default:
                return new Vector3(
                    x * (float)Common.Scale,
                    y * (float)Common.Scale,
                    z
                );
        }
    }
}
