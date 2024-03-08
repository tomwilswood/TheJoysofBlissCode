using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class MainCameraControlScript : MonoBehaviour
{
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("CameraTarget").transform;


    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(playerTransform, Vector3.up);
    }
}
