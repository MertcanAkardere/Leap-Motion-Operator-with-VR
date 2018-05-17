using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpointer : MonoBehaviour
{

    public int CheckpointNo;
    private TestCourse testCourse;

    // Use this for initialization
    void Start()
    {
        GameObject d = this.transform.parent.gameObject;
        testCourse = d;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
 
        if (!other.CompareTag("Player"))
            return; 
        else
        {
            testCourse.PassCheckpoint(CheckpointNo);
        }

    }
}
