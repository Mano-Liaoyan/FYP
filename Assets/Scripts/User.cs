using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nakama;

public class User : MonoBehaviour
{
    public static ISession session = null;
    public static int score = 0; // get from the server
    public static IClient client;
    public static float lastTime = 0;
    public static float currTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

}
