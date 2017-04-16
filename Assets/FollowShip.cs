using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowShip : MonoBehaviour {

    public GameObject following;
    public float rotationDamping = 20;
    public float positionDamping = 45;

    protected Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
    }
	
    void LateUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, following.transform.rotation, Time.deltaTime * rotationDamping);
        transform.position = Vector3.Lerp(transform.position, following.transform.position, Time.deltaTime * positionDamping);
    }
}
