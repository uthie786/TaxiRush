using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;

public class TaxiCOntroller : MonoBehaviour
{
   private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentbreakForce;
    private bool isBreaking;
    private Rigidbody rb;
    
    [SerializeField] private float motorForce, breakForce, maxSteerAngle;
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;
    [SerializeField] private float maxSpeedKmH = 100f;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private GameObject flipTaxiHint;
    [SerializeField] private AudioSource engineSound;
    [SerializeField] private AudioSource tireScreechSound;
    private float speed; // Speed in km/h
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate() {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        LimitSpeed();
    }

    private void GetInput() {
        // Steering Input
        horizontalInput = Input.GetAxis("Horizontal");

        // Acceleration Input
        verticalInput = Input.GetAxis("Vertical");

        // Breaking Input
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor() {
        rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
        rearRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking() {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering() {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels() {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform) {
        Vector3 pos;
        Quaternion rot; 
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    private void Update()
    {
        speed = rb.velocity.magnitude * 1.6f;
        speedText.text = "Speed: " + speed.ToString("0") + " km/h";

        engineSound.pitch = Mathf.Lerp(1f, 3f, speed / maxSpeedKmH);
        
        if (isBreaking && speed > 50f)
        {
            if (!tireScreechSound.isPlaying)
            {
                tireScreechSound.Play();
            }
        }
        else
        {
            if (tireScreechSound.isPlaying)
            {
                tireScreechSound.Stop();
            }
        }
        
        if (Mathf.Abs(transform.rotation.z) > 60)
        {
            Debug.Log("Flipped");
            //flipTaxiHint.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R))
            {
                transform.position = new Vector3(transform.position.x , 0, transform.position.z);
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0);
            }
        }
    }
    private void LimitSpeed() {
        float maxSpeedMs = maxSpeedKmH / 1.6f; // Convert max speed to m/s
        if (rb.velocity.magnitude > maxSpeedMs) {
            rb.velocity = rb.velocity.normalized * maxSpeedMs;
        }
    }
}

