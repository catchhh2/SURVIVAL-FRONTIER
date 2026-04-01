// 所有“状态”都继承这个基类
public abstract class EnemyStateBase
{
    protected EnemyAI enemy;
    public EnemyStateBase(EnemyAI e) { enemy = e; }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}
