using Unity.Behavior;

namespace CodeReverie
{
    [BlackboardEnum]
    public enum CombatManagerState
    {
        Inactive,
        Initiate,
        PreBattle,
        Battle,
        PostBattle,
        PlayerWin,
        PlayerLost,
        TargetSelect,
        PlayerMove,
        OnSkillUse,
        OnSkillUseEnd
    }
}