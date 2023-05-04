# region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

// using this tutorial: https://youtu.be/epRPKFsOPck

public class Scroller : MonoBehaviour
{
    Material material;
    Vector2 offset;

    public float xVelocity, yVelocity;
    //public float speed = 0.5f;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    private void Start()
    {
        offset = new Vector2(xVelocity, yVelocity);
    }

    private void Update()
    {
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}
