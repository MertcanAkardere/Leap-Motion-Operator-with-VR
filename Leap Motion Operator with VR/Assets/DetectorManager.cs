using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetectorManager : MonoBehaviour {

    public TextMeshProUGUI extendedFingerText;
    public TextMeshProUGUI fingerDirectionText;
    public TextMeshProUGUI palmDirectionText;

    // Use this for initialization
    void Start () {
		
	}
	
	public void ExtendedFingerSensor(int gesture, bool activate)
    {

    }
    public void ExtendedFingers_Rock_Activate()
    {
        extendedFingerText.text = "Rock on!";
    }
    public void ExtendedFingers_Rock_Deactivate()
    {
        extendedFingerText.text = "Meh..";
    }

    public void FingerDirectionSensor(int gesture, bool activate)
    {

    }
    public void FingerDirection_Activate()
    {
        fingerDirectionText.text = "This way!";
    }
    public void FingerDirection_Deactivate()
    {
        fingerDirectionText.text = "...";
    }

    public void PalmDirectionSensor(int gesture, bool activate)
    {

    }
    public void PalmDirection_Activate()
    {
        palmDirectionText.text = "Move!";
    }
    public void PalmDirection_Deactivate()
    {
        palmDirectionText.text = "...";
    }
}
