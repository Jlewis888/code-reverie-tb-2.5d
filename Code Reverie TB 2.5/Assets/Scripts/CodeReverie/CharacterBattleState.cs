namespace CodeReverie
{
    public enum CharacterBattleState
    {
        Active,
        Support,
        Inactive,
        MoveToStartingBattlePosition,
        MoveToRandomBattlePosition,
        Waiting,
        Command,
        SelectingCommand,
        WaitingCommand,
        WaitingQueue,
        WaitingAction,
        MoveToCombatActionPosition,
        Action,
        CompleteAction,
        Moving,
        Targetable,
        Interrupted
    }
}