using UnityEngine;

public class PlayerPlatformerMovementMotor : PhysicsObject
{
    public float jumpTakeOffSpeed = 14;

    private float maxMoveSpeed = 7f;
    private float maxFallSpeed = 18f;

    public float groundedAcceleration = 100f;
    public float airAcceleration = 15f;
    public float groundedDeceleration = 40f;
    public float airDeceleration = 10f;
    public float neutralDecelerationCoeff = .5f;

    private PlayerController controller;

    private void Start ()
    {
        controller = GetComponent<PlayerController> ();
    }

    protected override void ComputeVelocity ()
    {
        // jump
        if (controller.jumpRequired)
        {
            controller.jumpRequired = false;

            if (grounded)
            {
                velocity.y = jumpTakeOffSpeed;
            }
        }

        float acceleration;
        float deceleration;

        if (grounded)
        {
            acceleration = groundedAcceleration;
            deceleration = groundedDeceleration;
        }
        else
        {
            acceleration = airAcceleration;
            deceleration = airDeceleration;
        }

        if (controller.horizontalDirection == 0)
        {
            float sign = Mathf.Sign (velocity.x);
            targetVelocity.x = sign * Mathf.Max (0, sign * velocity.x - acceleration * neutralDecelerationCoeff * Time.deltaTime);

        }
        else
        {
            float sign = Mathf.Sign (controller.horizontalDirection);
            if (Mathf.Abs (velocity.x) > maxMoveSpeed && sign * velocity.x > 0)
            {
                targetVelocity.x = sign * Mathf.Max (maxMoveSpeed, sign * velocity.x - deceleration * Time.fixedDeltaTime);
            }
            else
            {
                targetVelocity.x = sign * Mathf.Min (maxMoveSpeed, sign * velocity.x + acceleration * Time.fixedDeltaTime);
            }
        }
    }
}