using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TransitionsPlus;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace CodeReverie
{
    public class CombatManager : SerializedMonoBehaviour
    {
        public static CombatManager Instance;
        public CombatManagerState combatManagerState;
        //[Range(1f, 6f)] 
        public float areaRange;
        public List<CharacterBattleManager> enemies;
        public CapsuleCollider areaCollider;
        public CapsuleCollider startingPositionCollider;
        
        [Header("Active Battle")]
        public List<CharacterBattleManager> allUnits;
        public List<CharacterBattleManager> playerUnits;
        public List<CharacterBattleManager> enemyUnits;
        public Dictionary<CharacterBattleManager, int> orderOfTurnsMap;
        public bool pause;
        public CharacterBattleManager selectedPlayerCharacter;
        public CharacterBattleManager selectedTarget;
        //public List<CharacterBattleManager> selectedTargets;
        public List<CharacterBattleManager> selectableTargets;
        public int targetIndex;
        public Queue<CharacterBattleManager> combatQueue;
        public MovePlayerObject movePlayerObject;
        //public CombatDetailsConfig combatDetailsConfig;
        public CombatConfigDetails combatConfigDetails;
        public string defaultAudioClip;
        public int targetSelectIndex;
        public float navigationDelay = 0.35f;
        public float navigationDelayTimer;
        
        private void Awake()
        {
            Instance = this;
            enemies = GetComponentsInChildren<CharacterBattleManager>().ToList();
            //combatDetailsConfig = PlayerManager.Instance.combatDetailsConfig;
            combatConfigDetails = PlayerManager.Instance.combatConfigDetails;
            defaultAudioClip = "LOOP_Elegant Enemies";
            // EventManager.Instance.combatEvents.onCombatPause += SetPauseTimer;
            // EventManager.Instance.combatEvents.onPlayerTurn += SetSelectedCharacter;
            // EventManager.Instance.combatEvents.onEnemyDeath += AllEnemyDeathCheck;
            // EventManager.Instance.combatEvents.onPlayerSelectTarget += OnPlayerSelectTarget;
            //EventManager.Instance.combatEvents.onCombatEnter += SetEnemyPositions;
            
            if (!string.IsNullOrEmpty(defaultAudioClip))
            {
                SoundManager.Instance.PlayMusic(defaultAudioClip);
            }

        }

        private void OnDestroy()
        {
            EventManager.Instance.combatEvents.onCombatPause -= SetPauseTimer;
            EventManager.Instance.combatEvents.onPlayerTurn -= SetSelectedCharacter;
            EventManager.Instance.combatEvents.onPlayerDeath -= AllPlayerDeathCheck;
            EventManager.Instance.combatEvents.onEnemyDeath -= AllEnemyDeathCheck;
            EventManager.Instance.combatEvents.onPlayerSelectTarget -= OnPlayerSelectTarget;
        }

        private void Start()
        {
            EventManager.Instance.combatEvents.onCombatPause += SetPauseTimer;
            EventManager.Instance.combatEvents.onPlayerTurn += SetSelectedCharacter;
            EventManager.Instance.combatEvents.onPlayerDeath += AllPlayerDeathCheck;
            EventManager.Instance.combatEvents.onEnemyDeath += AllEnemyDeathCheck;
            EventManager.Instance.combatEvents.onPlayerSelectTarget += OnPlayerSelectTarget;
            //SetEnemyPositionsTest();

            SetPreBattleConfigurations();
            
            TransitionAnimator.Start(
                TransitionType.Wipe, // transition type
                duration: 1f,
                invert: true
                //cellsDivisions: 1
            ).onTransitionEnd.AddListener(PreBattleConfigurationExtended);
            
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.P))
            {
                
                
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
               
                //UnsetBattle();
            }

            switch (combatManagerState)
            {
               
                case CombatManagerState.PreBattle:
                    
                    if (CheckIfCharactersInBattlePositions())
                    {
                        Debug.Log("this is now true");
                        pause = false;
                        combatManagerState = CombatManagerState.Battle;
                    }

                    break;
                case CombatManagerState.TargetSelect:
                    
                    if (GameManager.Instance.playerInput.GetButtonDown("Confirm Action"))
                    {
                        EventManager.Instance.combatEvents.onPlayerSelectTargetEnd(null);
                        combatManagerState = CombatManagerState.Battle;
                        ConfirmAction();
                    }
            
                    if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
                    {
                        EventManager.Instance.combatEvents.OnPrevCommandWheelSelect();
                        //EventManager.Instance.combatEvents.onPlayerSelectTargetEnd(null);
                        combatManagerState = CombatManagerState.Battle;
                        //CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.combatCommandMenu.TogglePrevMenu();
                    }
                    
                    
                    
                    if (navigationDelayTimer <= 0)
                    {
                        if (GameManager.Instance.playerInput.GetAxis("Navigate Combat Vertical Axis") < 0)
                        {
                            navigationDelayTimer = navigationDelay;
                            if (targetSelectIndex + 1 > selectableTargets.Count - 1)
                            {
                                targetSelectIndex = 0;
                            }
                            else
                            {
                                targetSelectIndex++;
                            }
                            
                            TargetNavigation();
                        }
                        else if (GameManager.Instance.playerInput.GetAxis("Navigate Combat Vertical Axis") > 0)
                        {
                            navigationDelayTimer = navigationDelay;
                            if (targetSelectIndex == 0)
                            {
                                targetSelectIndex = selectableTargets.Count - 1;
                            }
                            else
                            {
                                targetSelectIndex--;
                            }
                    
                            TargetNavigation();
                        }
                    }
                    else {
                        navigationDelayTimer -= Time.unscaledDeltaTime;
                    }
                    
                    
                    if (GameManager.Instance.playerInput.GetNegativeButtonDown("Navigate Combat Vertical"))
                    {
               
                        navigationDelayTimer = navigationDelay;
                        if (targetSelectIndex + 1 > selectableTargets.Count - 1)
                        {
                            targetSelectIndex = 0;
                        }
                        else
                        {
                            targetSelectIndex++;
                        }
                            
                        TargetNavigation();
                    }
                    else if (GameManager.Instance.playerInput.GetButtonDown("Navigate Combat Vertical"))
                    {
                
                
                        navigationDelayTimer = navigationDelay;
                        if (targetSelectIndex == 0)
                        {
                            targetSelectIndex = selectableTargets.Count - 1;
                        }
                        else
                        {
                            targetSelectIndex--;
                        }
                    
                        TargetNavigation();
                    }
                    
                    
                    break;
                case CombatManagerState.PlayerMove:
                    if (GameManager.Instance.playerInput.GetButtonDown("Confirm Action"))
                    {
                        // EventManager.Instance.combatEvents.onPlayerSelectTargetEnd(null);
                        // combatManagerState = CombatManagerState.Battle;
                        // ConfirmAction();
                    }
            
                    if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
                    {
                        EventManager.Instance.combatEvents.OnPrevCommandWheelSelect();
                        combatManagerState = CombatManagerState.Battle;
                    }
                    break;
            }
        }

        void SetGameOverScreen()
        {
            //return;
            CanvasManager.Instance.screenSpaceCanvasManager.hudManager.gameObject.SetActive(false);
            CanvasManager.Instance.screenSpaceCanvasManager.gameOverPanel.gameObject.SetActive(true);
        }

        public void SetPreBattleConfigurations()
        {
            CanvasManager.Instance.screenSpaceCanvasManager.hudManager.gameObject.SetActive(true);
            CanvasManager.Instance.screenSpaceCanvasManager.gameOverPanel.gameObject.SetActive(false);
            CameraManager.Instance.SetBattleCamera();
            
            CanvasManager.Instance.screenSpaceCanvasManager.hudManager.combatHudManager.gameObject.SetActive(true);
            pause = true;
            combatQueue = new Queue<CharacterBattleManager>();
            GameManager.Instance.playerInput.controllers.maps.SetAllMapsEnabled(false);
            GameManager.Instance.playerInput.controllers.maps.SetMapsEnabled(true, 2);
            orderOfTurnsMap = new Dictionary<CharacterBattleManager, int>();
            movePlayerObject = BattleManager.Instance.movePlayerObject;
            PlayerManager.Instance.SetCombatPartyUnits();
            playerUnits = PlayerManager.Instance.GetCharacterBattleManagers();
            allUnits.AddRange(playerUnits);
            
            // enemyUnits = GetComponentsInChildren<CharacterBattleManager>().ToList();
            InstantiateEnemyUnits();
            allUnits.AddRange(enemyUnits);

            foreach (CharacterBattleManager characterBattleManager in allUnits)
            {
                Character character = characterBattleManager.GetComponent<CharacterUnitController>().character;
                character.RemoveResonancePoints(1000);
                character.characterState = CharacterState.Alive;
                characterBattleManager.GetComponent<Health>().CurrentHealth = character.currentHealth;
            }
            
            DetermineOrderOfTurns();
            SetOrderOfTurnsLists();
            SetInitialCombatPositions();
        }

        public void PreBattleConfigurationExtended()
        {
            
            CanvasManager.Instance.screenSpaceCanvasManager.hudManager.combatHudManager.Init();
            combatManagerState = CombatManagerState.PreBattle;
        }

        public void InstantiateEnemyUnits()
        {
            foreach (CharacterDataContainer characterDataContainer in combatConfigDetails.enemyList)
            {
                Character character = new Character(characterDataContainer);
                character.SpawnCharacter(Vector3.zero);
                character.characterController.gameObject.SetActive(true);
                enemyUnits.Add(character.characterController.GetComponent<CharacterBattleManager>());
            }
        }

        public void InstantiatePlayerUnits()
        {
            
        }

        public void SetAreaMaterial()
        {
            Material[] materials = areaCollider.GetComponent<Renderer>().materials;
            Color color = materials[0].GetColor("_IntersectionColor");
            //Color color = materials[0].color;
            //materials[0].color = new Color(color.r, color.g, color.b, 1);
            materials[0].SetColor("_IntersectionColor", new Color(color.r, color.g, color.b, 1));
        }
        
        public void SetInitialCombatPositions()
        {
            foreach (CharacterBattleManager unit in allUnits)
            {
                float xBound = Random.Range(startingPositionCollider.bounds.min.x, startingPositionCollider.bounds.max.x);
                float zBound = Random.Range(startingPositionCollider.bounds.min.z, startingPositionCollider.bounds.max.z);

                unit.transform.position = new Vector3(xBound, 0, zBound);
            }
        }
        
        public void SetSelectedCharacter(CharacterBattleManager characterBattleManager)
        {
            selectedPlayerCharacter = characterBattleManager;
        }
        
        public void AllEnemyDeathCheck(CharacterUnitController characterController)
        {
            
            foreach (CharacterBattleManager characterBattleManager in enemyUnits)
            {
                if (characterBattleManager.GetComponent<CharacterUnitController>().character.characterState !=
                    CharacterState.Dead)
                {
                    return;
                }
            }
            
            EventManager.Instance.combatEvents.OnPlayerVictory();
            //UnsetBattle();
            combatManagerState = CombatManagerState.PostBattle;

            foreach (Character character in PlayerManager.Instance.currentParty)
            {
                Debug.Log("Set character inactive");
                character.characterController.GetComponent<CharacterBattleManager>().inCombat = false;
                character.characterController.GetComponent<CharacterBattleManager>().characterTimelineGaugeState = CharacterTimelineGaugeState.PostBattle;
                character.characterController.GetComponent<CharacterBattleManager>().battleState = CharacterBattleState.Inactive;

                //character.currentHealth = character.characterController.GetComponent<Health>().CurrentHealth;
            }
            
            TransitionAnimator.Start(
                TransitionType.Fade, // transition type
                duration: 1f,
                playDelay: 3f,
                sceneNameToLoad: PlayerManager.Instance.combatConfigDetails.returnSceneName
            );

            
            
        }
        
        public void AllPlayerDeathCheck(CharacterUnitController characterController)
        {
            
            foreach (CharacterBattleManager characterBattleManager in playerUnits)
            {
                if (characterBattleManager.GetComponent<CharacterUnitController>().character.characterState !=
                    CharacterState.Dead)
                {
                    return;
                }
            }
            
            
            
            EventManager.Instance.combatEvents.OnPlayerDefeat();
            Debug.Log("All Player Characters are Dead");
            //UnsetBattle();
            combatManagerState = CombatManagerState.PostBattle;
            
            TransitionAnimator transitionAnimator = TransitionAnimator.Start(
                TransitionType.DoubleWipe, // transition type
                duration: 1f,
                rotation: 90f,
                playDelay: 3f
            );

            transitionAnimator.onTransitionEnd.AddListener(SetGameOverScreen);
            
            // foreach (Character character in PlayerManager.Instance.currentParty)
            // {
            //     Debug.Log("Set character inactive");
            //     character.characterController.GetComponent<CharacterBattleManager>().inCombat = false;
            //     character.characterController.GetComponent<CharacterBattleManager>().characterTimelineGaugeState = CharacterTimelineGaugeState.PostBattle;
            //     character.characterController.GetComponent<CharacterBattleManager>().battleState = CharacterBattleState.Inactive;
            // }


        }
        
        
        public void OnPlayerSelectTarget(CharacterBattleManager characterBattleManager)
        {
            
            // selectedTargets = new List<CharacterBattleManager>();
            // selectedTargets.Add(characterBattleManager);

            selectedTarget = characterBattleManager;
            
            foreach (CharacterBattleManager selectableTargetcharacterBattleManager in selectableTargets)
            {

                if (selectableTargetcharacterBattleManager.namePanel == null)
                {
                    continue;
                }
                
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
        
        public void DetermineOrderOfTurns()
        {
            
            foreach (CharacterBattleManager character in allUnits)
            {
        
                int randomNum = Random.Range(1, 21);
        
                orderOfTurnsMap.Add(character, randomNum + (int)character.GetComponent<CharacterStatsManager>().GetStat(StatAttribute.Initiative));
                character.SetStartingPosition();
                character.battleState = CharacterBattleState.MoveToStartingBattlePosition;
                character.inCombat = true;
                CameraManager.Instance.AddToTargetGroup(character.transform);
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
        
        public void SetPauseTimer(bool isPaused)
        {
            pause = isPaused;

            if (!isPaused)
            {
                foreach (CharacterBattleManager characterBattleManager in allUnits)
                {
                    characterBattleManager.GetComponent<AnimationManager>().ResumeAnimation();
                    characterBattleManager.GetComponent<NavMeshAgent>().isStopped = false;
                }
            }
        }
        
        public void PauseAllAnimations()
        {
            foreach (CharacterBattleManager characterBattleManager in allUnits)
            {
                characterBattleManager.GetComponent<AnimationManager>().PauseAnimation();
                characterBattleManager.GetComponent<NavMeshAgent>().isStopped = true;
            }
        }
        
        public void PauseAnimationsForSkills()
        {
            // foreach (CharacterBattleManager characterBattleManager in allUnits)
            // {
            //
            //     if (characterBattleManager != selectedPlayerCharacter &&
            //         !selectedPlayerCharacter.selectedTargets.Contains(characterBattleManager))
            //     {
            //         characterBattleManager.GetComponent<AnimationManager>().PauseAnimation();
            //     }
            // }
        }
        
        public void SetSelectableTargets()
        {
            //EventManager.Instance.combatEvents.OnPlayerSelectTarget(selectedPlayerCharacter);
            combatManagerState = CombatManagerState.TargetSelect;
            targetSelectIndex = 0;
            selectableTargets = new List<CharacterBattleManager>();
            
            switch (selectedPlayerCharacter.characterBattleActionState)
            {
                case CharacterBattleActionState.Attack:
                case CharacterBattleActionState.Break:
                    selectableTargets = enemyUnits.FindAll(x => x.GetComponent<CharacterUnitController>().character.characterState == CharacterState.Alive);
                    
                    break;
                case CharacterBattleActionState.Skill:
                    selectableTargets = enemyUnits.FindAll(x => x.GetComponent<CharacterUnitController>().character.characterState == CharacterState.Alive);
                    break;
                case CharacterBattleActionState.Item:

                    switch (selectedPlayerCharacter.selectedItem.info.targetType)
                    {
                        case TargetType.All:
                        case TargetType.SingleTarget:
                            selectableTargets = allUnits.FindAll(x => x.GetComponent<CharacterUnitController>().character.characterState == CharacterState.Alive);
                            break;
                        case TargetType.SingleAlly:
                        case TargetType.AllAllies:
                            selectableTargets = playerUnits.FindAll(x => x.GetComponent<CharacterUnitController>().character.characterState == CharacterState.Alive);
                            break;
                        case TargetType.SingleEnemy:
                        case TargetType.AllEnemies:
                            selectableTargets = enemyUnits.FindAll(x => x.GetComponent<CharacterUnitController>().character.characterState == CharacterState.Alive);
                            break;
                        case TargetType.Self:
                            selectableTargets.Add(selectedPlayerCharacter);
                            break;
                    }
                    break;
            }
            
            TargetNavigation();
        }
        
        public void ConfirmAction()
        {

            switch (selectedPlayerCharacter.characterBattleActionState)
            {
                case CharacterBattleActionState.Attack:
                case CharacterBattleActionState.Break:
                    selectedPlayerCharacter.target = selectedTarget;
                    // selectedPlayerCharacter.SetActionRange();
                    selectedPlayerCharacter.SetAttackActionTargetPosition();
                    CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.ToggleCommandMenuHolderOff();
                    break;
                case CharacterBattleActionState.Skill:
                    selectedPlayerCharacter.GetComponent<AnimationManager>().ChangeAnimationState("cast");
                    //selectedPlayerCharacter.SetActionRange();
                    selectedPlayerCharacter.target = selectedTarget;
                    selectedPlayerCharacter.SetAttackActionTargetPosition();
                    CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.ToggleCommandMenuHolderOff();
                    selectedPlayerCharacter.currentSkillPoints -= selectedPlayerCharacter.selectedSkill.info.skillPointsCost;
                    selectedPlayerCharacter.GetComponent<CharacterUnitController>().character.RemoveResonancePoints(selectedPlayerCharacter.selectedSkill.info.resonancePointsCost);
                    break;
                case CharacterBattleActionState.Defend:
                    CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.ToggleCommandMenuHolderOff();
                    // selectedPlayerCharacter.SetActionRange();
                    // selectedPlayerCharacter.SetSkillCast();
                    selectedPlayerCharacter.battleState = CharacterBattleState.WaitingAction;
                    break;
                case CharacterBattleActionState.Item:
                    CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.ToggleCommandMenuHolderOff();
                    selectedPlayerCharacter.target = selectedTarget;
                    selectedPlayerCharacter.SetAttackActionTargetPosition();
                    selectedPlayerCharacter.battleState = CharacterBattleState.WaitingAction;
                    break;
                case CharacterBattleActionState.Move:
                    selectedPlayerCharacter.targetPosition = movePlayerObject.transform.position;
                    CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.ToggleCommandMenuHolderOff();
                    selectedPlayerCharacter.battleState = CharacterBattleState.WaitingAction;
                    break;
                
            }
            
            selectedPlayerCharacter.SetActionRange();
            selectedPlayerCharacter.SetSkillCast();
            
            foreach (CharacterBattleManager characterBattleManager in selectableTargets)
            {
                if (characterBattleManager.namePanel == null)
                {
                    continue;
                }
                characterBattleManager.namePanel.gameObject.SetActive(false);
            }

            selectedTarget = null;
            //selectedTargets = new List<CharacterBattleManager>();
            selectableTargets = new List<CharacterBattleManager>();
                        
            combatManagerState = CombatManagerState.Battle;
            CameraManager.Instance.ResetTargetGroupSetting();
            //CameraManager.Instance.UpdateCamera(currentBattleArea.transform);
            EventManager.Instance.combatEvents.OnActionSelected();
        }
        
        public void UnsetBattle()
        {
            
            // CameraManager.Instance.UnsetBattleCamera();
            // CameraManager.Instance.UpdateCamera(PlayerManager.Instance.currentParty[0].characterController.transform);
            TransitionAnimator.Start(
                TransitionType.Fade, // transition type
                duration: 1f,
                sceneNameToLoad: PlayerManager.Instance.combatConfigDetails.returnSceneName
            );
            
            
        }
        
        public bool CheckIfCharactersInBattlePositions()
        {

            foreach (CharacterBattleManager characterBattleManager in allUnits)
            {
                if (characterBattleManager.battleState != CharacterBattleState.Waiting && characterBattleManager.characterTimelineGaugeState != CharacterTimelineGaugeState.StartTurnPhase)
                {
                    return false;
                }
                
            }

            return true;
        }

        public void TargetNavigation()
        {
            CameraManager.Instance.SetSelectedPlayerWeight(selectableTargets[targetSelectIndex], 1000f, 2f);
            EventManager.Instance.combatEvents.OnPlayerSelectTarget(selectableTargets[targetSelectIndex]);
        }
        
    }
}