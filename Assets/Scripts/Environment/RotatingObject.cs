using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Whether to rotate around the X axis.")]
    private bool rotateX = true;
    
    [SerializeField]
    [Tooltip("Whether to rotate around the Y axis.")]
    private bool rotateY = true;
    
    [SerializeField]
    [Tooltip("Whether to rotate around the Z axis.")]
    private bool rotateZ = true;

    public float rotationSpeed = 30.0f;

    private void Update()
    {
        float xRotation = rotateX ? rotationSpeed * Time.deltaTime : 0f;
        float yRotation = rotateY ? rotationSpeed * Time.deltaTime : 0f;
        float zRotation = rotateZ ? rotationSpeed * Time.deltaTime : 0f;

        transform.Rotate(new Vector3(xRotation, yRotation, zRotation));
    }
}
