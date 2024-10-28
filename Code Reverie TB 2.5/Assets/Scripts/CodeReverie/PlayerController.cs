using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TransitionsPlus;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeReverie
{
    [DefaultExecutionOrder(-115)]
    public class PlayerController : SerializedMonoBehaviour
    {
        public PlayerMovementController playerMovementController;
        public PlayerCombatController playerCombatController;
        public Transform hollowFollowPoint;
        public Transform hollowFollowPointLeft;
        public Transform hollowFollowPointRight;
        public CharacterSuperState characterSuperState;
        public CharacterDirection characterDirection;
        
        
        
        public LevelUpGO levelUpGameObjectPF;
        
        protected void Awake()
        {
            
            DontDestroyOnLoad(this);
            //base.Awake();
            playerMovementController = GetComponent<PlayerMovementController>();
            playerCombatController = GetComponent<PlayerCombatController>();
        }

        private void OnEnable()
        {
            EventManager.Instance.playerEvents.onPlayerLock += SetCanAttackInventoryListener;
            EventManager.Instance.playerEvents.onCharacterSwap += CharacterSwap;
            EventManager.Instance.playerEvents.onLevelUp += OnLevelUp;
        }
        
        
        private void OnDisable()
        {
            EventManager.Instance.playerEvents.onPlayerLock -= SetCanAttackInventoryListener;
            EventManager.Instance.playerEvents.onCharacterSwap -= CharacterSwap;
            EventManager.Instance.playerEvents.onLevelUp -= OnLevelUp;
        }

        private void OnDestroy()
        {
            EventManager.Instance.playerEvents.onPlayerLock -= SetCanAttackInventoryListener;
            EventManager.Instance.playerEvents.onCharacterSwap -= CharacterSwap;
            
        }

        private void Update()
        {
           
        }

        public void SetCanAttackInventoryListener(bool canAttack)
        {
            
            GetComponent<PlayerCombatController>().canAttack = !canAttack;
            
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * 1f, transform.localScale.y, transform.localScale.z);
        }

        public void SwapCharacterListener(Character character, CharacterController characterUnitManager)
        {
            if (GetComponent<CharacterUnitController>().character == character)
            {
                
            }
        }

        public void CharacterSwap()
        {
            
        }


        public void CharacterMenuSwap(PartySlot partySlot)
        {
            
        }

        public void SetHollowFollowPointSideLeft()
        {
            //hollowFollowPoint.transform.position = new Vector3(Mathf.Abs(transform.localPosition.x) * -1f, transform.localPosition.y, transform.localPosition.z);
            //hollowFollowPoint.transform.position = hollowFollowPointLeft.transform.position;
        }
        
        public void SetHollowFollowPointSideRight()
        {
            //hollowFollowPoint.transform.position = new Vector3(Mathf.Abs(transform.localPosition.x) * 1f, transform.localPosition.y, transform.localPosition.z);
            //hollowFollowPoint.transform.position = hollowFollowPointRight.transform.position;
        }

        public void OnLevelUp()
        {
            // CameraManager.Instance.ScreenShake2();
            // Instantiate(levelUpGameObjectPF, transform);
        }
        
        
        private void OnTriggerEnter(Collider other)
        {
            
            //Debug.Log(other.name);
            
            if (other.TryGetComponent(out Interactable interactable))
            {
                
                // if (!PlayerManager.Instance.interactables.Contains(interactable))
                // { 
                //     PlayerManager.Instance.interactables.Add(interactable);
                //     interactable.SetQueue();
                //
                //     if (PlayerManager.Instance.interactables.Count == 1)
                //     {
                //         if (PlayerManager.Instance.interactables[0] != null)
                //         {
                //             PlayerManager.Instance.interactables[0].Activate();
                //         }
                //     }
                // }

                if (interactable.HasActiveInteractables())
                {
                    if (!CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.interactiveCommandMenuHolder.interactables.Contains(interactable))
                    {

                        if (CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.interactiveCommandMenuHolder.isActiveAndEnabled)
                        {
                            CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.interactiveCommandMenuHolder.AddInteractableButton(interactable);
                        }
                        else
                        {
                            CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.interactiveCommandMenuHolder.interactables.Add(interactable);
                            interactable.SetQueue();

                            if (CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.interactiveCommandMenuHolder.interactables.Count == 1)
                            {
                                if (CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.interactiveCommandMenuHolder.interactables[0] != null)
                                {
                                    CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.interactiveCommandMenuHolder.interactables[0].Activate();
                                }
                            }
                        
                            CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.ToggleInteractiveCommandMenuHolderOn();
                        }
                    }
                }
                
            }

            if (other.TryGetComponent(out CombatTrigger combatTrigger))
            {
                if (other.GetComponentInParent<ComponentTagManager>())
                {

                    ComponentTagManager componentTagManager = other.GetComponentInParent<ComponentTagManager>();
                    
                    if (componentTagManager.HasTag(ComponentTag.Enemy))
                    {
                  
                        // if (CombatManager.Instance.battleManagerState == BattleManagerState.Inactive && CombatManager.Instance.currentBattleArea != null)
                        // {
                        //     EventManager.Instance.combatEvents.OnCombatEnter();
                        // }
                        //
                    
                    }
                }
            }
            
            // if (other.GetComponent<ComponentTagManager>())
            // {
            //
            //     ComponentTagManager componentTagManager = other.GetComponentInParent<ComponentTagManager>();
            //         
            //     if (componentTagManager.HasTag(ComponentTag.Enemy))
            //     {
            //       
            //         // if (CombatManager.Instance.battleManagerState == BattleManagerState.Inactive && CombatManager.Instance.currentBattleArea != null)
            //         // {
            //         //     EventManager.Instance.combatEvents.OnCombatEnter();
            //         // }
            //         //
            //         
            //     }
            // }
            
            
            if (other.TryGetComponent(out ComponentTagManager tagManager))
            {
                
                
                if (tagManager.HasTag(ComponentTag.BattleArea))
                {
                    //BattleManager.Instance.currentBattleArea = other.GetComponent<BattleArea>();
                }
                
                if (tagManager.HasTag(ComponentTag.Enemy))
                {
                  
                    // if (BattleManager.Instance.battleManagerState == BattleManagerState.Inactive && BattleManager.Instance.currentBattleArea != null)
                    // {
                    //     EventManager.Instance.combatEvents.OnCombatEnter();
                    // }
                    
                    


                    if (AreaManager.instance != null && other.GetComponent<EnemyAI>() != null)
                    {
                        
                        // Destroy(other.gameObject);
                        
                        if (!AreaManager.instance.combatLocation.IsNullOrEmpty)
                        {
                            // CanvasManager.Instance.combatTransitionAnimator.gameObject.SetActive(true);
                            // CanvasManager.Instance.sceneTransitionAnimator.gameObject.SetActive(true);
                            

                            if (other.GetComponent<EnemyAI>().enemyList != null)
                            {
                                
                                GetComponent<PlayerMovementController>().enabled = false;
                                
                                
                                PlayerManager.Instance.combatConfigDetails = new CombatConfigDetails(
                                    returnSceneName: SceneManager.GetActiveScene().name,
                                    characterInstanceID: other.GetComponent<CharacterUnitController>().characterInstanceID,
                                    characterReturnPosition: transform.position,
                                    enemyList: other.GetComponent<EnemyAI>().enemyList.Count > 0 ? other.GetComponent<EnemyAI>().enemyList : new List<CharacterDataContainer>{other.GetComponent<CharacterUnitController>().character.info} 
                                    );
                                
                                TransitionAnimator.Start(
                                    TransitionType.Smear, // transition type
                                    duration: 1f,
                                    sceneNameToLoad: AreaManager.instance.combatLocation.SceneName
                                );
                            }
                            else
                            {
                                GetComponent<PlayerMovementController>().enabled = false;
                                
                                
                                PlayerManager.Instance.combatConfigDetails = new CombatConfigDetails(
                                    returnSceneName: SceneManager.GetActiveScene().name,
                                    characterInstanceID: other.GetComponent<CharacterUnitController>().characterInstanceID,
                                    characterReturnPosition: transform.position,
                                    enemyList: new List<CharacterDataContainer>{other.GetComponent<CharacterUnitController>().character.info} 
                                );
                                
                                TransitionAnimator.Start(
                                    TransitionType.Smear, // transition type
                                    duration: 1f,
                                    sceneNameToLoad: AreaManager.instance.combatLocation.SceneName
                                );
                            }
                            
                            
                            
                            //SceneManager.LoadScene(AreaManager.instance.combatLocation);
                        }
                        else
                        {
                            Destroy(other.gameObject);
                        }
                    }
                    
                }
            }
            
        }
        
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Interactable interactable))
            {
                // PlayerManager.Instance.interactables.Remove(interactable);
                // interactable.Deactivate();
                CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.interactiveCommandMenuHolder.RemoveInteractableButton(
                    interactable);
            }
            
            if (other.TryGetComponent(out ComponentTagManager tagManager))
            {
                
                if (tagManager.HasTag(ComponentTag.BattleArea))
                {
                    //BattleManager.Instance.currentBattleArea = null;
                }
            }
        }
        
        
        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            
            //Debug.Log(other.name);
            
            if (other.TryGetComponent(out Interactable interactable))
            {
                if (!PlayerManager.Instance.interactables.Contains(interactable))
                { 
                    PlayerManager.Instance.interactables.Add(interactable);
                    interactable.SetQueue();

                    if (PlayerManager.Instance.interactables.Count == 1)
                    {
                        if (PlayerManager.Instance.interactables[0] != null)
                        {
                            PlayerManager.Instance.interactables[0].Activate();
                        }
                    }
                    
                }
            }
            
            if (other.TryGetComponent(out ComponentTagManager tagManager))
            {
                
                
                if (tagManager.HasTag(ComponentTag.BattleArea))
                {
                    //BattleManager.Instance.currentBattleArea = other.GetComponent<BattleArea>();
                }
                
                if (tagManager.HasTag(ComponentTag.Enemy))
                {
                  
                    // if (CombatManager.Instance.battleManagerState == BattleManagerState.Inactive && CombatManager.Instance.currentBattleArea != null)
                    // {
                    //     EventManager.Instance.combatEvents.OnCombatEnter();
                    // }
                    
                    
                }
            }
            
        }
        
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Interactable interactable))
            {
                PlayerManager.Instance.interactables.Remove(interactable);
                interactable.Deactivate();
            }
            
            if (other.TryGetComponent(out ComponentTagManager tagManager))
            {
                
                if (tagManager.HasTag(ComponentTag.BattleArea))
                {
                    //BattleManager.Instance.currentBattleArea = null;
                }
            }
        }
        
    }
}