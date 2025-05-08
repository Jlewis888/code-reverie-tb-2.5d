using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class TargetInfoPanel : SerializedMonoBehaviour
    {
        public Image characterPortrait;
        public TMP_Text characterName;
        public TMP_Text targetCharacterActionName;

        public GameObject targetedGroupPanel;
        public GameObject targetPortraitGroupHolder;
        public GameObject targetPortraitGroupPF;
        
        
        public Image targetCharacterPortrait;
        
        
    }
}