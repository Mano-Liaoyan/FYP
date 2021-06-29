using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningButton : MonoBehaviour
{
    public float spinningSpeed = 50.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(Vector3.up,spinningSpeed * Time.deltaTime);
    }
}
