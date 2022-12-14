using System.Collections;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourThreshold = 45;
    public Transform origin = null;
    public GameObject streamPrefab = null;
    public GameObject liquid = null;
    private bool isPouring = false;
    public float pourRate = 0.5f;
    private float nextPour = 0.0f;
    private Stream currentStream = null;

    private void Update() {
        bool pourCheck = CalculatePourAngle() > pourThreshold;
        if(isPouring != pourCheck){
            isPouring = pourCheck;      
            if(isPouring & liquid.GetComponent<Renderer>().material.GetFloat("_Fill") > 0){
                StartPour();
            }
            else{
                EndPour();
            }
        }

        if(liquid.GetComponent<Renderer>().material.GetFloat("_Fill") > 0 ){
            if(isPouring){
                if (Time.time > nextPour){
                    nextPour = Time.time + pourRate;
                    Drain();
                }
                if (liquid.GetComponent<Renderer>().material.GetFloat("_Fill") <= 0){
                    EndPour();
                }
            }
        }
    }
    
    private void StartPour(){
        currentStream = CreateStream();
        currentStream.Begin();
    }

    private void EndPour(){
        currentStream.End();
        currentStream = null;
    }
    private float CalculatePourAngle(){
        float zAngle = 180 - Mathf.Abs(180 - transform.rotation.eulerAngles.z); 
        float xAngle = 180 - Mathf.Abs(180 - transform.rotation.eulerAngles.x);
        return Mathf.Max(zAngle, xAngle);
    }

    private Stream CreateStream(){
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        streamObject.GetComponent <LineRenderer> ().material.SetColor("_topColor", transform.GetChild(0).GetComponent<Renderer>().material.GetColor("_topColor"));
        streamObject.GetComponent <LineRenderer> ().material.SetColor("_sideColor", transform.GetChild(0).GetComponent<Renderer>().material.GetColor("_sideColor"));
        return streamObject.GetComponent<Stream>();
    }

    private void Drain(){
       liquid.GetComponent<Renderer>().material.SetFloat("_Fill", liquid.GetComponent<Renderer>().material.GetFloat("_Fill") - 0.01f);
    }
    private void Fill(){
       liquid.GetComponent<Renderer>().material.SetFloat("_Fill", liquid.GetComponent<Renderer>().material.GetFloat("_Fill") + 0.1f);
    }
     private void OnTriggerEnter(Collider other){
            if(!isPouring & other.gameObject.TryGetComponent(typeof(ParticleSystem), out Component component)){
                if (Time.time > nextPour){
                    nextPour = Time.time + pourRate;
                    transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_sideColor", Color.white);
                    transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_topColor", Color.white);
                    Fill();
                }
            }
    }

}
