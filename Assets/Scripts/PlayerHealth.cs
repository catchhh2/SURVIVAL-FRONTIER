using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float hp = 100f;
    private Animator anim;
    private PlayerMove playerMove;
    private SkinnedMeshRenderer bodyRenderer;
    public float smoothing = 5f;
    private PlayerShoot playerShoot;
    private Canvas canvas;

    public Slider hpSlider;

    private float hpTotal;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
        bodyRenderer = transform.Find("Player").GetComponent<SkinnedMeshRenderer>() as SkinnedMeshRenderer;
        playerShoot = this.GetComponentInChildren<PlayerShoot>();
        canvas = GetComponentInChildren<Canvas>();
        hpTotal = hp;
    }

    // Update is called once per frame
    void Update()
    {
        bodyRenderer.material.color = Color.Lerp(bodyRenderer.material.color, Color.white, smoothing * Time.deltaTime);
        if (Input.GetMouseButtonDown(1))
            TakeDamage(30f);
    }
    public void TakeDamage(float damage)
    {
        if (hp <= 0)
            return;
        hp -= damage;
        hpSlider.value = hp / hpTotal;
        bodyRenderer.material.color = Color.red;
        if (hp <= 0)
        {
            anim.SetBool("Dead", true);
            Dead();
        }
        
    }

    public void Heal(float amount)
    {
        hp = Mathf.Min(hp + amount, hpTotal);
        hpSlider.value = hp / hpTotal;
        // ÂÌÉ«ÉÁË¸Ð§¹û
        bodyRenderer.material.color = Color.green;
    }


    void Dead()
    {
        this.playerMove.enabled = false;
        this.playerShoot.enabled = false;
        canvas.enabled = false;
    }

}
