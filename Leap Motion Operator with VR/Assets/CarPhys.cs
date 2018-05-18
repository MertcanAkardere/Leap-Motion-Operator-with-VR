using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarPhys : MonoBehaviour {

    public Rigidbody rb;
    //public Text stateText;
    public TextMeshProUGUI stateText;

    public float speedLimit = 2.0f;
    public float straightSpeedLimit = 6.0f;
    public float turningSpeedLimit = 2.0f;

    public float turningSpeed = 2.0f;

    public float brakingPower = 1.0f;

    public float acceleration = 2.0f;
    public float turnAcceleration = 1.0f;

    public float CurrentVelocity = 0.0f;
    public float CurrentTurning = 0.0f;

    public float friction = 0.01f;
    public float rotfriction = 0.05f;

    //private bool turningLeft = false;
    //private bool turningRight = false;
//  private bool notTurning = false;  // Check with isTurning()
//  private bool goingForward = false; // decide if turning changes this? // isGoing() exists
    private bool stoppped = true;

    private void FixedUpdate()
    {
        //CurrentTurning  -= friction;
        //CurrentVelocity -= friction;
        stateText.text = stateMovement();
        applyFriction();

        var x = CurrentTurning * Time.deltaTime * 1.0f;
        var z = CurrentVelocity * Time.deltaTime * 1.0f;
        //Debug.Log("rot: " + x + " / vel: " + z);
        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        //transform.position += new Vector3(0.0f, 0.0f, z);
    }

    // MOVEMENT SET 1
    public void MS1_RightHand() // To be Called by Operator Input!
    {
        if (isTurningLeft())
            stabilize();
        else
            turnRight();
    }
    public void MS1_LeftHand() // To be Called by Operator Input!
    {
        if (isTurningRight())
            stabilize();
        else
            turnLeft();
    }
    public void MS1_BothHands() // To be Called by Operator Input!
    {
        if (isTurning())
            stabilize();
        moveForward();
    }
    public void MS1_Brakes()
    {
        applyBrakes();
    }

    // END OF MOVEMENT SET 1

    // MOVEMENT SET 2
    public void MS2_ForwardLeft() // To be Called by Operator Input!
    {
        //speedLimit = turningSpeedLimit;
        moveForward();
        turnLeft();
    }
    public void MS2_Forward() // To be Called by Operator Input!
    {
        //speedLimit = straightSpeedLimit;
        moveForward();
    }
    public void MS2_ForwardRight() // To be Called by Operator Input!
    {
        //speedLimit = turningSpeedLimit;
        moveForward();
        turnRight();
    }
    public void MS2_ReverseLeft() // To be Called by Operator Input!
    {
        //speedLimit = turningSpeedLimit;
        moveReverse();
        turnLeft();
    }
    public void MS2_Reverse() // To be Called by Operator Input!
    {
        speedLimit = turningSpeedLimit; // or straight?
        moveReverse();
    }
    public void MS2_ReverseRight() // To be Called by Operator Input!
    {
        //speedLimit = turningSpeedLimit;
        moveReverse();
        turnRight();
    }
    public void MS2_Brakes()
    {
        applyBrakes();
    }

    // END OF MOVEMENT SET 2



    // VEHICLE MOVEMENT
    private void moveForward()
    {
        //Debug.Log("PHYS: FORWARD");
        CurrentVelocity += acceleration;
        clampVelocity();
    }
    private void moveReverse()
    {
        //Debug.Log("PHYS: FORWARD");
        CurrentVelocity -= acceleration;
        clampVelocity();
    }
    private void clampVelocity()
    {
        // limit more if turning?
        CurrentVelocity = Mathf.Clamp(CurrentVelocity, -speedLimit, +speedLimit);
    }
    private void turnRight()
    {
        //isTurningRight() = true;
        CurrentTurning += turnAcceleration;
        clampTurning();
    }
    private void turnLeft()
    {
        //isTurningLeft() = true;
        CurrentTurning -= turnAcceleration;
        clampTurning();
    }
    private void clampTurning() { CurrentTurning = Mathf.Clamp(CurrentTurning, -turningSpeedLimit, +turningSpeedLimit); }

    private void stabilize()
    {
        if (isTurningRight())
        {
            CurrentTurning -= turnAcceleration;
            Mathf.Clamp(CurrentTurning, 0, turningSpeedLimit); // HARD STABILIZE. Change it to "slow down"
//            if(CurrentTurning==0)
//                turningRight = false;
        }
        if (isTurningLeft())
        {
            CurrentTurning += turnAcceleration;
            Mathf.Clamp(CurrentTurning, -turningSpeedLimit, 0);
//            if (CurrentTurning == 0)
//                isTurningLeft() = false;
        }
    }

    private void applyBrakes()
    {
        if (CurrentVelocity > 0)
        {
            if (CurrentVelocity > brakingPower)
                CurrentVelocity -= brakingPower;
            else
                CurrentVelocity = 0;
        }
        if (CurrentVelocity < 0)
        {
            if (CurrentVelocity < -brakingPower)
                CurrentVelocity += brakingPower;
            else
                CurrentVelocity = 0;
        }
    }

    private void applyFriction()
    {
        if (CurrentVelocity>0)
        {
            if (CurrentVelocity > friction)
                CurrentVelocity -= friction;
            else
                CurrentVelocity = 0; 
        }
        if (CurrentVelocity < 0)
        {
            if (CurrentVelocity < -friction)
                CurrentVelocity += friction;
            else
                CurrentVelocity = 0; 
        }

        if (CurrentTurning>0)
        {
            if (CurrentTurning > rotfriction)
                CurrentTurning -= rotfriction;
            else
                CurrentTurning = 0;
        }
        if (CurrentTurning < 0)
        {
            if (CurrentTurning < -rotfriction)
                CurrentTurning += rotfriction;
            else
                CurrentTurning = 0;
        }
    }

    private bool isTurning() { return (CurrentTurning != 0 ? true : false); }//{ return (turningLeft || turningRight); }
    private bool isGoing() { return (CurrentVelocity != 0 ? true : false); } // check if not going backwards
    private bool isTurningLeft() { return (CurrentTurning < 0 ? true : false); }
    private bool isTurningRight() { return (CurrentTurning > 0 ? true : false); }

    public string stateMovement() // Used to write velocity of the vehicle on the screen.
    {
        string state = "";

        state += "Velocity : " + System.Math.Round(CurrentVelocity, 2);

        if (isGoing())
        {
            //state += "Moving with: " + CurrentVelocity + " .";
            if (CurrentVelocity > 0)
                state += " Forward";
            else
                state += " Reversing";
        }
        else
            state += " Stationary";

        //state += "  \n  ";
        state += "\nRotation: " + System.Math.Round(CurrentTurning, 2);

        if (isTurning())
        {
            if (isTurningLeft())
                state += " Left";
                //state += " Turning Left with: " + CurrentTurning;
            else
                state += " Right";
                //state += " Turning Right with: " + CurrentTurning;
        }
        else
            state += " Not Turning";

        return state;
    }

    public void RestartLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
