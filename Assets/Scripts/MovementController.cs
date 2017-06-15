using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
    public Transform camera;
	
	// Update is called once per frame
	void Update () {
        camera.localEulerAngles -= new Vector3(Input.GetAxis("Mouse Y"), 0);
        transform.localEulerAngles += new Vector3(0, Input.GetAxis("Mouse X"));
        transform.position += transform.forward * Input.GetAxis("Vertical") * .15f;
        transform.position += transform.right * Input.GetAxis("Horizontal") * .15f;
    }
}
