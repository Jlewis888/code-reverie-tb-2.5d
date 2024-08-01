using UnityEngine;

namespace CodeReverie
{
    public class TooltipData
    {
        public string toolTipType;
        public string name;
        public Color color;
        public Color headerColor;
        public string description;


        public TooltipData()
        {

            if (ColorUtility.TryParseHtmlString("#525252", out Color hexColor))
            {
                headerColor = hexColor;
            }
        }
        
    }
}