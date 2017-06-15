using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noodle : MonoBehaviour {
    public int id;
    private bool connected;
    public GameObject gibs;
    private Rigidbody myRigid;
    private Collider myCollider;
    public Animator xAnimator;
    public ScoreCounter scoreCounter;


    void Start() {
        myRigid = this.GetComponent<Rigidbody>();
        myCollider = this.GetComponent<Collider>();
        scoreCounter = GameObject.Find("speaker").GetComponent<ScoreCounter>();
        xAnimator = scoreCounter.xAnimator;
        connected = false;
    }

    void OnTriggerEnter(Collider col) {
        Hook h;
        if ((h = col.transform.GetComponent<Hook>()) && (h.id == (scoreCounter.getScore() + 1)) && connected == false) {
            // If correct hook and not already hooked
            if (h.id == id) {
                // Connect
                connected = true;
                transform.parent = null;
                h.connected = true;
                myRigid.isKinematic = true;
                transform.localEulerAngles = new Vector3(-90, 0, 0);
                transform.position = col.transform.position;
                xAnimator.SetBool("Nope", false);
                scoreCounter.setScore(scoreCounter.getScore() + 1);
            } else if (h.connected != true) {
                print("Noodle's id was " + id + " and target id was " + h.id + ", score is " + scoreCounter.getScore());
                // If that isn't supposed to go there, we fall apart
                scoreCounter.SendMessage("Reset");
                
            }
        }
    }

	public void Disconnect() {
        transform.parent = null;
        // Disable so gibs don't go flying
        myCollider.isTrigger = true;
        myRigid.isKinematic = true;

        // Show shattered parts
        GameObject myChild = transform.GetChild(0).gameObject;
        myChild.gameObject.SetActive(true);
        // Disconnect ourselves
        myChild.transform.parent = null;

        Vector3 myVelocity = myRigid.velocity;
        foreach (Rigidbody r in myChild.GetComponentsInChildren<Rigidbody>()) {
            // Inherit velocity
            r.velocity = myRigid.GetPointVelocity(r.transform.position);
            r.AddExplosionForce(1f, transform.position, 2f);
        }

        Destroy(this.gameObject);

    }
}
