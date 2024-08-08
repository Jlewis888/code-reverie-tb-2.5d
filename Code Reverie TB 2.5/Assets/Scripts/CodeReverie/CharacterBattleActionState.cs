namespace CodeReverie
{
    public enum CharacterBattleActionState
    {
        Idle, //Character Does Nothing
        Attack, //Character Basic Attack
        Defend, //Character Defends to reduce damgage 
        Skill, //Character uses skill
        Item, //Character uses item
        Move, //Character moves to new location
        Break, //Character breaks/interrupts target 
    }
}