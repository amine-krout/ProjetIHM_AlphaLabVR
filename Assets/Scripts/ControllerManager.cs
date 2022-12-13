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

    private List<GameObject> equipment;
    public GameObject selectedEquipment;
    private List<Vector3> initialPositions;
    private bool positionsChanged = false;
    private bool startUpdate = false;


    // Start is called before the first frame update
    void Start()
    {
        NotificationManager.Instance.SetNewNotification("You arrived at the lab, check if you can walk and move your hands properly", 12f);
        StartCoroutine(ExecuteTPMessageAfterTime(12));
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

        equipment = new List<GameObject>();
        initialPositions = new List<Vector3>();
        Transform[] allEquipment = selectedEquipment.GetComponentsInChildren<Transform>();
        foreach (Transform child in selectedEquipment.transform)
        {
            if (child.CompareTag("LabMaterial"))
            {
                equipment.Add(child.gameObject);
                initialPositions.Add(child.gameObject.transform.position);
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
        if(!positionsChanged && startUpdate) { 
            for (int i = 0; i < equipment.Count; i++)
            {
                if (!equipment[i].gameObject.transform.position.Equals(initialPositions[i]))
                {
                    positionsChanged = true;
                    StartCoroutine(ExecuteTurnItMessageAfterTime(12));
                    StartCoroutine(ExecuteFinalMessageAfterTime(22));
                }
            }
        }

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
            StartCoroutine(ExecuteGrabMessageAfterTime(8));
        }
        children[modeActive].SetActive(true);
    }
    IEnumerator ExecuteTurnItMessageAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        NotificationManager.Instance.SetNewNotification("Great! Grab another glass and turn one into the other", 12f);
    }

    IEnumerator ExecuteFinalMessageAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        NotificationManager.Instance.SetNewNotification("Congratulations, you finished the tutorial!", 28f);
    }

    IEnumerator ExecuteGrabMessageAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        NotificationManager.Instance.SetNewNotification("Try to grab a glass using the grip button", 14f);
        startUpdate = true;
    }

    IEnumerator ExecuteTPMessageAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        NotificationManager.Instance.SetNewNotification("Press Y (Left hand) and then try to teleport to the lab table", 12f);
    }
}
