using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class ArchetypeTree : SerializedMonoBehaviour
    {
        public Archetype archetype;
        public Button equipArchetypeButton;
        public List<ArchetypeSkillNodeButton> skillNodeButtons = new List<ArchetypeSkillNodeButton>();

        public Dictionary<string, ArchetypeSkillNodeButton> skillNodeButtonMap =
            new Dictionary<string, ArchetypeSkillNodeButton>();
        public ArchetypeNodeConnector archetypeNodeConnectorPF;
        public List<ArchetypeNodeConnector> skillNodeConnectors = new List<ArchetypeNodeConnector>();



        public void Init()
        {
            archetype.InitializeSetSkillNodesMap();

            skillNodeButtons = GetComponentsInChildren<ArchetypeSkillNodeButton>().ToList();

            skillNodeButtonMap = new Dictionary<string, ArchetypeSkillNodeButton>();
            
            foreach (var archetypeSkillNodeButton in skillNodeButtons)
            {
                if (archetypeSkillNodeButton.skillNodeDataContainer != null)
                {
                    if (!skillNodeButtonMap.ContainsKey(archetypeSkillNodeButton.skillNodeDataContainer.id))
                    {
                        skillNodeButtonMap.Add(archetypeSkillNodeButton.skillNodeDataContainer.id, archetypeSkillNodeButton);
                    }
                }
            }
            
            SetSkillNodeButtons();
            
            equipArchetypeButton.onClick.AddListener(EquipArchetype);
            
        }

        public void EquipArchetype()
        {
            CanvasManager.Instance.characterMenuManager.ActivePartySlot.character.EquippedArchetype = archetype;
            EventManager.Instance.playerEvents.OnSkillSlotUpdate();
        }


        public void SetSkillNodeButtons()
        {
            if (archetype.skillNodesMap != null)
            {
                
                
                
                
                foreach (ArchetypeSkillNodeButton skillNodeButton in skillNodeButtons)
                {

                    if (skillNodeButton.skillNodeDataContainer != null)
                    {
                        if (archetype.skillNodesMap.ContainsKey(skillNodeButton.skillNodeDataContainer.id))
                        {


                            skillNodeButton.name = skillNodeButton.skillNodeDataContainer.id;

                            //Debug.Log(skillNodeButton.skillNodeDataContainer.id);

                            skillNodeButton.archetypeSkillNode =
                                archetype.skillNodesMap[skillNodeButton.skillNodeDataContainer.id];

                            if (skillNodeButton.archetypeSkillNode != null)
                            {
                                skillNodeButton.Init();

                                foreach (var archetypeSkillNodeDataContainer in skillNodeButton.skillNodeDataContainer.linkedSkillNodes)
                                {

                                    bool combo1 = skillNodeConnectors.Exists(x =>
                                        (x.skillNode1 == skillNodeButton.archetypeSkillNode && x.skillNode2 ==
                                            skillNodeButtonMap[archetypeSkillNodeDataContainer.id]
                                                .archetypeSkillNode));
                                    
                                    bool combo2 = skillNodeConnectors.Exists(x =>
                                        (x.skillNode2 == skillNodeButton.archetypeSkillNode && x.skillNode1 ==
                                            skillNodeButtonMap[archetypeSkillNodeDataContainer.id]
                                                .archetypeSkillNode));
                                    
                                    
                                    if (!combo1 && !combo2)
                                    {
                                        if (skillNodeButtonMap[archetypeSkillNodeDataContainer.id].archetypeSkillNode != null)
                                        {
                                            ArchetypeNodeConnector archetypeNodeConnector =
                                                Instantiate(archetypeNodeConnectorPF, skillNodeButton.transform);
                                    
                                            archetypeNodeConnector.transform.localPosition = Vector3.zero;
                                            archetypeNodeConnector.transform.parent = skillNodeButton.transform.parent;
                                            archetypeNodeConnector.skillNode1 = skillNodeButton.archetypeSkillNode;
                                            archetypeNodeConnector.skillNode2 = skillNodeButtonMap[archetypeSkillNodeDataContainer.id].archetypeSkillNode;
                                    
                                    
                                            //archetypeNodeConnector.endConnector.transform.localPosition = skillNodeButtonMap[archetypeSkillNodeDataContainer.id].transform.localPosition;
                                            archetypeNodeConnector.endConnector.transform.parent = skillNodeButtonMap[archetypeSkillNodeDataContainer.id].transform;
                                            archetypeNodeConnector.endConnector.transform.localPosition = Vector3.zero;
                                            archetypeNodeConnector.endConnector.transform.parent = archetypeNodeConnector.transform;
                                    
                                            skillNodeConnectors.Add(archetypeNodeConnector);
                                        }
                                    }
                                }
                                
                            }




                            //skillMasteryNodeButton.skillNodeName.text = skillMasteryNodeButton.skillNodeId;


                            // SkillMasteryNode skillMasteryNode = GetSkillNodeById(skillMasteryNodeButton.skillNodeId);
                            // if (skillMasteryNodeButton.skillPointsText != null)
                            // {
                            //     skillMasteryNodeButton.skillPointsText.text = $"{skillMasteryNode.skillPointsAssigned}/{skillMasteryNode.skillMasteryNodeDetails.maxAssignedPoints}";
                            //
                            // }
                        }
                    }

                }
            }
        }
        
    }
}