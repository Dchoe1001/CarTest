using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private float isBreaking;

    [SerializeField] private float motorForce = 10000f; // acceleration speed
    [SerializeField] private float breakForce = 2000f;
    [SerializeField] private float maxSteerAngle = 180f;
    [SerializeField] private float minSteerAngle = 0f; 
    [SerializeField] public float deadZone = 1f;  // The range of values around zero to ignore
    [SerializeField] public float steeringSensitivity = 45f;
    [SerializeField] public float accelerationFactor = 100f; 

    [SerializeField] public float breakDuration = 10f; 

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();

       /* if (Mathf.Abs(horizontalInput) < deadZone) {
            horizontalInput = 0f;  // Apply dead zone to the input
        }*/

        HandleSteering();
        UpdateWheels();
    }


    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Steering");
        verticalInput = Input.GetAxisRaw("GasPedal");
        isBreaking = Input.GetAxisRaw("BrakePedal");
        //Debug.Log("brake Pedal Input: " + isBreaking);
    }


    private void HandleMotor()
    {
        //float isTerTrue = 1;
        //float isTerFalse = 0;
        if(verticalInput < 0f) {
            frontLeftWheelCollider.motorTorque = -verticalInput * motorForce;
            frontRightWheelCollider.motorTorque = -verticalInput * motorForce;
        }
        else {
            frontLeftWheelCollider.motorTorque = 0;
            frontRightWheelCollider.motorTorque = 0;
        }
        currentbreakForce = isBreaking != 1f ? breakForce : 0f; // Apply breaking only if isBreaking is not equal to 1
        //currentbreakForce = isBreaking != 1f ? isTerTrue : isTerFalse;
        //Debug.Log("ANS: " + currentbreakForce);
        ApplyBreaking();          
    }

    private void ApplyBreaking()
{
    if (isBreaking > 1f) {
        // Gradually increase the brake force up to the maximum over a certain duration
        currentbreakForce = Mathf.MoveTowards(currentbreakForce, breakForce, Time.fixedDeltaTime * (breakForce / breakDuration));

        // Gradually decrease the motor force down to zero over a certain duration
        motorForce = Mathf.MoveTowards(motorForce, 0f, Time.fixedDeltaTime * (motorForce / breakDuration));
    } else {
        // Gradually decrease the brake force down to zero over a certain duration
        currentbreakForce = Mathf.MoveTowards(currentbreakForce, 0f, Time.fixedDeltaTime * (breakForce / breakDuration));

        // Gradually increase the motor force up to the maximum over a certain duration
        motorForce = Mathf.MoveTowards(motorForce, 10000f, Time.fixedDeltaTime * (10000f / breakDuration));
    }
    
    frontRightWheelCollider.brakeTorque = currentbreakForce;
    frontLeftWheelCollider.brakeTorque = currentbreakForce;
    rearLeftWheelCollider.brakeTorque = currentbreakForce;
    rearRightWheelCollider.brakeTorque = currentbreakForce;
}



   private void HandleSteering()
{
    
    float rawInput = Input.GetAxisRaw("Steering"); // Get the raw input value from the G920 steering wheel
    float input = Mathf.Clamp(rawInput, -1f, 1f); // Clamp the input value between -1 and 1
    
    float speedFactor = Mathf.Clamp01(GetComponent<Rigidbody>().velocity.magnitude / accelerationFactor); // Calculate the speed factor
    currentSteerAngle = Mathf.Lerp(minSteerAngle, maxSteerAngle, speedFactor) * input;


    //float targetSteerAngle = Mathf.Lerp(minSteerAngle, maxSteerAngle, speedFactor); // Calculate the target maximum steering angle using the speed factor

    //currentSteerAngle = (targetSteerAngle * horizontalInput) / steeringSensitivity;

    //Debug.Log(horizontalInput);
    //Debug.Log(currentSteerAngle);
    if (Mathf.Abs(currentSteerAngle) < deadZone) {
        horizontalInput = 0f;
    }

    frontLeftWheelCollider.steerAngle = currentSteerAngle / steeringSensitivity;
    frontRightWheelCollider.steerAngle = currentSteerAngle / steeringSensitivity;
}




    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
;       wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}

