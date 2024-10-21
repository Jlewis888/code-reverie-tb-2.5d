using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeReverie
{
    public class NodeSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        public DialogueGraphView dialogueGraph;
        public VisualElement target;
        public static List<SearchContextElement> elements;
        
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            // var tree = new List<SearchTreeEntry>
            // {
            //     new SearchTreeGroupEntry(new GUIContent("Create Node"), 0),
            //     new SearchTreeGroupEntry(new GUIContent("Dialogue"), 1),
            //     new SearchTreeEntry(new GUIContent("Dialogue Node", _indentationIcon))
            //     {
            //         level = 2, userData = new DialogueNodeBAK()
            //     },
            //     new SearchTreeEntry(new GUIContent("Comment Block",_indentationIcon))
            //     {
            //         level = 1,
            //         userData = new Group()
            //     }
            // };

            List<SearchTreeEntry> tree = new List<SearchTreeEntry>();
            tree.Add(new SearchTreeGroupEntry(new GUIContent("Nodes"), 0));
            elements = new List<SearchContextElement>();

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.CustomAttributes.ToList() != null)
                    {
                        //var attribute = type.CustomAttributes(typeof(NodeInfoAttribute));
                        var attribute = type.GetCustomAttribute(typeof(NodeInfoAttribute));
                        
                        if (attribute != null)
                        {
                            NodeInfoAttribute att = (NodeInfoAttribute)attribute;
                            var node = Activator.CreateInstance(type);
                            
                        
                            if (string.IsNullOrEmpty(att.Category))
                            {
                                continue;
                            }
                            elements.Add(new SearchContextElement(node, att.Category));
                        }
                    }
                }
            }
            

            //Sort by name
            elements.Sort((entry1, entry2) =>
            {
                string[] splits1 = entry1.category.Split('/');
                string[] splits2 = entry2.category.Split('/');

                for (int i = 0; i < splits1.Length; i++)
                {
                    if (i >= splits2.Length)
                    {
                        return 1;
                         
                    }

                    int value = splits1[i].CompareTo(splits2[i]);
                    if (value != 0)
                    {
                        // Make sure leaves go before nodes
                        if (splits1.Length != splits2.Length &&
                            (i == splits1[i].Length - 1 || i == splits2[i].Length - 1))
                        {
                            return splits1.Length < splits2.Length ? 1 : - 1;

                        }

                        return value;
                    }

                }
                
                return 0;
            });
            
            
            List<string> groups = new List<string>();

            foreach (SearchContextElement element in elements)
            {
                //string elementName = (string)element.GetType().GetProperty("Name").GetValue(element);
                string[] entryCategory = element.category.Split('/');
                string groupName = "";

                for (int i = 0; i < entryCategory.Length - 1; i++)
                {
                    groupName += entryCategory[i];

                    if (!groups.Contains(groupName))
                    {
                        tree.Add(new SearchTreeGroupEntry(new GUIContent(entryCategory[i]), i+1));
                        groups.Add(groupName);
                    }

                    groupName += "/";
                }
                
                
                SearchTreeEntry entry = new SearchTreeEntry(new GUIContent(entryCategory.Last()));
                entry.level = entryCategory.Length;
                entry.userData = new SearchContextElement(element.target, element.category);
                tree.Add(entry);
                
            }
            
            
            return tree;
        }
        
        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            //Editor window-based mouse position
            var mousePosition = dialogueGraph.ChangeCoordinatesTo(dialogueGraph,
                context.screenMousePosition - dialogueGraph.dialogueGraphEditorWindow.position.position);
            var graphMousePosition = dialogueGraph.contentViewContainer.WorldToLocal(mousePosition);

            SearchContextElement element = (SearchContextElement)SearchTreeEntry.userData;

            DialogueGraphNode graphNode = (DialogueGraphNode)element.target;
            graphNode.SetPosition(new Rect(graphMousePosition, new Vector2()));
            dialogueGraph.Add(graphNode);
            return true;
            
            // switch (SearchTreeEntry.userData)
            // {
            //     case DialogueNodeBAK dialogueNode:
            //         _graphViewBak.CreateNewDialogueNode("Dialogue Node",graphMousePosition);
            //         return true;
            //     case Group group:
            //         var rect = new Rect(graphMousePosition, _graphViewBak.DefaultCommentBlockSize);
            //         _graphViewBak.CreateCommentBlock(rect);
            //         return true;
            // }
            // return false;
        }
        
    }
}