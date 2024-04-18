using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Start is called before the first frame update
    // Velocidad de rotación en grados por segundo
    public float velocidadRotacion = 90f;

    void Update()
    {
        // Calcula la cantidad de rotación a aplicar en este frame
        float cantidadRotacion = velocidadRotacion * Time.deltaTime;

        // Aplica la rotación al objeto en el eje Y (puedes cambiar el eje según tus necesidades)
        transform.Rotate(Vector3.up, cantidadRotacion);
    }
}
