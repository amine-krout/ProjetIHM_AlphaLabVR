using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Test : MonoBehaviour
{
    //public OVRHand gameObject
    public GameObject handPresence;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);
        
        //gameObject = GetComponent<OVRHand>();
        if (devices.Count == 1)
        {
            handPresence.SetActive(false);
        }
        else
        {
            handPresence.SetActive(true);
        }
    }
}
