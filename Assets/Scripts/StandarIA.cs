using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class StandarIA : MonoBehaviour {

    Rigidbody fisis;
    public string state = "idle";
    public GameObject enemyAttack;

    //public float flowToMove;
    public float cdToMove = 1;

    public int hp;

    public float cdToAttack = 1;

    public float distanceToAtack = 2;

    public float hitTime = 1;

    public float distanceToTarget = 20;

    public bool furbo = false;
    GameObject player;
    bool flipright = false;
    SkeletonAnimation skeleton;

    Rigidbody playerFisis;
    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        fisis = GetComponent<Rigidbody>();
        playerFisis = player.GetComponent<Rigidbody>();
        skeleton = GetComponentInChildren<SkeletonAnimation>();
        ChangeState("idle");

    }

    float consumedMovedCd, consumedToAttack,consumedHitTime;

    private void FixedUpdate()
    {
        if (state != "dead")
        {
            if (distanceToTarget > Vector3.Distance(fisis.position, playerFisis.position))
            {

                if (consumedHitTime > hitTime)
                {
                    Vector3 aux = new Vector3(Mathf.Lerp(fisis.position.x, playerFisis.position.x, Time.fixedDeltaTime), fisis.position.y, Mathf.Lerp(fisis.position.z, playerFisis.position.z, Time.fixedDeltaTime));

                    if (fisis.position.x > playerFisis.position.x && flipright)
                    {
                        flipright = false;
                        skeleton.gameObject.transform.localScale = new Vector3(skeleton.gameObject.transform.localScale.x * -1, skeleton.gameObject.transform.localScale.y, skeleton.gameObject.transform.localScale.z);

                    } else if(fisis.position.x < playerFisis.position.x && !flipright)
                    {
                        flipright = true;
                        skeleton.gameObject.transform.localScale = new Vector3(skeleton.gameObject.transform.localScale.x * -1, skeleton.gameObject.transform.localScale.y, skeleton.gameObject.transform.localScale.z);

                    }
                    if (Vector3.Distance(aux, playerFisis.position)-0.1f <= distanceToAtack)
                    {
                        if (consumedToAttack > cdToAttack)
                        {
                            consumedToAttack = 0;

                            Attack();
                        }
                    }
                    else
                    {
                        if (consumedMovedCd > cdToMove)
                        {
                            ChangeState("walk");
                            fisis.MovePosition(aux);
                            consumedMovedCd = 0;
                        }
                    }

                }

                consumedMovedCd += Time.fixedDeltaTime;
                consumedToAttack += Time.fixedDeltaTime;
                consumedHitTime += Time.fixedDeltaTime;
            }
            else
            {
                ChangeState("idle");
            }
        }
    }
    // 0 = idle 1 = run 2 = attack
    void ChangeState(string newState)
    {
        if (newState != state)
        {
            state = newState;
            if (state == "dead")
            {
                SetAnimation(newState, false);

            }
            else
            {
                SetAnimation(newState, true);
            }
        }
    }

    void SetAnimation(string anim,bool loop)
    {
        skeleton.AnimationState.SetAnimation(0, anim, loop);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (consumedHitTime > hitTime)
        {

            if (collision.gameObject.tag == "Hit")
            {
                hp -= GameMaster.Instance.armDMG;

                Hitted(GameMaster.Instance.armDMG);
            }

            if (collision.gameObject.tag == "Bullet")
            {
                hp -= GameMaster.Instance.nerfDMG;

                collision.gameObject.GetComponent<Nerf>().Choco();

                Hitted(GameMaster.Instance.nerfDMG);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(consumedHitTime > hitTime) { 
        if (collision.gameObject.tag == "Hit")
        {
            hp -= GameMaster.Instance.armDMG;

            Hitted(GameMaster.Instance.armDMG);
        }

        if (collision.gameObject.tag == "Bullet")
        {
            hp -= GameMaster.Instance.nerfDMG;

            collision.gameObject.GetComponent<Nerf>().Choco();

            Hitted(GameMaster.Instance.nerfDMG);
        }
        }
    }

    void Attack() {
        int x = 1;
        if (fisis.position.x > playerFisis.position.x)
        {
            x = -1;
        }
        ChangeState("attack");
         GameObject aux = Instantiate(enemyAttack, new Vector3(transform.position.x + x, transform.position.y, transform.position.z), Quaternion.identity);
    }
    public void Hitted(int dmg)
    {
        consumedHitTime = 0;
        Vector3 offSet = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        ChangeState("hurt");

        GameMaster.Instance.ShowText(offSet, dmg.ToString(),Color.blue);
        Debug.Log("recibio un hit");

        if (hp < 0)
            Die(); 

    }


    

    public void Die()
    {
        ChangeState("dead");
        if (furbo)
        {
            Invoke("Win", 4f);
        }
        //gameObject.SetActive(false);
    }

    void Win()
    {
        GameMaster.Instance.Win();
    }
}
