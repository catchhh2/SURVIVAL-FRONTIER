using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("拾取提示文本 (UI Text)")]
    public TMP_Text pickupText;

    [Header("提示显示时长 (秒)")]
    public float showDuration = 1.5f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowPickupText(string msg)
    {
        StopAllCoroutines();
        StartCoroutine(ShowRoutine(msg));
    }

    IEnumerator ShowRoutine(string msg)
    {
        pickupText.text = msg;
        pickupText.gameObject.SetActive(true);
        yield return new WaitForSeconds(showDuration);
        pickupText.gameObject.SetActive(false);
    }
}
