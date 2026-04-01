using UnityEngine;

public class AttackState : EnemyStateBase
{
    float cooldown = 0f;

    public AttackState(EnemyAI e) : base(e) { }

    public override void OnEnter()
    {
        Debug.Log("ENTER AttackState");
        enemy.navAgent.isStopped = true;
        enemy.animator.SetTrigger("Attack");
        cooldown = 0.5f; // 首次攻击前摇
    }

    public override void OnUpdate()
    {
        Debug.Log("UPDATE AttackState, cooldown=" + cooldown);
        cooldown -= Time.deltaTime;
        if (cooldown <= 0f)
        {
            enemy.attack.DoAttack();            // 执行扣血
            cooldown = 1f / enemy.attack.fireRate; // 攻速控制
        }
        // 若玩家跑远，回 Chase
        var playerPos = GameObject.FindWithTag("Player").transform.position;
        if (Vector3.Distance(enemy.transform.position, playerPos) > enemy.attackRange)
            enemy.ChangeState(enemy.chaseState);
    }

    public override void OnExit() { }
}
