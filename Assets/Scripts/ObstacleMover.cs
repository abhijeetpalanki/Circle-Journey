using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public static float Speed { get; set; }

    void Awake()
    {
        Speed = 10f;
    }

    void Update()
    {
        transform.position += Speed * Time.deltaTime * Vector3.left;
    }
}
