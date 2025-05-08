using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    
    public class CombatConfigDetails
    {
        public string returnSceneName;
        public string characterInstanceID;
        public Vector3 characterReturnPosition;
        public List<CharacterDataContainer> enemyList;
        public float expToGive;
        public int archetypePointsToGive;
        public int lumiesToGive;
        public AreaManagerConfig areaManagerConfig;
        


        public CombatConfigDetails(string returnSceneName = "", string characterInstanceID = "", Vector3 characterReturnPosition = default, List<CharacterDataContainer> enemyList = null, AreaManagerConfig areaManagerConfig = null)
        {
            this.returnSceneName = returnSceneName;
            this.characterInstanceID = characterInstanceID;
            this.enemyList = enemyList;
            this.characterReturnPosition = characterReturnPosition;
            this.areaManagerConfig = areaManagerConfig;
            
            SetExpToGive();
        }

        public void SetExpToGive()
        {
            foreach (CharacterDataContainer characterDataContainer in enemyList)
            {
                expToGive += characterDataContainer.experienceToGive;
            }
        }
        
    }
}