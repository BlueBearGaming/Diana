using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleCameras : MonoBehaviour {

    public GameObject cockpitCameraObject;
    public GameObject chaseCameraObject;

    protected Camera cockpitCamera;
    protected Camera chaseCamera;

    // Use this for initialization
    void Start () {
        cockpitCamera = cockpitCameraObject.GetComponent<Camera>();
        chaseCamera = chaseCameraObject.GetComponent<Camera>();
        cockpitCamera.enabled = true;
        chaseCamera.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
        {
            cockpitCamera.enabled = !cockpitCamera.enabled;
            chaseCamera.enabled = !chaseCamera.enabled;
        }
    }
}
