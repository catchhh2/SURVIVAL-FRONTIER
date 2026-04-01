using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float hp;
    private Animator anim;
    private NavMeshAgent agent;
    private EnemyMove move;
    private CapsuleCollider capsuleCollider;
    private AudioSource audio;
    public AudioClip deathClip;
    private ParticleSystem particleSystem;
    private EnemyAttack enemyattack;
    public Slider hpslider;
    private Canvas canvas;
    private float hptotal;

    public int winscore;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        move = GetComponent<EnemyMove>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        audio = GetComponent<AudioSource>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        enemyattack = GetComponent<EnemyAttack>();
        canvas = GetComponentInChildren<Canvas>();
        hptotal = hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp<=0)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 0.2f);
            if (transform.position.y < -1)
                Destroy(gameObject);        
        }
    }

    public void TakeDamage(float damage, Vector3 hitPoint)
    {
        if (hp<=0)
            return;
        audio.Play();
        hp -= damage;
        hpslider.value = hp / hptotal;
        particleSystem.transform.position = hitPoint;
        particleSystem.Play();
        if (hp<=0)
        {
            Dead();
        }
    }
    void Dead()
    {
        anim?.SetBool("Dead", true);
        AudioSource.PlayClipAtPoint(deathClip, transform.position);

        // 关闭行为组件
        if (agent) agent.enabled          = false;
        if (move) move.enabled           = false;
        if (capsuleCollider) capsuleCollider.enabled= false;
        if (enemyattack) enemyattack.enabled    = false;
        if (canvas) canvas.enabled         = false;

        // 掉落战利品 —— 先判断有没有组件
        var loot = GetComponent<LootDrop>();
        if (loot != null) loot.TryDrop();

        // 计分
        ScoreManager.score += winscore;
    }
}
