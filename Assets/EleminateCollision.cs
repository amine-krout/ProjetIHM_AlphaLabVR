using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EleminateCollision : MonoBehaviour
{
    private List<GameObject> children;
    // Start is called before the first frame update
    void Start()
    {
        children = new List<GameObject>();
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in transform)
        {
            if (child.CompareTag("LabMaterial"))
            {
                children.Add(child.gameObject);
            }
        }

        foreach (GameObject child in children)
        {
            foreach (GameObject child2 in children)
            {
                if (!child.Equals(child2))
                {
                    GameObject firstChild = child.transform.GetChild(0).gameObject;
                    GameObject firstChild2 = child2.transform.GetChild(0).gameObject;
                    Physics.IgnoreCollision(child.GetComponent<BoxCollider>(), child2.GetComponent<BoxCollider>());
                    Physics.IgnoreCollision(child.GetComponent<BoxCollider>(), firstChild2.GetComponent<BoxCollider>());
                    Physics.IgnoreCollision(firstChild.GetComponent<BoxCollider>(), firstChild2.GetComponent<BoxCollider>());
                }
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
