using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextIndivual : MonoBehaviour {

    public Animator anim;

    public float timeAlive;

	// Use this for initialization
	void Start () {
        anim.SetTrigger("PLAYANIMATION");
	}
    float elapsedTime;
	// Update is called once per frame
	void Update () {

	}
}
