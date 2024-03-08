using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class TVStatic : MonoBehaviour
{

    Vector2 inScriptOffsetVector;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        inScriptOffsetVector.Set(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f));
        Renderer rend = GetComponent<Renderer>();
        rend.material.SetVector("_OffsetVector", inScriptOffsetVector);
    }
}
