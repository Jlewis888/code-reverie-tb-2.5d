using UnityEngine;

namespace CodeReverie
{
    public class PotionItem : Item
    {
        public PotionItem(ItemInfo itemInfo) : base(itemInfo) { }


        public override void UseItem()
        {
            base.UseItem();
            //PlayerManager.Instance.currentParty[0].characterController.GetComponent<Health>().ApplyHeal(100);
        }

        public override void UseMenuItemOnCharacter(CharacterBattleManager characterBattleManager)
        {
            base.UseMenuItemOnCharacter(characterBattleManager);
            
            characterBattleManager.GetComponent<Health>().ApplyHeal(150);
            
        }

        public override void UseCombatItem(CharacterBattleManager characterBattleManager)
        {
            base.UseCombatItem(characterBattleManager);
            
            characterBattleManager.GetComponent<Health>().ApplyHeal(150);
            
        }
    }
}