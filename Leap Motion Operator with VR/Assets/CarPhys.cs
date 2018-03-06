using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhys : MonoBehaviour {

    public Rigidbody rb;

    public float speedLimit = 20.0f;
    public float turningSpeed = 5.0f;

    public float acceleration = 2.0f;
    public float turnAcceleration = 1.0f;

    public float CurrentVelocity = 0.0f;
    public float CurrentTurning = 0.0f;

    public float friction = 0.1f;

    private bool turningLeft = false;
    private bool turningRight = false;
//  private bool notTurning = false;  // Check with isTurning()
    private bool goingForward = false;
    private bool stoppped = true;

    private void FixedUpdate()
    {
        CurrentTurning  -= friction;
        CurrentVelocity -= friction;

        var x = CurrentTurning * Time.deltaTime * 150.0f;
        var z = CurrentVelocity * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }

    // MOVEMENT SET 1
    public void MS1_RightHand() // To be Called by Operator Input!
    {
        if (turningLeft)
            stabilize();
        else
            turnRight();
    }
    public void MS1_LeftHand() // To be Called by Operator Input!
    {
        if (turningRight)
            stabilize();
        else
            turnLeft();
    }
    // END OF MOVEMENT SET 1

    // VEHICLE MOVEMENT
    private void turnRight()
    {
        turningRight = true;
        CurrentTurning += turnAcceleration;
        clampTurning();
    }
    private void turnLeft()
    {
        turningLeft = true;
        CurrentTurning -= turnAcceleration;
        clampTurning();
    }
    private void clampTurning() { Mathf.Clamp(CurrentTurning, -turningSpeed, +turningSpeed); }

    private void stabilize()
    {
        if (turningRight)
        {
            CurrentTurning -= turnAcceleration;
            Mathf.Clamp(CurrentTurning, 0, turningSpeed); // HARD STABILIZE. Change it to "slow down"
            if(CurrentTurning==0)
                turningRight = false;
        }
        if (turningLeft)
        {
            CurrentTurning += turnAcceleration;
            Mathf.Clamp(CurrentTurning, -turningSpeed, 0);
            if (CurrentTurning == 0)
                turningLeft = false;
        }
    }

    private bool isTurning() { return (turningLeft || turningRight); }
    private bool isGoing() { return (CurrentVelocity > 0 ? true : false); }

    public string stateMovement() // Used to write velocity of the vehicle on the screen.
    {
        string state = "";

        if (isGoing())
        {
            state += "Moving with: " + CurrentVelocity + " .";
            if (isTurning())
            {
                if (turningLeft)
                    state += " Turning Left with: " + CurrentTurning;
                else
                    state += " Turning Right with: " + CurrentTurning;
            }
        }
        else
            state += "Stationary.";

        return state;
    }
}
