using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleScript : MonoBehaviour
{
    public GameObject player;
    

    void Start()
    {
        transform.position = new Vector3(0, 1.5f, 11.64f);
    }

    void Update()
    {
        
    }

    void FixedUpdate() {
        return;
    }
}
