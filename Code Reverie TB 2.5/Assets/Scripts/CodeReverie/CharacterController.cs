using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class CharacterController : SerializedMonoBehaviour
    {
        public CharacterDataContainer characterInfo;
        public Character character;
        public CharacterUnit characterUnit;
        public bool canAttack;
        public AnimationManager animationManager;

        private void Awake()
        { 
            if (TryGetComponent(out ComponentTagManager componentTagManager))
            {
                if (componentTagManager.HasTag(ComponentTag.Enemy))
                { 
                    character = new Character(characterInfo);
                }
            }
        }


        private void OnEnable()
        {
            EventManager.Instance.generalEvents.characterUnitManagerReceiver += CharacterUnitManagerReceiver;
        }

        private void OnDisable()
        {
            EventManager.Instance.generalEvents.characterUnitManagerReceiver -= CharacterUnitManagerReceiver;
        }


        public void CharacterUnitManagerReceiver(Character character, string message)
        {

            if (this.character == null)
            {
                return;
            }
            
            if (this.character == character)
            {
                switch (message)
                {
                    case "SelectedPlayerCharacter":
                        Debug.Log("Does this hit");
                        GetComponent<PlayerMovementController>().enabled = true;
                        CameraManager.Instance.mainVirtualCamera.m_Follow = transform;
                        break;
                    case "InactivePartyCharacter":
                       
                        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                        GetComponent<PlayerMovementController>().enabled = false;
                        break;
                }
            }
        }
        
    }
}