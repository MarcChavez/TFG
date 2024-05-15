using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.IO;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    public RenderTexture MainCamara;
    public RenderTexture NpcCamera;
    public InputActionReference takePhotoReference = null;
    public InputActionReference takePhotoNPC = null;
    public InputActionReference changeModeReference = null;
    private bool allowNpcPhoto = false;
    
    private void Awake() {
        takePhotoReference.action.started += TakePhoto;
        takePhotoNPC.action.started += takePhotoFromNPC;
        changeModeReference.action.started += ChangeMode;
        FindObjectOfType<PlayerInteract>().onEnterRange.AddListener(allowNpcCamera);
        FindObjectOfType<PlayerInteract>().onExitRange.AddListener(quitNpcCamera);
    }

    private void allowNpcCamera(){
        allowNpcPhoto = true;
    }

    private void quitNpcCamera(){
        allowNpcPhoto = false;
    }

    private void OnDestroy() {
        takePhotoReference.action.started -= TakePhoto;
        takePhotoNPC.action.started += takePhotoFromNPC;
        changeModeReference.action.started -= ChangeMode;
    }

    private void ChangeMode(InputAction.CallbackContext callbackContext){
       if (gameObject.transform.parent.gameObject.transform.parent.gameObject.activeSelf)
        {
            Debug.Log("Change");
            gameObject.transform.Rotate(180.0f, 0.0f,180.0f, Space.Self);
        } 
    }

    private void TakePhoto(InputAction.CallbackContext callbackContext) {
        if (gameObject.transform.parent.gameObject.transform.parent.gameObject.activeSelf)
        {
            ExportPhoto(MainCamara);
        }
        else{
            Debug.Log("Toggle Selfie");
        }
    }



    private void takePhotoFromNPC(InputAction.CallbackContext callbackContext) {
        if (allowNpcPhoto)
        {
            ExportPhoto(NpcCamera);
        }
        else{
            Debug.Log("Toggle Selfie");
        }
    }

	// return file name

    public void ExportPhoto(RenderTexture overviewTexture) {
        byte[] bytes = toTexture2D(overviewTexture).EncodeToPNG();
        var dirPath = Application.dataPath+"/Photos";
        if (!Directory.Exists(dirPath)){
            Directory.CreateDirectory(dirPath);
        }
        DateTime now = DateTime.Now;

        string nombreFoto = "photo_" + now.ToString("dd_MM-HH_mm_ss") + ".png";
        File.WriteAllBytes(dirPath +"/"+nombreFoto, bytes);
        Debug.Log("Printed");
    }

    Texture2D toTexture2D(RenderTexture rTex){
    Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGBA32, false);
    RenderTexture.active = rTex;
    tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
    tex.Apply();
    return tex;
}
}
