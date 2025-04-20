using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProjectDawn.Navigation.Hybrid;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace CodeReverie
{
    public class PlayerCharacterBattleManager : CharacterBattleManager
    {
        
        private void Start()
        {
            actionRange = GetComponent<CharacterUnitController>().character.info.attackRange;
            // EventManager.Instance.combatEvents.onAttackEnd += OnAttackEnd;
            // EventManager.Instance.combatEvents.onSkillComplete += OnSkillEnd;
        }

        private void Update()
        {
            if (CombatManager.Instance != null)
            {
                if (CombatManager.Instance.combatManagerState == CombatManagerState.PreBattle)
                {
                    if (agent != null)
                    {
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

                                GetComponent<CharacterUnitController>().character.AddResonancePoints();
                                GetComponent<CharacterUnitController>().character.characterStats.UpdateTempStats();
                                SetRandomTargetMovePosition();
                                characterActionGaugeState = CharacterActionGaugeState.WaitPhase;

                                if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Enemy))
                                {
                                    AutoTurnPicks();
                                }

                                break;
                            case CharacterActionGaugeState.WaitPhase:
                                //skillCastTime = SkillCastTime.None;

                                if (!CombatManager.Instance.pause)
                                {
                                    // cooldownTimer += Time.deltaTime * (1 + GetComponent<CharacterUnitController>()
                                    //     .character.characterStats.GetStat(StatAttribute.Haste));


                                    // if (cooldownTimer >= actionPhaseCooldown * .75f)
                                    // {
                                    //     if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Player))
                                    //     {
                                    //         EventManager.Instance.combatEvents.OnPlayerTurn(this);
                                    //     }
                                    //     else
                                    //     {
                                    //         battleState = CharacterBattleState.WaitingAction;
                                    //     }
                                    //
                                    //     cooldownTimer = actionPhaseCooldown * .75f;
                                    //     characterActionGaugeState = CharacterActionGaugeState.CommandPhase;
                                    // }

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
                            case CharacterActionGaugeState.PreCommandPhase:
                                
                                cooldownTimer = actionPhaseCooldown * .75f;
                                
                                if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Player))
                                {
                                    
                                    if (!CombatManager.Instance.playerCharacterTurnQueue.Contains(
                                            this))
                                    {
                                        CombatManager.Instance.playerCharacterTurnQueue.Enqueue(this);
                                    }
                                    
                                   

                                    if (CombatManager.Instance.playerCharacterTurnQueue.Peek() != this)
                                    {
                                        return;
                                    }
                                    
                                    
                                    EventManager.Instance.combatEvents.OnPlayerTurn(this);
                                }
                                else
                                {
                                    battleState = CharacterBattleState.WaitingAction;
                                }

                                
                                characterActionGaugeState = CharacterActionGaugeState.CommandPhase;
                                
                                break;
                            case CharacterActionGaugeState.CommandPhase:

                                if (!CombatManager.Instance.pause)
                                {
                                    switch (battleState)
                                    {
                                        case CharacterBattleState.WaitingAction:
                                            // if (!CombatManager.Instance.pause)
                                            // {
                                            //     switch (skillCastTime)
                                            //     {
                                            //         case SkillCastTime.Instant:
                                            //             //cooldownTimer = actionPhaseCooldown;
                                            //             cooldownTimer += Time.deltaTime * 2f;
                                            //
                                            //             break;
                                            //         case SkillCastTime.Short:
                                            //             //cooldownTimer += Time.deltaTime * (1 + GetComponent<CharacterStatsManager>().GetStat(StatAttribute.Haste)); 
                                            //             cooldownTimer += Time.deltaTime;
                                            //             break;
                                            //         case SkillCastTime.Medium:
                                            //             cooldownTimer += Time.deltaTime * (1f / 3f);
                                            //             break;
                                            //         case SkillCastTime.Long:
                                            //             cooldownTimer += Time.deltaTime * (1f / 4f);
                                            //             break;
                                            //     }
                                            // }

                                            // if (cooldownTimer >= actionPhaseCooldown)
                                            // {
                                            //     switch (characterBattleActionState)
                                            //     {
                                            //         case CharacterBattleActionState.Attack:
                                            //         case CharacterBattleActionState.Defend:
                                            //         case CharacterBattleActionState.Item:
                                            //             break;
                                            //
                                            //         case CharacterBattleActionState.Skill:
                                            //             CombatManager.Instance.combatQueue.Enqueue(this);
                                            //             break;
                                            //     }
                                            //
                                            //     skillCastTime = SkillCastTime.None;
                                            //
                                            //     cooldownTimer = actionPhaseCooldown;
                                            //
                                            //     battleState = CharacterBattleState.WaitingQueue;
                                            //     characterActionGaugeState = CharacterActionGaugeState.ActionPhase;
                                            // }

                                            break;
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

                                                    // if (target == null)
                                                    // {
                                                    //     EndTurn();
                                                    // }

                                                    bool distance = Vector3.Distance(
                                                                        new Vector3(transform.position.x, 0,
                                                                            transform.position.z),
                                                                        new Vector3(targetPosition.x, 0,
                                                                            targetPosition.z)) <=
                                                                    0.15f;

                                                    agent.SetDestination(targetPosition);
                                                    Debug.Log($"Current distance for {this}: {distance}");

                                                    //if (!IsAgentMoving() || distance)
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
                                                // characterActionGaugeState = CharacterActionGaugeState.WaitPhase;
                                            });

                                            break;
                                    }
                                }

                                break;
                            case CharacterActionGaugeState.EndTurnPhase:
                                GetComponent<AnimationManager>().ChangeAnimationState("idle");
                                switch (prevCharacterBattleActionState)
                                {
                                    case CharacterBattleActionState.Skill:
                                        EndTurnResetSkill();
                                        break;
                                    default:

                                        EndTurnReset();
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

        
        
    }
}