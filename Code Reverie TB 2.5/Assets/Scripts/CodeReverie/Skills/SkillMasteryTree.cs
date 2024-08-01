using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class SkillMasteryTree : SerializedMonoBehaviour
    {
        public string skillId;
        public string skillTreePath;
        public Dictionary<string, SkillMasteryNode> skillNodesMap = new Dictionary<string, SkillMasteryNode>();
        public List<SkillMasteryNodeButton> skillMasteryNodeButtons;

        public int availableSkillPoints;
        public int pointsSpent;
        public SkillMasteryNode selectedSkillMasteryNode;
        public SkillMasteryNodeButton selectedNodeButton;
        
        
        private void Awake()
        {
            skillNodesMap = new Dictionary<string, SkillMasteryNode>();
            Init();
            availableSkillPoints = 5;
        }

        private void Update()
        {
            if (GameManager.Instance.playerInput.GetButtonDown("Confirm"))
            {
                if (selectedNodeButton != null)
                {
                    //selectedNodeButton.UseButton();
                }
            } 
            else if (GameManager.Instance.playerInput.GetButtonDown("Menu Option 1"))
            {
               // AssignPoints(-1);
            }
        }


        public void Init()
        {
            skillNodesMap = new Dictionary<string, SkillMasteryNode>();

            skillNodesMap = GetAllSkillNodes();
            SetSkillNodeButtons();
        }

        public Dictionary<string, SkillMasteryNode> GetAllSkillNodes()
        {
            SkillMasteryNodeDetails[] skillMasteryNodeDetails = Resources.LoadAll<SkillMasteryNodeDetails>($"Skill Tree/{skillTreePath}");

            Dictionary<string, SkillMasteryNode> allSkillNodes = new Dictionary<string, SkillMasteryNode>();
         
            foreach (SkillMasteryNodeDetails skillMasteryNodeDetail  in skillMasteryNodeDetails)
            {
                if (allSkillNodes.ContainsKey(skillMasteryNodeDetail.id))
                {
                    Debug.Log("Skill with duplicate ID");
                }
                else
                {
                   // allSkillNodes.Add(skillMasteryNodeDetail.id, new SkillMasteryNode(skillMasteryNodeDetail)); 
                }
                
                
            }
            
            return allSkillNodes;
        }


        public void SetSkillNodeButtons()
        {
            if (skillNodesMap != null)
            {
                foreach (SkillMasteryNodeButton skillMasteryNodeButton in skillMasteryNodeButtons)
                {
                    
                    if (skillNodesMap.ContainsKey(skillMasteryNodeButton.skillNodeId))
                    {
                        skillMasteryNodeButton.skillNodeName.text = skillMasteryNodeButton.skillNodeId;


                        SkillMasteryNode skillMasteryNode = GetSkillNodeById(skillMasteryNodeButton.skillNodeId);
                        if (skillMasteryNodeButton.skillPointsText != null)
                        {
                            skillMasteryNodeButton.skillPointsText.text = $"{skillMasteryNode.skillPointsAssigned}/{skillMasteryNode.skillMasteryNodeDetails.maxAssignedPoints}";

                        }
                    }
                  
                }
            }
        }

        public SkillMasteryNode GetSkillNodeById(string id)
        {
            SkillMasteryNode skillMasteryNode = skillNodesMap[id];

            if (skillMasteryNode == null)
            {
                Debug.Log("Skill node is null");
            }

            return skillMasteryNode;
        }

        public void SetActiveSkillNodeById(string id)
        {
            selectedSkillMasteryNode = GetSkillNodeById(id);
        }

        public void SetActiveSkillNodeNull()
        {
            selectedSkillMasteryNode = null;
        }
        

        public void ResetAllSkillTreePoints()
        {
            
        }
        
        public void AssignPoints(int points)
        {
        
            if (points < 0)
            {
                if (pointsSpent > 0)
                {
                    availableSkillPoints -= points;
                    pointsSpent += points;
                    //selectedSkillMasteryNode.AssignPoints(points); 
                }
                
            }
            else if (points > 0)
            {
              
                if (points <= availableSkillPoints)
                {
                    availableSkillPoints -= points;
                    pointsSpent += points;
                    //selectedSkillMasteryNode.AssignPoints(points);
                }
            }

            if (selectedNodeButton.skillPointsText != null)
            {
                selectedNodeButton.skillPointsText.text = $"{selectedSkillMasteryNode.skillPointsAssigned}/{selectedSkillMasteryNode.skillMasteryNodeDetails.maxAssignedPoints}";

            }
        }
        
    }
}