using UnityEngine;

[DisallowMultipleComponent]
public class Paddle : MonoBehaviour
{
    public float speed = 10f;
    public float left = -5f;
    public float right = 5f;
    public Vector3 initialPosition;
}

//DO NOT ADD MONOBEHAVIOUR MESSAGES (Start, Update, etc.)