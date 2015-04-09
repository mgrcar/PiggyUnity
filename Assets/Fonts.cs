using UnityEngine;
using System.Collections.Generic;

public enum Font
{ 
    Default
}

public class FontInfo
{
    public string    ResourceName;
    public string    Chars;
    public int       CharWidth;
    public int       LineHeight;
    public int       XSpacing;
    public int       YSpacing;
    public Texture2D Texture; 
}

public static class Fonts
{
    private static Dictionary<Font, FontInfo> mFonts
        = new Dictionary<Font, FontInfo>();
    
    static Fonts()
    {
        mFonts.Add(Font.Default, new FontInfo() {
            ResourceName = "default_font",
            Chars        = " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_£abcdefghijklmnopqrstuvwxyz{|}~©",
            CharWidth    = 8,
            LineHeight   = 8,
            XSpacing     = 0,
            YSpacing     = 1,
            Texture      = (Texture2D)Resources.Load("default_font")
        });
    }   

    public static FontInfo GetFontInfo(Font font)
    {
        return mFonts[font];
    }
}
