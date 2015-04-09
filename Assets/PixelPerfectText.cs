using UnityEngine;

public class PixelPerfectText : MonoBehaviour 
{
    public string Text
        = "";
    public Font Font
        = Font.Default;

    void Start() 
    {
        // get font info
        FontInfo fontInfo = Fonts.GetFontInfo(Font);
        int textWidth  = fontInfo.CharWidth * Text.Length;
        int textHeight = fontInfo.LineHeight;
        Color[] srcPixels = fontInfo.Texture.GetPixels();
        // create dest texture
        Texture2D destTexture = new Texture2D(textWidth, textHeight, fontInfo.Texture.format, mipmap: false);
        destTexture.wrapMode = fontInfo.Texture.wrapMode;
        Color[] destPixels = destTexture.GetPixels();
        // copy chars to dest
        int destX = 0;
        foreach (char ch in Text) 
        {
            int idx = fontInfo.Chars.IndexOf(ch);
            int x = idx * fontInfo.CharWidth;
            Common.CopyPixels(srcPixels, destPixels, fontInfo.Texture.width, destTexture.width, x, 0, fontInfo.CharWidth, fontInfo.LineHeight, destX, 0);
            destX += fontInfo.CharWidth;
        }
        // create sprite
        destTexture.SetPixels(destPixels);
        destTexture.Apply();
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = Common.CreateSpriteFrom(destTexture);
	}
}
