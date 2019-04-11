using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderLogger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            Debug.Log("Player touched an object!");
            Debug.Log(other.name);
        }
    }
}
