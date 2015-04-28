using UnityEngine;

public class PixelPerfectPosition : MonoBehaviour 
{
    public float X
        = 0;
    public float Y
        = 0;
    public Common.Snap SnapTo
        = Common.Snap.Subpixel;
    public int SubpixelLevels
        = 0;

    public void SetPosition(float x, float y)
    {
        X = x;
        Y = y;
        Update();
    }

    public void Update()
    {
        if (SnapTo == Common.Snap.Subpixel && SubpixelLevels != 0)
        {
            float s = SubpixelLevels;
            Vector3 pos = Common.SnapTo(X * s, Y * s, gameObject.transform.position.z, Common.Snap.SmallPixel);
            pos.x /= s;
            pos.y /= s;
            gameObject.transform.position = pos;
        }
        else
        {
            gameObject.transform.position = Common.SnapTo(X, Y, gameObject.transform.position.z, SnapTo);
        }
    }
}
