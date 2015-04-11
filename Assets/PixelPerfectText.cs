using UnityEngine;

public class PixelPerfectText : MonoBehaviour 
{
    public enum VerticalTextAlignment
    {
        Top,
        Bottom,
        Center
    }

    public enum HorizontalTextAlignment
    {
        Left,
        Right,
        Center
    }

    public string Text
        = "";
    public Font Font
        = Font.Default;
    public HorizontalTextAlignment HorizontalAlignment
        = HorizontalTextAlignment.Center;
    public VerticalTextAlignment VerticalAlignment
        = VerticalTextAlignment.Center;

    float AnchorX(HorizontalTextAlignment textAlignment)
    {
        switch (textAlignment)
        { 
            case HorizontalTextAlignment.Left:
                return 0;
            case HorizontalTextAlignment.Right:
                return 1;
            default:
                return 0.5f;
        }
    }

    float AnchorY(VerticalTextAlignment textAlignment)
    {
        switch (textAlignment)
        {
            case VerticalTextAlignment.Top:
                return 1;
            case VerticalTextAlignment.Bottom:
                return 0;
            default:
                return 0.5f;
        }
    }

    void Start() 
    {
        // get font info
        FontInfo fontInfo = Fonts.GetFontInfo(Font);
        int textWidth  = fontInfo.CharWidth * Text.Length;
        int textHeight = fontInfo.LineHeight;
        if (textWidth % 2 != 0 || textHeight % 2 != 0) { Debug.Log("WARNING: Pixel perfect text's dimensions are not even!"); }
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
        renderer.sprite = Common.CreateSpriteFrom(destTexture, AnchorX(HorizontalAlignment), AnchorY(VerticalAlignment));
	}
}
