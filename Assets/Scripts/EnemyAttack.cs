using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class EnemyAttack : MonoBehaviour
{
    [Header("攻击伤害")]
    public float attackHp = 5f;
    [Header("攻击间隔 (秒)")]
    public float attackTime = 1f;
    [Header("攻击半径")]
    public float attackRadius = 1.2f;

    private EnemyHealth enemyHealth;

    /// <summary>
    /// 状态机中用来控制攻速的属性
    /// </summary>
    public float fireRate => attackTime;

    void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    /// <summary>
    /// 状态机调用此方法执行一次攻击
    /// </summary>
    public void DoAttack()
    {
        // Physics.OverlapSphere 会返回所有在半径内的 Collider
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRadius);
        foreach (var h in hits)
        {
            if (h.CompareTag("Player"))
            {
                h.GetComponent<PlayerHealth>()?.TakeDamage(attackHp);
            }
        }
        // 测试范围
        Debug.DrawLine(transform.position, transform.position + Vector3.up * attackRadius, Color.red, 0.1f);
    }

    // 可选：在 Scene 视图中可视化攻击范围
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
