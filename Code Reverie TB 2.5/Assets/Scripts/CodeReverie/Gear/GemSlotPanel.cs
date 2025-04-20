using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class GemSlotPanel : SerializedMonoBehaviour
    {
        public GemSlotUI gemSlot1;
        public GemSlotUI gemSlot2;
        public GemSlotUI gemSlot3;
        public List<GemSlotUI> gemSlotUiList = new List<GemSlotUI>();
        
        

        public void Init()
        {
            
            gemSlotUiList = new List<GemSlotUI>();
            
            if (gemSlot1.gemSlot == null)
            {
                gemSlot1.gameObject.SetActive(false);
            }
            else
            {
                gemSlot1.gameObject.SetActive(true);
                gemSlotUiList.Add(gemSlot1);
            } 
           
            if (gemSlot2.gemSlot == null)
            {
                
                gemSlot2.gameObject.SetActive(false);
            }
            else
            {
                gemSlot2.gameObject.SetActive(true);
                gemSlotUiList.Add(gemSlot2);
            } 

            
            if (gemSlot3.gemSlot == null)
            {
                
                gemSlot3.gameObject.SetActive(false);
            }else
            {
                gemSlot3.gameObject.SetActive(true);
                gemSlotUiList.Add(gemSlot3);
            } 

        }

        // public List<GemSlotUI> GetActiveGemSlotUIList()
        // {
        //     List<GemSlotUI> gemSlotUiList = new List<GemSlotUI>();
        //
        //
        //
        //     return gemSlotUiList;
        // }

        public void EquipGemSlotItem(int slot, Item item)
        {
            if (slot == 1)
            {
                gemSlot1.gemSlot.EquipGemSlotItem(item);
            }
            if (slot == 2)
            {
                gemSlot2.gemSlot.EquipGemSlotItem(item);
            }
            if (slot == 3)
            {
                gemSlot3.gemSlot.EquipGemSlotItem(item);
            }
        }

    }
}