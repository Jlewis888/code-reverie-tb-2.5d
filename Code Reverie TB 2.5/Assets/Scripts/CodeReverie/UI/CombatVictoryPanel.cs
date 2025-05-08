using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using TransitionsPlus;
using UnityEngine;

namespace CodeReverie
{
    public class CombatVictoryPanel : SerializedMonoBehaviour
    {
        public TMP_Text expToGiveText;
        public TMP_Text lumiesToGiveText;
        public TMP_Text archetypePointsToGiveText;

        public List<CharacterVictoryPanel> characterVictoryPanels;
        public CharacterVictoryPanel characterVictoryPanel1;
        public CharacterVictoryPanel characterVictoryPanel2;
        public CharacterVictoryPanel characterVictoryPanel3;
        public CharacterVictoryPanel characterVictoryPanel4;

        private void Awake()
        {
            // characterVictoryPanels = new List<CharacterVictoryPanel>();
            // characterVictoryPanels.Add(characterVictoryPanel1);
            // characterVictoryPanels.Add(characterVictoryPanel2);
            // characterVictoryPanels.Add(characterVictoryPanel3);
            // characterVictoryPanels.Add(characterVictoryPanel4);
            //
            // foreach (CharacterVictoryPanel characterVictoryPanel in characterVictoryPanels)
            // {
            //     characterVictoryPanel.gameObject.SetActive(false);
            // }
        }

        private void Start()
        {

            
            
        }

        private void OnEnable()
        {

            expToGiveText.text = CombatManager.Instance.combatConfigDetails.expToGive.ToString();
            lumiesToGiveText.text = CombatManager.Instance.combatConfigDetails.lumiesToGive.ToString();
            archetypePointsToGiveText.text = CombatManager.Instance.combatConfigDetails.archetypePointsToGive.ToString();

            
            foreach (CharacterUnitAreaStateManager characterUnitAreaStateManager in PlayerManager.Instance.combatConfigDetails.areaManagerConfig.characterUnitAreaStateManagers)
            {
                
                Debug.Log(characterUnitAreaStateManager.characterID);
                
                if (characterUnitAreaStateManager.characterID ==
                    PlayerManager.Instance.combatConfigDetails.characterInstanceID)
                {
                    characterUnitAreaStateManager.characterState = CharacterState.Dead;
                    break;
                }
            }
            
            
            int count = 0;
            foreach (Character character in PlayerManager.Instance.currentParty)
            {
                character.characterController.GetComponent<CharacterBattleManager>().inCombat = false;
                character.characterController.GetComponent<CharacterBattleManager>().characterActionGaugeState = CharacterActionGaugeState.PostBattle;
                character.characterController.GetComponent<CharacterBattleManager>().battleState = CharacterBattleState.Inactive;
            
                //character.currentHealth = character.characterController.GetComponent<Health>().CurrentHealth;
                characterVictoryPanels[count].gameObject.SetActive(true);
                characterVictoryPanels[count].Set(character, CombatManager.Instance.combatConfigDetails.expToGive);
                
                count++;
            }
            
            
        }

        private void OnDisable()
        {
            foreach (CharacterVictoryPanel characterVictoryPanel in characterVictoryPanels)
            {
                characterVictoryPanel.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                
                Exit();
                
            }
        }

        public void Exit()
        {
            Debug.Log("Exit out of Encounter");
            
            CombatManager.Instance.UnsetBattle();
            // TransitionAnimator.Start(
            //     TransitionType.Fade, // transition type
            //     duration: 1f,
            //     playDelay: 3f,
            //     sceneNameToLoad: PlayerManager.Instance.combatConfigDetails.returnSceneName
            // );
        }


        public void Reset()
        {
            
        }
    }
}