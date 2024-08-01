using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class ArchetypeSkillNode
    {
        public ArchetypeSkillNodeDataContainer skillNodeDataContainer;
        
        [SerializeField]
        private ArchetypeSkillNodeState skillNodeState;
        public int assignedPoints;
        public bool linkedNode;
        public string archetypeID;
        public SkillNodeLink skillNodeLink;
        
        public ArchetypeSkillNode(ArchetypeSkillNodeDataContainer info)
        {
            skillNodeDataContainer = info;
        }

        public ArchetypeSkillNodeState SkillNodeState
        {
            get { return skillNodeState; }

            set
            {
                if (value != skillNodeState)
                {
                    skillNodeState = value;
                }
            }
        }
        

        public int TotalLinkedAssignedPoints()
        {
            int totalAssignPoints = 0;
            
            // foreach (ArchetypeSkillNodeDataContainer linkedArchetypeSkillNodeDataContainer in skillNodeLink.linkedArchetypeSkillNodeDataContainers)
            // {
            //     totalAssignPoints += CanvasManager.Instance.characterMenuManager.ActiveArchetypeTree.archetype.skillNodesMap[linkedArchetypeSkillNodeDataContainer.id]
            //         .assignedPoints;
            // }

            return totalAssignPoints;
        }

        public bool CanAssignPoints()
        {

            if (linkedNode)
            {
                Debug.Log("Linked Node");
                Debug.Log($"Total Assigned Points: {TotalLinkedAssignedPoints()}");
                if (TotalLinkedAssignedPoints() < skillNodeLink.maxAssignedPoints)
                {
                    return true;
                }

                return false;
            }
            
            
            
            if (assignedPoints < skillNodeDataContainer.maxAssignedPoints)
            {
                return true;
            }
            
            
            return false;
        }

        public bool IsConnectedToAnAssignRootNode(ArchetypeSkillNode startNode)
        {
            if (skillNodeDataContainer.archetypeSkillNodeType == ArchetypeSkillNodeType.Root)
            {
                return true;
            }

            HashSet<ArchetypeSkillNode> visitedSkillNodes = new HashSet<ArchetypeSkillNode>();


            bool Dfs(ArchetypeSkillNode node)
            {
                if (node.skillNodeState == ArchetypeSkillNodeState.Assigned && node.skillNodeDataContainer.archetypeSkillNodeType == ArchetypeSkillNodeType.Root)
                {
                    return true;
                }

                visitedSkillNodes.Add(node);

                foreach (ArchetypeSkillNodeDataContainer archetypeSkillNodeDataContainer in node.skillNodeDataContainer.linkedSkillNodes)
                {
                    ArchetypeSkillNode skillNode =
                        CanvasManager.Instance.characterMenuManager.ActiveArchetypeTree.archetype.skillNodesMap[
                            archetypeSkillNodeDataContainer.id];


                    if (!visitedSkillNodes.Contains(skillNode))
                    {
                        if (Dfs(skillNode))
                        {
                            return true;
                        }
                    }
                    
                }
                
                return false;
            }
            
            
            
            return Dfs(startNode);
        }


        public bool AreAllAssignedNeighborsConnectedToRoot()
        {

            bool connected = true;
            
            
            foreach (ArchetypeSkillNodeDataContainer archetypeSkillNodeDataContainer in skillNodeDataContainer.linkedSkillNodes)
            {
                ArchetypeSkillNode skillNode =
                    CanvasManager.Instance.characterMenuManager.ActiveArchetypeTree.archetype.skillNodesMap[
                        archetypeSkillNodeDataContainer.id];
                
                
                Debug.Log(archetypeSkillNodeDataContainer.id);
                Debug.Log(CanvasManager.Instance.characterMenuManager.ActiveArchetypeTree.archetype.skillNodesMap[
                    archetypeSkillNodeDataContainer.id].SkillNodeState);
                
                if (skillNode.skillNodeState != ArchetypeSkillNodeState.Assigned)
                {
                    continue;
                }
                
                connected = IsNeighborConnectedToAnAssignRootNode(skillNode);
                
                Debug.Log(connected);
            
                if (!connected)
                {
                    break;
                }
                
            }
            
            
            return connected;
        }
        
        
        
        //todo Doesnt work
        // public bool IsNeighborConnectedToAnAssignRootNode()
        // {
        //     
        //     
        //     if (skillNodeDataContainer.archetypeSkillNodeType == ArchetypeSkillNodeType.Root)
        //     {
        //         return true;
        //     }
        //
        //     HashSet<ArchetypeSkillNode> visitedSkillNodes = new HashSet<ArchetypeSkillNode>(){this};
        //     //HashSet<string> visitedSkillNodesID = new HashSet<string>(){skillNodeDataContainer.id};
        //
        //     bool connected = true;
        //     
        //
        //     bool Dfs(ArchetypeSkillNode node)
        //     {
        //         
        //         bool connected = true;
        //         visitedSkillNodes.Add(node);
        //         
        //         if (node.assigned && node.skillNodeDataContainer.archetypeSkillNodeType != ArchetypeSkillNodeType.Root)
        //         {
        //             
        //             //visitedSkillNodesID.Add(node.skillNodeDataContainer.id);
        //             
        //             foreach (ArchetypeSkillNodeDataContainer archetypeSkillNodeDataContainer in node.skillNodeDataContainer.linkedSkillNodes)
        //             {
        //                 ArchetypeSkillNode skillNode =
        //                     CanvasManager.Instance.characterMenuManager.ActiveArchetypeTree.archetype.skillNodesMap[
        //                         archetypeSkillNodeDataContainer.id];
        //
        //             
        //                 if (!visitedSkillNodes.Contains(skillNode))
        //                 {
        //                     if (!Dfs(skillNode) && skillNode.assigned)
        //                     {
        //                         // Debug.Log(node.skillNodeDataContainer.name);
        //                         // Debug.Log(skillNode.skillNodeDataContainer.name);
        //                         connected = false;
        //                     } 
        //                 }
        //
        //                 if (!connected)
        //                 {
        //                     break;
        //                 }
        //             
        //             
        //             }
        //         }
        //         
        //         return connected;
        //     }
        //     
        //     // ArchetypeSkillNode skillNode = CanvasManager.Instance.characterMenuManager.ActiveArchetypeTree.archetype.skillNodesMap[skillNodeDataContainer.linkedSkillNodes[0].id];
        //     
        //     //return Dfs(skillNode);
        //     return Dfs(this);
        // }
        
        public bool IsNeighborConnectedToAnAssignRootNode(ArchetypeSkillNode startNode)
        {
            
            
            if (skillNodeDataContainer.archetypeSkillNodeType == ArchetypeSkillNodeType.Root)
            {
                return true;
            }
        
            HashSet<ArchetypeSkillNode> visitedSkillNodes = new HashSet<ArchetypeSkillNode>(){this};
            //HashSet<string> visitedSkillNodesID = new HashSet<string>(){skillNodeDataContainer.id};
        
           // bool connected = true;
            
        
            bool Dfs(ArchetypeSkillNode node)
            {
                
                if (node.skillNodeState == ArchetypeSkillNodeState.Assigned && node.skillNodeDataContainer.archetypeSkillNodeType == ArchetypeSkillNodeType.Root)
                {
                    return true;
                }
                
                
                visitedSkillNodes.Add(node);
                //todo placeholder fix from starting from title screen. Needs fix
                visitedSkillNodes.Add(CanvasManager.Instance.characterMenuManager.ActiveArchetypeTree.archetype.skillNodesMap[
                    skillNodeDataContainer.id]);
                //visitedSkillNodesID.Add(node.skillNodeDataContainer.id);
                
                
        
                foreach (ArchetypeSkillNodeDataContainer archetypeSkillNodeDataContainer in node.skillNodeDataContainer.linkedSkillNodes)
                {
                    ArchetypeSkillNode skillNode =
                        CanvasManager.Instance.characterMenuManager.ActiveArchetypeTree.archetype.skillNodesMap[
                            archetypeSkillNodeDataContainer.id];

                    if (skillNode.skillNodeState != ArchetypeSkillNodeState.Assigned)
                    {
                        visitedSkillNodes.Add(skillNode);
                        continue;
                    }
                    
                    
                    if (!visitedSkillNodes.Contains(skillNode))
                    {
                        if (Dfs(skillNode))
                        {
                            Debug.Log("fdhadjkslfhdsakjfgaskhjfgkjas");
                           // Debug.Log(skillNode.skillNodeDataContainer.name);
                            return true;
                        }
                    }
                    
                    
                    
                }
                
                return false;
            }
            
            return Dfs(startNode);
        }
        
        
    }
}