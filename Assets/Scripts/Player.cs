using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 0.1f;

    public Slider lifebar;

    public Image brasillo, misilito;
    public float cdDMG;

    float translationZ, translationX;

    public Rigidbody fisis;

    bool flipRight = true;

    public Transform sprite;

    public float CD = 1;

    public AttackSystem attackSystem;

    public Attacks currentAttack;

    public int hp = 100;

    public bool playing = true;

    public bool arm = false;

    public bool chargedWeapon = false;

    public AnimatorHandler anim;

    bool weapon2 = false;

    public AudioSource audiodmg, audiohit;
    // Start is called before the first frame update
    void Start()
    {
       // fisis.MovePosition(new Vector3(0, 0, 0));
        anim = GetComponentInChildren<AnimatorHandler>();
        anim.ChangeSkin("tiger1");
        misilito.gameObject.SetActive(false);
        brasillo.gameObject.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (playing) { 
            translationZ = Input.GetAxis("Vertical");

            translationX = Input.GetAxis("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (weapon2)
                {
                    if (currentAttack == Attacks.Arm)
                    {
                        misilito.gameObject.SetActive(true);
                        brasillo.gameObject.SetActive(false);

                        currentAttack = Attacks.Nerf;
                    } else
                    {
                        brasillo.gameObject.SetActive(true);
                        misilito.gameObject.SetActive(false);

                        currentAttack = Attacks.Arm;
                    }
                }
            }
            if (translationX > 0.2f)
                flipRight = true;
            else if (translationX < -0.2f)
                flipRight = false;
            Flip();

            if (Mathf.Abs(translationX) + Mathf.Abs(translationZ) > 0.1f)
            {
                anim.PlayerWalk();
            } else
            {
                anim.PlayerIdle();
            }
            if ((Input.GetKey(KeyCode.LeftControl)|| Input.GetKey(KeyCode.RightControl) )&& CD >= 0.5F)
            {
                if (arm)
                {
                    Attack();

                }
                CD = 0;
            }

            CD += 0.01f;
        }
    }
    float consumedHitTime;

    private void FixedUpdate()
    {
        Vector3 newPos = new Vector3(fisis.position.x + translationX * speed, fisis.position.y, fisis.position.z + translationZ * speed);
        fisis.MovePosition(newPos);
        consumedHitTime += Time.fixedDeltaTime;
    }

    //  void ShootNerf()
    //    {
    //    GameObject nerf = Instantiate(fireBall, transform.position, Quaternion.identity);

    // }
    float totalrotation = 0;

    public float speedToRotate = 0.1f;

    void Flip()
    {
        if (flipRight && sprite.localScale.x < .6)
        {

            totalrotation += Time.deltaTime * speedToRotate;

            sprite.localScale = new Vector3(Mathf.Clamp(totalrotation, -.6f, .6f), .6f, 1);
        }
        else if (!flipRight && sprite.localScale.x > -.6f)
        {

            totalrotation -= Time.deltaTime * speedToRotate;


            sprite.localScale = new Vector3(Mathf.Clamp(totalrotation, -.6f, .6f), .6f, 1f);
        }
        else
        {
            totalrotation = sprite.localScale.x;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (consumedHitTime > cdDMG)
        {

            if (other.gameObject.tag == "enemytag")

            {

                Hitted(GameMaster.Instance.enemyHit);

                cdDMG = 0;
            }
        }
        if (other.gameObject.tag == "brasito")
        {
            brasillo.gameObject.SetActive(true);
            arm = true;
            anim.ChangeSkin("tiger3");

            GameObject.Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "brasito2")
        {
            anim.ChangeSkin("tiger4");

            GameObject.Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "piernita")
        {
            speed *= 2;
            anim.ChangeSkin("tiger2");
            GameObject.Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "misiles")
        {
            chargedWeapon = true;
            weapon2 = true;
            anim.ChangeSkin("tiger6");
            GameObject.Destroy(other.gameObject);
        }


        if (other.gameObject.tag == "balatirada")
        {
            chargedWeapon = true;
            anim.AgarrasteBala();
            GameObject.Destroy(other.gameObject);
        }
        

    }
    public void Attack()
    {
       switch (currentAttack)
        {
            case Attacks.None:
                return;
            case Attacks.Nerf:
                if (chargedWeapon)
                {
                    anim.Attack();
                    chargedWeapon = false;
                    attackSystem.NerfAttack(flipRight);
                }
                return;
            case Attacks.Arm:
                anim.Melee();
                return;
        }
    }
    public void ArmAttack()
    {
        attackSystem.ArmAttack(flipRight);
    }
    public void Hitted(int dmg)
    {
        consumedHitTime = 0;
        Vector3 offSet = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        GameMaster.Instance.ShowText(offSet, dmg.ToString(),Color.red);
        hp -= dmg;
        lifebar.value = (float)hp / 100f;
        if (hp < 0)
            Die();

    }

    void Die()
    {
        playing = false;
        GameMaster.Instance.Lose();
       // gameObject.SetActive(false);
        
    }

}