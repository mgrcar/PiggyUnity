using System;
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
    public static readonly int Scale;
}
