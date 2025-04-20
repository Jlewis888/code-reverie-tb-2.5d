using Unity.Behavior;

namespace CodeReverie
{
    [BlackboardEnum]
    public enum CharacterActionGaugeState
    {
        PreBattle,
        StartTurnPhase,
        WaitPhase,
        PreCommandPhase,
        CommandPhase,
        ActionPhase,
        EndTurnPhase,
        PostBattle
    }
}