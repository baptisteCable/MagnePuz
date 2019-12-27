using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool jumpRequired = false;
    private bool jumpRequested = false;

    public int requestedDirection = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetAxis("P1Jump") > .5f && !jumpRequested)
        {
            jumpRequired = true;
            jumpRequested = true;
        }

        if (Input.GetAxis ("P1Jump") <= .5f)
        {
            jumpRequested = false;
        }

        if (Input.GetAxis ("P1Horizontal") > .5f)
        {
            requestedDirection = 1;
        }
        else if (Input.GetAxis ("P1Horizontal") < -.5f)
        {
            requestedDirection = -1;
        }
        else
        {
            requestedDirection = 0;
        }
    }
}
