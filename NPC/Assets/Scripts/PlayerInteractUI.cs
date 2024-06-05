using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private PlayerInteract playerInteract;

    public void Show() {
        containerGameObject.SetActive(true);
    }

    public void Hide() {
        containerGameObject.SetActive(false);
    }

}
