using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class InfernalRuneGameObject : SerializedMonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ComponentTagManager componentTagManager))
            {
                if (componentTagManager.HasTag(ComponentTag.Player))
                {
                    TeamStatModifier teamStatModifier = new TeamStatModifier();

                    teamStatModifier.id = "InfernalRune";

                    teamStatModifier.stat = new Stat(StatAttribute.Atk, 30, StatType.Percentage);
                    
                    PlayerManager.Instance.teamStatsModifiers.Add(teamStatModifier); 
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out ComponentTagManager componentTagManager))
            {
                if (componentTagManager.HasTag(ComponentTag.Player))
                {
                    TeamStatModifier teamStatModifier =
                        PlayerManager.Instance.teamStatsModifiers.FirstOrDefault(x => x.id == "InfernalRune");
                    
                    PlayerManager.Instance.teamStatsModifiers.Remove(teamStatModifier); 
                }
            }
        }
    }
}