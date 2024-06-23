using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.Events;

public class GuardarDatosEnPDF : MonoBehaviour
{
    [System.Serializable]
    public class Registro
    {
        public Vector3 posicion;
        public Vector3 rotacion;
        public string timestamp;
        public string action;
        public string photoName;
    }

    private List<Registro> registros = new List<Registro>();
    public String id;
    public UnityEvent <Vector3, Quaternion> eventTP;
    public UnityEvent <string, string> eventPhoto;
    public string NAME = "test";
    public bool NAMEGRAB = true;

    void Start()
    {
        // Comenzar la captura de datos
        eventTP.AddListener(CapturarDatos);
        eventPhoto.AddListener(CapturarPhoto);
       if (NAMEGRAB) id =  PlayerPrefs.GetString("userId");
       else {
        id = NAME;
       }
    }

    public void CapturarDatos(Vector3 pos,Quaternion rotate)
    {
            // Crear registros
            Registro nuevoRegistro = new Registro
            {
                posicion = pos,
                rotacion = rotate.eulerAngles,
                timestamp = System.DateTime.Now.ToString(),
                action = "Teleport"
            };
            Debug.Log("aaa");

            // Agregar el registro a la lista
            registros.Add(nuevoRegistro);
    }

    public void CapturarPhoto(string name, string takenBy)
    {
            // Crear registros
            Registro nuevoRegistro = new Registro
            {
                timestamp = System.DateTime.Now.ToString(),
                action = "Photo_taken_by_"+takenBy,
                photoName = name
            };

            // Agregar el registro a la lista
            registros.Add(nuevoRegistro);
    }

    private void OnDestroy()
    {
        

        // Guardar la lista de registros en formato JSON
        GuardarJSON(registros);
    }

    private void GuardarJSON(List<Registro> datos)
    {
        // Convertir la lista de registros a formato JSON
        string json = JsonUtility.ToJson(new Wrapper(datos));

        if (id == "") id = "unnamed";

        // Especificar la ruta del archivo JSON donde se guardarán los datos
        string rutaArchivo = Application.dataPath + "/Data/"+id;
        string nombreArchivo = Application.dataPath + "/Data/"+id+"/datos.json";
        if (!Directory.Exists(rutaArchivo)){
            Directory.CreateDirectory(rutaArchivo);
            }

        // Escribir el JSON en el archivo
        File.WriteAllText(nombreArchivo, json);
    }

    // Clase de envoltura para evitar la serialización incorrecta de la lista de registros
    [System.Serializable]
    private class Wrapper
    {
        public List<Registro> items;

        public Wrapper(List<Registro> items)
        {
            this.items = items;
        }
    }
}
