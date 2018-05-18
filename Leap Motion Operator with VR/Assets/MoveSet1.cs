using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveSet1 : MonoBehaviour {

    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;
    public TextMeshProUGUI brakeText;

    public bool LeftHand  = false;
    public bool RightHand = false;

    public float dzMove = 300; // deadzone for movement
    public float dzStop = 300; // deadzone for stopping

    public float dzLeft;
    public float dzRight;

    public GameObject vehicle;
    CarPhys controls; // for car physics 1

    private void Start()
    {
        GetVehicleControls();
        dzLeft = dzStop;
        dzRight = dzStop;
    }

    void FixedUpdate ()
    {
        //inputDecay();


        if (LeftHand)
        {
            if (RightHand)
            {
                //both hands
                controls.MS1_BothHands();
            }
            else
            {
                //only left hand
                controls.MS1_LeftHand();
            }
        }
        else if(RightHand)
        {
            //only right hand
            controls.MS1_RightHand();
        }
	}

    private void inputDecay()
    {
        if (brakeText.color.a > 0)
            brakeText.color = new Color(255, 0, 0, brakeText.color.a - 1);

        if (LeftHand)
            dzLeft--;
        else if (dzLeft < dzStop)
            dzLeft++;

        if (RightHand)
            dzRight--;
        else if (dzRight < dzStop)
            dzRight++;

        if (dzLeft < 0)
        {
            exitLeft();
            dzLeft = dzStop;
        }
        if (dzRight < 0)
        {
            exitRight();
            dzRight = dzStop;
        }
    }

    private void deadzoneCheck(bool isLeft)
    {
        if(isLeft)
        {
            if (!LeftHand)
            {
                if (dzLeft == 0)
                {
                    enterLeft();
                    dzLeft = dzMove;
                }
                else
                    dzLeft--;
            }
            else if (dzLeft < dzMove)
                dzLeft++;
        }
        else
        {
            if (!RightHand)
            {
                if (dzRight == 0)
                {
                    enterRight();
                    dzRight = dzMove;
                }
                else
                    dzRight--;
            }
            else if (dzRight < dzMove)
                dzRight++;
        }
    }
    private void GetVehicleControls()
    {
        //car physics 1
        controls = vehicle.GetComponent<CarPhys>();
    }

    // These are to be called by enter exit collisions from LP Sensors!
    public void enterRight() { RightHand = true; rightText.color = Color.green; }
    public void enterLeft() { LeftHand = true; leftText.color = Color.green; }
    public void exitRight() { RightHand = false; rightText.color = Color.red; }
    public void exitLeft() { LeftHand = false; leftText.color = Color.red; }

    public void applyBrakes() { controls.MS1_Brakes(); brakeText.color = Color.red; }

    public void LeftSensor()  { deadzoneCheck(true); }
    public void RightSensor() { deadzoneCheck(false); }
}
