
public interface IEnemyState
{
    void Enter();    // 进入状态时触发一次
    void Execute();  // 每帧执行
    void Exit();     // 退出状态时触发一次
}