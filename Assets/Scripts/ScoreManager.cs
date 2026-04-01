using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;
    public static bool gameOver = false;  // 添加静态标志

    private static Text text;
    // Start is called before the first frame update
    void Start()
    {
        text=GetComponent<Text>();
        score = 0;
        gameOver = false; // 确保每次启动时是 false
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver && text != null)
        {
            text.text = "Score: " + score;
        }
    }
}
