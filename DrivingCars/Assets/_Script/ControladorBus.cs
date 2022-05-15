using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorBus : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentBreakForce;
    private bool isBreaking;

    [SerializeField] private float motorforce;
    [SerializeField] private float breakforce;
    [SerializeField] private float maxSteerInAngle;

    [SerializeField] private WheelCollider FrontLeftWheelc;
    [SerializeField] private WheelCollider FrontRightWheelc;
    [SerializeField] private WheelCollider RearLeftWheelc;
    [SerializeField] private WheelCollider RearALeftWheelc;
    [SerializeField] private WheelCollider RearRightWheelc;
    [SerializeField] private WheelCollider RearARightWheelc;

    [SerializeField] private Transform FrontLeftWheelt;
    [SerializeField] private Transform FrontRightWheelt;
    [SerializeField] private Transform RearALeftWheelt;
    [SerializeField] private Transform RearLeftWheelt;
    [SerializeField] private Transform RearRightWheelt;
    [SerializeField] private Transform RearARightWheelt;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        FrontLeftWheelc.motorTorque = verticalInput * motorforce;
        FrontRightWheelc.motorTorque = verticalInput * motorforce;
        currentBreakForce = isBreaking ? breakforce : 0f;
        FrontRightWheelc.brakeTorque = currentBreakForce;
        FrontLeftWheelc.brakeTorque = currentBreakForce;
        RearRightWheelc.brakeTorque = currentBreakForce;
        RearARightWheelc.brakeTorque = currentBreakForce; // Llanta secundaria
        RearLeftWheelc.brakeTorque = currentBreakForce;
        RearALeftWheelc.brakeTorque = currentBreakForce; // Llanta secundaria
    }


    private void HandleSteering()
    {
        currentSteerAngle = maxSteerInAngle * horizontalInput;
        FrontLeftWheelc.steerAngle = currentSteerAngle;
        FrontRightWheelc.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheels(FrontLeftWheelc, FrontLeftWheelt);
        UpdateSingleWheels(FrontRightWheelc, FrontRightWheelt);
        UpdateSingleWheels(RearLeftWheelc, RearLeftWheelt);
        UpdateSingleWheels(RearALeftWheelc, RearLeftWheelt);
        UpdateSingleWheels(RearRightWheelc, RearRightWheelt);
        UpdateSingleWheels(RearARightWheelc, RearARightWheelt);
    }

    private void UpdateSingleWheels(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
