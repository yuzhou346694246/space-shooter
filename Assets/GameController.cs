using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public float waveDelay;
    public float generateDelay;
    public GameObject playerPref;
    public Transform playerOrigin;
    public GameObject asteroidPref;
    public Text scoreText;
    public int asteroidNum;
    public float xMin;
    public float xMax;
    public float zPosition;

    private int score=0;

    private void Start()
    {
        UpdateScore();
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


}
