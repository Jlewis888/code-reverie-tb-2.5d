using System;
using Sirenix.OdinInspector;
using UnityEngine.UI;

namespace CodeReverie
{
    public class ActiveWeaponSlotUI : SerializedMonoBehaviour
    {
        public Image buttonImage;
        public Image weaponImage;


        private void OnEnable()
        {
            EventManager.Instance.playerEvents.onPartyUpdate += Init;
            EventManager.Instance.playerEvents.onCharacterSwap += Init;
            EventManager.Instance.playerEvents.onGearUpdate += Init;
        }


        private void OnDestroy()
        {
            EventManager.Instance.playerEvents.onPartyUpdate -= Init;
            EventManager.Instance.playerEvents.onCharacterSwap -= Init;
            EventManager.Instance.playerEvents.onGearUpdate -= Init;
        }

        public void Init()
        {

            if (PlayerManager.Instance.currentParty[0].characterGear.weaponSlot.item != null)
            {
                weaponImage.sprite = PlayerManager.Instance.currentParty[0].characterGear.weaponSlot.item.info
                    .uiIcon;
            }
            else
            {
                weaponImage.sprite = null;
            }
            
            
        }
        
        public void Init(Character character)
        {
            if (PlayerManager.Instance.currentParty[0].characterGear.weaponSlot.item != null)
            {
                weaponImage.sprite = PlayerManager.Instance.currentParty[0].characterGear.weaponSlot.item.info
                    .uiIcon;
            }
            else
            {
                weaponImage.sprite = null;
            }
        }
        
    }
}