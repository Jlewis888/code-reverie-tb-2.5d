using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class BattlePositionContainer : SerializedMonoBehaviour
    {
        public List<BattlePosition> battlePositions;

        private void Awake()
        {
            battlePositions = GetComponentsInChildren<BattlePosition>().ToList();
        }
    }
}