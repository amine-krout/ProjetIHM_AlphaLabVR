using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    //public string sceneName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadChallengesScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScenarioScene(string sceneName) {
	SceneManager.LoadScene(sceneName);
    }
}
