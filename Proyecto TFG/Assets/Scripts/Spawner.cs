using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject male;
    public GameObject female;
    public NpcInteract[] npc;
    public bool selectGender;
    public int genderSelected = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        int gender = PlayerPrefs.GetInt("gender");
        if (selectGender){
            gender = genderSelected;
        }
        if (gender == 1) {
            male.SetActive(false);
            CameraManager childScript = male.GetComponentInChildren<CameraManager>();
            foreach (NpcInteract child in npc)
            {
                child.setPlayer(childScript, male.transform);
            }
        }
        else{
            female.SetActive(false);
            CameraManager childScript = female.GetComponentInChildren<CameraManager>();
            foreach (NpcInteract child in npc)
            {
                child.setPlayer(childScript, female.transform);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
