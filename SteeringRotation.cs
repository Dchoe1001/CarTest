using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringRotation : MonoBehaviour
{
    [SerializeField] public float rotationSpeed = 50f;  // The speed at which the object should rotate
    public string inputAxisName = "Steering";  // The name of the input axis to read

    [SerializeField] public float deadZone = 1f;  // The range of values around zero to ignore

    private float currentRotation = 0f;  // The current rotation of the object
    private Quaternion initialRotation;  // The initial rotation of the object

    void Start()
    {
        initialRotation = transform.rotation;
    }

    void Update()
    {
        float inputAxisValue = Input.GetAxis(inputAxisName);  // Read the input value

        if (Mathf.Abs(inputAxisValue) < deadZone) {
            inputAxisValue = 0f;  // Apply dead zone to the input
            //currentRotation = initialRotation.z;  // Reset the current rotation
        }

        // Calculate the new rotation based on the input value
        float newRotation = currentRotation - inputAxisValue * rotationSpeed * Time.deltaTime;

        // Apply the new rotation to the object
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        eulerRotation.z = newRotation;
        transform.rotation = Quaternion.Euler(eulerRotation);

        currentRotation = newRotation;  // Update the current rotation
    }
}
