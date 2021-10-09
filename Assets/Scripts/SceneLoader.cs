using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        // // Don't destroy this game object when load to a new scene
        // DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void LoadScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }

    public void LoadSceneByName()
    {
        SceneManager.LoadScene(sceneName);
    }
}
