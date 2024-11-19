using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class ActiveSkill : SerializedMonoBehaviour
    {
        public Skill basicSkill;
        public Skill coreSkill;
        public Skill alchemicBurstSkill;
        public float cooldownTimer;
        public SkillObject currentSkillObject;
        public SkillObject currentCoreSkillObject;
        public SkillObject currentBurstSkillObject;
        public Vector2 lookAtRotation;
        public Vector3 aimInput;
        public bool canMove;
        public float angle;
        public float rotationSpeed;


        private void OnEnable()
        {
            EventManager.Instance.playerEvents.onSkillSlotUpdate += SetSkillObject;
            EventManager.Instance.playerEvents.onActionBarSet += SetSkillObject;
            EventManager.Instance.combatEvents.onPlayerBasicSkillTrigger += Attack;
            EventManager.Instance.combatEvents.onPlayerCoreSkillTrigger += AttackCore;
        }

        private void OnDisable()
        {
            EventManager.Instance.playerEvents.onSkillSlotUpdate -= SetSkillObject;
            EventManager.Instance.playerEvents.onActionBarSet -= SetSkillObject;
            EventManager.Instance.combatEvents.onPlayerBasicSkillTrigger -= Attack;
            EventManager.Instance.combatEvents.onPlayerCoreSkillTrigger -= AttackCore;
        }


        private void Update()
        {
            
            if (cooldownTimer > 0)
            {
                cooldownTimer -= Time.deltaTime;
            }
            
            switch (GameManager.Instance.currentControlScheme)
            {
                case ControlSchemeType.KeyboardMouse:
                    //KeyboardMouseAim();
                    KeyboardMouseAim2();
                    break;
                case ControlSchemeType.Gamepad:
                    GamepadAim();
                    break;
            }
        }

        private void LateUpdate()
        {
            switch (GameManager.Instance.currentControlScheme)
            {
                case ControlSchemeType.KeyboardMouse:

                    if (GetComponentInParent<CharacterUnitController>().characterUnit.spriteRenderer.flipX)
                    {
                        //transform.rotation = Quaternion.Euler(180,0,angle);
                        transform.localScale = new Vector3(1, -1, 1);
                    }
                    else
                    {
                        //transform.rotation = Quaternion.Euler(0,0,angle);
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    transform.rotation = Quaternion.Euler(0,0,angle);
                        
                    break;
                case ControlSchemeType.Gamepad:
                    
                    if (lookAtRotation.x != 0 || lookAtRotation.y != 0)
                    {
                            
                        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                        transform.rotation =
                            (Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed));
                            
                    }
                    
                    break;
            }
        }


        public void KeyboardMouseAim2()
        {
            //Vector3 mousePos = Input.mousePosition;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

            Vector3 rotation = mousePos - transform.position;
            
            angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        }
        
        
        public void GamepadAim()
        {
            aimInput.x = GameManager.Instance.playerInput.GetAxis("Aim Horizontal");
            aimInput.y = GameManager.Instance.playerInput.GetAxis("Aim Vertical");

                
            angle = Mathf.Atan2(aimInput.y, aimInput.x) * Mathf.Rad2Deg;
                
            if (aimInput.normalized.magnitude > 0)
            {
                lookAtRotation.x = GameManager.Instance.playerInput.GetAxis("Aim Horizontal");
                lookAtRotation.y = GameManager.Instance.playerInput.GetAxis("Aim Vertical");

            }
            else
            {
                lookAtRotation.x = GameManager.Instance.playerInput.GetAxis("Move Horizontal");
                lookAtRotation.y = GameManager.Instance.playerInput.GetAxis("Move Vertical");
                
            }
        }

        public void Attack()
        {
            // if (cooldownTimer <= 0)
            // {
            //     if (BasicSkill != null && currentSkillObject != null)
            //     {
            //         //PlayerManager.Instance.currentParty[0].character.characterController.GetComponent<AnimationManager>().ChangeAnimationState("basic_attack_1");
            //         currentSkillObject.Attack();
            //         cooldownTimer = 2f;
            //     }
            //
            // }
            
            if (BasicSkill != null && currentSkillObject != null)
            {
                //PlayerManager.Instance.currentParty[0].character.characterController.GetComponent<AnimationManager>().ChangeAnimationState("basic_attack_1");
                currentSkillObject.Attack();
                cooldownTimer = 2f;
            }
        }
        
        public void AttackCore()
        {
            // if (cooldownTimer <= 0)
            // {
            //     if (BasicSkill != null && currentSkillObject != null)
            //     {
            //         //PlayerManager.Instance.currentParty[0].character.characterController.GetComponent<AnimationManager>().ChangeAnimationState("basic_attack_1");
            //         currentSkillObject.Attack();
            //         cooldownTimer = 2f;
            //     }
            //
            // }
            
            if (CoreSkill != null && currentCoreSkillObject != null)
            {
                //PlayerManager.Instance.currentParty[0].character.characterController.GetComponent<AnimationManager>().ChangeAnimationState("basic_attack_1");
                currentCoreSkillObject.Attack();
                cooldownTimer = 2f;
            }
        }
        

        public Skill BasicSkill
        {
            get { return basicSkill; }

            set
            {
                if (value != basicSkill)
                {
                    basicSkill = value;

                    if (basicSkill == null)
                    {
                        Destroy(currentSkillObject.gameObject);
                    } else
                    {
                        if (currentSkillObject != null)
                        {
                            Destroy(currentSkillObject.gameObject);
                        }

                        if (basicSkill.info == null)
                        {
                            return;
                        } 
                        
                        
                        if (basicSkill.info.skillGameObject == null)
                        {
                            currentSkillObject = null;
                            return;
                        }

                        currentSkillObject = Instantiate(basicSkill.info.skillGameObject, transform.position, Quaternion.identity);
                        
                        
                        currentSkillObject.transform.parent = transform;
                        currentSkillObject.transform.localRotation = Quaternion.Euler(0, 0, 0f);
                        currentSkillObject.transform.localScale = Vector3.one;
                        //currentSkillObject.gameObject.SetActive(false);
                        cooldownTimer = 0;

                    }
                    
                }
            }
        }
        
        public Skill CoreSkill
        {
            get { return coreSkill; }

            set
            {
                if (value != coreSkill)
                {
                    coreSkill = value;

                    if (coreSkill == null)
                    {
                        Destroy(currentCoreSkillObject.gameObject);
                    } else
                    {
                        if (currentCoreSkillObject != null)
                        {
                            Destroy(currentCoreSkillObject.gameObject);
                        }

                        if (coreSkill.info == null)
                        {
                            return;
                        } 
                        
                        
                        if (coreSkill.info.skillGameObject == null)
                        {
                            currentCoreSkillObject = null;
                            return;
                        }

                        currentCoreSkillObject = Instantiate(coreSkill.info.skillGameObject, transform.position, Quaternion.identity);
                        
                        
                        currentCoreSkillObject.transform.parent = transform;
                        currentCoreSkillObject.transform.localRotation = Quaternion.Euler(0, 0, 0f);
                        currentCoreSkillObject.transform.localScale = Vector3.one;
                        //currentSkillObject.gameObject.SetActive(false);
                        cooldownTimer = 0;

                    }
                    
                }
            }
        }
        
        

        public void SetSkillObject()
        {
            
            // BasicSkill = PlayerManager.Instance.currentParty[0]
            //     .EquippedArchetype.archetypeSkills.equippedSkills[SkillType.Basic].skill;
            //
            //
            // CoreSkill = PlayerManager.Instance.currentParty[0]
            //     .EquippedArchetype.archetypeSkills.equippedSkills[SkillType.Action].skill;
        }
        
    }
}