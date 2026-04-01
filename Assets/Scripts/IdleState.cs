using UnityEngine;
using UnityEngine.AI;

public class IdleState : EnemyStateBase
{
    float timer;

    public IdleState(EnemyAI e) : base(e) { }

    public override void OnEnter()
    {
        // 让 NavMeshAgent 继续运动
        enemy.navAgent.isStopped = false;
        PickNewDestination();
    }

    public override void OnUpdate()
    {
        // 到达目的地 or 到时 -> 重新选点
        if (!enemy.navAgent.pathPending && enemy.navAgent.remainingDistance <= 0.5f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f) PickNewDestination();
        }

        // 发现玩家 -> 切换到 Chase
        if (enemy.CanSeePlayer()) enemy.ChangeState(enemy.chaseState);
    }

    void PickNewDestination()
    {
        Vector3 randomDir = Random.insideUnitSphere * enemy.patrolRadius;
        randomDir += enemy.transform.position;              // 以当前位置为中心

        // 把随机点投射到 NavMesh 上，半径 2m
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDir, out hit, 2f, NavMesh.AllAreas))
        {
            enemy.navAgent.SetDestination(hit.position);
            timer = enemy.wanderInterval;                   // 重置计时器
        }
    }

    public override void OnExit() { }
}
