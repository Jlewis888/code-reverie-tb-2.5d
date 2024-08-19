using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeReverie
{
    public class HudManager : MenuManager
    {
        public CommandMenu commandMenu;
        public CombatHudManager combatHudManager;
        public CurrentEnemyHealthPanel currentEnemyHealthPanel;
        public PartyHudPanelManager partyHudPanelManager;
        public QuestHud questHud;
        public Slider interactionSlider;
        public NotificationCenter notificationCenter;

        private void Awake()
        {
            currentEnemyHealthPanel.gameObject.SetActive(false);
        }


        private void OnEnable()
        {
            EventManager.Instance.combatEvents.onCombatEnter += ToggleCombatHudManager;
        }

        private void OnDisable()
        {
            EventManager.Instance.combatEvents.onEnemyDamageTaken -= EnableEnemyHealthPanel;
            EventManager.Instance.combatEvents.onCombatEnter -= ToggleCombatHudManager;
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                foreach (TrackedQuestHud trackedQuestHud in questHud.trackedQuestHuds)
                {
                    LayoutRebuilder.ForceRebuildLayoutImmediate(trackedQuestHud.transform as RectTransform);
                }
            }
        }


        public void EnableEnemyHealthPanel(DamageProfile damageProfile)
        {
            currentEnemyHealthPanel.health = damageProfile.damageTarget;
            currentEnemyHealthPanel.gameObject.SetActive(true);
        }

        public void ToggleCombatHudManager()
        {
            combatHudManager.gameObject.SetActive(true);
        }
        
        
    }
}