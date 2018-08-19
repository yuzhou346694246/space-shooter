using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public float waveDelay;
    public float generateDelay;
    public GameObject playerPref;
    public Transform playerOrigin;
    public GameObject asteroidPref;
    public GameObject enemyManagerPref;
    public Text scoreText;
    public Text gameoverText;
    public int asteroidNum;
    public float xMin;
    public float xMax;
    public float zPosition;
    public bool gameOver;
    //public bool restart;
    
    private int score=0;
    private EnemyManager manager;
    private GameObject em;
    private void Start()
    {
        gameOver = false;
        // restart = false;
        gameoverText.text = "";
        UpdateScore();
        em = Instantiate(enemyManagerPref, transform.position, transform.rotation);
        manager = em.GetComponent<EnemyManager>();
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        //生成player
        Instantiate<GameObject>(playerPref, playerOrigin.position,playerOrigin.rotation);
        yield return new WaitForSeconds(1);

        while (true)
        {
            for(int i=0;i< asteroidNum; i++)
            {
                Vector3 position = new Vector3(Random.Range(xMin, xMax), 0, zPosition);
                //var rotation = Quaternion.identity;
                Instantiate(asteroidPref, position, Quaternion.identity);
                yield return new WaitForSeconds(generateDelay);
            }
            yield return new WaitForSeconds(waveDelay);
            if(gameOver){
                break;//游戏结束就要跳出循环
            }
        }
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score:" + score.ToString();
    }

    public void GameOver(){
        gameOver = true;
        gameoverText.text = "GAME OVER";
    }

    private void Update()
    {
        if (gameOver && Input.GetKey(KeyCode.R))//游戏结束之后才能按R重启
        {
            // restart = true;
            //Application.LoadLevel(Application.loadedLevel);
            SceneManager.LoadScene(0);
        }
    }
}
