using UnityEngine;
using UnityEngine.AI;

public class DeadState : EnemyStateBase
{
    public DeadState(EnemyAI e) : base(e) { }

    public override void OnEnter()
    {
        // 1. 动画
        if (enemy.animator) enemy.animator.SetBool("Dead", true);

        // 2. 关闭行为 —— 用 if 判断，不要用 ?.
        if (enemy.navAgent) enemy.navAgent.enabled  = false;
        if (enemy.move) enemy.move.enabled      = false;
        if (enemy.attack) enemy.attack.enabled    = false;

        var col = enemy.GetComponent<Collider>();
        if (col) col.enabled = false;

        if (enemy.canvas) enemy.canvas.enabled    = false;

        // 3. 掉落
        enemy.GetComponent<LootDrop>()?.TryDrop();
    }

    public override void OnUpdate() { }
    public override void OnExit() { }
}
