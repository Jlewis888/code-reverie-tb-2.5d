using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class SelectArchetypeTreeButton : SerializedMonoBehaviour
    {
        private Button button;
        public ArchetypeTree archetypeTree;
        public Archetype archetype;

        private void Awake()
        {
            button = GetComponent<Button>();
            
            button.onClick.AddListener(SetActiveArchetypeTree);
            
        }

        public void Init()
        {
            if (archetype != null)
            {
                archetypeTree = archetype.info.archetypeTree;
            }
        }
        

        public void SetActiveArchetypeTree()
        {
            CanvasManager.Instance.characterMenuManager.ActiveArchetypeTree = archetypeTree;
        }
        
        
    }
}