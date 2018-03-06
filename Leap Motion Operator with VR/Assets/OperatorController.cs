using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorController : MonoBehaviour {


	public Rigidbody rb;
	public float forwardSpeed = 2000.0f;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{

		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);
		
	}

	public void goForward()
	{
		//x = Time.deltaTime * 250.0f;
		rb.AddForce(0, 0, forwardSpeed * Time.deltaTime);
        
	}
    public void turnRight()
    {
        var r = 5.0f * Time.deltaTime;
        transform.Rotate(0, r, 0);
    }
    public void turnLeft()
    {
        var r = 5.0f * Time.deltaTime;
        transform.Rotate(0, -r, 0);
    }
}
