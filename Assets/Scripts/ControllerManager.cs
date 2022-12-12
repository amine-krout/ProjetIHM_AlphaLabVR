using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerManager : MonoBehaviour
{
    public InputActionProperty switchMode;

    private List<GameObject> children;
    private int modeActive;
    private bool isTeleportationActive;
    public GameObject Equipments;



    // Start is called before the first frame update
    void Start()
    {
        NotificationManager.Instance.SetNewNotification("You arrived at the lab, check if you can walk and move your hands properly", 8f);
        StartCoroutine(ExecuteTPMessageAfterTime(8));
        switchMode.action.performed += ctx => PerformSwitchMode();
        isTeleportationActive = false;
        children = new List<GameObject>();
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in transform)
        {
            if (child.CompareTag("ControllerMode"))
            {
                children.Add(child.gameObject);
            }
        }

        foreach (GameObject child in children)
        {
            child.SetActive(false);
        }
        modeActive = 0;
        children[modeActive].SetActive(true);
    }

    private void Update()
    {
        

    }

    private void PerformSwitchMode()
    {
        foreach (GameObject child in children)
        {
            child.SetActive(false);
        }
        modeActive = (modeActive + 1) % children.Count;
        isTeleportationActive = (modeActive == 1) ? true : false;
        if (isTeleportationActive) {
            ExecuteGrabMessageAfterTime(4);
        }
        children[modeActive].SetActive(true);
    }


    IEnumerator ExecuteGrabMessageAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        NotificationManager.Instance.SetNewNotification("Try to grab an empty glass", 18f);
    }

    IEnumerator ExecuteTPMessageAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        NotificationManager.Instance.SetNewNotification("Teleport to the lab table and the to the computer table", 8f);
    }
}
