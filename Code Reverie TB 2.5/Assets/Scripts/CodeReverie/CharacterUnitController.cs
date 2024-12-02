using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace CodeReverie
{
    [Serializable]
    public class CharacterUnitController : SerializedMonoBehaviour
    {
       
        [SerializeField]public string characterInstanceID;
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

            characterUnit = GetComponentInChildren<CharacterUnit>();
        }


        private void OnEnable()
        {
            EventManager.Instance.generalEvents.characterUnitManagerReceiver += CharacterUnitManagerReceiver;
        }

        private void OnDisable()
        {
            EventManager.Instance.generalEvents.characterUnitManagerReceiver -= CharacterUnitManagerReceiver;
        }

        private void Reset()
        {
            SetInstanceID();
        }

        public void SetInstanceID()
        {
#if UNITY_EDITOR
            characterInstanceID = Guid.NewGuid().ToString();

            EditorUtility.SetDirty(this);
#endif
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
                        //Debug.Log("Does this hit");
                        GetComponent<PlayerMovementController>().enabled = true;
                        CameraManager.Instance.mainVirtualCamera.Follow = transform;
                        break;
                    case "InactivePartyCharacter":
                       
                        GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
                        GetComponent<PlayerMovementController>().enabled = false;
                        break;
                }
            }
        }
        
    }
}