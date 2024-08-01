
using UnityEngine;

namespace CodeReverie
{
    [DefaultExecutionOrder(-119)]
    public class EventManager : ManagerSingleton<EventManager>
    {

        public GeneralEvents generalEvents;
        public WeaponEvents weaponEvents;
        public QuestEvents questEvents;
        public CombatEvents combatEvents;
        public InventoryEvents inventoryEvents;
        public PlayerEvents playerEvents;
        
        
        protected override void Awake()
        {
            base.Awake();


            generalEvents = new GeneralEvents();
            weaponEvents = new WeaponEvents();
            questEvents = new QuestEvents();
            combatEvents = new CombatEvents();
            inventoryEvents = new InventoryEvents();
            playerEvents = new PlayerEvents();

        }
    }
}