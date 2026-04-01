using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnTime = 5f;
    private float timer = 0f;

    private int lastscorecheckpoint=0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(spawnTime>=5&&ScoreManager.score%5==0&&ScoreManager.score!=lastscorecheckpoint)
        {
            spawnTime = spawnTime - 2;
            lastscorecheckpoint=ScoreManager.score;
            
        }
        if(timer > spawnTime)
        {
            timer -= spawnTime;
            SpawnEnemy();
        }
       
    }

    void SpawnEnemy()
    {
        GameObject.Instantiate(enemyPrefab, transform.position, transform.rotation);
    }


}
