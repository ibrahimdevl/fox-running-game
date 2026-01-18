using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private float zDistance;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        
        // Set a nice sky blue background color
        if (mainCamera != null)
        {
            mainCamera.backgroundColor = new Color(0.5f, 0.8f, 1.0f, 1.0f); // Sky blue
        }

        if( target != null )
        {
            zDistance = target.position.z - transform.position.z;
        }
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 position = transform.position;
        position.z = target.position.z - zDistance;
        transform.position = position;
    }
}
