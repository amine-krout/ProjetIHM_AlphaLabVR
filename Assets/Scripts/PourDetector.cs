using System.Collections;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourThreshold = 45;
    public Transform origin = null;
    public GameObject streamPrefab = null;
    public GameObject liquid = null;

    private bool isPouring = false;
    private Stream currentStream = null;

    private void Update() {
        bool pourCheck = CalculatePourAngle() > pourThreshold;
        if(isPouring != pourCheck){
            isPouring = pourCheck;
            if(isPouring){
                StartPour();
            }
            else{
                EndPour();
            }
        }
    }
    
    private void StartPour(){
        currentStream = CreateStream();
        currentStream.Begin();
        print("Start");
    }

    private void EndPour(){
        currentStream.End();
        currentStream = null;
        print("End");
    }
    private float CalculatePourAngle(){
        float zAngle = 180 - Mathf.Abs(180 - transform.rotation.eulerAngles.z); 
        float xAngle = 180 - Mathf.Abs(180 - transform.rotation.eulerAngles.x);
        return Mathf.Max(zAngle, xAngle);
    }

    private Stream CreateStream(){
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        streamObject.GetComponent <LineRenderer> ().material.color = liquid.GetComponent <MeshRenderer> ().material.GetColor("_topColor");
        return streamObject.GetComponent<Stream>();
    }
}