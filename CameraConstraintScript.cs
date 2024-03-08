using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEditor.Rendering;
using UnityEngine;

public class CameraConstraintScript : MonoBehaviour
{

    public GameObject anchor;
    float constrainDistance = 3.42982f;

    GameObject mainBodyParent;
    GameObject player;

    Vector3 camToPlayerVector;
    public bool shouldMove;

    GameObject target;

    Vector3 mainBodyIntialPos;

    void Start()
    {
        anchor = transform.Find("CameraMoveAnchor").gameObject;
        mainBodyParent = transform.Find("MainBodyParent").gameObject;
        constrainDistance = Vector3.Distance(anchor.transform.position, mainBodyParent.transform.position);
        player = GameObject.Find("CameraTarget");

        target = transform.Find("CameraMoveTarget").gameObject;
        mainBodyIntialPos = mainBodyParent.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        camToPlayerVector = player.transform.position - mainBodyParent.transform.position;

        if (Vector3.Distance(anchor.transform.position, (mainBodyParent.transform.position + divideVector(camToPlayerVector, 10.0f))) < constrainDistance)
        //if the camera moving in the direction of the player would be within constrain distance.
        {
            shouldMove = true;
        }
        else
        {
            shouldMove = false;
        }

        target.transform.position = anchor.transform.position + ((player.transform.position - anchor.transform.position).normalized * (mainBodyIntialPos - anchor.transform.position).magnitude);
        mainBodyParent.transform.position = target.transform.position;

    }

    public Vector3 divideVector(Vector3 vectorToDivide, float numToDivideBy)
    {
        return new Vector3(vectorToDivide.x / numToDivideBy, vectorToDivide.y / numToDivideBy, vectorToDivide.z / numToDivideBy);
    }
}
