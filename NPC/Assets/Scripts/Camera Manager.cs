using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    [System.Serializable]
    public class RegistroImagenes
    {
        public Texture2D bytes;
        public string photoPath;
        public string photoName;
    }

    private List<RegistroImagenes> registros = new List<RegistroImagenes>();
    public RenderTexture MainCamara;
    public RenderTexture NpcCamera;
    public InputActionReference takePhotoReference = null;
    public InputActionReference takePhotoNPC = null;
    public InputActionReference changeModeReference = null;
    private bool allowNpcPhoto = false;
    private bool isProcessingPhoto = false; 
    private AudioSource audioSource;
    public GameObject selfieStick;
    public GameObject Camera;
    public GameObject Screen;
    public bool selfieMode = false;
    private String id;
    public GuardarDatosEnPDF guardarDatosEnPDF;
    public bool guardaPhotosAvaliable = false;
    public string NAME;
    
    private void Awake() {
        audioSource = gameObject.GetComponent<AudioSource>();
        takePhotoReference.action.started += TakePhoto;
        takePhotoNPC.action.started += takePhotoFromNPC;
        changeModeReference.action.started += ChangeMode;
        if (name ==  "test") id =  PlayerPrefs.GetString("userId");
       else {
        id = NAME;
       }
    }

    public void allowNpcCamera(){
        allowNpcPhoto = true;
    }

    public void quitNpcCamera(){
        allowNpcPhoto = false;
    }

    private void OnDestroy() {
        takePhotoReference.action.started -= TakePhoto;
        takePhotoNPC.action.started -= takePhotoFromNPC;
        changeModeReference.action.started -= ChangeMode;
        guardaPhotos();
    }

    private void ChangeMode(InputAction.CallbackContext callbackContext){
       if (selfieStick.activeSelf)
        {
            Debug.Log("Change");
            Camera.transform.Rotate(180.0f, 0.0f,180.0f, Space.Self);
            selfieMode = !selfieMode;
            Screen.transform.localScale = new Vector3(-Screen.transform.localScale.x, Screen.transform.localScale.y, Screen.transform.localScale.z);
        } 
    }

    private void TakePhoto(InputAction.CallbackContext callbackContext) {
         if (isProcessingPhoto) return;
        
        if (selfieStick.activeSelf && !audioSource.isPlaying)
        {
            isProcessingPhoto = true;
            PlayPhotoSound();
            if (guardaPhotosAvaliable) {
                string nameP = ExportPhoto(MainCamara);
                guardarDatosEnPDF.eventPhoto.Invoke(nameP, "User");
            }
            isProcessingPhoto = false;
        }
        else{
            Debug.Log("Toggle Selfie");
        }
    }



    private void takePhotoFromNPC(InputAction.CallbackContext callbackContext) {
        if (isProcessingPhoto) return;
        
        if (allowNpcPhoto)
        {
            isProcessingPhoto = true;
            PlayPhotoSound();
            isProcessingPhoto = false;
            if (guardaPhotosAvaliable) {
                string nameP = ExportPhoto(NpcCamera);
                guardarDatosEnPDF.eventPhoto.Invoke(nameP, "NPC");
            }
        }
        else{
            Debug.Log("Toggle Selfie");
        }
    }

	// return file name

    private string ExportPhoto(RenderTexture overviewTexture) {
        Texture2D texture = toTexture2D(overviewTexture);
        var dirPath = Application.dataPath+"/Photos/"+id;
        DateTime now = DateTime.Now;
        string nombreFoto = "photo_" + now.ToString("dd_MM-HH_mm_ss") + ".png";
        RegistroImagenes nuevoRegistro = new RegistroImagenes
            {
                bytes = texture,
                photoPath = dirPath,
                photoName = nombreFoto
        };
        registros.Add(nuevoRegistro);
        return nombreFoto;
    }

    private void guardaPhotos() {
        for (int i = 0; i  < registros.Count; ++i)
        {
            if (!Directory.Exists(registros[0].photoPath)){
            Directory.CreateDirectory(registros[0].photoPath);
            }
            byte[] photoBytes =  registros[i].bytes.EncodeToPNG();
            File.WriteAllBytes(registros[i].photoPath +"/"+ registros[i].photoName, photoBytes);
            Debug.Log("Printed");
        }
    }

    Texture2D toTexture2D(RenderTexture rTex){
        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGBA32, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

    private void PlayPhotoSound() {
    if (audioSource != null)
    {
        audioSource.Play();
    }
}
}
