using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextGeneral : MonoBehaviour {

    public static TextGeneral instance;

    public Animator anim;

    public float timeAlive;

    public TextMesh Tox;

    public void Awake()
    {
        instance = this;
    }

    public void SetText(string Texti,Color colo)
    {
        Tox.text = Texti;
        Tox.color = colo;
    }

    void Start()
    {
    }

    float elapsedTime = 0;

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > timeAlive)
        {
            Destroy(gameObject);
        }
    }


}
