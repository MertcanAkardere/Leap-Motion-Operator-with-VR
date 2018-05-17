using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveSet2 : MonoBehaviour {

    // texts
    public TextMeshProUGUI directionText;

    public int inputDirection = 0;
    //public bool[] inputDirection = new bool[3]; // Left Forward Right
    public bool manoeuvring = false;

    public bool manoeuvreToRight = false;
    public int manoeuvreTime = 500; // depending on speed?
    public int manoeuvreTimeLeft = 0;


    public GameObject vehicle;
    CarPhys controls; // for car physics 1

    private void Start()
    {
        GetVehicleControls();
    }

        private void FixedUpdate()
    {

        if(!manoeuvring)
        {
            //Debug.Log("WAT");
            switch (inputDirection)
            {
                case 0: directionText.text = "..."; break;
                case -1: controls.MS2_Brakes(); directionText.text = "Brakes"; break;
                case 7: controls.MS2_ForwardLeft(); directionText.text = "Forward Left"; break;
                case 8: controls.MS2_Forward(); directionText.text = "Forward"; break;
                case 9: controls.MS2_ForwardRight(); directionText.text = "Forward Right"; break;
                case 1: controls.MS2_ReverseLeft(); directionText.text = "Reverse Left"; break;
                case 2: controls.MS2_Reverse(); directionText.text = "Reverse"; break;
                case 3: controls.MS2_ReverseRight(); directionText.text = "Reverse Right"; break;
                default: break; // error: unrecognized direction
            }
        }
        else
        {
            if (manoeuvreToRight)
            {
                if(manoeuvreTimeLeft > manoeuvreTime/2) // first half
                    controls.MS2_ForwardRight();
                else
                    controls.MS2_ForwardLeft();
            }
            else
            {
                if (manoeuvreTimeLeft > manoeuvreTime / 2) // first half
                    controls.MS2_ForwardLeft();
                else
                    controls.MS2_ForwardRight();
            }

            manoeuvreTimeLeft--;

            if (manoeuvreTimeLeft <= 0)
                manoeuvring = false;
        }
    }



    private void GetVehicleControls()
    {
        //car physics 1
        controls = vehicle.GetComponent<CarPhys>();
    }
    //public void enterDirection(int dir) { inputDirection[dir] = true; }
    //public void exitDirection(int dir) { inputDirection[dir] = false; }
    public void enterDirection(int dir) { inputDirection = dir; }
    public void exitDirection(int dir) { if(dir == inputDirection) inputDirection = 0; } // don't reset if already changed directions

    public void enterManoeuvre(bool toRight)
    {
        if (!manoeuvring)
        {
            manoeuvring = true;
            manoeuvreTimeLeft = manoeuvreTime;
            manoeuvreToRight = toRight;
        }
    }

}
