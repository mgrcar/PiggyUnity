using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PixelPerfectSprite : MonoBehaviour 
{
    private Sprite mSourceSprite;

	private void Start() 
    {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        mSourceSprite = renderer.sprite;
        int w = mSourceSprite.texture.width;
        int h = mSourceSprite.texture.height;
        if (w % 2 != 0) { Debug.Log("ERROR: Pixel perfect sprite's width must be even!"); }
        if (h % 2 != 0) { Debug.Log("ERROR: Pixel perfect sprite's height must be even!"); }
        Debug.Log(w);
        Debug.Log(h);
        Debug.Log(Common.Scale);
        Color[] originalPixels = mSourceSprite.texture.GetPixels();
        List<Color> pixels = new List<Color>(w * Common.Scale * h * Common.Scale);
        List<Color> row = new List<Color>(w * Common.Scale);
        for (int y = 0; y < h; y++) 
        {
            for (int x = 0; x < w; x++) 
            {
                row.AddRange(Enumerable.Repeat(originalPixels[w * y + x], Common.Scale));
            }
            for (int r = 0; r < Common.Scale; r++)
            {
                pixels.AddRange(row);
            }
            row.Clear();
        }
        Texture2D texture = new Texture2D(w * Common.Scale, h * Common.Scale, mSourceSprite.texture.format, mipmap: false);
        texture.SetPixels(pixels.ToArray());
        texture.Apply();
        renderer.sprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f),
            1
        );
        gameObject.transform.position = new Vector3(
            Common.Scale * gameObject.transform.position.x,
            Common.Scale * gameObject.transform.position.y,
            gameObject.transform.position.z
        );
    }
}
