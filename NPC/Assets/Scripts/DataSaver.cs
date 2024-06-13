using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DataSaver : MonoBehaviour
{
    
     public TMP_InputField inputField;
     private int gender = 0;
     public TMP_Dropdown dropdown;
     private bool photo = true;
     public bool a = false;

    void Start()
    {
        if (dropdown != null)
        {
            // Añadir un listener para cuando el valor del dropdown cambie
            dropdown.onValueChanged.AddListener(delegate { DropdownValueChanged(dropdown); });
        }

    }

    public void ChangePhoto(){
        photo = !photo;
    }

    // Método que se llama cuando el valor del dropdown cambia
    void DropdownValueChanged(TMP_Dropdown change)
    {
        // Obtener el valor seleccionado
        int value = change.value;
        gender = value;
    }

    private void Update() {
        if (a){
            saveIdUser();
        }
    }

    void OnDestroy()
    {
        if (dropdown != null)
        {
            // Remover el listener para evitar posibles errores
            dropdown.onValueChanged.RemoveAllListeners();
        }
    }
    public void saveIdUser() {
        string inputValue = inputField.text;
        Debug.Log("Input Value: " + inputValue);
        PlayerPrefs.SetString("userId", inputValue);
        PlayerPrefs.SetInt("gender", gender);
        Debug.Log("Gender Value: " + gender);
        if (photo){
            SceneManager.LoadScene("TFG Scene");
        }
        else {
            SceneManager.LoadScene("TFG Scene NoPhoto");
        }
        
    }
}
