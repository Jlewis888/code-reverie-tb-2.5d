using Sirenix.OdinInspector;
using TMPro;

namespace CodeReverie
{
    public class CommandWheelToolTip : SerializedMonoBehaviour
    {
        public CombatToolTipData combatToolTipData;
        public TMP_Text actionTitle;
        public TMP_Text actionDescription;
        public TMP_Text actionFooterText;
        public TMP_Text actionCost;
        public TMP_Text actionCastTime;


        public void SetToolTipData()
        {
            Clear();
            
            if (combatToolTipData != null)
            {
                actionTitle.text = combatToolTipData.actionTitle;
                actionDescription.text = combatToolTipData.actionDescription;
                actionFooterText.text = $"MP: {combatToolTipData.actionCost} Cast Time: {combatToolTipData.actionCastTime}";
                // actionCost.text = combatToolTipData.actionCost.ToString();
                // actionCastTime.text = combatToolTipData.actionCastTime.ToString();
            }
        }

        public void Clear()
        {
            actionTitle.text = "";
            actionDescription.text = "";
            actionFooterText.text = "";
            // actionCost.text = "";
            // actionCastTime.text = "";
        }
        
        
    }
}