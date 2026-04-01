using UnityEngine;

public class ChaseState : EnemyStateBase
{
    public ChaseState(EnemyAI e) : base(e) { }

    public override void OnEnter()
    {
        enemy.navAgent.isStopped = false;
        enemy.animator.SetBool("Move", true);
    }

    public override void OnUpdate()
    {
        var playerPos = GameObject.FindWithTag("Player").transform.position;
        enemy.navAgent.SetDestination(playerPos);

        float dist = Vector3.Distance(enemy.transform.position, playerPos);

        if (dist <= enemy.attackRange)
            enemy.ChangeState(enemy.attackState);
        else if (dist > enemy.chaseRange)
            enemy.ChangeState(enemy.idleState);
    }

    public override void OnExit()
    {
        enemy.animator.SetBool("Move", false);
    }
}
