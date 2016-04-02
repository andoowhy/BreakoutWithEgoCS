using UnityEngine;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class BrickInstantiation : MonoBehaviour
{
    public List<GameObject> tiles = new List<GameObject>();
    public int rows = 6;
    public int columns = 10;
}

//DO NOT ADD MONOBEHAVIOUR MESSAGES (Start, Update, etc.)