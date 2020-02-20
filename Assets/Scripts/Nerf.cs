using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nerf : MonoBehaviour {

    public Vector3 speed ;

    public float force;
    public Rigidbody fisis;

    public GameObject nerfPrefab;

    public float timeAlive;

	// Use this for initialization
	void Start () {
		
	}

    public void Shoot(bool right)
    {
        if (right)
        {
            fisis.AddForce(speed * force);
        }
        else
        {
            speed.x *= -1;

            fisis.AddForce(speed * force);
        }
    }
    float elapsedTime = 0;
    private void FixedUpdate()
    {
        if (timeAlive < elapsedTime)
        {
            Choco();
        }
        else
        {
            elapsedTime += Time.fixedDeltaTime;
        }
    }
    public void Choco()
    {
        Vector3 aux = new Vector3(transform.position.x, -2, transform.position.z);
        Instantiate(nerfPrefab, aux, Quaternion.identity);
        GameObject.Destroy(gameObject);
    }
}
