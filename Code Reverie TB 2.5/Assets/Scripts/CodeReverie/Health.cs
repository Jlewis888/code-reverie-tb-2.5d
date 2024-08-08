using System;
using System.Collections;
using System.Collections.Generic;
using DamageNumbersPro;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class Health : SerializedMonoBehaviour, IDamageable
    {
        [SerializeField]
        private float currentHealth;
        [SerializeField] 
        private float maxHealth;
        public GameObject damageNumberHolder;
        public SpriteRenderer spriteRenderer;
        public Material defaultMaterial;
        public Material replacementMaterial;
        public float damageFlashDuration;
        public bool invincible;
        public bool canTakeDamage = true;
        public float iFramesTimer;
        public float iFramesTime = 1f;
        public int healthBarCount;
        public int currentHealthBarCount;
        public CharacterState characterState;

        private void Awake()
        {
            canTakeDamage = true;
            damageFlashDuration = 0.1f;
            healthBarCount = 10;

            // if (transform.CompareTag("Player"))
            // {
            //     //defaultMaterial = GetComponent<CharacterController>().characterUnit.spriteRenderer.material;
            //     defaultMaterial = spriteRenderer.material;
            // }
            // else if (transform.CompareTag("Enemy"))
            // {
            //     defaultMaterial = GetComponent<SpriteRenderer>().material;
            // }
            
            //defaultMaterial = spriteRenderer.material;
        }

        private void Start()
        {
            SetHealth();
        }

        private void OnEnable()
        {
            
            // if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Player))
            // {
            //     
            //     EventManager.Instance.playerEvents.onDashStart += EnableInvincibility;
            //     EventManager.Instance.playerEvents.onDashEnd += DisableInvincibility;
            // }
            
            EventManager.Instance.playerEvents.onDodgeStart += EnableIFrames;
            EventManager.Instance.playerEvents.onDodgeEnd += DisableIFrames;
            
            // EventManager.Instance.playerEvents.onDashStart += EnableIFrames;
            // EventManager.Instance.playerEvents.onDashEnd += DisableIFrames;
            
        }


        private void OnDisable()
        {
            // if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Player))
            // {
            //    
            //     EventManager.Instance.playerEvents.onDashStart -= EnableInvincibility;
            //     EventManager.Instance.playerEvents.onDashEnd -= DisableInvincibility;
            // }
            
            
            EventManager.Instance.playerEvents.onDodgeStart -= EnableIFrames;
            EventManager.Instance.playerEvents.onDodgeEnd -= DisableIFrames;
            
            // EventManager.Instance.playerEvents.onDashStart -= EnableIFrames;
            // EventManager.Instance.playerEvents.onDashEnd -= DisableIFrames;
        }


        public void Update()
        {
            if (TryGetComponent(out CharacterStatsManager characterStatsManager))
            {
                MaxHealth = characterStatsManager.GetStat(StatAttribute.Health);
            }


            if (Input.GetKeyDown(KeyCode.Z))
            {
                StartCoroutine(ApplyDamageFlash());
                if (transform.CompareTag("Player"))
                {
                    
                    //DamageProfile damageProfile = new DamageProfile(GetComponent<CharacterBattleManager>(), this, new List<DamageTypes>());
                }
                
                
            }

            if (iFramesTimer > 0)
            {
                canTakeDamage = false;
                iFramesTimer -= Time.deltaTime;
            }
            else
            {
                canTakeDamage = true;
            }


            // if (CurrentHealth <= 0)
            // {
            //     Death();
            // }
           
        }
        
        
        public void SetHealth()
        {
            if (TryGetComponent(out CharacterStatsManager characterStatsManager))
            {
                MaxHealth = characterStatsManager.GetStat(StatAttribute.Health);
            }

            if (TryGetComponent(out CharacterController characterController))
            {
                healthBarCount = characterController.character.info.healthBarCount;
            }
            

            CurrentHealth = MaxHealth;
        }


        public float CurrentHealth
        {
            get => currentHealth;
            set => currentHealth = value;
        }

        public float MaxHealth { 
            get => maxHealth; 
            set => maxHealth = value; 
        }

        public int CurrentHealthBarCount
        {
            get => (int)Mathf.Ceil(CurrentHealth / HealthPerBar);
        }

        public float HealthPerBar
        {
            get => MaxHealth / healthBarCount;
        }

        public float CurrentHealthBar
        {
            get => CurrentHealth - (HealthPerBar * Mathf.Floor(CurrentHealth / HealthPerBar));
        }
        
        
        public void ApplyDamage(DamageProfile damageProfile)
        {
            if (canTakeDamage && !invincible)
            {
                // if (GetComponent<CharacterController>().character.characterState != CharacterState.Dead)
                // {
                //     CreateDamagePopup(damageProfile);
                //     //StartCoroutine(ApplyDamageFlash());
                //     
                //     CurrentHealth -= damageProfile.damageAmount;
                //
                //     if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Enemy))
                //     {
                //         EventManager.Instance.combatEvents.OnEnemyDamageTaken(damageProfile);
                //         
                //     }
                //     
                //     if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Player))
                //     {
                //         EventManager.Instance.combatEvents.OnPlayerDamageTaken(damageProfile);
                //     }
                //
                //     if (GetComponent<CharacterBattleManager>().battleState == CharacterBattleState.WaitingAction)
                //     {
                //         GetComponent<CharacterBattleManager>().battleState = CharacterBattleState.Interrupted;
                //     }
                //     
                //     
                //     
                //     EventManager.Instance.combatEvents.OnDamage(damageProfile);
                //     SoundManager.Instance.PlayOneShotSound("punch_general_body_impact_08", true);
                //
                //     if (CurrentHealth <= 0)
                //     {
                //         Death();
                //     }
                // }
                
                //CreateDamagePopup(damageProfile);
                //StartCoroutine(ApplyDamageFlash());
                    
                CurrentHealth -= damageProfile.damageAmount;

                if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Enemy))
                {
                    EventManager.Instance.combatEvents.OnEnemyDamageTaken(damageProfile);
                        
                }
                    
                if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Player))
                {
                    EventManager.Instance.combatEvents.OnPlayerDamageTaken(damageProfile);
                }

                if (GetComponent<CharacterBattleManager>().battleState == CharacterBattleState.WaitingAction && damageProfile.isBreak)
                {
                    GetComponent<CharacterBattleManager>().battleState = CharacterBattleState.Interrupted;
                }
                    
                    
                    
                EventManager.Instance.combatEvents.OnDamage(damageProfile);
                SoundManager.Instance.PlayOneShotSound("punch_general_body_impact_08", true);

                if (CurrentHealth <= 0)
                {
                    Death();
                }
            }
            else
            {
                
                Debug.Log("Dodged");
            }
        }

        public void ApplyHeal(float amount)
        {
            //throw new System.NotImplementedException();
            CurrentHealth += amount;

            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            
        }

        public void Death()
        {

            if (GetComponent<CharacterController>().character.characterState != CharacterState.Dead)
            {
                if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Enemy))
                {
                    EventManager.Instance.combatEvents.OnEnemyDeath(GetComponent<CharacterController>()); 
                }
            
            
                if (CompareTag("Player"))
                {
                    EventManager.Instance.combatEvents.OnPlayerDeath(GetComponent<CharacterController>());
                    
                    
                    // PlayerManager.Instance.SwapCharacterOnDeath();
                    //
                    //
                    //
                    // if (PlayerManager.Instance.AllPartyMembersDeathCheck())
                    // {
                    //     Debug.Log("All Party Members have died");
                    // }
                    
                    
                }
            
                GetComponent<CharacterController>().character.characterState = CharacterState.Dead;
                GetComponent<AnimationManager>().ChangeAnimationState("death");
                EventManager.Instance.combatEvents.OnCharacterDeath(GetComponent<CharacterBattleManager>());
                EventManager.Instance.combatEvents.OnDeath(GetComponent<CharacterController>().character.info.id);
                
            }
            
            //todo Placeholder: To use the above code
            // if (characterState != CharacterState.Dead)
            // {
            //     characterState = CharacterState.Dead;
            //     
            //     if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Enemy))
            //     {
            //         EventManager.Instance.combatEvents.OnEnemyDeath(GetComponent<CharacterController>()); 
            //     }
            //     
            //     
            //     if (CompareTag("Player"))
            //     {
            //         EventManager.Instance.combatEvents.OnPlayerDeath(GetComponent<CharacterController>());
            //         
            //         
            //         // PlayerManager.Instance.SwapCharacterOnDeath();
            //         //
            //         //
            //         //
            //         // if (PlayerManager.Instance.AllPartyMembersDeathCheck())
            //         // {
            //         //     Debug.Log("All Party Members have died");
            //         // }
            //         
            //         
            //     }
            // }
            
            
        }
        
        void CreateDamagePopup(DamageProfile damageProfile)
        {
            DamageNumber damagePopup = new DamageNumberGUI();
            
            
            if (damageProfile.isCrit)
            {
                //Debug.Log("Crit Popup");
                damagePopup = Resources.Load<DamageNumber>("DamageNumberCriticalPopup").Spawn(damageNumberHolder.transform.position, damageProfile.damageAmount);
            }
            else
            {
               // Debug.Log("Normal Popup");
                damagePopup = Resources.Load<DamageNumber>("DamageNumberPopup").Spawn(damageNumberHolder.transform.position, damageProfile.damageAmount);
            }
            
            damagePopup.SetAnchoredPosition(damageNumberHolder.transform, Vector2.zero);
        }
        
        void CreateHealPopup( float number, bool isCrit = false)
        {
            DamageNumber damagePopup = new DamageNumberGUI();
            
            
            if (isCrit)
            {
                damagePopup = Resources.Load<DamageNumber>("HealNumberCriticalPopup").Spawn(damageNumberHolder.transform.position, number);
            }
            else
            {
                damagePopup = Resources.Load<DamageNumber>("HealNumberPopup").Spawn(damageNumberHolder.transform.position, number);
            }
            
            damagePopup.SetAnchoredPosition(damageNumberHolder.transform, Vector2.zero);
        }
        
        public IEnumerator ApplyDamageFlash()
        {
            Material[] materials = spriteRenderer.materials;
            materials[0] = replacementMaterial;
            Color color = spriteRenderer.color;
            spriteRenderer.materials = materials;
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(damageFlashDuration);
            materials[0] = defaultMaterial;
            spriteRenderer.materials = materials;
            spriteRenderer.color = color;
        }

        public void EnableIFrames()
        {
            
            if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Player))
            {
                
                canTakeDamage = false;
            }
          
        }

        public void DisableIFrames()
        {
            if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Player))
            {
                iFramesTimer = iFramesTime;
            }
        }
        
        
    }
}