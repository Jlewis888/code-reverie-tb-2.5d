using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;



namespace CodeReverie
{
    
    //[DefaultExecutionOrder(-650)]
    public class ManagerSingleton<T> : SerializedMonoBehaviour where T : ManagerSingleton<T>
    {

        private static T instance;

        public static T Instance
        {
            get { return instance; }
        }

        protected virtual void Awake()
        {
            
           // Debug.Log(name);
            
            if (instance == null)
            {
                instance = (T)this;

                if (!gameObject.transform.parent)
                {
                    DontDestroyOnLoad(instance);
                }
            }
            else
            {
               // Debug.Log("hereh ere wafdsaf");
                Destroy(gameObject);
               
            }
        }
    }
}
