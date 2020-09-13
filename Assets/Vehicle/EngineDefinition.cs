using UnityEngine;

[CreateAssetMenu]
public class EngineDefinition : ScriptableObject
{
    public float idleSpeed;
    public float acceleration;
    public float maxForwardVelocity;
    public float maxReverseVelocity;
    public float maxAngularVelocity;
    public float naturalBrakingForce;

    public float turnSpeed;
    public AnimationCurve turnSpeedCurve;

    public float drag;
    public float angularDrag;
}