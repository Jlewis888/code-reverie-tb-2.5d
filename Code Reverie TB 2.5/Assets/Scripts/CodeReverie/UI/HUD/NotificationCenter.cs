using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class NotificationCenter : SerializedMonoBehaviour
    {
        public GameObject notificationHolder;
        public Notification notificationPF;
        public Queue<Notification> notifications;
        public int notificationsMax;
        public float time;
        public float timer;

        private void Awake()
        {
            Clear();

            if (time <= 0)
            {
                time = 5f;
            }
        }
        
        private void OnEnable()
        {
            //EventManager.Instance.playerEvents.onItemPickup += ItemPickupTrigger;
        }
        
        private void OnDisable()
        {
            //EventManager.Instance.playerEvents.onItemPickup -= ItemPickupTrigger;
        }

        private void Start()
        {
            
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.B))
            {
                // Notification notification = Instantiate(notificationPF, notificationHolder.transform);
                // notifications.Enqueue(notification);
                NotificationTrigger("This is a test");
            }

            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                notificationHolder.SetActive(false);

                if (notificationHolder.transform.childCount > 0)
                {
                    Clear();
                }
            }
            
        }


        public void Clear()
        {
            foreach (Transform child in notificationHolder.transform)
            {
                Destroy(child.gameObject);
            }
            notifications.Clear();
        }
        
        public void NotificationTrigger(string message)
        {
            // foreach (var itemPickup in notifications)
            // {
            //     if (itemPickup.itemId == id)
            //     {
            //         timer = time;
            //         return;
            //     }
            // }

            timer = time;
            notificationHolder.SetActive(true);
            
            Notification notification =Instantiate(notificationPF, notificationHolder.transform);

            notification.notificationMessage.text = message;
            notification.gameObject.SetActive(true);
            notifications.Enqueue(notification);
            
            QueueFullCheck();
            
        }

        public void QueueFullCheck()
        {
            if (notifications.Count > notificationsMax)
            {
                Notification nextNotification = notifications.Peek();
                Destroy(nextNotification.gameObject);
                notifications.Dequeue();
            }
        }
        
    }
}