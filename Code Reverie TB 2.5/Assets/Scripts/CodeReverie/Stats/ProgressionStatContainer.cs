using System.Collections.Generic;

namespace CodeReverie
{
    public class ProgressionStatContainer
    {
        public StatAttribute statAttribute;
        public Dictionary<int, float> statMap = new Dictionary<int, float>();


        public ProgressionStatContainer()
        {
            statMap = new Dictionary<int, float>();
            for (int i = 1; i < 99; i++)
            {
                statMap.Add(i, 0);
            }
        }

        public void UpdateProgression(float baseValue)
        {
            if (statMap == null)
            {
                statMap = new Dictionary<int, float>();
            }
            
            
            if (!statMap.ContainsKey(1))
            {
                statMap.Add(1, baseValue);
            }
            else
            {
                statMap[1] = baseValue;
            }
            
            for (int i = 1; i < 99; i++)
            {
                if (!statMap.ContainsKey(i+1))
                {
                    statMap.Add(i + 1, (int)(statMap[i] * 1.5f));
                }
                else
                {
                    statMap[i + 1] = (int)(statMap[i] * 1.5f);
                }
                
            }
        }
        
    }
}