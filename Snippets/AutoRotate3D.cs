using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AutoRotate3D : MonoBehaviour
{

    public Vector3 torque;

    void Update()
    {
        transform.Rotate(torque * Time.deltaTime);
    }

    //void FixedUpdate()
    //{
    //  transform.Rotate(torque);
    //}
}
