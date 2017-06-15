using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noisy : MonoBehaviour {
    AudioSource mySound;

    void Start() {
        mySound = this.GetComponent<AudioSource>();
    }

	void OnCollisionEnter(Collision col) {
        float magnitude = Vector3.Magnitude(col.relativeVelocity);
        if (magnitude > 5) {
            //print(magnitude);
            mySound.volume = magnitude / 50;
            mySound.Play();
        }
    }
}
