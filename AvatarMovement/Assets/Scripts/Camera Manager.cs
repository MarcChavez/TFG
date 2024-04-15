using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.IO;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    public RenderTexture overviewTexture;
    public InputActionReference takePhotoReference = null;
    public InputActionReference changeModeReference = null;
    
    private void Awake() {
        takePhotoReference.action.started += TakePhoto;
        changeModeReference.action.started += ChangeMode;
    }

    private void OnDestroy() {
        takePhotoReference.action.started -= TakePhoto;
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
            ExportPhoto();
        }
        else{
            Debug.Log("Toggle Selfie");
        }
    }

	// return file name

    public void ExportPhoto() {
        byte[] bytes = toTexture2D(overviewTexture).EncodeToPNG();
        var dirPath = Application.dataPath+"/Photos";
        if (!System.IO.Directory.Exists(dirPath)){
            System.IO.Directory.CreateDirectory(dirPath);
        }
        System.IO.File.WriteAllBytes(dirPath +"/Photo.png", bytes);
        Debug.Log("Printed");
    }

    Texture2D toTexture2D(RenderTexture rTex){
        Texture2D tex = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0,0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
}
