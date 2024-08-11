using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class CurrentEnemyHealthPanel : SerializedMonoBehaviour
    {

        public Health health;
        public TMP_Text enemyName;
        public Slider healthSlider;
        public Image healthSliderBG;
        public Image healthSliderFG;
        public List<float> healthBars;
        
        public TMP_Text currentHealthBars;
        //public float healthPerBar;
        public float currentHealthBar;

        private void Awake()
        {
            
            

            if (health != null)
            {
                currentHealthBars.text = health.healthBarCount.ToString();
                //healthPerBar = health.MaxHealth / health.healthBarCount;
            }
            
        }


        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.V))
            {
                if (health != null)
                {
                    //healthPerBar = health.MaxHealth / health.healthBarCount;
                }
            }
            
            if (health != null)
            {
                //healthPerBar = health.MaxHealth / health.healthBarCount;
                //currentHealthBars.text = Mathf.Ceil(health.CurrentHealth / healthPerBar).ToString();
                currentHealthBars.text = health.CurrentHealthBarCount.ToString();
                healthSlider.maxValue = health.HealthPerBar;
                //currentHealthBar = health.CurrentHealth - (health.HealthPerBar * Mathf.Floor(health.CurrentHealth / health.HealthPerBar));
                healthSlider.value = health.CurrentHealthBar;


                if (Mathf.Floor(health.CurrentHealth / health.HealthPerBar) == 0)
                {
                    healthSliderBG.color = Color.black;
                    healthSliderFG.color = Color.red;
                }
                


                if (health.GetComponent<CharacterUnitController>().character.characterState == CharacterState.Dead)
                {
                    gameObject.SetActive(false);
                }
                
            }
        }
        
        
        
    }
}