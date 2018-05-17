using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class TestCourse : MonoBehaviour {

    public TextMeshProUGUI timeText;

    public int passedCheckpoints = 0;
    public int totalCheckpoint = 5;
    public float[] timeCP = new float[5];
	// Use this for initialization
	void Start () {
        Debug.Log("asd");
        print(Application.persistentDataPath+"");
        CrearArchivoCSV("test1");
	}
	
	// Update is called once per frame
	void Update () {
        timeText.text = "Time: " + Time.time +" s";
	}

    public void PassCheckpoint(int cp)
    {
        if (cp == passedCheckpoints + 1) // if next checkpoint is passed
        {
            passedCheckpoints++;
            if (cp < timeCP.Length)
                timeCP[cp] = Time.time;
        }
    }

 


    public void CrearArchivoCSV(string nombreArchivo)
    {

        string ruta = "C:\\Users\\mertc\\OneDrive\\Masaüstü" + "/" + nombreArchivo + ".csv";
        Debug.Log(ruta);
        //El archivo existe? lo BORRAMOS
        if (File.Exists(ruta))
        {
            File.Delete(ruta);
        }

        //Crear el archivo
        var sr = File.CreateText(ruta);

        string datosCSV = "valor1,valor2,valor3,valor4" + "\n";
        datosCSV += "valor1,valor2,valor3,valor4" + "/n";
        datosCSV += "valor1,valor2,valor3,valor4" + "/n";
        datosCSV += "valor1,valor2,valor3,valor4" + "/n";
        datosCSV += "valor1,valor2,valor3,valor4";

        sr.WriteLine(datosCSV);

        //Dejar como sólo de lectura
        FileInfo fInfo = new FileInfo(ruta);
        fInfo.IsReadOnly = true;

        //Cerrar
        sr.Close();
        

        //Abrimos archivo recien creado
        Application.OpenURL(ruta);
    }
    public int AtCheckpoint() { return passedCheckpoints; }

    public static implicit operator TestCourse(GameObject v)
    {
        throw new NotImplementedException();
    }
}
