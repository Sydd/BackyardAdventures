using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {
    public static GameMaster Instance;
    public GameObject textPrefab;
    public GameObject lose, win,ui;
    public List<GameObject> levelPrefabs = new List<GameObject>();

    public List<GameObject> totalLevels = new List<GameObject>();
    public Player player;
    public int armDMG;

    public GameObject but;

    public int nerfDMG;

    public int enemyHit;

    public int levelNumber;
    public AudioSource dmg;

    private void Awake()
    {
        Instance = this;
                ui.SetActive(false);

 
    }


    public void Play()
    {
        ui.SetActive(true);

        player.playing = true;
        but.SetActive(false);
    }
    private void Start()
    {
        Vector3 offSet = new Vector3(40,0,0);

    //    for (int i = 0; i < levelNumber; i++)
       // {
    //        int x = Random.Range(0, levelPrefabs.Count - 1 );

   //         GameObject aux = Instantiate(levelPrefabs[x], offSet, Quaternion.identity);

        //    offSet.x += 40;
      //  }
        

        
    }

    public void Lose()
    {
        lose.SetActive(true);
        ui.SetActive(false);

        Invoke("Reset", 5f);
    }

    public void Win()
    {
        ui.SetActive(false);
        win.SetActive(true);
        player.playing = false;
        Invoke("Reset", 10f);

    }
    public void Reset()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void ShowText(Vector3 pos, string text,Color color)
    {
        GameObject aux = Instantiate(textPrefab, pos, Quaternion.identity);

        aux.GetComponent<TextGeneral>().SetText(text,color);

    }


}
