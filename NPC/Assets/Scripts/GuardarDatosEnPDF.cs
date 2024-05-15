using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GuardarDatosEnPDF : MonoBehaviour
{
    [System.Serializable]
    public class Registro
    {
        public Vector3 posicion;
        public Vector3 rotacion;
        public string timestamp;
    }

    private List<Registro> registros = new List<Registro>();
    private float tiempoEntreCapturas = 1f; // Intervalo de tiempo entre cada captura (en segundos)

    void Start()
    {
        // Comenzar la captura de datos
        StartCoroutine(CapturarDatos());
    }

    IEnumerator CapturarDatos()
    {
        while (true)
        {
            // Crear registros
            Registro nuevoRegistro = new Registro
            {
                posicion = transform.position,
                rotacion = transform.eulerAngles,
                timestamp = System.DateTime.Now.ToString()
            };

            // Agregar el registro a la lista
            registros.Add(nuevoRegistro);

            // Esperar el tiempo especificado antes de la próxima captura
            yield return new WaitForSeconds(tiempoEntreCapturas);
        }
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

        // Especificar la ruta del archivo JSON donde se guardarán los datos
        string rutaArchivo = Application.dataPath + "/datos.json";

        // Escribir el JSON en el archivo
        File.WriteAllText(rutaArchivo, json);
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
