using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelGenerator : MonoBehaviour
{
    public Transform sourcePortal;
    public Transform tunnelWorld;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //cube.transform.localScale = new Vector3(4, 1, 20);
        //cube.transform.position = new Vector3(0, 0.5f, 0);
        //cube.transform.rotation = sourcePortal.rotation;

        tunnelWorld.rotation = sourcePortal.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
