using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Body))]
public class Engine : MonoBehaviour
{
    public EngineDefinition definition;
    public bool isRunning;
    private GameObject[] m_engineHardPoints;

    [Range(0, 1)]
    public float throttle;

    [Range(0, 1)]
    public float brake;

    [Range(-1, 1)]
    public float turn;

    public void StartEngine()
    {
        isRunning = true;
    }

    public void ShutdownEngine()
    {
        isRunning = false;
    }

    public bool CanStartEngine()
    {
        return !isRunning;
    }

    public bool CanShutDownEngine()
    {
        return isRunning;
    }

    public void ApplyDefinition(EngineDefinition definition)
    {
        this.definition = definition;
        BroadcastMessage(Messages.EngineDefinitionChanged);
    }

    private void EngineDefinitionChanged()
    {
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.drag = definition.drag;
        rigidbody.angularDrag = definition.angularDrag;
    }

    private void BodyDefinitionChanged()
    {
        m_engineHardPoints = GetComponent<Body>().GetHardPoints("Engine");

        if (!m_engineHardPoints.Any())
        {
            enabled = false;
            throw new Exception("Vehicle has no engine hard points and will not move. Disabling the engine.");
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        ApplyDefinition(definition);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var rigidbody = GetComponent<Rigidbody>();

        if (isRunning)
        {
            if (Math.Abs(turn) > 0.001f)
            {
                float speedT = rigidbody.velocity.magnitude / definition.maxForwardVelocity;
                float speedScalar = definition.turnSpeedCurve.Evaluate(speedT);
                float finalTurnSpeed = definition.turnSpeed * speedScalar;
                transform.Rotate(Vector3.up * turn * finalTurnSpeed, Space.Self);
            }

            Vector3 forceDirection = transform.forward * throttle - transform.forward * brake;
            Vector3 force = forceDirection * definition.acceleration;

            foreach (GameObject engineHardPoint in m_engineHardPoints)
            {
                
            }

            if (brake > 0)
            {

            }
            else if (throttle > 0)
            {
                rigidbody.AddForce(force, ForceMode.Acceleration);
            }
            else
            {
                //Vector3 naturalBrakingForce = Vector3.Project(-rigidbody.velocity, transform.forward) * definition.naturalBrakingForce;
                Vector3 naturalBrakingForce = -rigidbody.velocity * definition.naturalBrakingForce;
                rigidbody.AddForce(naturalBrakingForce, ForceMode.Impulse);
                //rigidbody.velocity = rigidbody.velocity * definition.naturalBrakingForce;
            }
        }

        ClampSpeeds();
    }

    private void ClampSpeeds()
    {
        var rigidbody = GetComponent<Rigidbody>();

        float clampedVelocityMag = Mathf.Clamp(rigidbody.velocity.magnitude, -definition.maxReverseVelocity, definition.maxForwardVelocity);
        rigidbody.velocity = rigidbody.velocity.normalized * clampedVelocityMag;

        float clampedAngularVelocity = Mathf.Clamp(rigidbody.angularVelocity.magnitude, -definition.maxAngularVelocity, definition.maxAngularVelocity);
        rigidbody.angularVelocity = rigidbody.angularVelocity.normalized * clampedAngularVelocity;
    }

    public static class Messages
    {
        public static readonly string EngineDefinitionChanged = "EngineDefinitionChanged";
    }
}