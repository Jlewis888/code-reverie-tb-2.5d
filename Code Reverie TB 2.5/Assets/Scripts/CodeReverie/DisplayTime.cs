using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace CodeReverie
{
    public class DisplayTime : SerializedMonoBehaviour
    {
        public float time = 0;
        public bool timerIsRunning = false;
        public bool isCountDown;
        public bool isAdTimer;
        public bool isCombatTimer;
        //public TMP_Text timeText;
        public string timeText;


        // Start is called before the first frame update
        void Start()
        {
            // if (!isCombatTimer)
            // {
            //     timerIsRunning = true;    
            // }
            // else
            // {
            //     timerIsRunning = false;
            // }
            
        }

        // Update is called once per frame
        void Update()
        {
            if (timerIsRunning)
            {
                if (isCountDown)
                {
                    if (time > 0)
                    {
                        time -= Time.unscaledDeltaTime;
                        Display(time);
                    }
                    else
                    {
                        time = 0;
                        timerIsRunning = false;
                    }
                }
                else
                {
                    time += Time.unscaledDeltaTime;
                    Display(time);
                }

            }
        }

        void Display(float timeToDisplay)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60) % 60;
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            float hours = Mathf.FloorToInt(timeToDisplay / 3600);
            float days = Mathf.FloorToInt(timeToDisplay / 86400) % 86400;


            if (isAdTimer)
            {
                if (timeToDisplay < 60)
                {
                    //timeText.text = seconds + "s Left";
                    timeText = seconds + "s Left";
                }
                else if (timeToDisplay > 86400)
                {
                    //timeText.text = days + " days Left";
                    timeText = days + " days Left";
                }
                else
                {
                    //timeText.text = hours + "h " + minutes + "m Left";
                    timeText = hours + "h " + minutes + "m Left";
                }
            }
            else
            {
                if (timeToDisplay < 10)
                {
                    //timeText.text = $"{minutes}:0{seconds}";
                    timeText = $"{minutes}:0{seconds}";
                }
                else
                {
                    //timeText.text = $"{minutes}:{seconds}";
                    timeText = $"{minutes}:{seconds}";
                }
                
            }
            
        }
    }
}