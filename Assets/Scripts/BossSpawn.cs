using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject BossPrefab;
    private int bossnum;
    void Start()
    {
        bossnum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.score == 25 && bossnum == 0)
        {
            SpawnBoss();
            bossnum++;
        }

    }

    void SpawnBoss()
    {
        GameObject.Instantiate(BossPrefab, transform.position, transform.rotation);
    }
}
