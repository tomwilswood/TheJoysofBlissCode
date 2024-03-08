using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraArmsStopScript : MonoBehaviour
{
    public GameObject localArm1;
    GameObject mainBodyParent;
    // Start is called before the first frame update
    void Start()
    {
        localArm1 = transform.Find("Arm 1").gameObject;
        localArm1.GetComponent<Rigidbody>().isKinematic = false;
        mainBodyParent = transform.Find("MainBodyParent").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainBodyParent.GetComponent<Rigidbody>().IsSleeping())
        {
            localArm1.GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            localArm1.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
