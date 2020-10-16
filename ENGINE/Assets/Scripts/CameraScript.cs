using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            Debug.LogError("No target assigned");
        }
        else
        {
            offset = new Vector3(this.transform.position.x - target.position.x, this.transform.position.y - target.position.y, this.transform.position.z - target.position.z);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position = target.position + offset;
    }
}
