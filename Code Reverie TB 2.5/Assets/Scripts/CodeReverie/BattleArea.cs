using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class BattleArea : SerializedMonoBehaviour
    {
        public List<CharacterBattleManager> enemies;
        public Collider areaCollider;
        
        private void Awake()
        {
            enemies = GetComponentsInChildren<CharacterBattleManager>().ToList();
            EventManager.Instance.combatEvents.onCombatEnter += SetEnemyPositions;
        }

        public void SetEnemyPositions()
        {
            int count = 0;
            foreach (CharacterBattleManager enemy in enemies)
            {
                //enemy.battlePosition = enemyPositions.battlePositions[count];
                enemy.inCombat = true;

                count++;
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ComponentTagManager tagManager))
            {
                
                
                if (tagManager.HasTag(ComponentTag.Player))
                {
                    BattleManager.Instance.currentBattleArea = this;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ComponentTagManager tagManager))
            {
                
                
                if (tagManager.HasTag(ComponentTag.Player) && BattleManager.Instance.battleManagerState == BattleManagerState.Inactive)
                {
                    BattleManager.Instance.currentBattleArea = null;
                }
            }
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            
            if (other.TryGetComponent(out ComponentTagManager tagManager))
            {
                
                
                if (tagManager.HasTag(ComponentTag.Player))
                {
                    BattleManager.Instance.currentBattleArea = this;
                }
            }
            
        }
        
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out ComponentTagManager tagManager))
            {
                
                
                if (tagManager.HasTag(ComponentTag.Player) && BattleManager.Instance.battleManagerState == BattleManagerState.Inactive)
                {
                    BattleManager.Instance.currentBattleArea = null;
                }
            }
        }
        
    }
}