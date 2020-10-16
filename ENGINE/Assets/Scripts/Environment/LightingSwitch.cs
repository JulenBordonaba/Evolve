using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject lighting1;
    [SerializeField]
    private GameObject lighting2;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (lighting1.activeInHierarchy)
            {
                lighting1.SetActive(false);
                lighting2.SetActive(true);
            }
            else
            {
                lighting2.SetActive(false);
                lighting1.SetActive(true);
            }
            
        }
    }
}
