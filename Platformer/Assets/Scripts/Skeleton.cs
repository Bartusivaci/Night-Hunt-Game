using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float walkSpeed = 2f;

    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * walkSpeed);
    }
}
