using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera _Camera;

    // LateUpdate is called after Update
    private void LateUpdate()
    {
        transform.LookAt(transform.position + _Camera.transform.rotation * Vector3.forward, _Camera.transform.rotation * Vector3.up);
    }
}
