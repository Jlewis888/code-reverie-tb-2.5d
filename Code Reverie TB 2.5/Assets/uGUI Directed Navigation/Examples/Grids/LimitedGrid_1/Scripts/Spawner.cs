using IEVO.UI.uGUIDirectedNavigation;
using UnityEngine;

namespace LimitedGrid_1
{
    [RequireComponent(typeof(RectTransform))]
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject pfbBtnCell;
        [SerializeField] private int spawnQuantity = 36;


        private void Start()
        {
            RectTransform rectTransform = transform as RectTransform;

            for (int i = 0; i < spawnQuantity; i++)
            {
                GameObject go = Instantiate(pfbBtnCell, transform);
                DirectedNavigation dirNav = go.GetComponent<DirectedNavigation>();

                dirNav.ConfigDown.RectTransform.RectTransform = rectTransform;
                dirNav.ConfigUp.RectTransform.RectTransform = rectTransform;
                dirNav.ConfigLeft.RectTransform.RectTransform = rectTransform;
                dirNav.ConfigRight.RectTransform.RectTransform = rectTransform;
            }
        }

    }
}
