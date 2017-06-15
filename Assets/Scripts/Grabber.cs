using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour {
    public Transform grabbedItem, camera, reticle;
    public bool lastClick;
    float lastGrab;
    static float cooldown = .3f;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update () {
        if (grabbedItem && grabbedItem.transform.parent != reticle) {
            grabbedItem = null;
        }

        if (Time.timeSinceLevelLoad - lastGrab > cooldown) {
            if (Input.GetAxis("Fire1") > 0 && lastClick == false) {
                lastClick = true;
                lastGrab = Time.timeSinceLevelLoad;
                if (grabbedItem != null) {
                    grabbedItem.GetComponent<Rigidbody>().isKinematic = false;
                    grabbedItem.transform.parent = null;
                    grabbedItem = null;
                    
                } else {
                    RaycastHit[] hits = Physics.RaycastAll(camera.position, reticle.position - camera.position, Vector3.Distance(reticle.position, camera.position) * 3);
                    foreach (RaycastHit hit in hits) {
                        Rigidbody r;
                        ScoreCounter s;
                        if (s = hit.transform.GetComponent<ScoreCounter>()) {
                            s.Reset();
                            break;
                        }
                        if (hit.transform.GetComponent<Noodle>() && !(r = hit.transform.GetComponent<Rigidbody>()).isKinematic) {
                            hit.transform.parent = reticle;
                            r.isKinematic = true;
                            grabbedItem = hit.transform;
                            break;
                        }
                    }
                }
                
            } else {
                lastClick = false;
            }
        }
		
	}
}
