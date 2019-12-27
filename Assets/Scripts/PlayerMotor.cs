using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private float jumpSpeed = 13.5f;
    private float maxMoveSpeed = 10f;
    private float maxFallSpeed = 18f;

    private float groundedAcceleration = 150f;
    private float airAcceleration = 30f;
    private float groundedDeceleration = 40f;
    private float airDeceleration = 10f;

    public Transform[] groundCastPositions;

    public LayerMask mask;

    private PlayerController controller;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start ()
    {
        controller = GetComponent<PlayerController> ();
        rb = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        bool grounded = IsGrounded ();

        Vector2 velocity = rb.velocity;

        // jump
        if (controller.jumpRequired)
        {
            controller.jumpRequired = false;

            if (grounded)
            {
                velocity.y = jumpSpeed;
            }
        }

        // air speed (falls)
        if (velocity.y < -maxFallSpeed)
        {
            velocity.y = -maxFallSpeed;
        }

        float acceleration;
        float deceleration;

        // horizontal speed
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

        // natural horizontal speed reduction
        if (Mathf.Abs (velocity.x) > maxMoveSpeed)
        {
            float dec = Mathf.Min (Mathf.Abs (velocity.x), deceleration * Time.fixedDeltaTime) * Mathf.Sign (velocity.x);
            velocity.x -= dec;
        }


        // direction
        if (controller.requestedDirection != 0)
        {
            float maxSpeed = Mathf.Max (Mathf.Abs (velocity.x), maxMoveSpeed);
            velocity.x += acceleration * Time.fixedDeltaTime * controller.requestedDirection;
            velocity.x = Mathf.Min (maxSpeed, velocity.x);
            velocity.x = Mathf.Max (-maxSpeed, velocity.x);
        }
        else
        {
            float dec = Mathf.Min (Mathf.Abs (velocity.x), deceleration * Time.fixedDeltaTime) * Mathf.Sign (velocity.x);
            velocity.x -= dec;
        }

        rb.velocity = velocity;
    }

    public bool IsGrounded ()
    {
        RaycastHit2D hit;
        Vector2 origin;

        foreach (Transform t in groundCastPositions)
        {
            origin = t.position;
            hit = Physics2D.Raycast (origin, Vector2.down, .05f, mask);

            if (hit.collider != null)
            {
                return true;
            }
        }
        return false;
    }
}
