using IEVO.UI.uGUIDirectedNavigation;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LimitedGrid_2
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject pfbBtnCell;
        [SerializeField] private int spawnQuantity = 30;


        private void Start()
        {
            List<GameObject> btnCellsGOs = new List<GameObject>();
            List<Selectable> btnCellsSelectables = new List<Selectable>();
            List<DirectedNavigation> btnCellsDirNavs = new List<DirectedNavigation>();

            for (int i = 0; i < spawnQuantity; i++)
            {
                GameObject go = Instantiate(pfbBtnCell, transform);
                btnCellsGOs.Add(go);
                btnCellsSelectables.Add(go.GetComponent<Selectable>());
                btnCellsDirNavs.Add(go.GetComponent<DirectedNavigation>());
            }

            Selectable[] btnCellsSelectablesArr = btnCellsSelectables.ToArray();

            for (int i = 0; i < btnCellsDirNavs.Count; i++)
            {
                btnCellsDirNavs[i].ConfigDown.SelectableList.SelectableList = btnCellsSelectablesArr;
                btnCellsDirNavs[i].ConfigUp.SelectableList.SelectableList = btnCellsSelectablesArr;
                btnCellsDirNavs[i].ConfigLeft.SelectableList.SelectableList = btnCellsSelectablesArr;
                btnCellsDirNavs[i].ConfigRight.SelectableList.SelectableList = btnCellsSelectablesArr;
            }
        }

    }
}
