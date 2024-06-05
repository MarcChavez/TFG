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
    
    private void Awake() {
        audioSource = gameObject.GetComponent<AudioSource>();
        takePhotoReference.action.started += TakePhoto;
        takePhotoNPC.action.started += takePhotoFromNPC;
        changeModeReference.action.started += ChangeMode;
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
       if (gameObject.transform.parent.gameObject.transform.parent.gameObject.activeSelf)
        {
            Debug.Log("Change");
            gameObject.transform.Rotate(180.0f, 0.0f,180.0f, Space.Self);
        } 
    }

    private void TakePhoto(InputAction.CallbackContext callbackContext) {
         if (isProcessingPhoto) return;
        
        if (gameObject.transform.parent.gameObject.transform.parent.gameObject.activeSelf && !audioSource.isPlaying)
        {
            isProcessingPhoto = true;
            PlayPhotoSound();
            ExportPhoto(MainCamara);
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
            ExportPhoto(NpcCamera);
            isProcessingPhoto = false;
        }
        else{
            Debug.Log("Toggle Selfie");
        }
    }

	// return file name

    private void ExportPhoto(RenderTexture overviewTexture) {
        Texture2D texture = toTexture2D(overviewTexture);
        var dirPath = Application.dataPath+"/Photos";
        DateTime now = DateTime.Now;
        string nombreFoto = "photo_" + now.ToString("dd_MM-HH_mm_ss") + ".png";
        RegistroImagenes nuevoRegistro = new RegistroImagenes
            {
                bytes = texture,
                photoPath = dirPath,
                photoName = nombreFoto
        };
        registros.Add(nuevoRegistro);
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
