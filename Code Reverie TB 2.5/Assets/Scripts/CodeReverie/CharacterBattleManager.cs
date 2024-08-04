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
        private Rigidbody rb;
        public CharacterBattleState battleState;
        public CharacterBattleActionState characterBattleActionState;
        public CharacterTimelineGaugeState characterTimelineGaugeState;
        public BattlePosition battlePosition;
        public List<CharacterBattleManager> selectedTargets;
        public Transform targetPosition;
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


        private void Awake()
        {
            if (namePanel != null)
            {
                namePanel.SetActive(false);
            }

            rb = GetComponent<Rigidbody>();
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
            actionRange = GetComponent<CharacterController>().character.info.attackRange;
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
            if (BattleManager.Instance.pause)
            {
                if (inCombat && GetComponent<CharacterController>().character.characterState == CharacterState.Alive)
                {
                    switch (characterTimelineGaugeState)
                    {
                        case CharacterTimelineGaugeState.WaitPhase:
                            skillCastTime = SkillCastTime.None;
                            

                            switch (battleState)
                            {
                                case CharacterBattleState.Waiting:
                                    GetComponent<AnimationManager>().ChangeAnimationState("idle");
                                    break;

                                case CharacterBattleState.MoveToStartingBattlePosition:

                                    repositionTimer -= Time.deltaTime;

                                    rb.MovePosition(rb.position + moveDir * (15f * Time.fixedDeltaTime));

                                    if (repositionTimer <= 0)
                                    {
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
                if (inCombat && GetComponent<CharacterController>().character.characterState == CharacterState.Alive)
                {
                    switch (characterTimelineGaugeState)
                    {
                        case CharacterTimelineGaugeState.WaitPhase:
                            skillCastTime = SkillCastTime.None;

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
                                    AutoTurnPicks();
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

                                    rb.MovePosition(rb.position + moveDir * (15f * Time.fixedDeltaTime));

                                    if (repositionTimer <= 0)
                                    {
                                        battleState = CharacterBattleState.Waiting;
                                    }

                                    break;
                                case CharacterBattleState.MoveToRandomBattlePosition:
                                    GetComponent<AnimationManager>().ChangeAnimationState("run");
                                    repositionTimer -= Time.deltaTime;

                                    rb.MovePosition(rb.position + moveDir * (5f * Time.fixedDeltaTime));

                                    if (repositionTimer <= 0)
                                    {
                                        battleState = CharacterBattleState.Waiting;
                                    }

                                    break;
                            }


                            break;
                        case CharacterTimelineGaugeState.CommandPhase:

                            if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Player))
                            {
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
                                            
                                            
                                            cooldownTimer = actionPhaseCooldown;
                                            
                                            battleState = CharacterBattleState.WaitingQueue;
                                            characterTimelineGaugeState = CharacterTimelineGaugeState.ActionPhase;
                                        }

                                        break;
                                    case CharacterBattleState.Interrupted:
                                        cooldownTimer /= 2;
                                        battleState = CharacterBattleState.Waiting;
                                        characterTimelineGaugeState = CharacterTimelineGaugeState.WaitPhase;
                                        break;
                                }
                            }
                            else
                            {
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
                                    //AutoTurnPicks();
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
                                    
                                    
                                    cooldownTimer = actionPhaseCooldown;
                                    battleState = CharacterBattleState.WaitingQueue;
                                    characterTimelineGaugeState = CharacterTimelineGaugeState.ActionPhase;
                                }
                            }

                            break;
                        case CharacterTimelineGaugeState.ActionPhase:

                            switch (battleState)
                            {
                                case CharacterBattleState.WaitingQueue:
                                    
                                    if (characterBattleActionState == CharacterBattleActionState.Attack)
                                    {
                                        battleState = CharacterBattleState.MoveToCombatActionPosition;
                                    }
                                    else
                                    {
                                        if (BattleManager.Instance.combatQueue.Peek() == this)
                                        {
                                            //EventManager.Instance.combatEvents.OnCombatPause(true);
                                            battleState = CharacterBattleState.MoveToCombatActionPosition;
                                        }  
                                    }
                                    
                                    

                                    break;

                                case CharacterBattleState.MoveToCombatActionPosition:

                                    if (characterBattleActionState == CharacterBattleActionState.Defend)
                                    {
                                        battleState = CharacterBattleState.Action;
                                    }
                                    else
                                    {
                                        // GetComponent<AnimationManager>().ChangeAnimationState("run");
                                        // MoveToPosition(targetPosition, () =>
                                        // {
                                        //     battleState = CharacterBattleState.Action;
                                        //
                                        // }, actionRange);


                                        GetComponent<AnimationManager>().ChangeAnimationState("run");

                                        Vector3 direction = targetPosition.position - transform.position;

                                        rb.MovePosition(rb.position + direction * (2f * Time.fixedDeltaTime));

                                        if (Vector3.Distance(GetPosition(), targetPosition.position) <= actionRange)
                                        {
                                            battleState = CharacterBattleState.Action;
                                        }
                                    }


                                    break;


                                case CharacterBattleState.Action:
                                    PerformAction(() =>
                                    {
                                        
                                        
                                        

                                        // switch (characterBattleActionState)
                                        // {
                                        //     case CharacterBattleActionState.Skill:
                                        //         CameraManager.Instance.combatVirtualCamera.m_Follow = transform;
                                        //
                                        //         Vector3 targetDirection =
                                        //             selectedTargets[0].transform.position - transform.position;
                                        //
                                        //         float speed = 1f;
                                        //         float singleStep = speed * Time.deltaTime;
                                        //
                                        //         Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
                                        //
                                        //
                                        //
                                        //         //transform.rotation = Quaternion.LookRotation(newDirection) * Quaternion.Euler(new Vector3(0,15,0));
                                        //         transform.rotation = Quaternion.LookRotation(newDirection);
                                        //
                                        //         if (Vector3.Angle(newDirection, targetDirection) < 0.0001f)
                                        //         {
                                        //             characterBattleActionState = CharacterBattleActionState.Idle;
                                        //         }
                                        //         break;
                                        // }
                                        
                                        
                                        
                                        
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
                    targetPosition = transform;
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
                    actionRange = GetComponent<CharacterController>().character.info.attackRange;
                    break;
                case CharacterBattleActionState.Skill:
                    if (selectedSkill != null)
                    {
                        actionRange = selectedSkill.info.skillRange;
                    }
                    else
                    {
                        actionRange = GetComponent<CharacterController>().character.info.attackRange;
                    }


                    break;
                case CharacterBattleActionState.Defend:
                    actionRange = GetComponent<CharacterController>().character.info.attackRange;
                    break;
                case CharacterBattleActionState.Item:
                    actionRange = GetComponent<CharacterController>().character.info.attackRange;
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
                // Debug.Log("use Skill");
                EventManager.Instance.combatEvents.OnCombatPause(true);
                selectedSkill.source = this;
                selectedSkill.UseSkill();
                //StartCoroutine(Rotate());
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
            targetPosition = selectedTargets[0].transform;
            battleState = CharacterBattleState.WaitingAction;
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

        // public void OldCharacterActionSwitch()
        // {
        //     switch (battleState)
        //         {
        //             case CharacterBattleState.MoveToStartingBattlePosition:
        //                 MoveToPosition(battlePosition.transform, () =>
        //                 {
        //                     
        //                     if (BattleManager.Instance.battleManagerState == BattleManagerState.Battle)
        //                     {
        //                         battleState = CharacterBattleState.CompleteAction;
        //                         EventManager.Instance.combatEvents.OnCombatPause(false);
        //                         if (BattleManager.Instance.combatQueue.Peek() == this)
        //                         {
        //                             BattleManager.Instance.combatQueue.Dequeue();
        //                         }
        //                     } 
        //                     else if (BattleManager.Instance.battleManagerState == BattleManagerState.PreBattle || BattleManager.Instance.battleManagerState == BattleManagerState.Initiate)
        //                     {
        //                         battleState = CharacterBattleState.Waiting;
        //                     }
        //                 });
        //                 
        //                 break;
        //             case CharacterBattleState.MoveToRandomBattlePosition:
        //                 // transform.position = Vector2.Lerp(transform.position, targetBattlePosition, 5f * Time.deltaTime);
        //                 // if (Vector3.Distance(transform.position, targetBattlePosition) <= 0.01f)
        //                 // {
        //                 //     //battleState = CharacterBattleState.Waiting;
        //                 //     battleState = CharacterBattleState.CompleteAction;
        //                 // }
        //
        //                 repositionTimer -= Time.deltaTime;
        //                 
        //                 rb.MovePosition(rb.position + moveDir * (5f * Time.fixedDeltaTime));
        //                 
        //                 if (repositionTimer <= 0)
        //                 {
        //                     battleState = CharacterBattleState.CompleteAction;
        //                 }
        //                 
        //                 break;
        //             case CharacterBattleState.Waiting:
        //                 
        //                 
        //                 break;
        //             case CharacterBattleState.Command:
        //                 battleState = CharacterBattleState.SelectingCommand;
        //                 selectedTargets = new List<CharacterBattleManager>();
        //
        //                 if (TryGetComponent(out ComponentTagManager componentTagManager))
        //                 {
        //                     if (componentTagManager.HasTag(ComponentTag.Player))
        //                     {
        //                         EventManager.Instance.combatEvents.OnPlayerTurn(this);
        //                         //EventManager.Instance.combatEvents.OnCombatPause(true);
        //                     }
        //                     else
        //                     {
        //                         AutoTurnPicks();
        //                     }
        //                 }
        //                 
        //                 
        //                 break;
        //             case CharacterBattleState.WaitingAction:
        //                 GetComponent<AnimationManager>().ChangeAnimationState("Idle");
        //                 break;
        //             case CharacterBattleState.Action:
        //                 
        //                 PerformAction(() =>
        //                 {
        //                     
        //                     switch (characterBattleActionState)
        //                     {
        //                         case CharacterBattleActionState.Attack:
        //                         case CharacterBattleActionState.Skill:
        //                         case CharacterBattleActionState.Item:
        //                             characterBattleActionState = CharacterBattleActionState.Idle;
        //                             SetRoamingDirection();
        //                             repositionTimer = repositionTime;
        //                     
        //                             EventManager.Instance.combatEvents.OnCombatPause(false);
        //                             if (BattleManager.Instance.combatQueue.Peek() == this)
        //                             {
        //                                 BattleManager.Instance.combatQueue.Dequeue();
        //                             }
        //                             battleState = CharacterBattleState.MoveToRandomBattlePosition;
        //                             break;
        //         
        //                         case CharacterBattleActionState.Defend:
        //                             battleState = CharacterBattleState.CompleteAction;
        //                             break;
        //                     }
        //                     
        //                     
        //                     
        //                     
        //                 });
        //                 
        //                 break;
        //             case CharacterBattleState.CompleteAction:
        //                 break;
        //             case CharacterBattleState.MoveToCombatActionPosition:
        //                 MoveToPosition(targetPosition, () =>
        //                 {
        //                     battleState = CharacterBattleState.Action;
        //                     
        //                 });
        //                 break;
        //             case CharacterBattleState.WaitingQueue:
        //
        //                 if (BattleManager.Instance.combatQueue.Peek() == this)
        //                 {
        //                     EventManager.Instance.combatEvents.OnCombatPause(true);
        //                     battleState = CharacterBattleState.MoveToCombatActionPosition;
        //                 }
        //                 break;
        //         }
        // }
    }
}