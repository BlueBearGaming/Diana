using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour {
    public float speed = 0.001f;
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector3 rotation = new Vector3(0, speed, 0);
        transform.Rotate(rotation);
    }
}
