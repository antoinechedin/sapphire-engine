using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solid : MonoBehaviour
{
    public void Move(Vector2 move)
    {
        transform.Translate(move);
    }
}
