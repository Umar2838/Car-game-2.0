using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public WheelCollider frontRightWheel;
    public WheelCollider frontLeftWheel;
    public WheelCollider rearRightWheel;
    public WheelCollider rearLeftWheel;

    public Transform frontRightWheelTransform;
    public Transform frontLeftWheelTransform;
    public Transform rearRightWheelTransform;
    public Transform rearLeftWheelTransform;

    public float motorForce = 500f;
    public float steerAngle = 40f;
    public float breakForce = 1000f;
    public float driftStiffness = 0.5f; // Stiffness for drifting

    float verticalInput;
    float horizontalInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput();
        MotorForce();
        Steering();
        ApplyBreaks();
        Drift(); // Add drift logic
        UpdateWheel();
        // PowerSteering();
    }

    void GetInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
    }

    void ApplyBreaks()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Apply handbrake
            rearRightWheel.brakeTorque = breakForce;
            rearLeftWheel.brakeTorque = breakForce;
            GetComponent<Rigidbody>().linearDamping = 1f;
            SetRearFriction(driftStiffness);
        }
        else
        {
            rearRightWheel.brakeTorque = 0f;
            rearLeftWheel.brakeTorque = 0f;
            SetRearFriction(1f); // Reset stiffness to normal
            GetComponent<Rigidbody>().linearDamping = 0f;

        }
    }

    void MotorForce()
    {
        rearRightWheel.motorTorque = motorForce * verticalInput;
        rearLeftWheel.motorTorque = motorForce * verticalInput;
    }

    void Steering()
    {
        frontRightWheel.steerAngle = steerAngle * horizontalInput;
        frontLeftWheel.steerAngle = steerAngle * horizontalInput;
    }
    // void PowerSteering(){
    //     if(horizontalInput == 0){
    //         transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0,0,0),Time.deltaTime);
    //     }
    // }

    void UpdateWheel()
    {
        RotateWheel(frontRightWheel, frontRightWheelTransform);
        RotateWheel(frontLeftWheel, frontLeftWheelTransform);
        RotateWheel(rearRightWheel, rearRightWheelTransform);
        RotateWheel(rearLeftWheel, rearLeftWheelTransform);
    }

    void RotateWheel(WheelCollider wheelCollider, Transform transform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        transform.position = pos;
        transform.rotation = rot;
    }

    void Drift()
    {
        if (Mathf.Abs(horizontalInput) > 0.5f && verticalInput > 0.1f)
        {
            // Reduce grip on rear wheels during aggressive steering and throttle
            SetRearFriction(driftStiffness);
        }
        else
        {
            // Restore normal grip when not drifting
            SetRearFriction(1f);
        }
    }

    void SetRearFriction(float stiffness)
    {
        WheelFrictionCurve rearFriction = rearRightWheel.sidewaysFriction;
        rearFriction.stiffness = stiffness;
        rearRightWheel.sidewaysFriction = rearFriction;

        rearFriction = rearLeftWheel.sidewaysFriction;
        rearFriction.stiffness = stiffness;
        rearLeftWheel.sidewaysFriction = rearFriction;
    }
}
