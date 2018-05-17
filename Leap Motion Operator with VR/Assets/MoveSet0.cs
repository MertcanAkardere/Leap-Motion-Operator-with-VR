using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSet0 : MonoBehaviour {

    CarPhys controls;
    public GameObject vehicle;


    // Use this for initialization
    void Start () {
        GetVehicleControls();
    }
	
	// Update is called once per frame
	void Update () {
        var z = Input.GetAxis("Horizontal");
        var x = Input.GetAxis("Vertical");
        if(x>0)
        {
            if (z > 0)
                controls.MS2_ForwardRight();
            else if (z < 0)
                controls.MS2_ForwardLeft();
            else
                controls.MS2_Forward();
        }
        else if (x < 0)
        {
            if (z > 0)
                controls.MS2_ReverseRight();
            else if (z < 0)
                controls.MS2_ReverseLeft();
            else
                controls.MS2_Reverse();
        }
        else
        {/*
            if (z > 0)
                controls.MS2_();
            else if (z < 0)
                controls.MS2_Left();*/
        }

    }

    private void GetVehicleControls()
    {
        //car physics 1
        controls = vehicle.GetComponent<CarPhys>();
    }
}
