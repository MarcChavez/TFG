using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineFromData : MonoBehaviour
{
    [System.Serializable]
    public class Registro
    {
        public Vector3 posicion;
        public Vector3 rotacion;
        public string timestamp;
    }

    [System.Serializable]
    public class DataList
    {
        public Registro[] items;
    }

    
    public TextAsset jsonFile; // Asigna tu archivo JSON en el inspector
    public LineRenderer lineRenderer;
    public GameObject pointPrefab; // Prefab de punto para crear los GameObjects en cada punto

    public DataList dataList = new DataList();
    public Material firstMaterialPoke;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        ParseJSON();
        CreatePokers();
    }

    private void Update() {
        DrawLines();
    }

    private void ParseJSON()
    {
        if (jsonFile != null)
        {
            dataList = JsonUtility.FromJson<DataList>(jsonFile.text);
        }
        else
        {
            Debug.LogError("No se ha asignado ningún archivo JSON.");
        }
    }

    private void CreatePokers()
    {
        for (int i = 0; i < dataList.items.Length-1; i++)
        {
            // Crea un GameObject en el punto actual con la rotación correspondiente
            Vector3 rotation = new Vector3(dataList.items[i].rotacion.x + 90, dataList.items[i].rotacion.y, dataList.items[i].rotacion.z);
            Vector3 position = new Vector3(dataList.items[i].posicion.x, dataList.items[i].posicion.y+1.15f, dataList.items[i].posicion.z);
            GameObject point = Instantiate(pointPrefab, position, Quaternion.Euler(rotation), transform);
            if (i == 0) {
                Renderer renderer = point.GetComponent<Renderer>();
                if (renderer != null)
                {
                    // Asigna el nuevo material al Renderer
                    renderer.material = firstMaterialPoke;
                }
            }
        }
    }

    private void DrawLines()
    {
        lineRenderer.positionCount = dataList.items.Length-1;
        for (int i = 0; i < dataList.items.Length-1; i++)
        {
            // Crea una línea entre el punto actual y el punto anterior
            Vector3 position = new Vector3(dataList.items[i].posicion.x, dataList.items[i].posicion.y+1.15f, dataList.items[i].posicion.z);
            lineRenderer.SetPosition(i, position);
        }
    }
}
