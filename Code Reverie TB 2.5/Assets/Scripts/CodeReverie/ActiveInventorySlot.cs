using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class ActiveInventorySlot : SerializedMonoBehaviour
    {
        public WeaponInfo weaponInfo;
        public GameObject activeHighlight;
        public Image equippedItemImage;
    }
}