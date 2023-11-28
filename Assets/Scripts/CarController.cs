using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class CarController : MonoBehaviour
{
    [Header("Wheels")]
    [SerializeField]  WheelCollider FLW;
    [SerializeField] WheelCollider FRW;
    [SerializeField] WheelCollider BLW;
    [SerializeField] WheelCollider BRW;
    [SerializeField] GameObject FLWMesh;
    [SerializeField] GameObject FRWMesh;
    [SerializeField] GameObject BRWMesh;
    [SerializeField] GameObject BLWMesh;

    [Header("Control Values")]
    [SerializeField] int     maxSpeed;
    [SerializeField] int     maxReversSpeed;
    [SerializeField] int     accelerationMultiplier;
    [SerializeField] int     maxSpeedAngle;
    [SerializeField] float   steeringSpeed;
    [SerializeField] int     brakeForce;
    [SerializeField] int     declerationMultiplier;
    [SerializeField] float handBrakeForDriftMultiplier;

    [Header("Wheels Physics")]
    WheelFrictionCurve FLWFriction;
    WheelFrictionCurve FRWFriction;
    WheelFrictionCurve RLWFriction;
    WheelFrictionCurve RRWFriction;

    float FLWextremumSlip;
    float FRWextremumSlip;
    float RLWextremumSlip;
    float RRWextremumSlip;

    [Header("Inner Physics")]
    Rigidbody carRigidbody; 
    float     steerAxis; 
    float     throttAxis = 0; 
    float     driftAxis;
    float     zVelocity;
    public float     XVelocity;
    bool      isdeceleratingCar;

    public float carSpeed;
    public bool isDrifting;
    public bool isTractionLocked;

    [Header("Particals System")]
    CarParticalsSystem carParticalsSystem;
    void Start()
    {
        carParticalsSystem = GetComponent<CarParticalsSystem>();
        /////////////////////////////////////////////////////////
        FLWFriction = new WheelFrictionCurve();
        FLWFriction.extremumSlip = FLW.sidewaysFriction.extremumSlip;
        FLWextremumSlip = FLW.sidewaysFriction.extremumSlip;
        FLWFriction.extremumValue = FLW.sidewaysFriction.extremumValue;
        FLWFriction.asymptoteSlip = FLW.sidewaysFriction.asymptoteSlip;
        FLWFriction.asymptoteValue = FLW.sidewaysFriction.asymptoteValue;
        FLWFriction.stiffness = FLW.sidewaysFriction.stiffness;
        FRWFriction = new WheelFrictionCurve();
        FRWFriction.extremumSlip = FRW.sidewaysFriction.extremumSlip;
        FRWextremumSlip = FRW.sidewaysFriction.extremumSlip;
        FRWFriction.extremumValue = FRW.sidewaysFriction.extremumValue;
        FRWFriction.asymptoteSlip = FRW.sidewaysFriction.asymptoteSlip;
        FRWFriction.asymptoteValue = FRW.sidewaysFriction.asymptoteValue;
        FRWFriction.stiffness = FRW.sidewaysFriction.stiffness;
        RLWFriction = new WheelFrictionCurve();
        RLWFriction.extremumSlip = BLW.sidewaysFriction.extremumSlip;
        RLWextremumSlip = BLW.sidewaysFriction.extremumSlip;
        RLWFriction.extremumValue = BLW.sidewaysFriction.extremumValue;
        RLWFriction.asymptoteSlip = BLW.sidewaysFriction.asymptoteSlip;
        RLWFriction.asymptoteValue = BLW.sidewaysFriction.asymptoteValue;
        RLWFriction.stiffness = BLW.sidewaysFriction.stiffness;
        RRWFriction = new WheelFrictionCurve();
        RRWFriction.extremumSlip = BRW.sidewaysFriction.extremumSlip;
        RRWextremumSlip = BRW.sidewaysFriction.extremumSlip;
        RRWFriction.extremumValue = BRW.sidewaysFriction.extremumValue;
        RRWFriction.asymptoteSlip = BRW.sidewaysFriction.asymptoteSlip;
        RRWFriction.asymptoteValue = BRW.sidewaysFriction.asymptoteValue;
        RRWFriction.stiffness = BRW.sidewaysFriction.stiffness;

        carRigidbody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        UpdateWheelMesh();

        carSpeed = (2 * Mathf.PI * FRW.radius * FRW.rpm * 60) / 1000;

        if (Input.GetKey(KeyCode.W))
        {
            Forward();
        }
        if (Input.GetKey(KeyCode.S))
        {
            Revers();
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            ForwardSlip();
            carParticalsSystem.ApplyParticals(true);

        }
        if (Input.GetKey(KeyCode.A))
        {
            Left();
        }
        if(Input.GetKey(KeyCode.D))
        {
            Right();
        }
        if(Input.GetKey(KeyCode.Space))
        {
            HandBrake();
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            ApplyTraction();
        }
        if(!Input.GetKey(KeyCode.S) &&!Input.GetKey(KeyCode.W))
        {
            StopCar();
        }
        if(!Input.GetKey(KeyCode.A)&& !Input.GetKey(KeyCode.D))
        {
            ResetSteeringAngle();
        }
            XVelocity = transform.InverseTransformDirection(carRigidbody.velocity).x;
            zVelocity = transform.InverseTransformDirection(carRigidbody.velocity).z;
        }
    private void ResetSteeringAngle()
    {
        //TODO Dear me for third time plz finish this function using lerp :)
        FRW.steerAngle = 0f;
        FLW.steerAngle = 0f;
    }
    private void Right()
    {
        steerAxis = steerAxis + (Time.deltaTime * steeringSpeed);
        if (steerAxis > 1)
        {
            steerAxis = 1;
        }

        FRW.steerAngle = Mathf.Lerp(FRW.steerAngle, steerAxis * maxSpeedAngle, steeringSpeed);
        FLW.steerAngle = Mathf.Lerp(FRW.steerAngle, steerAxis * maxSpeedAngle, steeringSpeed);

    }
    private void Left()
    {
        steerAxis = steerAxis - (Time.deltaTime * steeringSpeed);
        if(steerAxis < -1 )
        {
            steerAxis = -1;
        }

        FRW.steerAngle = Mathf.Lerp(FRW.steerAngle, steerAxis * maxSpeedAngle, steeringSpeed);
        FLW.steerAngle = Mathf.Lerp(FRW.steerAngle, steerAxis * maxSpeedAngle, steeringSpeed);

    }
    private void Forward()
    {
        if (Mathf.Abs(XVelocity) > 2.5)
        {
            isDrifting = true;
            carParticalsSystem.ApplyParticals();

        }
        else
           isDrifting =false;
        carParticalsSystem.ApplyParticals();


        throttAxis = throttAxis + (Time.deltaTime * 3f);
        if (throttAxis > 1f)
        {
            throttAxis = 1f;

        }
        if (zVelocity < -1)
        {
            
            BreakHold();
        }
        else
        if(carSpeed < maxSpeed)
        {
            BrakeExit();

            FLW.motorTorque = (accelerationMultiplier * 50) * throttAxis;
            FRW.motorTorque = (accelerationMultiplier * 50) * throttAxis;
            BLW.motorTorque = (accelerationMultiplier * 50) * throttAxis;
            BRW.motorTorque = (accelerationMultiplier * 50) * throttAxis;
        }
        else
        {
            StopThrottle();
        }
    }
    private void ForwardSlip()
    {
        if (Mathf.Abs(XVelocity) > 2.5)
        {
            isDrifting = true;
        }
        else
            isDrifting = false;

        throttAxis = throttAxis + (Time.deltaTime * 3f);
        if (throttAxis > 1f)
        {
            throttAxis = 1f;

        }
        if (zVelocity < -1)
        {

            BreakHold();
        }
        else
        if (carSpeed < maxSpeed)
        {
            FLW.brakeTorque = 1000;
            FRW.brakeTorque = 1000;
            BLW.brakeTorque = 0;
            BRW.brakeTorque = 0;

         //   FLW.motorTorque = (accelerationMultiplier * 50) * throttAxis;
          //  FRW.motorTorque = (accelerationMultiplier * 50) * throttAxis;
            BLW.motorTorque = (accelerationMultiplier * 500) * throttAxis;
            BRW.motorTorque = (accelerationMultiplier * 500) * throttAxis;
        }
        else
        {
            StopThrottle();
        }
    }
    private void BrakeExit()
    {
        FLW.brakeTorque = 0;
        FRW.brakeTorque = 0;
        BLW.brakeTorque = 0;
        BRW.brakeTorque = 0;
    }
    private void Revers()
    {
       if (Mathf.Abs(XVelocity) > 2.5)
        {
            isDrifting = true;
            carParticalsSystem.ApplyParticals();

        }
        else
            isDrifting = false;
        carParticalsSystem.ApplyParticals();


        throttAxis = throttAxis - (Time.deltaTime * 3f);
        if (throttAxis < -1f)
        {
            throttAxis = -1f;

        }
        if (zVelocity > 1)
        {

            BreakHold();
        }
        else
        if (carSpeed < maxReversSpeed)
        {
            BrakeExit();

            FLW.motorTorque = (accelerationMultiplier * 50) * throttAxis;
            FRW.motorTorque = (accelerationMultiplier * 50) * throttAxis;
            BLW.motorTorque = (accelerationMultiplier * 50) * throttAxis;
            BRW.motorTorque = (accelerationMultiplier * 50) * throttAxis;
        }
        else
        {
            StopThrottle();
        }
    }
    private void BreakHold()
    {
        FLW.brakeTorque = brakeForce;
        FRW.brakeTorque = brakeForce;
        BLW.brakeTorque = brakeForce;
        BRW.brakeTorque = brakeForce;
    }
    private void StopThrottle()
    {
        FLW.motorTorque = 0;
        FRW.motorTorque = 0;
        BLW.motorTorque = 0;
        BRW.motorTorque = 0;
    }
    private void StopCar()
    {
        if (Mathf.Abs(XVelocity) > 2.5)
        {
            isDrifting = true;
            carParticalsSystem.ApplyParticals();

        }
        else
            isDrifting = false;
            carParticalsSystem.ApplyParticals();


        if (throttAxis != 0 )
        {
            if(throttAxis > 0)
            {
                throttAxis = throttAxis - (Time.deltaTime * 10);
            }else
            if (throttAxis < 0)
            {
                throttAxis = throttAxis + (Time.deltaTime * 10);
            }
        }

        FLW.motorTorque = 0;
        FRW.motorTorque = 0;
        BLW.motorTorque = 0;
        BRW.motorTorque = 0;
    }
    private void HandBrake()
    {
        CancelInvoke("ApplyTraction");
        driftAxis = driftAxis + (Time.deltaTime );
      float startDriftPoint = driftAxis * FLWextremumSlip * handBrakeForDriftMultiplier;
      
        if(startDriftPoint < FLWextremumSlip)
        {
            driftAxis = FLWextremumSlip / (FLWextremumSlip * handBrakeForDriftMultiplier);
        }
        if(driftAxis > 1)
        {
            driftAxis = 1;
        }
        if(Mathf.Abs(XVelocity)> 2.5)
        {
            isDrifting = true;
        }else
            isDrifting = false;

        if(driftAxis < 1)
        {
            FLWFriction.extremumSlip = driftAxis * FLWextremumSlip * handBrakeForDriftMultiplier;
            FLW.sidewaysFriction = FLWFriction;

            FRWFriction.extremumSlip = driftAxis * FRWextremumSlip * handBrakeForDriftMultiplier;
            FRW.sidewaysFriction = FRWFriction;

            RLWFriction.extremumSlip = driftAxis * RLWextremumSlip * handBrakeForDriftMultiplier;
            BLW.sidewaysFriction = RLWFriction;

            RRWFriction.extremumSlip = driftAxis * RRWextremumSlip * handBrakeForDriftMultiplier;
            BRW.sidewaysFriction = RRWFriction;


        }
        isTractionLocked = true;
        carParticalsSystem.ApplyParticals();
    }
    private void ApplyTraction()
    {
        isTractionLocked = false;
        driftAxis = driftAxis - (Time.deltaTime / 1.5f);
        if(driftAxis < 0)
        {
            driftAxis = 0;
        }

        if (FLWFriction.extremumSlip > FLWextremumSlip)
        {
            FLWFriction.extremumSlip = driftAxis * FLWextremumSlip * handBrakeForDriftMultiplier;
            FLW.sidewaysFriction = FLWFriction;

            FRWFriction.extremumSlip = driftAxis * FRWextremumSlip * handBrakeForDriftMultiplier;
            FRW.sidewaysFriction = FRWFriction;

            RLWFriction.extremumSlip = driftAxis * RLWextremumSlip * handBrakeForDriftMultiplier;
            BLW.sidewaysFriction = RLWFriction;

            RRWFriction.extremumSlip = driftAxis * RRWextremumSlip * handBrakeForDriftMultiplier;
            BRW.sidewaysFriction = RRWFriction;
            Invoke("ApplyTraction", Time.deltaTime);
        }else
            if(FLWFriction.extremumSlip < FLWextremumSlip)
        {
            FLWFriction.extremumSlip =  FLWextremumSlip ;
            FLW.sidewaysFriction = FLWFriction;

            FRWFriction.extremumSlip = FRWextremumSlip;
            FRW.sidewaysFriction = FRWFriction;

            RLWFriction.extremumSlip = RLWextremumSlip;
            BLW.sidewaysFriction = RLWFriction;

            RRWFriction.extremumSlip = RRWextremumSlip;
            BRW.sidewaysFriction = RRWFriction;
            driftAxis = 0;
        }
    }
    private void UpdateWheelMesh()
    {

        Quaternion FLWRotation;
        Vector3 FLWPosition;
        FLW.GetWorldPose(out FLWPosition, out FLWRotation);
        FLWMesh.transform.position = FLWPosition;
        FLWMesh.transform.rotation = FLWRotation;

        Quaternion FRWRotation;
        Vector3 FRWPosition;
        FRW.GetWorldPose(out FRWPosition, out FRWRotation);
        FRWMesh.transform.position = FRWPosition;
        FRWMesh.transform.rotation = FRWRotation;

        Quaternion RLWRotation;
        Vector3 RLWPosition;
        BLW.GetWorldPose(out RLWPosition, out RLWRotation);
        BLWMesh.transform.position = RLWPosition;
        BLWMesh.transform.rotation = RLWRotation;

        Quaternion RRWRotation;
        Vector3 RRWPosition;
        BRW.GetWorldPose(out RRWPosition, out RRWRotation);
        BRWMesh.transform.position = RRWPosition;
        BRWMesh.transform.rotation = RRWRotation;
    }
}
    
