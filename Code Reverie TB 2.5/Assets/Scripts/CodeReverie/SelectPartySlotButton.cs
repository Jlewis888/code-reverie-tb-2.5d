using System;
using Sirenix.OdinInspector;
using UnityEngine.UI;

namespace CodeReverie
{
    public class SelectPartySlotButton : SerializedMonoBehaviour
    {
        public Button button;
        public Character character;
        public Image image;


        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(SetActivePartySlot);
        }


        public void Init()
        {
            image.sprite = character.GetCharacterPortrait();
        }


        public void SetActivePartySlot()
        {
            // if (CanvasManager.Instance.archetypeMenuManager.gameObject.activeInHierarchy)
            // {
            //     CanvasManager.Instance.characterMenuManager.ActivePartySlot = character;
            // }
            // if (CanvasManager.Instance.inventory.gameObject.activeInHierarchy)
            // {
            //     CanvasManager.Instance.inventory.ActivePartySlot = character;
            // }
            
            //EventManager.Instance.playerEvents.OnCharacterSwap();
        }
        
        
    }
}