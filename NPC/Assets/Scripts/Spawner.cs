using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject male;
    public GameObject female;
    
    // Start is called before the first frame update
    void Start()
    {
        int gender = PlayerPrefs.GetInt("gender");
        if (gender == 1) {
            male.SetActive(false);
        }
        else{
            female.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
