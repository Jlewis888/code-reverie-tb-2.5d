using System;
using CodeReverie;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class CharacterActionGauge : SerializedMonoBehaviour
    {
        public CharacterBattleManager characterBattleManager;
        public GameObject iconGroup;
        public Transform iconHolder;
        public Image characterPortrait;
        public float rotationSpeed;

        public float minValue = 0f; // Minimum value of the gauge
        public float maxValue = 100f; // Maximum value of the gauge
        public float value = 0f; // Current value of the gauge

        public float timer;

        private void Update()
        {
            if (characterBattleManager != null)
            {
                RunCharacterActionTimer();
                // transform.Rotate(0,0, -rotationSpeed * characterBattleManager.cooldownTimer);
                // transform.Rotate(0,0, -angle);


                value = characterBattleManager.cooldownTimer;
                float normalizedValue = (value - minValue) / (maxValue - minValue);
                float angle = normalizedValue * 360f;
                transform.rotation = Quaternion.Euler(0, 0, -angle);

                if (iconHolder != null)
                {
                    Vector3 currentRotation = iconHolder.eulerAngles;
                    iconHolder.rotation = Quaternion.Euler(iconHolder.transform.eulerAngles.x,0, transform.rotation.z * -1f);
                }
            }
        }

        public void Init()
        {
            maxValue = characterBattleManager.actionPhaseCooldown;
            value = characterBattleManager.cooldownTimer;


            if (characterBattleManager != null)
            {
                if (characterBattleManager.GetComponent<CharacterUnitController>() != null)
                {
                    if (characterBattleManager.GetComponent<CharacterUnitController>().character != null)
                    {
                        characterPortrait.sprite = characterBattleManager.GetComponent<CharacterUnitController>()
                            .character
                            .GetCharacterPortrait();
                    }
                }
            }
        }

        public void SetSliderIconPosition()
        {
            if (characterBattleManager.TryGetComponent(out ComponentTagManager componentTagManager))
            {
                if (componentTagManager.HasTag(ComponentTag.Enemy))
                {
                    RectTransform rectTransform = iconGroup.GetComponent<RectTransform>();

                    rectTransform.localPosition = new Vector3(46.6f,
                        0, 0);
                }
            }
        }


        public void OnCharacterDeath(CharacterBattleManager characterBattleManager)
        {
            if (this.characterBattleManager == characterBattleManager)
            {
                Destroy(gameObject);
            }
        }

        public void OnPlayerSelectTarget(CharacterBattleManager characterBattleManager)
        {
            if (this.characterBattleManager == characterBattleManager)
            {
                UpdateCanvasOrder(1);
                //sliderIconHolder.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
            else
            {
                UpdateCanvasOrder(0);
                ScaleToNormal(null);
            }
        }

        public void ScaleToNormal(CharacterBattleManager characterBattleManager)
        {
            UpdateCanvasOrder(0);
            //sliderIconHolder.transform.localScale = Vector3.one;
        }

        public void UpdateCanvasOrder(int order)
        {
            GetComponent<Canvas>().sortingOrder = order;
            GetComponent<Canvas>().enabled = false;
            GetComponent<Canvas>().enabled = true;
        }

        public void RunCharacterActionTimer()
        {
            if (CombatManager.Instance.combatManagerState == CombatManagerState.Battle)
            {
                if (characterBattleManager.GetComponent<CharacterUnitController>().character.characterState ==
                    CharacterState.Alive)
                {
                    switch (characterBattleManager.characterActionGaugeState)
                    { 
                        case CharacterActionGaugeState.WaitPhase:
                            if (!CombatManager.Instance.pause)
                            {
                                if (characterBattleManager.cooldownTimer >=
                                    characterBattleManager.actionPhaseCooldown * .75f)
                                {
                                    characterBattleManager.characterActionGaugeState =
                                        CharacterActionGaugeState.PreCommandPhase;
                                    
                                    // if (characterBattleManager.GetComponent<ComponentTagManager>().HasTag(ComponentTag.Player))
                                    // {
                                    //
                                    //     if (!CombatManager.Instance.playerCharacterTurnQueue.Contains(
                                    //             characterBattleManager))
                                    //     {
                                    //         CombatManager.Instance.playerCharacterTurnQueue.Enqueue(characterBattleManager);
                                    //     }
                                    //     
                                    //     
                                    // }
                                    
                                    
                                    
                                }
                                else
                                {
                                    characterBattleManager.cooldownTimer += Time.deltaTime * (1 + characterBattleManager
                                        .GetComponent<CharacterUnitController>().character.characterStats
                                        .GetStat(StatAttribute.Haste));
                                }
                            }

                            break;

                        case CharacterActionGaugeState.CommandPhase:

                            if (!CombatManager.Instance.pause)
                            {
                                switch (characterBattleManager.battleState)
                                {
                                    case CharacterBattleState.WaitingAction:
                                        if (!CombatManager.Instance.pause)
                                        {
                                            switch (characterBattleManager.skillCastTime)
                                            {
                                                case SkillCastTime.Instant:
                                                    //cooldownTimer = actionPhaseCooldown;
                                                    characterBattleManager.cooldownTimer += Time.deltaTime * 2f;

                                                    break;
                                                case SkillCastTime.Short:
                                                    //cooldownTimer += Time.deltaTime * (1 + GetComponent<CharacterStatsManager>().GetStat(StatAttribute.Haste)); 
                                                    characterBattleManager.cooldownTimer += Time.deltaTime;
                                                    break;
                                                case SkillCastTime.Medium:
                                                    characterBattleManager.cooldownTimer += Time.deltaTime * (1f / 3f);
                                                    break;
                                                case SkillCastTime.Long:
                                                    characterBattleManager.cooldownTimer += Time.deltaTime * (1f / 4f);
                                                    break;
                                            }
                                        }

                                        if (characterBattleManager.cooldownTimer >=
                                            characterBattleManager.actionPhaseCooldown)
                                        {
                                            switch (characterBattleManager.characterBattleActionState)
                                            {
                                                case CharacterBattleActionState.Attack:
                                                case CharacterBattleActionState.Defend:
                                                case CharacterBattleActionState.Item:
                                                    break;

                                                case CharacterBattleActionState.Skill:
                                                    CombatManager.Instance.combatQueue.Enqueue(characterBattleManager);
                                                    break;
                                            }

                                            characterBattleManager.skillCastTime = SkillCastTime.None;

                                            characterBattleManager.cooldownTimer =
                                                characterBattleManager.actionPhaseCooldown;

                                            characterBattleManager.battleState = CharacterBattleState.WaitingQueue;
                                            characterBattleManager.characterActionGaugeState =
                                                CharacterActionGaugeState.ActionPhase;
                                        }

                                        break;
                                    case CharacterBattleState.Interrupted:
                                        //todo To bring code from CharacterBattleManager.cs
                                        break;
                                }
                            }

                            break;
                    }
                }
            }
        }
    }
}