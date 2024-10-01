using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [RequireComponent(typeof(CommandWheel))]
    public class CommandWheelExtension : SerializedMonoBehaviour
    {
        public CommandWheel commandWheel;

        private void Awake()
        {
            commandWheel = GetComponent<CommandWheel>();
        }
        
        
        public virtual void InitExtension()
        {
            
        }
    }
}