using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour {


    public GameObject armGameObject;

    public GameObject nerfPrefab;

    public void ArmAttack(bool right)
    {
        int x = 2;
        if (!right) x *= -1;
            
        GameObject aux = Instantiate(armGameObject,new Vector3( transform.position.x + x,transform.position.y ,transform.position.z ), Quaternion.identity);
    }

    public void NerfAttack(bool right)
    {
        int x = 2;
        if (!right) x *= -1;
        GameObject aux = Instantiate(nerfPrefab, new Vector3(transform.position.x + x, transform.position.y + 3, transform.position.z), Quaternion.identity);
        aux.GetComponent<Nerf>().Shoot(right);
    }
}
