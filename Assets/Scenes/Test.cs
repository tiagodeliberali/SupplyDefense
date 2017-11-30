using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    [SerializeField] GameObject prefab;

	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            GameObject shoot = Instantiate(prefab);
            shoot.transform.position = transform.position + Camera.main.transform.forward;

            Rigidbody rb = shoot.GetComponent<Rigidbody>();

            rb.velocity = Camera.main.transform.forward * 40f;
        }        
	}
}
