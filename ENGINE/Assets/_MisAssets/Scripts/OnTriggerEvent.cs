using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class OnTriggerEvent : MonoBehaviour
{
    public string[] triggerTags;

    public UnityEvent onTriggerEnter = new UnityEvent();
    public UnityEvent onTriggerStay = new UnityEvent();
    public UnityEvent onTriggerExit = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        foreach(string s in triggerTags)
        {
            if(other.CompareTag(s))
            {
                onTriggerEnter?.Invoke();
                return;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        foreach (string s in triggerTags)
        {
            if (other.CompareTag(s))
            {
                onTriggerStay?.Invoke();
                return;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (string s in triggerTags)
        {
            if (other.CompareTag(s))
            {
                onTriggerExit?.Invoke();
                return;
            }
        }
    }

}
