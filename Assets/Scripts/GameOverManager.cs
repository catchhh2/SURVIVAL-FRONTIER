using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerHealth.hp <= 0 && !ScoreManager.gameOver)
        {
            anim.SetTrigger("GameOver");
            ScoreManager.gameOver = true; // 标记 game over，停止刷分
        }
        // 当得分达到50，触发胜利动画
        if (ScoreManager.score >= 50 && !ScoreManager.gameOver)
        {
            anim.SetTrigger("YouWin");
            ScoreManager.gameOver = true;
        }
    }
}
