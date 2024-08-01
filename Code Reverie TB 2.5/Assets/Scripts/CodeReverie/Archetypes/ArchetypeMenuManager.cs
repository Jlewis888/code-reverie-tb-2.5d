using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class ArchetypeMenuManager : MenuManager
    {
       
        public List<ArchetypeTree> archetypeTrees;
        public GameObject treeHolder;
        public ArchetypeTree activeArchetypeTree;
        public SelectPartySlotButton selectPartySlotButtonPF;
        
        public GameObject selectPartySlotButtonHolder;
        public GameObject selectArchetypeTreeButtonHolder;
        public SelectArchetypeTreeButton selectArchetypeTreeButtonPF;
        public PartySlot activePartySlot;
        
        
        public void OnEnable()
        {
            CameraManager.Instance.ToggleSkillCamera();
            EventManager.Instance.playerEvents.OnPlayerLock(true);
            EventManager.Instance.playerEvents.onCharacterMenuSwap += InitCharacterTrees;
            
            foreach (Character partySlot in PlayerManager.Instance.availableCharacters)
            {
                SelectPartySlotButton selectPartySlotButton =
                    Instantiate(selectPartySlotButtonPF, selectPartySlotButtonHolder.transform);


                // selectPartySlotButton.partySlot = partySlot;
                // selectPartySlotButton.Init();
                // selectPartySlotButton.gameObject.SetActive(true);
            }

            //ActivePartySlot = PlayerManager.Instance.currentParty[0];
        }
        

        private void OnDisable()
        {
            
           // PlayerController.Instance.CharacterMenuSwap(PlayerManager.Instance.currentParty[0]);
            
            foreach (Transform child in selectPartySlotButtonHolder.transform)
            {
                Destroy(child.gameObject);
            }
            
            EventManager.Instance.playerEvents.onCharacterMenuSwap -= InitCharacterTrees;
        }


        public PartySlot ActivePartySlot
        {
            get { return activePartySlot; }

            set
            {
                if (value != activePartySlot)
                {
                    activePartySlot = value;
                    
                    //PlayerController.Instance.CharacterMenuSwap(activePartySlot);
                    //InitCharacterTrees();
                    EventManager.Instance.playerEvents.OnCharacterMenuSwap();
                }
            }
        }


        public void InitCharacterTrees()
        {

            foreach (Transform child in selectArchetypeTreeButtonHolder.transform)
            {
                Destroy(child.gameObject);
            }
            
            foreach (Transform child in treeHolder.transform)
            {
                Destroy(child.gameObject);
            }
            
            

            if (activePartySlot.character.availableArchetypes.Count > 0)
            {
                archetypeTrees = new List<ArchetypeTree>();
                
                int count = 0;
                foreach (Archetype archetype in activePartySlot.character.availableArchetypes)
                {
                    
                    ArchetypeTree archetypeTree = Instantiate(archetype.info.archetypeTree, treeHolder.transform);

                    archetypeTree.archetype = archetype;
                    archetypeTree.Init();
                    
                    SelectArchetypeTreeButton selectArchetypeTreeButton = Instantiate(selectArchetypeTreeButtonPF,
                        selectArchetypeTreeButtonHolder.transform);

                    
                    selectArchetypeTreeButton.archetypeTree = archetypeTree;
                    
                    if (count == 0)
                    {
                        archetypeTree.gameObject.SetActive(true);
                        ActiveArchetypeTree = archetypeTree;
                    }
                    else
                    {
                        archetypeTree.gameObject.SetActive(false);
                    }

                    archetypeTrees.Add(archetypeTree);
                    
                    count++;

                    //activePartySlot.character.equippedArchetype = archetype;
                   
                    //selectArchetypeTreeButton.Init();

                }


                //activeArchetypeTree = activePartySlot.character.equippedArchetype.info.archetypeTree;
                
                

            }
            else
            {
                ActiveArchetypeTree = null;
            }
           
            
        }

        public void SetActiveTree()
        {
            foreach (ArchetypeTree archetypeTree in archetypeTrees)
            {

                if (ActiveArchetypeTree == null)
                {
                    archetypeTree.gameObject.SetActive(false);
                }
                else
                {
                    if (archetypeTree == ActiveArchetypeTree)
                    {
                        archetypeTree.gameObject.SetActive(true);
                    }
                    else
                    {
                        archetypeTree.gameObject.SetActive(false);
                    }
                }
                
                
            }
        }


        public ArchetypeTree ActiveArchetypeTree
        {
            get { return activeArchetypeTree; }
            set
            {
                
                if (value != activeArchetypeTree)
                {
                   
                    activeArchetypeTree = value;
                    
                }
                
                
                SetActiveTree();
            }
        }
        
        
    }
}