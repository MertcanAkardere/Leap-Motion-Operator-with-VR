using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSensor : MonoBehaviour {

    public bool isLeftHand = true;
    public bool isBrake = false;
    public GameObject Operator;
    private MoveSet1 moveset1;


    public void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("LeapHands"))
        //{
            if (isBrake)
            {
                Debug.Log("Brake enter. " + other.name);
                return;
            }
            // if other is hand
            ChangeSensorData(true);
            if (isLeftHand)
                Debug.Log("Left hand enter. " + other.name);
            else
                Debug.Log("Right hand enter. " + other.name);
        //}
        //else
          //  Debug.Log("foreign obj: " + other.name);
    }
    public void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.CompareTag("LeapHands"))
        //{
            if (isBrake)
            {
                Debug.Log("Brake exit.");
                return;
            }
            // if other is hand
            ChangeSensorData(false);
            if (isLeftHand)
                Debug.Log("Left hand exit.");
            else
                Debug.Log("Right hand exit.");
        //}
    }

    
    private void OnTriggerStay(Collider other)
    {
        //if (other.gameObject.CompareTag("LeapHands"))
        //{
            if (isBrake)
            {
                moveset1.applyBrakes();
                return;
            }
            if (isLeftHand)
                moveset1.LeftSensor();
            else
                moveset1.RightSensor();
        //}
    }
    

    // deprecated
    private void ChangeSensorData(bool enter)
    {
        //if left if right
        if (isLeftHand)
        {
            if (enter)
                moveset1.enterLeft();
            else
                moveset1.exitLeft();
        }
        else
        {
            if (enter)
                moveset1.enterRight();
            else
                moveset1.exitRight();
        }

    }

    // Use this for initialization
    void Start ()
    {
            moveset1 = Operator.GetComponent<MoveSet1>();

    }
	

}
