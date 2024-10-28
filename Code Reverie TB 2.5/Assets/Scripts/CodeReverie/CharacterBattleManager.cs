using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace CodeReverie
{
    public class CharacterBattleManager : SerializedMonoBehaviour
    {
        //private Rigidbody rb;
        public CharacterController characterController;
        public CharacterBattleState battleState;
        public CharacterBattleActionState characterBattleActionState;
        public CharacterBattleActionState prevCharacterBattleActionState;
        public CharacterTimelineGaugeState characterTimelineGaugeState;
        public BattlePosition battlePosition;
        public CharacterBattleManager target;
        //public List<CharacterBattleManager> selectedTargets;
        public Vector3 targetPosition;
        public Vector3 targetMovePosition;

        public Vector2 targetBattlePosition;

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
        public NavMeshAgent agent;

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
            
            agent = GetComponent<NavMeshAgent>() != null ? GetComponent<NavMeshAgent>() : null;
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
                    //     characterTimelineGaugeState = CharacterTimelineGaugeState.StartTurnPhase;
                    // }


                    if (agent != null)
                    {
                        if (!agent.hasPath)
                        {
                            SetRandomTargetMovePosition();
                            agent.SetDestination(targetMovePosition);
                        }
                        else
                        {
                            if (!IsAgentMoving())
                            {
                                agent.SetDestination(GetPosition());
                                Debug.Log("Agent has stopped moving");
                                FaceNearestEnemy();
                                
                                //
                                // if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Enemy))
                                // {
                                //     AutoTurnPicks();
                                // }
                                
                                battleState = CharacterBattleState.Waiting;
                                characterTimelineGaugeState = CharacterTimelineGaugeState.StartTurnPhase;
                            }
                        }
                    }
                    
                    
                }
                //else if (CombatManager.Instance.combatManagerState == CombatManagerState.Battle && !CombatManager.Instance.pause)
                else if (CombatManager.Instance.combatManagerState == CombatManagerState.Battle)
                {
                    if (GetComponent<CharacterUnitController>().character.characterState == CharacterState.Alive)
                    {
                        switch (characterTimelineGaugeState)
                        {
                            case CharacterTimelineGaugeState.StartTurnPhase:

                                GetComponent<CharacterUnitController>().character.AddResonancePoints();
                                SetRandomTargetMovePosition();
                                characterTimelineGaugeState = CharacterTimelineGaugeState.WaitPhase;
                                
                                if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Enemy))
                                {
                                    AutoTurnPicks();
                                }

                                break;
                            case CharacterTimelineGaugeState.WaitPhase:
                                //skillCastTime = SkillCastTime.None;

                                if (!CombatManager.Instance.pause)
                                {
                                    cooldownTimer += Time.deltaTime *
                                                     (1 + GetComponent<CharacterStatsManager>()
                                                         .GetStat(StatAttribute.Haste));


                                    if (cooldownTimer >= actionPhaseCooldown * .8f)
                                    {
                                        if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Player))
                                        {
                                            EventManager.Instance.combatEvents.OnPlayerTurn(this);
                                        }
                                        else
                                        {
                                            battleState = CharacterBattleState.WaitingAction;
                                        }

                                        cooldownTimer = actionPhaseCooldown * .8f;
                                        characterTimelineGaugeState = CharacterTimelineGaugeState.CommandPhase;
                                    }

                                    switch (battleState)
                                    {
                                        case CharacterBattleState.Waiting:
                                            FaceNearestEnemy();
                                            GetComponent<AnimationManager>().ChangeAnimationState("idle");
                                            break;

                                        case CharacterBattleState.MoveToStartingBattlePosition:
                                            //TODO Happens in the PRE-BATTLE
                                            Debug.Log("Move to starting position. Need to update!!!!!!!!");
                                            
                                            

                                            // repositionTimer -= Time.deltaTime;
                                            //
                                            // //rb.MovePosition(rb.position + moveDir * (15f * Time.fixedDeltaTime));
                                            // //characterController.Move(moveDir * (4f * Time.fixedDeltaTime));
                                            // characterController.Move(moveDir * (1f * Time.fixedDeltaTime));
                                            // FlipSpriteMoveDirection();
                                            //
                                            // StopMovementOnReachingMaxDistance();
                                            //
                                            // if (repositionTimer <= 0)
                                            // {
                                            //     //FaceNearestEnemy();
                                            //     
                                            //     if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Enemy))
                                            //     {
                                            //         AutoTurnPicks();
                                            //     }
                                            //
                                            //     battleState = CharacterBattleState.Waiting;
                                            // }

                                            break;
                                        case CharacterBattleState.MoveToRandomBattlePosition:
                                            GetComponent<AnimationManager>().ChangeAnimationState("run");
                                            // repositionTimer -= Time.deltaTime;
                                            //
                                            // //rb.MovePosition(rb.position + moveDir * (5f * Time.fixedDeltaTime));
                                            //
                                            // //characterController.Move(moveDir * (4f * Time.fixedDeltaTime));
                                            // characterController.Move(moveDir * (1f * Time.fixedDeltaTime));
                                            // FlipSpriteMoveDirection();
                                            // StopMovementOnReachingMaxDistance();
                                            // if (repositionTimer <= 0)
                                            // {
                                            //     
                                            //     //FaceNearestEnemy();
                                            //     
                                            //     // if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Enemy))
                                            //     // {
                                            //     //     AutoTurnPicks();
                                            //     // }
                                            //
                                            //     battleState = CharacterBattleState.Waiting;
                                            // }
                                            
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
                            case CharacterTimelineGaugeState.CommandPhase:

                                if (!CombatManager.Instance.pause)
                                {
                                    switch (battleState)
                                    {
                                        case CharacterBattleState.WaitingAction:
                                            if (!CombatManager.Instance.pause)
                                            {
                                                switch (skillCastTime)
                                                {
                                                    case SkillCastTime.Instant:
                                                        //cooldownTimer = actionPhaseCooldown;
                                                        cooldownTimer += Time.deltaTime;
                                                        
                                                        break;
                                                    case SkillCastTime.Short:
                                                        //cooldownTimer += Time.deltaTime * (1 + GetComponent<CharacterStatsManager>().GetStat(StatAttribute.Haste)); 
                                                        cooldownTimer += Time.deltaTime;
                                                        break;
                                                    case SkillCastTime.Medium:
                                                        cooldownTimer += Time.deltaTime * (1f / 3f);
                                                        break;
                                                    case SkillCastTime.Long:
                                                        cooldownTimer += Time.deltaTime * (1f / 4f);
                                                        break;
                                                }
                                            }

                                            if (cooldownTimer >= actionPhaseCooldown)
                                            {
                                                switch (characterBattleActionState)
                                                {
                                                    case CharacterBattleActionState.Attack:
                                                    case CharacterBattleActionState.Defend:
                                                    case CharacterBattleActionState.Item:
                                                        break;

                                                    case CharacterBattleActionState.Skill:
                                                        CombatManager.Instance.combatQueue.Enqueue(this);
                                                        break;
                                                }

                                                skillCastTime = SkillCastTime.None;

                                                cooldownTimer = actionPhaseCooldown;

                                                battleState = CharacterBattleState.WaitingQueue;
                                                characterTimelineGaugeState = CharacterTimelineGaugeState.ActionPhase;
                                            }

                                            break;
                                        case CharacterBattleState.Interrupted:
                                            cooldownTimer /= 2;
                                            battleState = CharacterBattleState.Waiting;
                                            characterTimelineGaugeState = CharacterTimelineGaugeState.WaitPhase;

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
                            case CharacterTimelineGaugeState.ActionPhase:
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
                                                    //Debug.Log($"{name}: Change to MoveToCombatActionPosition");
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
                                                // default:
                                                //     battleState = CharacterBattleState.MoveToCombatActionPosition;
                                                //     break;
                                            }

                                            break;

                                        case CharacterBattleState.MoveToCombatActionPosition:

                                            Vector3 direction = targetPosition - GetPosition();

                                            //Debug.Log($"{name}: In MoveToCombatActionPosition");

                                            switch (characterBattleActionState)
                                            {
                                                case CharacterBattleActionState.Defend:
                                                    battleState = CharacterBattleState.Action;
                                                    break;
                                                case CharacterBattleActionState.Move:
                                                    GetComponent<AnimationManager>().ChangeAnimationState("run");

                                                    // transform.position = Vector3.MoveTowards(GetPosition(),
                                                    //     targetPosition,
                                                    //     4f * Time.fixedDeltaTime);
                                                    //
                                                    // if (targetPosition.x < GetPosition().x)
                                                    // {
                                                    //     GetComponentInChildren<CharacterUnit>().spriteRenderer.flipX = true;
                                                    // } 
                                                    // else if (targetPosition.x > GetPosition().x)
                                                    // {
                                                    //     GetComponentInChildren<CharacterUnit>().spriteRenderer.flipX = false;
                                                    // }
                                                    //
                                                    //
                                                    // if (Vector3.Distance(
                                                    //         new Vector3(transform.position.x, 0, transform.position.z),
                                                    //         new Vector3(targetPosition.x, 0, targetPosition.z)) <=
                                                    //     0.001f)
                                                    // {
                                                    //     GetComponent<AnimationManager>().ChangeAnimationState("idle");
                                                    //     battleState = CharacterBattleState.Action;
                                                    // }

                                                    bool distance = Vector3.Distance(
                                                                        new Vector3(transform.position.x, 0,
                                                                            transform.position.z),
                                                                        new Vector3(targetPosition.x, 0,
                                                                            targetPosition.z)) <=
                                                                    0.001f;
                                                    
                                                    agent.SetDestination(targetPosition);
                                            
                                                    //if (!IsAgentMoving() || distance)
                                                    if (distance)
                                                    {
                                                        GetComponent<AnimationManager>().ChangeAnimationState("idle");
                                                        battleState = CharacterBattleState.Action;
                                                    }
                                                    
                                                    

                                                    break;

                                                default:
                                                    //Debug.Log($"{name}: In MoveToCombatActionPosition Default");
                                                    GetComponent<AnimationManager>().ChangeAnimationState("run");


                                                    // //rb.MovePosition(GetPosition() + direction * 2f * Time.fixedDeltaTime);
                                                    // transform.position = Vector3.MoveTowards(GetPosition(),
                                                    //     target.transform.position,
                                                    //     4f * Time.fixedDeltaTime);
                                                    //
                                                    // if (target.transform.position.x < GetPosition().x)
                                                    // {
                                                    //     GetComponentInChildren<CharacterUnit>().spriteRenderer.flipX = true;
                                                    // } 
                                                    // else if (target.transform.position.x > GetPosition().x)
                                                    // {
                                                    //     GetComponentInChildren<CharacterUnit>().spriteRenderer.flipX = false;
                                                    // }
                                                    //
                                                    //
                                                    // if (Vector3.Distance(
                                                    //         new Vector3(transform.position.x, 0, transform.position.z),
                                                    //         new Vector3(target.transform.position.x, 0,
                                                    //             target.transform.position.z)) <=
                                                    //     actionRange)
                                                    // {
                                                    //     GetComponent<AnimationManager>().ChangeAnimationState("idle");
                                                    //     battleState = CharacterBattleState.Action;
                                                    // }
                                                    
                                                    //agent.SetDestination(targetPosition);
                                                    agent.SetDestination(target.transform.position);
                                            
                                                    // if (!IsAgentMoving())
                                                    // {
                                                    //     GetComponent<AnimationManager>().ChangeAnimationState("idle");
                                                    //     battleState = CharacterBattleState.Action;
                                                    // }
                                                    
                                                    if (Vector3.Distance(
                                                            new Vector3(transform.position.x, 0, transform.position.z),
                                                            new Vector3(target.transform.position.x, 0, target.transform.position.z)) <=
                                                        actionRange)
                                                    {
                                                        agent.SetDestination(transform.position);
                                                        GetComponent<AnimationManager>().ChangeAnimationState("idle");
                                                        battleState = CharacterBattleState.Action;
                                                    }

                                                    break;
                                            }

                                            break;


                                        case CharacterBattleState.Action:
                                            PerformAction(() =>
                                            {
                                                // switch (characterBattleActionState)
                                                // {
                                                //     case CharacterBattleActionState.Attack:
                                                //     case CharacterBattleActionState.Skill:
                                                //     case CharacterBattleActionState.Item:
                                                //         characterBattleActionState = CharacterBattleActionState.Idle;
                                                //         SetRoamingDirection();
                                                //         repositionTimer = repositionTime;
                                                //
                                                //         
                                                //         battleState = CharacterBattleState.MoveToRandomBattlePosition;
                                                //         break;
                                                //
                                                //     case CharacterBattleActionState.Defend:
                                                //         battleState = CharacterBattleState.CompleteAction;
                                                //         break;
                                                // }
                                                //
                                                // EventManager.Instance.combatEvents.OnCombatPause(false);
                                                // if (CombatManager.Instance.combatQueue.Peek() == this)
                                                // {
                                                //     CombatManager.Instance.combatQueue.Dequeue();
                                                // }
                                                // cooldownTimer = 0;
                                                // characterTimelineGaugeState = CharacterTimelineGaugeState.WaitPhase;
                                            });

                                            break;
                                    }
                                }

                                break;
                            case CharacterTimelineGaugeState.EndTurnPhase:
                                GetComponent<AnimationManager>().ChangeAnimationState("idle");
                                switch (prevCharacterBattleActionState)
                                {
                                    case CharacterBattleActionState.Skill:
                                        CameraManager.Instance.ResetCombatFollowTarget();
                                        CameraManager.Instance.ResetTargetGroupSetting();
                                        characterBattleActionState = CharacterBattleActionState.Idle;
                                        SetRoamingDirection();
                                        SetRandomTargetMovePosition();
                                        repositionTimer = repositionTime;
                                        battleState = CharacterBattleState.MoveToRandomBattlePosition;

                                        EventManager.Instance.combatEvents.OnCombatPause(false);
                                        Debug.Log("This shopuld change to false");
                                        DequeueAction();
                                        cooldownTimer = 0;
                                        transform.rotation = Quaternion.Euler(0, 0, 0);
                                        prevCharacterBattleActionState = CharacterBattleActionState.Idle;
                                        characterTimelineGaugeState = CharacterTimelineGaugeState.StartTurnPhase;
                                        
                                        break;
                                    default:

                                        characterBattleActionState = CharacterBattleActionState.Idle;

                                        SetRoamingDirection();
                                        SetRandomTargetMovePosition();
                                        repositionTimer = repositionTime;
                                        battleState = CharacterBattleState.MoveToRandomBattlePosition;
                                        cooldownTimer = 0;
                                        prevCharacterBattleActionState = CharacterBattleActionState.Idle;
                                        characterTimelineGaugeState = CharacterTimelineGaugeState.StartTurnPhase;
                                        break;
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

        private void GetDistanceFromBattleAreaCenter()
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

        private void FixedUpdate()
        {
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
                    skillCastTime = SkillCastTime.Instant;
                    break;
                case CharacterBattleActionState.Break:
                    skillCastTime = SkillCastTime.Instant;
                    break;
                case CharacterBattleActionState.Skill:
                    if (selectedSkill != null)
                    {
                        skillCastTime = selectedSkill.info.skillCastTime;
                        actionRange = selectedSkill.info.skillRange;
                    }
                    else
                    {
                        skillCastTime = SkillCastTime.Instant;
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
            
            DequeueAction();
        }

        public void HitTarget()
        {
            if (Vector3.Distance(
                    new Vector3(transform.position.x, 0, transform.position.z),
                    new Vector3(target.transform.position.x, 0,
                        target.transform.position.z)) <=
                actionRange)
            {
                Debug.Log("Target hit");
                
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
            DequeueAction();
        }

        public void EndTurn()
        {
            Debug.Log($"{name}: End Turn");
            characterTimelineGaugeState = CharacterTimelineGaugeState.EndTurnPhase;
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
        //         characterTimelineGaugeState = CharacterTimelineGaugeState.WaitPhase;
        //     }
        // }

        public void UseSkill()
        {
            if (selectedSkill != null)
            {
                GetComponent<AnimationManager>().ChangeAnimationState("idle");
                characterBattleActionState = CharacterBattleActionState.Idle;
                prevCharacterBattleActionState = CharacterBattleActionState.Skill;
                EventManager.Instance.combatEvents.OnCombatPause(true);
                CombatManager.Instance.PauseAnimationsForSkills();
                selectedSkill.source = this;
                selectedSkill.UseSkill();
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
        //         characterTimelineGaugeState = CharacterTimelineGaugeState.WaitPhase;
        //         transform.rotation = Quaternion.Euler(0, 0, 0);
        //     }
        // }

        public void Defend()
        {
            characterTimelineGaugeState = CharacterTimelineGaugeState.EndTurnPhase;
        }

        public void MovePos()
        {
            characterTimelineGaugeState = CharacterTimelineGaugeState.EndTurnPhase;
        }


        public void UseItem()
        {
            if (selectedItem != null)
            {
                characterBattleActionState = CharacterBattleActionState.Idle;
                //selectedItem.UseCombatItem(target);
                selectedItem.UseItem(ItemUseSectionType.Combat, this);
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
        
        
        bool IsAgentMoving()
        {
            // Check if the velocity is above a small threshold to account for floating-point precision errors

            if (agent == null)
            {
                return false;
            }
            
            return agent.velocity.sqrMagnitude > 0.01f && !agent.isStopped;
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