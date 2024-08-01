using System.Collections.Generic;
using UnityEngine;

namespace CodeReverie
{
    public class WorldTierManager : ManagerSingleton<WorldTierManager>
    {
        public int currentWorldTier;

        public Dictionary<int, WorldTierStatsModifier> worldTierStatsModifiers =
            new Dictionary<int, WorldTierStatsModifier>();

        protected override void Awake()
        {
            base.Awake();
            
            for (int i = 0; i < 6; i++)
            {
                if (!worldTierStatsModifiers.ContainsKey(i))
                {
                    worldTierStatsModifiers.Add(i, null);
                }
            }
            
            
        }


        public WorldTierStatsModifier CurrentWorldTierStatsModifier()
        {
            
            if (worldTierStatsModifiers.ContainsKey(currentWorldTier))
            {
                return worldTierStatsModifiers[currentWorldTier];
            }

            return null;
        }
    }
}