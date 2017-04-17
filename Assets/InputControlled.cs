using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class InputControlled : MonoBehaviour
{
    protected Rigidbody rb;
    protected Text speedGUI;
    protected Text thrustGUI;

    public float acceleration = 400;
    public float thrustExpo = 10;
    public float torque = 10;
    public float torqueExpo = 10;
    public float counterDragFactor = 0.8f;

    public GameObject engine;
    public float lightIntensityMultiplier = 0.01f;
    public float particleEmissionMultiplier = 1.5f;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speedGUI = GameObject.Find("Speed").GetComponent<Text>();
        thrustGUI = GameObject.Find("Thrust").GetComponent<Text>();
    }

    void Update()
    {
        //Debug.DrawLine(transform.position, transform.position + rb.velocity, Color.red);
    }

    // Update is called once per iteration
    void FixedUpdate()
    {
        // Fetch inputs
        float thrustInput = Input.GetAxis("Thrust");
        float yawInput = Input.GetAxis("Yaw");
        float rollInput = Input.GetAxis("Roll");
        float pitchInput = Input.GetAxis("Pitch");

        // Apply yaw, roll and pitch
        rb.AddRelativeTorque(Vector3.up * ApplyExpo(yawInput, torqueExpo) * torque / Time.fixedDeltaTime);
        rb.AddRelativeTorque(Vector3.forward * ApplyExpo(rollInput, torqueExpo) * torque / Time.fixedDeltaTime);
        rb.AddRelativeTorque(Vector3.left * ApplyExpo(pitchInput, torqueExpo) * torque / Time.fixedDeltaTime);

        // Compute thrust
        float thrust = ApplyExpo(thrustInput, thrustExpo) * acceleration;

        Vector3 thrustVector = Vector3.zero;
        if (thrust != 0) { // Tiny optimization to skip vector computation when thrust is zero
            // Compute counter drag force: prevent too much drift in any direction but Z
            Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity); // get local velocity

            localVelocity.z = 0; // Remove "forward" component because it's already applied by standard thrust
            localVelocity.Normalize(); // Then normalize it
            Vector3 counterDragForce = -localVelocity * Mathf.Abs(thrust) * counterDragFactor;

            // Apply thrust
            thrustVector = Vector3.forward * thrust + counterDragForce;
            rb.AddRelativeForce(thrustVector / Time.fixedDeltaTime);
        }
        UpdateEngine(thrustVector);

        speedGUI.text = "Speed: " + rb.velocity.magnitude;
        thrustGUI.text = "Thrust: " + thrust;
    }

    protected float ApplyExpo(float input, float expo)
    {
        return Mathf.Sign(input) * (Mathf.Pow(expo, Mathf.Abs(input)) - 1) / (expo - 1);
    }

    protected void UpdateEngine(Vector3 thrustVector)
    {
        if (null == engine)
        {
            return;
        }

        // Change the amount of light emitted by engine
        EllipsoidParticleEmitter emitter = engine.GetComponent<EllipsoidParticleEmitter>();
        emitter.maxEmission = thrustVector.magnitude * particleEmissionMultiplier;

        // Change amount of particles emitted by engine
        Light light = engine.GetComponentInChildren<Light>();
        light.intensity = thrustVector.magnitude * lightIntensityMultiplier;

        // Change engine direction to match the physics @WIP
        //engine.transform.LookAt(engine.transform.TransformDirection(-thrustVector));
        //Debug.DrawLine(transform.position, transform.TransformDirection(-thrustVector), Color.red);
    }
}
