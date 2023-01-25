using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Bullet 
{
    public Vector3 direction;
    public Vector3 previousPos;
    public GameObject bulletReady;
    public float speed;
    public float radius;
    public float damage;
}
