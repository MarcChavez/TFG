using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdGenerator : MonoBehaviour
{
    public GuardarDatosEnPDF guardarDatosEnPDF;
    public CameraManager cameraManager;
    public String id;
    
    // Start is called before the first frame update
    void Start()
    {
        id =  PlayerPrefs.GetString("userId");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
