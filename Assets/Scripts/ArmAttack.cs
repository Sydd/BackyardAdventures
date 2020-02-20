using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAttack : MonoBehaviour {

    public float timeALive;
    public GameObject fx;
    // Use this for initialization

 
    void Start () {
        if (fx != null)
            Instantiate(fx, transform.position, Quaternion.identity);
    }

    float timeElapsed = 0;
    // Update is called once per frame
    void Update() {

        timeElapsed += Time.deltaTime;

        if (timeElapsed > timeALive)
        {

            GameObject.Destroy(this.gameObject);
            //gameObject.SetActive(false);
        }
    		
	}
}
