using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime);
    }
}
