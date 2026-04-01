using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator), typeof(EnemyHealth))]
public class EnemyAI : MonoBehaviour
{
    // —— 外部可调 —— 
    [Header("Idle 巡逻参数")]
    [Tooltip("在 NavMesh 上随机点的半径（米）")]
    public float patrolRadius = 50f;
    [Tooltip("达到目标点后重新选点的间隔（秒）")]
    public float wanderInterval = 3f;
    public float chaseRange = 8f;
    public float attackRange = 1.5f;

    // —— 组件引用 —— 
    [HideInInspector] public NavMeshAgent navAgent;
    [HideInInspector] public Animator animator;
    [HideInInspector] public EnemyMove move;
    [HideInInspector] public EnemyAttack attack;
    [HideInInspector] public EnemyHealth health;
    [HideInInspector] public Canvas canvas;
    [HideInInspector] public Slider hpSlider;

    // —— FSM 相关 —— 
    EnemyStateBase currentState;
    public IdleState idleState;
    public ChaseState chaseState;
    public AttackState attackState;
    public DeadState deadState;

    void Awake()
    {
        // 1) 缓存所有组件
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        move     = GetComponent<EnemyMove>();
        attack   = GetComponent<EnemyAttack>();
        health   = GetComponent<EnemyHealth>();
        canvas   = GetComponentInChildren<Canvas>();
        hpSlider = GetComponentInChildren<Slider>();

        // 2) 构造所有状态实例
        idleState   = new IdleState(this);
        chaseState  = new ChaseState(this);
        attackState = new AttackState(this);
        deadState   = new DeadState(this);

        // 3) 初始进入 Idle
        ChangeState(idleState);
    }

    void Update()
    {
        // 4) 优先检测死亡
        if (health.hp <= 0 && currentState != deadState)
        {
            ChangeState(deadState);
            return;
        }
        // 5) 调用当前状态逻辑
        currentState.OnUpdate();
    }

    /// <summary>
    /// 状态切换核心
    /// </summary>
    public void ChangeState(EnemyStateBase next)
    {
        if (currentState != null) currentState.OnExit();
        currentState = next;
        currentState.OnEnter();
    }

    // 侦测玩家的辅助方法 -> Idle / Chase / Attack 都能用
    public bool CanSeePlayer()
    {
        // ① 找到玩家（你也可以缓存到字段中）
        if (player == null) player = GameObject.FindWithTag("Player")?.transform;
        if (player == null) return false;

        // ② 距离判定：超出追击范围直接返回 false
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist > chaseRange) return false;

        // ③ 视线判定：用 Raycast 判断中间是否被墙挡住
        Vector3 dir = (player.position + Vector3.up) - (transform.position + Vector3.up);
        if (Physics.Raycast(transform.position + Vector3.up, dir.normalized, out RaycastHit hit, chaseRange))
        {
            return hit.collider.CompareTag("Player");   // 射线击中了玩家 -> 可见
        }

        return false;
    }

    #region
    Transform player;          // 加一行缓存
    #endregion

}
