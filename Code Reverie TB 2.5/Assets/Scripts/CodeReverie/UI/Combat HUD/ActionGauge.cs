using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class ActionGauge : SerializedMonoBehaviour
    {
        public RectTransform[] playerIcons;  // Player unit icons
        public RectTransform[] enemyIcons;   // Enemy unit icons
        public float radius = 100f;          // Radius of the circle
        public float totalTime = 10f;        // Total time to complete a circle


        public Transform playerIcon;
        public Transform enemyIcon;
        public float rotationSpeed;
        
        private float elapsedTime;

        void Update()
        {
            elapsedTime += Time.deltaTime;
            
            // playerIcon.Rotate(0, 0,  -rotationSpeed * Time.deltaTime);
            
            
            // foreach (RectTransform icon in playerIcons)
            // {
            //     MoveIconAlongCircle(icon, elapsedTime);
            // }
            //
            // foreach (RectTransform icon in enemyIcons)
            // {
            //     MoveIconAlongCircle(icon, elapsedTime * 1.2f);  // Enemies could move at different speeds
            // }
        }

        void MoveIconAlongCircle(RectTransform icon, float time)
        {
            float angle = (time / totalTime) * 360f;  // Calculate the angle based on time
            float rad = Mathf.Deg2Rad * angle;
        
            float x = Mathf.Cos(rad) * radius;
            float y = Mathf.Sin(rad) * radius;
        
            icon.anchoredPosition = new Vector2(x, y);  // Set the icon’s position
        }
    }
}