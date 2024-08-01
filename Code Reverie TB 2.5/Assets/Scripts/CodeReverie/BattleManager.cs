using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeReverie
{
    public class BattleManager : ManagerSingleton<BattleManager> 
    {
        public BattleManagerState battleManagerState;
        public List<Transform> playerSpawnPoints;
        public BattleArea currentBattleArea;
        public List<CharacterBattleManager> allUnits;
        public List<CharacterBattleManager> playerUnits;
        public List<CharacterBattleManager> enemyUnits;
        public Dictionary<CharacterBattleManager, int> orderOfTurnsMap;
        public bool pause;
        public CharacterBattleManager selectedPlayerCharacter;
        public List<CharacterBattleManager> selectedTargets;
        public List<CharacterBattleManager> selectableTargets;
        public int targetIndex;
        public Queue<CharacterBattleManager> combatQueue;

        protected override void Awake()
        {
            base.Awake();
            
        }

        private void OnEnable()
        {
            EventManager.Instance.combatEvents.onCombatEnter += SetBattle;
            EventManager.Instance.combatEvents.onCombatPause += SetPauseTimer;
            EventManager.Instance.combatEvents.onPlayerTurn += SetSelectedCharacter;
            EventManager.Instance.combatEvents.onEnemyDeath += AllEnemyDeathCheck;
            EventManager.Instance.combatEvents.onPlayerSelectTarget += OnPlayerSelectTarget;
        }

        private void OnDisable()
        {
            EventManager.Instance.combatEvents.onCombatEnter -= SetBattle;
            EventManager.Instance.combatEvents.onCombatPause -= SetPauseTimer;
            EventManager.Instance.combatEvents.onPlayerTurn -= SetSelectedCharacter;
            EventManager.Instance.combatEvents.onEnemyDeath -= AllEnemyDeathCheck;
            EventManager.Instance.combatEvents.onPlayerSelectTarget -= OnPlayerSelectTarget;
        }
        
        
        private void Start()
        {
            
            
        }


        private void Update()
        {
            switch (battleManagerState)
            {
                case BattleManagerState.PreBattle:
                    if (CheckIfCharactersInBattlePositions())
                    {
                        pause = false;
                        battleManagerState = BattleManagerState.Battle;
                    }
                    break;
                case BattleManagerState.PlayerSelectingTargets:
                    // NavigateTargets();
                    //
                    // if (selectedTargets.Count == 0)
                    // {
                    //     //selectedPlayerCharacter[];
                    //     enemyUnits[0].namePanel.SetActive(true);
                    // }

                    // if (GameManager.Instance.playerInput.GetButtonDown("Confirm Action"))
                    // {
                    //     ConfirmAction();
                    // }
                    
                    
                    break;
            }
        }


        public void SetBattle()
        {
            
            //return;
            CanvasManager.Instance.hudManager.combatHudManager.gameObject.SetActive(true);
            battleManagerState = BattleManagerState.Initiate;
            pause = true;
            combatQueue = new Queue<CharacterBattleManager>();
            GameManager.Instance.playerInput.controllers.maps.SetAllMapsEnabled(false);
            GameManager.Instance.playerInput.controllers.maps.SetMapsEnabled(true, 2);
            
            orderOfTurnsMap = new Dictionary<CharacterBattleManager, int>();
            
            currentBattleArea.SetEnemyPositions();
            PlayerManager.Instance.SetPlayerBattleMode();
            
            
            playerUnits = PlayerManager.Instance.GetCharacterBattleManagers();
            allUnits.AddRange(playerUnits);
            
            enemyUnits = currentBattleArea.enemies;
            allUnits.AddRange(currentBattleArea.enemies);
            
            DetermineOrderOfTurns();
            SetOrderOfTurnsLists();
            CanvasManager.Instance.hudManager.combatHudManager.Init();
            battleManagerState = BattleManagerState.PreBattle;
            CameraManager.Instance.SetBattleCamera(currentBattleArea);
        }

        public void UnsetBattle()
        {
            Destroy(currentBattleArea.gameObject);
            battleManagerState = BattleManagerState.Inactive;
            pause = true;
            GameManager.Instance.playerInput.controllers.maps.SetAllMapsEnabled(false);
            GameManager.Instance.playerInput.controllers.maps.SetMapsEnabled(true, 0);
            
            orderOfTurnsMap = new Dictionary<CharacterBattleManager, int>();

            allUnits = new List<CharacterBattleManager>();
            playerUnits = new List<CharacterBattleManager>();
            enemyUnits = new List<CharacterBattleManager>();
            PlayerManager.Instance.UnsetBattle();
            //CanvasManager.Instance.hudManager.combatHudManager.actionBarManager.gameObject.SetActive(false);
            CanvasManager.Instance.hudManager.combatHudManager.gameObject.SetActive(false);
            CameraManager.Instance.UnsetBattleCamera();
            CameraManager.Instance.UpdateCamera(PlayerManager.Instance.currentParty[0].characterController.transform);
            
            currentBattleArea = null;
        }

        
        
        public void DetermineOrderOfTurns()
        {
            
            foreach (CharacterBattleManager character in allUnits)
            {
        
                int randomNum = Random.Range(1, 21);
        
                orderOfTurnsMap.Add(character, randomNum + (int)character.GetComponent<CharacterStatsManager>().GetStat(StatAttribute.Initiative));
                character.SetStartingPosition();
                character.battleState = CharacterBattleState.MoveToStartingBattlePosition;
                character.inCombat = true;
            }
            
            // SetOrderOfTurnsLists(orderOfTurnsMap, orderOfTurnsList);
            //
            //onDetermineOrderOfTurnsComplete();
        }
        
        public void SetOrderOfTurnsLists()
        {

            List<CharacterBattleManager> orderOfTurnsList = new List<CharacterBattleManager>();

            int count = 0;
            float totalInitiative = orderOfTurnsMap.Values.Sum();
            
            
            foreach (KeyValuePair<CharacterBattleManager, int> character in orderOfTurnsMap.OrderByDescending(key => key.Value))
            {
                orderOfTurnsList.Add(character.Key);
                float relativeInitiative = character.Value / totalInitiative;
                character.Key.cooldownTimer = (5f * .8f) - ((5f * .8f) * (1 - relativeInitiative));
            }
            
        }

        public bool CheckIfCharactersInBattlePositions()
        {

            foreach (CharacterBattleManager characterBattleManager in allUnits)
            {
                if (characterBattleManager.battleState != CharacterBattleState.Waiting)
                {
                    return false;
                }
                
                
                // if (characterBattleManager.transform.position !=
                //     characterBattleManager.battlePosition.transform.position)
                // {
                //     return false;
                // }
            }

            return true;
        }
        
        public void SetPauseTimer(bool isPaused)
        {
            pause = isPaused;
        }

        public void SetSelectedCharacter(CharacterBattleManager characterBattleManager)
        {
            selectedPlayerCharacter = characterBattleManager;
        }

        public void SetSelectableTargets()
        {

            selectableTargets = new List<CharacterBattleManager>();
            
            switch (selectedPlayerCharacter.characterBattleActionState)
            {
                case CharacterBattleActionState.Attack:
                    selectableTargets = enemyUnits.FindAll(x =>
                        x.GetComponent<CharacterController>().character.characterState == CharacterState.Alive);
                    
                    // //todo Placeholder: To use the above code
                    // selectableTargets = enemyUnits.FindAll(x =>
                    //     x.GetComponent<Health>().characterState == CharacterState.Alive);
                    break;
                case CharacterBattleActionState.Skill:
                    // selectableTargets = enemyUnits.FindAll(x =>
                    //     x.GetComponent<CharacterController>().character.characterState == CharacterState.Alive);
                    
                    //todo Placeholder: To use the above code
                    selectableTargets = enemyUnits.FindAll(x =>
                        x.GetComponent<Health>().characterState == CharacterState.Alive);
                    break;
            }
        }
        
        public void AllEnemyDeathCheck(CharacterController characterController)
        {
            for (int i = 0; i < enemyUnits.Count; i++)
            {
                if (enemyUnits[i].GetComponent<Health>().CurrentHealth >= 1)
                {
                    return;
                }
            }
            EventManager.Instance.combatEvents.OnPlayerWin();
            Debug.Log("All Enemies Dead");
            UnsetBattle();

            foreach (Character character in PlayerManager.Instance.currentParty)
            {
                Debug.Log("Set character inactive");
                character.characterController.GetComponent<CharacterBattleManager>().inCombat = false;
                character.characterController.GetComponent<CharacterBattleManager>().battleState = CharacterBattleState.Inactive;
            }
            
            
        }

        public void OnPlayerSelectTarget(CharacterBattleManager characterBattleManager)
        {
            
            selectedTargets = new List<CharacterBattleManager>();
            selectedTargets.Add(characterBattleManager);
            
            foreach (CharacterBattleManager selectableTargetcharacterBattleManager in selectableTargets)
            {
                if (selectableTargetcharacterBattleManager != characterBattleManager)
                {
                    selectableTargetcharacterBattleManager.namePanel.gameObject.SetActive(false);
                }
                else
                {
                    selectableTargetcharacterBattleManager.namePanel.gameObject.SetActive(true);
                }
                        
            }
        }
        
        

        public void ConfirmAction()
        {

            switch (selectedPlayerCharacter.characterBattleActionState)
            {
                case CharacterBattleActionState.Attack:
                    selectedPlayerCharacter.selectedTargets = selectedTargets;
                    // selectedPlayerCharacter.SetActionRange();
                    selectedPlayerCharacter.SetAttackActionTargetPosition();
                    CanvasManager.Instance.hudManager.combatHudManager.commandMenu.ToggleCommandMenuHolderOff();
                    break;
                case CharacterBattleActionState.Skill:
                    selectedPlayerCharacter.GetComponent<AnimationManager>().ChangeAnimationState("cast");
                    //selectedPlayerCharacter.SetActionRange();
                    selectedPlayerCharacter.selectedTargets = selectedTargets;
                    selectedPlayerCharacter.SetAttackActionTargetPosition();
                    CanvasManager.Instance.hudManager.combatHudManager.commandMenu.ToggleCommandMenuHolderOff();
                    selectedPlayerCharacter.currentSkillPoints -= selectedPlayerCharacter.selectedSkill.info.skillPointsCost;
                    break;
                case CharacterBattleActionState.Defend:
                    CanvasManager.Instance.hudManager.combatHudManager.commandMenu.ToggleCommandMenuHolderOff();
                    // selectedPlayerCharacter.SetActionRange();
                    // selectedPlayerCharacter.SetSkillCast();
                    selectedPlayerCharacter.battleState = CharacterBattleState.WaitingAction;
                    break;
                case CharacterBattleActionState.Item:
                    CanvasManager.Instance.hudManager.combatHudManager.commandMenu.ToggleCommandMenuHolderOff();
                    selectedPlayerCharacter.selectedTargets = selectedTargets;
                    selectedPlayerCharacter.SetAttackActionTargetPosition();
                    selectedPlayerCharacter.battleState = CharacterBattleState.Action;
                    break;
                
            }
            
            selectedPlayerCharacter.SetActionRange();
            selectedPlayerCharacter.SetSkillCast();
            
            foreach (CharacterBattleManager characterBattleManager in selectableTargets)
            {
                characterBattleManager.namePanel.gameObject.SetActive(false);
            }

            selectedTargets = new List<CharacterBattleManager>();
            selectableTargets = new List<CharacterBattleManager>();
                        
            battleManagerState = BattleManagerState.Battle;
            CameraManager.Instance.UpdateCamera(currentBattleArea.transform);
            EventManager.Instance.combatEvents.OnActionSelected();
        }
        

    }
}