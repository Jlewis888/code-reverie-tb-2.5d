using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class BattleArea : SerializedMonoBehaviour
    {
        //[Range(1f, 6f)] 
        public float areaRange;
        public List<CharacterBattleManager> enemies;
        public CapsuleCollider areaCollider;
        
        private void Awake()
        {
            enemies = GetComponentsInChildren<CharacterBattleManager>().ToList();
            EventManager.Instance.combatEvents.onCombatEnter += SetEnemyPositions;
        }

        public void SetAreaMaterial()
        {
            Material[] materials = areaCollider.GetComponent<Renderer>().materials;
            Color color = materials[0].GetColor("_IntersectionColor");
            //Color color = materials[0].color;
            //materials[0].color = new Color(color.r, color.g, color.b, 1);
            materials[0].SetColor("_IntersectionColor", new Color(color.r, color.g, color.b, 1));
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