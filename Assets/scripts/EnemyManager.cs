using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    //这个类负责生成敌军飞船
    public GameObject enemyPref;
    
    public int enemyNum = 10;
    public float generateDelay =0.5f;
    public float waveDelay = 2.0f;

    private float xMin;
    private float xMax;
    private float zPosition;
    private GameController gameController;
    private void Start()
    {
        GameObject controller = GameObject.FindGameObjectWithTag("GameController");
        gameController = controller.GetComponent<GameController>();
        if (gameController == null)
        {
            Debug.Log("GameController is empty");
        }
        xMin = gameController.xMin;
        xMax = gameController.xMax;
        zPosition = gameController.zPosition;

        StartCoroutine(GeneratorLoop());
    }

    IEnumerator GeneratorLoop()
    {
        while (true)
        {
            for (int i = 0; i < enemyNum; i++)
            {
                Vector3 position = new Vector3(Random.Range(xMin, xMax), 0, zPosition);
                //var rotation = Quaternion.identity;
                Instantiate(enemyPref, position, Quaternion.identity);
                yield return new WaitForSeconds(generateDelay);
            }
            yield return new WaitForSeconds(waveDelay);
            if (gameController.gameOver)
            {
                Destroy(gameObject);//摧毁自己
                break;//游戏结束就要跳出循环
            }
        }
    }
}
