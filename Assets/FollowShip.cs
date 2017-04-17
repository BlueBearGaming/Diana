using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowShip : MonoBehaviour {

    public GameObject following;
    public float rotationDamping = 20;
    public float positionDamping = 45;
	
    protected Vector3 dampingVelocity;

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, following.transform.rotation, 0.8f);
        transform.position = Vector3.Lerp(transform.position, following.transform.position, 0.8f);
    }
}
