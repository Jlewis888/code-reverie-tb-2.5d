using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeReverie
{
    public class CharacterBattleManager : SerializedMonoBehaviour
    {
        //private Rigidbody rb;
        public CharacterController characterController;
        public CharacterBattleState battleState;
        public CharacterBattleActionState characterBattleActionState;
        public CharacterTimelineGaugeState characterTimelineGaugeState;
        public BattlePosition battlePosition;
        public List<CharacterBattleManager> selectedTargets;
        public Vector3 targetPosition;
        public Vector2 targetBattlePosition;
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
        public int actionPointsMax;
        public int currentActionPoints;
        public int skillPointsMax;
        public int currentSkillPoints;
        public float actionRange;
        public SkillCastTime skillCastTime;
        public GameObject castObjectHolder;
        public float distanceFromCenter;


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
        }

        private void Start()
        {
            actionRange = GetComponent<CharacterUnitController>().character.info.attackRange;
            EventManager.Instance.combatEvents.onAttackEnd += OnAttackEnd;
            EventManager.Instance.combatEvents.onSkillComplete += OnSkillEnd;
        }

        private void OnEnable()
        {
            EventManager.Instance.combatEvents.onAttackEnd += OnAttackEnd;
            EventManager.Instance.combatEvents.onSkillComplete += OnSkillEnd;
        }

        private void OnDisable()
        {
            EventManager.Instance.combatEvents.onAttackEnd -= OnAttackEnd;
            EventManager.Instance.combatEvents.onSkillComplete -= OnAttackEnd;
        }


        private void Update()
        {

            if (BattleManager.Instance.currentBattleArea != null)
            {
                
            }
            
            
            if (BattleManager.Instance.pause)
            {
                if (inCombat && GetComponent<CharacterUnitController>().character.characterState == CharacterState.Alive)
                {
                    switch (characterTimelineGaugeState)
                    {
                        case CharacterTimelineGaugeState.WaitPhase:
                            //skillCastTime = SkillCastTime.None;
                            

                            switch (battleState)
                            {
                                case CharacterBattleState.Waiting:
                                    GetComponent<AnimationManager>().ChangeAnimationState("idle");
                                    break;

                                case CharacterBattleState.MoveToStartingBattlePosition:
                                    
                                    repositionTimer -= Time.deltaTime;

                                    //rb.MovePosition(rb.position + moveDir * (15f * Time.fixedDeltaTime));
                                    characterController.Move( moveDir * (8f * Time.fixedDeltaTime));

                                    StopMovementOnReachingMaxDistance();

                                    if (repositionTimer <= 0)
                                    {
                                        if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Enemy))
                                        {
                                            AutoTurnPicks();
                                        }
                                        battleState = CharacterBattleState.Waiting;
                                    }

                                    break;
                            }


                            break;
                    }
                }

                return;
            }
            else
            {
                if (inCombat && GetComponent<CharacterUnitController>().character.characterState == CharacterState.Alive)
                {
                    switch (characterTimelineGaugeState)
                    {
                        case CharacterTimelineGaugeState.WaitPhase:
                            //skillCastTime = SkillCastTime.None;

                            if (!BattleManager.Instance.pause)
                            {
                                cooldownTimer += Time.deltaTime *
                                                 (1 + GetComponent<CharacterStatsManager>()
                                                     .GetStat(StatAttribute.Haste));
                            }

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
                                    GetComponent<AnimationManager>().ChangeAnimationState("idle");
                                    break;

                                case CharacterBattleState.MoveToStartingBattlePosition:

                                    repositionTimer -= Time.deltaTime;

                                    //rb.MovePosition(rb.position + moveDir * (15f * Time.fixedDeltaTime));
                                    characterController.Move( moveDir * (8f * Time.fixedDeltaTime));
                                    
                                    StopMovementOnReachingMaxDistance();

                                    if (repositionTimer <= 0)
                                    {
                                        if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Enemy))
                                        {
                                            AutoTurnPicks();
                                        }
                                        
                                        battleState = CharacterBattleState.Waiting;
                                        
                                        
                                    }

                                    break;
                                case CharacterBattleState.MoveToRandomBattlePosition:
                                    GetComponent<AnimationManager>().ChangeAnimationState("run");
                                    repositionTimer -= Time.deltaTime;

                                    //rb.MovePosition(rb.position + moveDir * (5f * Time.fixedDeltaTime));

                                    characterController.Move( moveDir * (4f * Time.fixedDeltaTime));
                                    StopMovementOnReachingMaxDistance();
                                    if (repositionTimer <= 0)
                                    {
                                        if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Enemy))
                                        {
                                            AutoTurnPicks();
                                        }
                                        battleState = CharacterBattleState.Waiting;
                                    }

                                    break;
                            }


                            break;
                        case CharacterTimelineGaugeState.CommandPhase:

                            switch (battleState)
                                {
                                    case CharacterBattleState.WaitingAction:
                                        if (!BattleManager.Instance.pause)
                                        {
                                            switch (skillCastTime)
                                            {
                                                case SkillCastTime.Instant:
                                                    cooldownTimer = actionPhaseCooldown;
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
                                                    break;
                                
                                                case CharacterBattleActionState.Skill:
                                                case CharacterBattleActionState.Item:
                                                    BattleManager.Instance.combatQueue.Enqueue(this);
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
                            
                            break;
                        case CharacterTimelineGaugeState.ActionPhase:

                            switch (battleState)
                            {
                                case CharacterBattleState.WaitingQueue:

                                    switch (characterBattleActionState)
                                    {
                                        case CharacterBattleActionState.Attack:
                                        case CharacterBattleActionState.Break:
                                        case CharacterBattleActionState.Move:
                                            battleState = CharacterBattleState.MoveToCombatActionPosition;
                                            break;
                                        
                                        default:
                                            if (BattleManager.Instance.combatQueue.Peek() == this)
                                            {
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
                                            GetComponent<AnimationManager>().ChangeAnimationState("run");

                                            transform.position = Vector3.MoveTowards(GetPosition(), targetPosition,
                                                4f * Time.fixedDeltaTime);


                                            if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(targetPosition.x, 0, targetPosition.z)) <= 0.001f)
                                            {
                                                GetComponent<AnimationManager>().ChangeAnimationState("idle");
                                                battleState = CharacterBattleState.Action;
                                            }

                                            break;
                                        
                                        default:
                                            GetComponent<AnimationManager>().ChangeAnimationState("run");

                                            //rb.MovePosition(GetPosition() + direction * 2f * Time.fixedDeltaTime);
                                            transform.position = Vector3.MoveTowards(GetPosition(), selectedTargets[0].transform.position,
                                                4f * Time.fixedDeltaTime);
                                            
                                            
                                            if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(selectedTargets[0].transform.position.x, 0, selectedTargets[0].transform.position.z)) <= actionRange)
                                            {
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
                                        // if (BattleManager.Instance.combatQueue.Peek() == this)
                                        // {
                                        //     BattleManager.Instance.combatQueue.Dequeue();
                                        // }
                                        // cooldownTimer = 0;
                                        // characterTimelineGaugeState = CharacterTimelineGaugeState.WaitPhase;
                                    });

                                    break;
                            }

                            break;
                    }
                }
            }


            
        }

        private void GetDistanceFromBattleAreaCenter()
        {
            distanceFromCenter = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(BattleManager.Instance.currentBattleArea.areaCollider.transform.position.x, 0, BattleManager.Instance.currentBattleArea.areaCollider.transform.position.z));
        }


        public void StopMovementOnReachingMaxDistance()
        {
            GetDistanceFromBattleAreaCenter();
            if (distanceFromCenter > BattleManager.Instance.currentBattleArea.areaRange)
            {
                repositionTimer = 0;
                Vector3 vect =  transform.position - BattleManager.Instance.currentBattleArea.areaCollider.transform.position;
                vect *= BattleManager.Instance.currentBattleArea.areaRange/distanceFromCenter;
                transform.position = BattleManager.Instance.currentBattleArea.areaCollider.transform.position + vect;
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

            selectedTargets = new List<CharacterBattleManager>();
            SetSkillCast();
            switch (characterBattleActionState)
            {
                case CharacterBattleActionState.Attack:
                    randomNum = Random.Range(0, BattleManager.Instance.playerUnits.Count);
                    selectedTargets.Add(BattleManager.Instance.playerUnits[randomNum]);
                    SetAttackActionTargetPosition();
                    break;

                case CharacterBattleActionState.Defend:
                    //Defend();

                    battleState = CharacterBattleState.Action;
                    targetPosition = transform.position;
                    break;

                case CharacterBattleActionState.Skill:
                    randomNum = Random.Range(0, BattleManager.Instance.playerUnits.Count);
                    selectedTargets.Add(BattleManager.Instance.playerUnits[randomNum]);

                    SetAttackActionTargetPosition();
                    break;

                case CharacterBattleActionState.Item:
                    randomNum = Random.Range(0, BattleManager.Instance.playerUnits.Count);
                    selectedTargets.Add(BattleManager.Instance.playerUnits[randomNum]);
                    SetAttackActionTargetPosition();
                    break;
                case CharacterBattleActionState.Break:
                    randomNum = Random.Range(0, BattleManager.Instance.playerUnits.Count);
                    selectedTargets.Add(BattleManager.Instance.playerUnits[randomNum]);
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
            characterBattleActionState = CharacterBattleActionState.Idle;
            GetComponent<AnimationManager>().ChangeAnimationState("attack");
            List<DamageTypes> damageTypes = new List<DamageTypes>();
            damageTypes.Add(DamageTypes.Physical);
            DamageProfile damage = new DamageProfile(this, selectedTargets[0].GetComponent<Health>(), damageTypes);
            DequeueAction();
        }
        
        public void Break()
        {
            characterBattleActionState = CharacterBattleActionState.Idle;
            GetComponent<AnimationManager>().ChangeAnimationState("attack");
            List<DamageTypes> damageTypes = new List<DamageTypes>();
            damageTypes.Add(DamageTypes.Physical);
            DamageProfile damage = new DamageProfile(this, selectedTargets[0].GetComponent<Health>(), damageTypes, true);
            DequeueAction();
        }

        public void OnAttackEnd(CharacterBattleManager characterBattleManager)
        {
            if (characterBattleManager == this)
            {
                //Debug.Log("On Attack end here buddy boy");
                GetComponent<AnimationManager>().ChangeAnimationState("idle");
                characterBattleActionState = CharacterBattleActionState.Idle;
                SetRoamingDirection();
                repositionTimer = repositionTime;
                battleState = CharacterBattleState.MoveToRandomBattlePosition;

                // if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Player))
                // {
                //     EventManager.Instance.combatEvents.OnCombatPause(false);
                // }
                
                //DequeueAction();
                cooldownTimer = 0;
                characterTimelineGaugeState = CharacterTimelineGaugeState.WaitPhase;
                
            }
        }
        
        public void UseSkill()
        {
            if (selectedSkill != null)
            {
                GetComponent<AnimationManager>().ChangeAnimationState("idle");
                characterBattleActionState = CharacterBattleActionState.Idle;
                EventManager.Instance.combatEvents.OnCombatPause(true);
                BattleManager.Instance.PauseAnimationsForSkills();
                selectedSkill.source = this;
                selectedSkill.UseSkill();
            }
        }

        public void OnSkillEnd(CharacterBattleManager characterBattleManager)
        {
            if (characterBattleManager == this)
            {
                CameraManager.Instance.ResetCombatFollowTarget();
                CameraManager.Instance.ResetTargetGroupSetting();
                characterBattleActionState = CharacterBattleActionState.Idle;
                SetRoamingDirection();
                repositionTimer = repositionTime;
                battleState = CharacterBattleState.MoveToRandomBattlePosition;

                EventManager.Instance.combatEvents.OnCombatPause(false);
                DequeueAction();
                cooldownTimer = 0;
                characterTimelineGaugeState = CharacterTimelineGaugeState.WaitPhase;
                transform.rotation = Quaternion.Euler(0,0,0);
            }
        }

        public void Defend()
        {
            characterBattleActionState = CharacterBattleActionState.Idle;
            cooldownTimer = 0;
            characterTimelineGaugeState = CharacterTimelineGaugeState.WaitPhase;

            // BattleManager.Instance.selectedPlayerCharacter.characterBattleActionState =
            //     CharacterBattleActionState.Defend;
            // BattleManager.Instance.selectedPlayerCharacter.battleState = CharacterBattleState.Action;
        }

        public void MovePos()
        {
            characterBattleActionState = CharacterBattleActionState.Idle;
            cooldownTimer = 0;
            characterTimelineGaugeState = CharacterTimelineGaugeState.WaitPhase;
        }


        public void UseItem()
        {
            if (selectedItem != null)
            {
                characterBattleActionState = CharacterBattleActionState.Idle;
                selectedItem.UseCombatItem(selectedTargets[0]);
            }
        }

        public void MovePosition()
        {
        }

        public void PerformAction(Action onActionComplete)
        {
            switch (characterBattleActionState)
            {
                case CharacterBattleActionState.Attack:
                    Attack();
                    break;
                case CharacterBattleActionState.Defend:
                    Defend();
                    break;
                case CharacterBattleActionState.Skill:
                    UseSkill();
                    break;
                case CharacterBattleActionState.Item:
                    UseItem();
                    break;
                case CharacterBattleActionState.Break:
                    Break();
                    break;
                case CharacterBattleActionState.Move:
                    MovePos();
                    break;
            }

            onActionComplete();
        }

        public void DequeueAction()
        {

            if (BattleManager.Instance.combatQueue.Count <= 0)
            {
                return;
            }
            
            if (BattleManager.Instance.combatQueue.Peek() == this)
            {
                BattleManager.Instance.combatQueue.Dequeue();
            }
        }


        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void SetAttackActionTargetPosition()
        {
            //targetPosition = selectedTargets[0].battlePosition.combatActionPosition;
            targetPosition = selectedTargets[0].transform.position;
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

        public IEnumerator ToNewPositionRoutine()
        {
            bool toPositionCheck = true;
            while (toPositionCheck)
            {
                yield return new WaitForSeconds(2f);
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

        public void GetNewRoamingPosition()
        {
            Vector3 pos = GetRoamingPosition();
            moveDir = pos;
            targetBattlePosition = new Vector3(transform.position.x + pos.x, transform.position.y + pos.y, transform.position.z + pos.z);
        }

        public IEnumerator Rotate(Action onComplete)
        {
            CameraManager.Instance.combatVirtualCamera.m_Follow = transform;
                                        
            Vector3 targetDirection =
                selectedTargets[0].transform.position - transform.position;
            
            float speed = 3f;
            float singleStep = speed * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            while (Vector3.Angle(newDirection, targetDirection) >= 0.0001f)
            {
                singleStep = speed * Time.deltaTime;
                newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
                yield return null;
            }

            onComplete();
        }
    }
}