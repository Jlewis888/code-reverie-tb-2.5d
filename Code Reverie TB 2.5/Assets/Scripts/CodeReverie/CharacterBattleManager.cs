using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProjectDawn.Navigation.Hybrid;
using Sirenix.OdinInspector;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Action = System.Action;
using Random = UnityEngine.Random;

namespace CodeReverie
{
    public class CharacterBattleManager : SerializedMonoBehaviour
    {
        //private Rigidbody rb;
        public CharacterController characterController;
        private BehaviorGraphAgent _behaviorGraphAgent;
        public CharacterBattleState battleState;
        [SerializeField] private CharacterBattleActionState _characterBattleActionState;
        public CharacterBattleActionState prevCharacterBattleActionState;
        [SerializeField] private CharacterActionGaugeState _characterActionGaugeState;
        
        public BattlePosition battlePosition;
        public CharacterBattleManager target;
        public List<CharacterBattleManager> targetList = new List<CharacterBattleManager>();
        //public List<CharacterBattleManager> selectedTargets;
        public Vector3 targetPosition;
        
        public Vector3 targetMovePosition;
        public Vector2 targetBattlePosition;
        public Vector3 skillRecallTargetPosition;

        //TODO Remove at some point due to combat no longer being Dynamic
        public bool inCombat;
        public GameObject namePanel;
        public int currentMaxTargets;
        public float actionPhaseCooldown = 5f;
        public float cooldownTimer;
        public Vector3 moveDir;
        public float repositionTime;
        public float repositionTimer;
        public float moveSpeed;
        public Skill selectedSkill;
        public Item selectedItem;
        // public int actionPointsMax;
        // public int currentActionPoints;
        public int skillPointsMax;
        public int currentSkillPoints;
        public float actionRange;
        public SkillCastTime skillCastTime;
        public GameObject castObjectHolder;
        public float distanceFromCenter;
        
        // RADIAL MENU POSITIONING //
        public float staminaGauge;
        //public NavMeshAgent agent;
        public AgentAuthoring agent;

        public float hasteVal;

        private void Awake()
        {
            if (namePanel != null)
            {
                namePanel.SetActive(false);
            }

            //rb = GetComponent<Rigidbody>();
            characterController = GetComponent<CharacterController>();
            moveSpeed = 5f;

            if (actionPhaseCooldown == 0)
            {
                actionPhaseCooldown = 5f;
            }


            cooldownTimer = 0;

            //repositionTime = 0.5f;
            repositionTime = .75f;

            skillPointsMax = 100;
            currentSkillPoints = 100;
            staminaGauge = 5f;
            
            //agent = GetComponent<NavMeshAgent>() != null ? GetComponent<NavMeshAgent>() : null;
            agent = GetComponent<AgentAuthoring>() != null ? GetComponent<AgentAuthoring>() : null;

            //hasteVal = GetComponent<CharacterUnitController>().character.characterStats.GetStat(StatAttribute.Haste);


            _behaviorGraphAgent = GetComponent<BehaviorGraphAgent>();

        }

        private void Start()
        {
            actionRange = GetComponent<CharacterUnitController>().character.info.attackRange;
            // EventManager.Instance.combatEvents.onAttackEnd += OnAttackEnd;
            // EventManager.Instance.combatEvents.onSkillComplete += OnSkillEnd;
        }

        private void OnEnable()
        {
            // EventManager.Instance.combatEvents.onAttackEnd += OnAttackEnd;
            // EventManager.Instance.combatEvents.onSkillComplete += OnSkillEnd;
        }

        private void OnDisable()
        {
            // EventManager.Instance.combatEvents.onAttackEnd -= OnAttackEnd;
            // EventManager.Instance.combatEvents.onSkillComplete -= OnAttackEnd;
        }


        private void Update()
        {
            if (CombatManager.Instance != null)
            {
                if (CombatManager.Instance.combatManagerState == CombatManagerState.PreBattle)
                {
                    // repositionTimer -= Time.deltaTime;
                    //
                    // //rb.MovePosition(rb.position + moveDir * (15f * Time.fixedDeltaTime));
                    // //characterController.Move(moveDir * (8f * Time.fixedDeltaTime));
                    // characterController.Move(moveDir * (4f * Time.fixedDeltaTime));
                    // FlipSpriteMoveDirection();
                    //
                    // StopMovementOnReachingMaxDistance();
                    //
                    // if (repositionTimer <= 0)
                    // {
                    //     FaceNearestEnemy();
                    //     
                    //     
                    //     if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Enemy))
                    //     {
                    //         AutoTurnPicks();
                    //     }
                    //
                    //     battleState = CharacterBattleState.Waiting;
                    //     characterActionGaugeState = CharacterActionGaugeState.StartTurnPhase;
                    // }
                    
                    

                    if (agent != null)
                    {
                        //if (!agent.hasPath)
                        if (!GetComponent<AgentNavMeshAuthoring>().HasEntityPath)
                        {
                            SetRandomTargetMovePosition();
                            agent.SetDestination(targetMovePosition);
                        }
                        else
                        {
                            if (!IsAgentMoving())
                            {
                                agent.SetDestination(GetPosition());
                                FaceNearestEnemy();
                                
                                //
                                // if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Enemy))
                                // {
                                //     AutoTurnPicks();
                                // }
                                
                                battleState = CharacterBattleState.Waiting;
                                characterActionGaugeState = CharacterActionGaugeState.StartTurnPhase;
                            }
                        }
                    }
                    
                    
                }
                //else if (CombatManager.Instance.combatManagerState == CombatManagerState.Battle && !CombatManager.Instance.pause)
                else if (CombatManager.Instance.combatManagerState == CombatManagerState.Battle)
                {
                    if (GetComponent<CharacterUnitController>().character.characterState == CharacterState.Alive)
                    {
                        switch (characterActionGaugeState)
                        {
                            
                            case CharacterActionGaugeState.StartTurnPhase:
                                
                                GetComponent<CharacterUnitController>().character.characterStats.UpdateTempStats();
                                break;
                            
                            
                            case CharacterActionGaugeState.WaitPhase:
                                
                                if (!CombatManager.Instance.pause)
                                {
                                    

                                    switch (battleState)
                                    {
                                        case CharacterBattleState.Waiting:
                                            FaceNearestEnemy();
                                            GetComponent<AnimationManager>().ChangeAnimationState("idle");
                                            break;
                                        
                                        case CharacterBattleState.MoveToRandomBattlePosition:
                                            GetComponent<AnimationManager>().ChangeAnimationState("run");
                                            
                                            agent.SetDestination(targetMovePosition);
                                            
                                            if (!IsAgentMoving())
                                            {
                                                agent.SetDestination(GetPosition());
                                                battleState = CharacterBattleState.Waiting;
                                            }
                                            
                                            

                                            break;
                                    }
                                }


                                break;
                            case CharacterActionGaugeState.PreCommandPhase:
                                cooldownTimer = actionPhaseCooldown * .75f;
                                break;
                            case CharacterActionGaugeState.CommandPhase:

                                if (!CombatManager.Instance.pause)
                                {
                                    switch (battleState)
                                    {
                                        case CharacterBattleState.Interrupted:
                                            cooldownTimer /= 2;
                                            battleState = CharacterBattleState.Waiting;
                                            characterActionGaugeState = CharacterActionGaugeState.WaitPhase;

                                            if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Enemy))
                                            {
                                                AutoTurnPicks();
                                            }
                                            else
                                            {
                                                skillCastTime = SkillCastTime.None;
                                            }

                                            break;
                                    }
                                }

                                break;
                            case CharacterActionGaugeState.ActionPhase:
                                if (!CombatManager.Instance.pause)
                                {
                                    switch (battleState)
                                    {
                                        case CharacterBattleState.WaitingQueue:

                                            switch (characterBattleActionState)
                                            {
                                                case CharacterBattleActionState.Attack:
                                                case CharacterBattleActionState.Break:
                                                case CharacterBattleActionState.Move:
                                                case CharacterBattleActionState.Defend:
                                                case CharacterBattleActionState.Item:
                                                    SetRandomTargetMovePosition();
                                                    battleState = CharacterBattleState.MoveToCombatActionPosition;
                                                    break;
                                                case CharacterBattleActionState.Skill:
                                                    if (CombatManager.Instance.combatQueue.Peek() == this)
                                                    {
                                                        SetRandomTargetMovePosition();
                                                        //EventManager.Instance.combatEvents.OnCombatPause(true);
                                                        battleState = CharacterBattleState.MoveToCombatActionPosition;
                                                    }

                                                    break;
                                               
                                            }

                                            break;

                                        case CharacterBattleState.MoveToCombatActionPosition:

                                            Vector3 direction = targetPosition - GetPosition();

                                            switch (characterBattleActionState)
                                            {
                                                case CharacterBattleActionState.Defend:
                                                    battleState = CharacterBattleState.Action;
                                                    break;
                                                case CharacterBattleActionState.Move:
                                                    
                                                    if (target == null)
                                                    {
                                                        EndTurn();
                                                    }

                                                    bool distance = Vector3.Distance(
                                                                        new Vector3(transform.position.x, 0,
                                                                            transform.position.z),
                                                                        new Vector3(targetPosition.x, 0,
                                                                            targetPosition.z)) <=
                                                                    0.15f;
                                                    
                                                    agent.SetDestination(targetPosition);
                                            
                                                   
                                                    if (distance)
                                                    {
                                                        GetComponent<AnimationManager>().ChangeAnimationState("idle");
                                                        battleState = CharacterBattleState.Action;
                                                    }
                                                    else
                                                    {
                                                        GetComponent<AnimationManager>().ChangeAnimationState("run");
                                                    }
                                                    
                                                    

                                                    break;

                                                default:
                                                    
                                                    if (target == null)
                                                    {
                                                        EndTurn();
                                                    }
                                                    
                                                    
                                                    agent.SetDestination(target.transform.position);
                                                    
                                                    if (Vector3.Distance(
                                                            new Vector3(transform.position.x, 0, transform.position.z),
                                                            new Vector3(target.transform.position.x, 0,
                                                                target.transform.position.z)) <=
                                                        actionRange)
                                                    {
                                                        agent.SetDestination(transform.position);
                                                        agent.Stop();
                                                        
                                                        GetComponent<AnimationManager>().ChangeAnimationState("idle");
                                                        battleState = CharacterBattleState.Action;
                                                    }
                                                    else
                                                    {
                                                        GetComponent<AnimationManager>().ChangeAnimationState("run");
                                                    }
                                                    
                                                   

                                                    break;
                                            }
                                            
                                            break;


                                        case CharacterBattleState.Action:
                                            PerformAction(() => { });

                                            break;
                                    }
                                }

                                break;
                            
                        }
                    }
                }
                else if (CombatManager.Instance.combatManagerState == CombatManagerState.PostBattle)
                {
                    GetComponent<AnimationManager>().ChangeAnimationState("idle");
                }
            }
        }

        public void EndTurnResetSkill()
        {
            CameraManager.Instance.ResetCombatFollowTarget();
            CameraManager.Instance.ResetTargetGroupSetting();
            characterBattleActionState = CharacterBattleActionState.Idle;
            SetRoamingDirection();
            SetRandomTargetMovePosition();
            repositionTimer = repositionTime;
            battleState = CharacterBattleState.MoveToRandomBattlePosition;

            EventManager.Instance.combatEvents.OnCombatPause(false);
            DequeueAction();
            cooldownTimer = 0;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            prevCharacterBattleActionState = CharacterBattleActionState.Idle;
            characterActionGaugeState = CharacterActionGaugeState.StartTurnPhase;

        }

        public CharacterBattleActionState characterBattleActionState
        {
            get => _characterBattleActionState;
            set
            {

                if (value != _characterBattleActionState)
                {
                    prevCharacterBattleActionState = _characterBattleActionState;
                   
                    
                   
                }
                
                _characterBattleActionState = value;

                
                
                
            }
        }
        
        public CharacterActionGaugeState characterActionGaugeState
        {
            get => _characterActionGaugeState;
            set
            {
                _characterActionGaugeState = value;
                
                if (_behaviorGraphAgent)
                {
                    _behaviorGraphAgent.SetVariableValue("Character Action Gauge State", _characterActionGaugeState);
                }
            }
        }
        
        

        protected void GetDistanceFromBattleAreaCenter()
        {
            distanceFromCenter = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                new Vector3(CombatManager.Instance.areaCollider.transform.position.x, 0,
                    CombatManager.Instance.areaCollider.transform.position.z));
        }


        public void StopMovementOnReachingMaxDistance()
        {
            GetDistanceFromBattleAreaCenter();
            if (distanceFromCenter > CombatManager.Instance.areaRange)
            {
                repositionTimer = 0;
                Vector3 vect = transform.position - CombatManager.Instance.areaCollider.transform.position;
                vect *= CombatManager.Instance.areaRange / distanceFromCenter;
                transform.position = CombatManager.Instance.areaCollider.transform.position + vect;
            }
        }

        public void SelectAction()
        {
            battleState = CharacterBattleState.WaitingAction;
        }

        public void SetStartingPosition()
        {
            SetRoamingDirection();
            repositionTimer = 0.25f;
        }


        public void AutoTurnPicks()
        {
            List<CharacterBattleActionState> combatAbilityChoices = Enum.GetValues(typeof(CharacterBattleActionState))
                .Cast<CharacterBattleActionState>().ToList();
            combatAbilityChoices.Remove(CharacterBattleActionState.Idle);
            combatAbilityChoices.Remove(CharacterBattleActionState.Move);
            characterBattleActionState = combatAbilityChoices[Random.Range(0, combatAbilityChoices.Count)];

            int randomNum = 0;
            characterBattleActionState = CharacterBattleActionState.Attack;

            //selectedTargets = new List<CharacterBattleManager>();
            SetSkillCast();
            switch (characterBattleActionState)
            {
                case CharacterBattleActionState.Attack:
                    randomNum = Random.Range(0, CombatManager.Instance.playerUnits.Count);
                    target = CombatManager.Instance.playerUnits[randomNum];
                    //selectedTargets.Add(CombatManager.Instance.playerUnits[randomNum]);
                    SetAttackActionTargetPosition();
                    break;

                case CharacterBattleActionState.Defend:
                    //Defend();

                    battleState = CharacterBattleState.Action;
                    targetPosition = transform.position;
                    break;

                case CharacterBattleActionState.Skill:
                    randomNum = Random.Range(0, CombatManager.Instance.playerUnits.Count);
                    target = CombatManager.Instance.playerUnits[randomNum];
                    //selectedTargets.Add(CombatManager.Instance.playerUnits[randomNum]);

                    SetAttackActionTargetPosition();
                    break;

                case CharacterBattleActionState.Item:
                    randomNum = Random.Range(0, CombatManager.Instance.playerUnits.Count);
                    target = CombatManager.Instance.playerUnits[randomNum];
                    //selectedTargets.Add(CombatManager.Instance.playerUnits[randomNum]);
                    SetAttackActionTargetPosition();
                    break;
                case CharacterBattleActionState.Break:
                    randomNum = Random.Range(0, CombatManager.Instance.playerUnits.Count);
                    target = CombatManager.Instance.playerUnits[randomNum];
                    //selectedTargets.Add(CombatManager.Instance.playerUnits[randomNum]);
                    SetAttackActionTargetPosition();
                    break;
                // case CharacterBattleActionState.Move:
                //     
                //     break;
            }
        }

        public void EndTurnReset()
        {
            DequeueAction();
            characterBattleActionState = CharacterBattleActionState.Idle;

            SetRoamingDirection();
            SetRandomTargetMovePosition();
            repositionTimer = repositionTime;
            battleState = CharacterBattleState.MoveToRandomBattlePosition;
            cooldownTimer = 0;
            prevCharacterBattleActionState = CharacterBattleActionState.Idle;
            characterActionGaugeState = CharacterActionGaugeState.StartTurnPhase;
        }

        public void SetActionRange()
        {
            switch (characterBattleActionState)
            {
                case CharacterBattleActionState.Attack:
                    actionRange = GetComponent<CharacterUnitController>().character.info.attackRange;
                    break;
                case CharacterBattleActionState.Break:
                    actionRange = GetComponent<CharacterUnitController>().character.info.attackRange;
                    break;
                case CharacterBattleActionState.Skill:
                    if (selectedSkill != null)
                    {
                        actionRange = selectedSkill.info.skillRange;
                    }
                    else
                    {
                        actionRange = GetComponent<CharacterUnitController>().character.info.attackRange;
                    }


                    break;
                case CharacterBattleActionState.Defend:
                    actionRange = GetComponent<CharacterUnitController>().character.info.attackRange;
                    break;
                case CharacterBattleActionState.Item:
                    actionRange = GetComponent<CharacterUnitController>().character.info.attackRange;
                    break;
                case CharacterBattleActionState.Move:
                    actionRange = 0;
                    break;
            }
        }

        public void SetSkillCast()
        {
            switch (characterBattleActionState)
            {
                case CharacterBattleActionState.Attack:
                    skillCastTime = SkillCastTime.Short;
                    break;
                case CharacterBattleActionState.Break:
                    skillCastTime = SkillCastTime.Short;
                    break;
                case CharacterBattleActionState.Skill:
                    if (selectedSkill != null)
                    {
                        skillCastTime = selectedSkill.info.skillCastTime;
                        actionRange = selectedSkill.info.skillRange;
                    }
                    else
                    {
                        skillCastTime = SkillCastTime.Short;
                    }


                    break;
                case CharacterBattleActionState.Defend:
                    skillCastTime = SkillCastTime.Instant;
                    break;
                case CharacterBattleActionState.Item:
                    skillCastTime = SkillCastTime.Instant;
                    break;
                case CharacterBattleActionState.Move:
                    skillCastTime = SkillCastTime.Instant;
                    break;
            }
        }

        public void Attack()
        {
            //Debug.Log($"{name}: Attacked"); 
            
            if (target.transform.position.x < GetPosition().x)
            {
                GetComponentInChildren<CharacterUnit>().spriteRenderer.flipX = true;
            } 
            else if (target.transform.position.x > GetPosition().x)
            {
                GetComponentInChildren<CharacterUnit>().spriteRenderer.flipX = false;
            }
            
            characterBattleActionState = CharacterBattleActionState.Idle;
            GetComponent<AnimationManager>().ChangeAnimationState("attack");
            
            //DequeueAction();
        }

        public void HitTarget()
        {
            if (Vector3.Distance(
                    new Vector3(transform.position.x, 0, transform.position.z),
                    new Vector3(target.transform.position.x, 0,
                        target.transform.position.z)) <=
                actionRange)
            {
                //Debug.Log("Target hit");
                
                List<DamageTypes> damageTypes = new List<DamageTypes>();
                damageTypes.Add(DamageTypes.Physical);
                DamageProfile damage = new DamageProfile(this, target.GetComponent<Health>(), damageTypes);
            }
            else
            {
                Debug.Log("Target Missed");
            }
        }

        public void Break()
        {
            
            if (target.transform.position.x < GetPosition().x)
            {
                GetComponentInChildren<CharacterUnit>().spriteRenderer.flipX = true;
            } 
            else if (target.transform.position.x > GetPosition().x)
            {
                GetComponentInChildren<CharacterUnit>().spriteRenderer.flipX = false;
            }
            
            characterBattleActionState = CharacterBattleActionState.Idle;
            GetComponent<AnimationManager>().ChangeAnimationState("attack");
            List<DamageTypes> damageTypes = new List<DamageTypes>();
            damageTypes.Add(DamageTypes.Physical);
            DamageProfile damage =
                new DamageProfile(this, target.GetComponent<Health>(), damageTypes, true);
            //DequeueAction();
        }

        public void EndTurn()
        {
            //Debug.LogError($"{name}: End Turn");
            characterActionGaugeState = CharacterActionGaugeState.EndTurnPhase;
        }

        // public void OnAttackEnd(CharacterBattleManager characterBattleManager)
        // {
        //     if (characterBattleManager == this)
        //     {
        //         //Debug.Log("On Attack end here buddy boy");
        //         GetComponent<AnimationManager>().ChangeAnimationState("idle");
        //         characterBattleActionState = CharacterBattleActionState.Idle;
        //         SetRoamingDirection();
        //         repositionTimer = repositionTime;
        //         battleState = CharacterBattleState.MoveToRandomBattlePosition;
        //
        //         // if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Player))
        //         // {
        //         //     EventManager.Instance.combatEvents.OnCombatPause(false);
        //         // }
        //
        //         //DequeueAction();
        //         cooldownTimer = 0;
        //         characterActionGaugeState = CharacterActionGaugeState.WaitPhase;
        //     }
        // }

        public void UseSkill()
        {
            GetComponent<AnimationManager>().ChangeAnimationState("idle");
            if (selectedSkill != null)
            {
                CameraManager.Instance.SetSkillCharacterFocusVirtualCamera(gameObject);
                // CameraManager.Instance.SetBattleCamera();
                //
                // if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Enemy))
                // {
                //     CombatManager.Instance.selectedSkillPlayerCharacter = this;
                //     characterBattleActionState = CharacterBattleActionState.Idle;
                //     prevCharacterBattleActionState = CharacterBattleActionState.Skill;
                //     selectedSkill.source = this;
                //     
                //     selectedSkill.UseSkill();
                //     return;
                // }

                



                targetList = new List<CharacterBattleManager>();
                targetList.Add(target);
                switch (selectedSkill.info.skillDamageTargetType)
                {
                    
                    
                    
                    case SkillDamageTargetType.Circle:
                        targetList.AddRange(target.GetUnitsInSkillRadius(selectedSkill.info.aoeRadius));
                        targetList.Remove(this);
                        break;
                }
                
                targetList = targetList.Distinct().ToList();
                
                
                CombatManager.Instance.selectedSkillPlayerCharacter = this;
                EventManager.Instance.combatEvents.OnCombatPause(true);
                GetComponent<AnimationManager>().ChangeAnimationState("idle");
                skillRecallTargetPosition = GetPosition();
                
                CombatManager.Instance.MoveCharactersToSkillPositions(targetList);
                Debug.Log("Set Skill Camera");
                
                GetComponent<AnimationManager>().ChangeAnimationState("idle");
                characterBattleActionState = CharacterBattleActionState.Idle;
                prevCharacterBattleActionState = CharacterBattleActionState.Skill;
                EventManager.Instance.combatEvents.OnCombatPause(true);
                CombatManager.Instance.PauseAnimationsForSkills();
                
                
                StartCoroutine(CameraManager.Instance.SkillCharacterFocusTimer(1f, () =>
                {
                    CameraManager.Instance.ToggleSkillCamera();
                    selectedSkill.source = this;
                    selectedSkill.UseSkill();
                }));
                // StartCoroutine(CameraManager.Instance.RotateSkillCamera(() =>
                // {
                //     
                //     
                //     //TODO Placeholder for animations and skills VFX
                //     // List<DamageTypes> damageTypes = new List<DamageTypes>();
                //     // damageTypes.Add(DamageTypes.Fire);
                //     // DamageProfile damage = new DamageProfile(this, target.GetComponent<Health>(), damageTypes);
                //     // CameraManager.Instance.SetBattleCamera();
                //     // CombatManager.Instance.RecallCharacters();
                //     // CombatManager.Instance.selectedSkillPlayerCharacter = null;
                //     // EndTurn();
                // }));
                
            }
        }

        // public void OnSkillEnd(CharacterBattleManager characterBattleManager)
        // {
        //     if (characterBattleManager == this)
        //     {
        //         CameraManager.Instance.ResetCombatFollowTarget();
        //         CameraManager.Instance.ResetTargetGroupSetting();
        //         characterBattleActionState = CharacterBattleActionState.Idle;
        //         SetRoamingDirection();
        //         repositionTimer = repositionTime;
        //         battleState = CharacterBattleState.MoveToRandomBattlePosition;
        //
        //         EventManager.Instance.combatEvents.OnCombatPause(false);
        //         DequeueAction();
        //         cooldownTimer = 0;
        //         characterActionGaugeState = CharacterActionGaugeState.WaitPhase;
        //         transform.rotation = Quaternion.Euler(0, 0, 0);
        //     }
        // }

        public void Defend()
        {
            Stat stat = new Stat(StatAttribute.Defense, 50, StatType.Additive);
            StatModifier statModifier = new StatModifier(stat, 2);
            
            GetComponent<CharacterUnitController>().character.characterStats.tempStatModifiers.Add(statModifier);
            characterActionGaugeState = CharacterActionGaugeState.EndTurnPhase;
        }

        public void MovePos()
        {
            characterActionGaugeState = CharacterActionGaugeState.EndTurnPhase;
        }


        public void UseItem()
        {
            if (selectedItem != null)
            {
                characterBattleActionState = CharacterBattleActionState.Idle;
                //selectedItem.UseCombatItem(target);
                selectedItem.UseItem(ItemUseSectionType.Combat, this);
                EndTurn();
            }
        }

        public void MovePosition()
        {
        }

        public void PerformAction(Action onActionComplete)
        {
            switch (characterBattleActionState)
            {
                case CharacterBattleActionState.Idle:
                    break;
                case CharacterBattleActionState.Attack:
                    prevCharacterBattleActionState = CharacterBattleActionState.Attack;
                    Attack();
                    break;
                case CharacterBattleActionState.Defend:
                    prevCharacterBattleActionState = CharacterBattleActionState.Defend;
                    Defend();
                    break;
                case CharacterBattleActionState.Skill:
                    prevCharacterBattleActionState = CharacterBattleActionState.Skill;
                    UseSkill();
                    break;
                case CharacterBattleActionState.Item:
                    prevCharacterBattleActionState = CharacterBattleActionState.Item;
                    UseItem();
                    break;
                case CharacterBattleActionState.Break:
                    prevCharacterBattleActionState = CharacterBattleActionState.Break;
                    Break();
                    break;
                case CharacterBattleActionState.Move:
                    prevCharacterBattleActionState = CharacterBattleActionState.Move;
                    MovePos();
                    break;
            }

            onActionComplete();
        }

        public void DequeueAction()
        {
            if (CombatManager.Instance.combatQueue.Count <= 0)
            {
                return;
            }

            if (CombatManager.Instance.combatQueue.Peek() == this)
            {
                CombatManager.Instance.combatQueue.Dequeue();
            }
        }


        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void SetRandomTargetMovePosition()
        {
            Vector3 randomDirection = Random.insideUnitSphere * 2f;

            targetMovePosition = transform.position + randomDirection;
        }
        
        
        protected bool IsAgentMoving()
        {
            // Check if the velocity is above a small threshold to account for floating-point precision errors

            if (agent == null)
            {
                return false;
            }

            return agent.DefaultLocomotion.Acceleration == 0;
            //return agent.velocity.sqrMagnitude > 0.01f && !agent.isStopped;
        }
        
        

        public void SetAttackActionTargetPosition()
        {
            //targetPosition = target.battlePosition.combatActionPosition;
            targetPosition = target.transform.position;
            if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Player))
            {
                battleState = CharacterBattleState.WaitingAction;
            }

            //
        }

        public void MoveToPosition(Transform position, Action onMoveComplete, float distance = 1f)
        {
            transform.position += (position.position - GetPosition()) * 2f * Time.deltaTime;

            if (Vector3.Distance(GetPosition(), position.position) <= distance)
            {
                //transform.position = position.position;
                onMoveComplete();
            }
        }


        public Vector3 GetRoamingPosition()
        {
            //return new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f)).normalized;
            return new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        }

        public void SetRoamingDirection()
        {
            moveDir = GetRoamingPosition();
        }

        public void FlipSpriteMoveDirection()
        {
            if (moveDir.x > 0)
            {
                GetComponentInChildren<CharacterUnit>().spriteRenderer.flipX = false;
            } 
            else if (moveDir.x < 0)
            {
                GetComponentInChildren<CharacterUnit>().spriteRenderer.flipX = true;
            }
        }


        public List<CharacterBattleManager> GetUnitsInSkillRadius(float radius)
        {
            List<CharacterBattleManager> characterBattleManagers = new List<CharacterBattleManager>();

            foreach (CharacterBattleManager characterBattleManager in CombatManager.Instance.allUnits)
            {

                float distance = Vector3.Distance(GetPosition(), characterBattleManager.GetPosition());

                if (distance <= radius)
                {

                    // if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Enemy) && characterBattleManager.GetComponent<ComponentTagManager>().HasTag(ComponentTag.Enemy))
                    // {
                    //     characterBattleManagers.Add(characterBattleManager);
                    // }
                    
                    characterBattleManagers.Add(characterBattleManager);
                }
            }


            return characterBattleManagers;

        }

        public void FaceNearestEnemy()
        {
            
            CharacterBattleManager nearestTarget = null;
            float closestDistance = Mathf.Infinity;

            if (TryGetComponent(out ComponentTagManager componentTagManager))
            {
                if (componentTagManager.HasTag(ComponentTag.Player))
                {
                    foreach (CharacterBattleManager characterBattleManager in CombatManager.Instance.enemyUnits)
                    {
                        float currentEnemyDistance = Vector3.Distance(transform.position, characterBattleManager.transform.position);


                        if (currentEnemyDistance < closestDistance)
                        {
                            closestDistance = currentEnemyDistance;

                            nearestTarget = characterBattleManager;
                        }
                        
                    }
                } else if (componentTagManager.HasTag(ComponentTag.Enemy))
                {
                    foreach (CharacterBattleManager characterBattleManager in CombatManager.Instance.playerUnits)
                    {
                        float currentEnemyDistance = Vector3.Distance(transform.position, characterBattleManager.transform.position);


                        if (currentEnemyDistance < closestDistance)
                        {
                            closestDistance = currentEnemyDistance;

                            nearestTarget = characterBattleManager;
                        }
                        
                    }
                }
            }

            if (nearestTarget != null)
            {
                if (nearestTarget.transform.position.x < transform.position.x)
                {
                    GetComponentInChildren<CharacterUnit>().spriteRenderer.flipX = true;
                } 
                else if (nearestTarget.transform.position.x > transform.position.x)
                {
                    GetComponentInChildren<CharacterUnit>().spriteRenderer.flipX = false;
                }
            }
            
            
        }

        public void GetNewRoamingPosition()
        {
            Vector3 pos = GetRoamingPosition();
            moveDir = pos;
            targetBattlePosition = new Vector3(transform.position.x + pos.x, transform.position.y + pos.y,
                transform.position.z + pos.z);
        }

        public IEnumerator Rotate(Action onComplete)
        {
            CameraManager.Instance.combatVirtualCamera.Follow = transform;
            
            Vector3 targetDirection =
                target.transform.position - transform.position;
            
            float speed = 3f;
            float singleStep = speed * Time.deltaTime;
            
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            
            while (Vector3.Angle(newDirection, targetDirection) >= 0.01f)
            {
                singleStep = speed * Time.deltaTime;
                newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
                yield return null;
            }
            //yield return null;
            onComplete();
        }
    }
}