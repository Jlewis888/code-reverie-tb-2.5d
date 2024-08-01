using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class PauseMenuNavigationButton : SerializedMonoBehaviour
    {
        public PauseMenuNavigationState pauseMenuNavigationState;
        public GameObject selector;
        public TMP_Text nameText;
        public Image icon;
        public bool canNavigate = true;
    }
}