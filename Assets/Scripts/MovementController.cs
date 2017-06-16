using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Basic player and camera movement
 * And some checks to ensure some people don't look upside down and backwards
 */
public class MovementController : MonoBehaviour {
    public Transform camera;
	
	// Update is called once per frame
    void FixedUpdate () {
        float mouseY = Input.GetAxis("Mouse Y");
        float requestedAngle = camera.localEulerAngles.x - mouseY;
        print(requestedAngle);
        if (!(requestedAngle > 90 && requestedAngle < 180) && !(requestedAngle < 270 && requestedAngle > 180))
            camera.localEulerAngles -= new Vector3(Input.GetAxis("Mouse Y"), 0);
        transform.localEulerAngles += new Vector3(0, Input.GetAxis("Mouse X"));
        transform.position += transform.forward * Input.GetAxis("Vertical") * .15f;
        transform.position += transform.right * Input.GetAxis("Horizontal") * .15f;

        if (Input.GetAxis("Cancel") > 0) {
            Application.Quit();
        }
    }
}
