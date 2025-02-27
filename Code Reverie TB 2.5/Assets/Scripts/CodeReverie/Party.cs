﻿using System.Collections.Generic;

namespace CodeReverie
{
    public class Party
    {
       
        public List<PartySlot> team = new List<PartySlot>();
       
        public int activePartySlotIndex;

        public Party()
        {
            activePartySlotIndex = 0;
        }
        
        public int ActivePartySlotIndex
        {
            get { return activePartySlotIndex; }

            set
            {
                if (value != activePartySlotIndex && value >= 0 && value < team.Count)
                {
                    activePartySlotIndex = value;
                }
            }
        }

        public void SetPartyUnits()
        {
            
            
            
            if (ActivePartySlot == null)
            {
                //ActivePartySlot = team[0];
            }
        }

        public PartySlot ActivePartySlot
        {
            get
            {
                if (activePartySlotIndex >= 0 && activePartySlotIndex < team.Count)
                {
                    return team[activePartySlotIndex];
                }

                return null;

            }

        }


        public void InitFromLoad()
        {
            List<PartySlot> teamTemp = new List<PartySlot>();


            int count = 0;
            
            foreach (PartySlot partySlot in team)
            {
                // PartySlot availableCharacterPartySlot =
                //     PlayerManager.Instance.availableCharacters.Find(x =>
                //         x.character.info.id == partySlot.character.info.id);
                
                //teamTemp.Add(PlayerManager.Instance.GetAvailableCharacterPartySlot(partySlot.character.info.id));
                //teamTemp.Add(PlayerManager.Instance.GetAvailableCharacterPartySlot(partySlot.character.info.id));
                
                count++;
            }

            team = teamTemp;

            foreach (PartySlot partySlot in team)
            {
                // PartySlot availableCharacterPartySlot =
                //     PlayerManager.Instance.availableCharacters.Find(x =>
                //         x.character.info.id == partySlot.character.info.id);

                
                
                

                // foreach (SkillSlot skillSlot in partySlot.character.EquippedArchetype.archetypeSkills.equippedSkills.Values)
                // {
                //
                //     if (skillSlot != null)
                //     {
                //         if (skillSlot.skill != null)
                //         {
                //             skillSlot.skill.SetSkillInfo();
                //         }
                //     }
                //    
                // }
                //
                //
                // foreach (Skill skill in partySlot.character.EquippedArchetype.archetypeSkills.equippedPassivesSkills)
                // {
                //
                //     if (skill != null)
                //     {
                //         skill.SetSkillInfo();
                //     }
                //    
                // }
               
            }

        }
        
        
        
        
        
    }
}