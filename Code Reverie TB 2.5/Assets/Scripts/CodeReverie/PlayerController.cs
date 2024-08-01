using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

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
            if (GetComponent<CharacterController>().character == character)
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
                  
                    if (BattleManager.Instance.battleManagerState == BattleManagerState.Inactive && BattleManager.Instance.currentBattleArea != null)
                    {
                        EventManager.Instance.combatEvents.OnCombatEnter();
                    }
                    
                    
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