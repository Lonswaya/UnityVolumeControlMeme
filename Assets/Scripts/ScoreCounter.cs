using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour {
    public GameObject[] noods;
    public Animator xAnimator;
    public int score = 0;
    private AudioSource[] audioSource;

    void Start() {
        audioSource = this.GetComponents<AudioSource>();
    }

    public void setScore(int i) {
        score = i;
        audioSource[0].volume = score / 5f;
    }
    public int getScore() {
        return score;
    }
    /**
     *  A central method that we use to spawn more noodles after we destroy the current ones
     */
    public void spawnNewNoods() {
        Vector3 position = new Vector3(Random.Range(-5f, 5f), 10, Random.Range(3f, 5f));
        foreach (GameObject nood in noods) {
            GameObject.Instantiate(nood, position, Quaternion.Euler(new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180))));
        }
    }
    /**
     * Called by either the noodles hitting the wrong collider, or clicking on the speaker
     * Resets score, disconnects and destroys all noodles, and and unhooks all hooks
     */
    public void Reset() {
        if (score == 0) return;
        audioSource[1].Play();
        setScore(0);
        Noodle[] allNoodles = GameObject.FindObjectsOfType<Noodle>();
        xAnimator.SetBool("Nope", true);
        foreach (Noodle nood in allNoodles) {
            if (nood != this) {
                nood.Disconnect();
            }
        }
        Hook[] allHooks = GameObject.FindObjectsOfType<Hook>();
        foreach (Hook hook in allHooks) {
            hook.connected = false;
        }
        spawnNewNoods();
    }
}
