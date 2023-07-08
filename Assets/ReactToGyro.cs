using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactToGyro : MonoBehaviour
{

    public GameObject cube;
    public UPDClient Client;
    // Start is called before the first frame update
    void Start()
    {
        cube = GameObject.Find("Cube");
    }

    // Update is called once per frame
    void Update()
    {
        cube.transform.Rotate(Client.gyro[0], Client.gyro[1], Client.gyro[2]);

    }
}
