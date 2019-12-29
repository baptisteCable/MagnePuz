using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool jumpRequired = false;
    private bool jumpRequested = false;

    public int horizontalDirection = 0;

    public int playerNumber;

    private string jumpAxis;
    private string invertAxis;
    private string verticalAxis;
    private string horizontalAxis;

    // Start is called before the first frame update
    void Start ()
    {
        horizontalAxis = "P" + playerNumber + "Horizontal";
        verticalAxis = "P" + playerNumber + "Vertical";
        jumpAxis = "P" + playerNumber + "Jump";
        invertAxis = "P" + playerNumber + "Invert";
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetAxis (jumpAxis) > .5f && !jumpRequested)
        {
            jumpRequired = true;
            jumpRequested = true;
        }

        if (Input.GetAxis (jumpAxis) <= .5f)
        {
            jumpRequested = false;
        }

        if (Input.GetAxis (horizontalAxis) > .5f)
        {
            horizontalDirection = 1;
        }
        else if (Input.GetAxis (horizontalAxis) < -.5f)
        {
            horizontalDirection = -1;
        }
        else
        {
            horizontalDirection = 0;
        }
    }
}
