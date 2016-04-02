using UnityEngine;
using System.Collections;

public static class TransformExtension
{
    public static Transform SetX( this Transform transform, float x )
    {
        var newPos = transform.position;
        newPos.x = x;
        transform.position = newPos;
        return transform;
    }

    public static Transform SetY( this Transform transform, float y )
    {
        var newPos = transform.position;
        newPos.y = y;
        transform.position = newPos;
        return transform;
    }

    public static Transform SetZ( this Transform transform, float z )
    {
        var newPos = transform.position;
        newPos.z = z;
        transform.position = newPos;
        return transform;
    }
}
