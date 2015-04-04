using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Common 
{
    static Common()
    {
        int s1 = (int)Math.Floor((double)Screen.width / (double)BackgroundWidth);
        int s2 = (int)Math.Ceiling((double)Screen.width / (double)BackgroundWidth);
        Scale = Math.Abs(s1 * BackgroundWidth - Screen.width) > Math.Abs(s2 * BackgroundWidth - Screen.width) ? s2 : s1;
    }

    public static readonly int BackgroundWidth 
        = 160;
    public static readonly int NumFlakes
        = 500;
    public static readonly int Scale;

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
        texture.SetPixels(pixels.ToArray());
        texture.Apply();
        return Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f),
            1
        );
    }
}
