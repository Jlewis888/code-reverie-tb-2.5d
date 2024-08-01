using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace CodeReverie
{
    public class QuestEditorWindow : OdinMenuEditorWindow
    {
        //private OdinMenuTree tree;
        
        [MenuItem("Tools/Quest/Quest Editor")]
        static void Open()
        {
            var window = GetWindow<QuestEditorWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true);
            tree.DefaultMenuStyle.IconSize = 28.00f;
            tree.Config.DrawSearchToolbar = true;

            List<QuestDataContainer> questDataContainers = AssetDatabase.FindAssets("t:QuestDataContainer", null).Select(guid => AssetDatabase.LoadAssetAtPath<QuestDataContainer>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();
            
            //HashSet<OdinMenuItem> result = new HashSet<OdinMenuItem>();
            foreach (QuestDataContainer questDataContainer in questDataContainers)
            {
                
                string menuPath = Regex.Replace(questDataContainer.name, "(?<!^)_?([A-Z])", " $1");
                
                string assetPath = AssetDatabase.GetAssetPath(questDataContainer);
                string assetPathClean = assetPath.Replace($"/{questDataContainer.name}.asset", "");
              
                
                string menuItemName = Regex.Replace(questDataContainer.name, "(?<!^)_?([A-Z])", " $1");  
                
                OdinMenuItem odinMenuItem = new OdinMenuItem(tree, "Quest", AssetDatabase.LoadAssetAtPath<QuestDataContainer>(assetPath));
                
                tree.AddMenuItemAtPath(menuPath, odinMenuItem);
                
                
                AddDragHandles(odinMenuItem);
                
                
                
                
                
                List<QuestStepDataContainer> questStepDataContainers = AssetDatabase.FindAssets("t:QuestStepDataContainer", new []{assetPathClean}).Select(guid => AssetDatabase.LoadAssetAtPath<QuestStepDataContainer>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();
                

                foreach (QuestStepDataContainer questStepDataContainer in questStepDataContainers)
                {
                    
                    
                    
                    string questStepAssetPath = AssetDatabase.GetAssetPath(questStepDataContainer);
                    string questStepAssetPathClean = questStepAssetPath.Replace($"/{questStepDataContainer.name}.asset", "");

                    string folderName = new DirectoryInfo(Path.GetDirectoryName(questStepAssetPath)).Name;
                    
                    string questStepMenuItemName = String.IsNullOrEmpty(questStepDataContainer.id)
                        ? questStepDataContainer.name
                        : questStepDataContainer.id;  
                    
                    string questStepMenuPath = $"{menuPath}/Quest Steps/{folderName}";
                    
                    
                    
                    OdinMenuItem questStepNodeMenuItem = new OdinMenuItem(tree, "Quest Step", AssetDatabase.LoadAssetAtPath<QuestStepDataContainer>(questStepAssetPath));
                    
                    tree.AddMenuItemAtPath(questStepMenuPath, questStepNodeMenuItem);
                    AddDragHandles(questStepNodeMenuItem);
                    
                    
                    List<QuestStepObjectiveData> questObjectiveDataList = AssetDatabase.FindAssets("t:QuestStepObjectiveData", new []{questStepAssetPathClean}).Select(guid => AssetDatabase.LoadAssetAtPath<QuestStepObjectiveData>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();
                    
                    foreach (QuestStepObjectiveData questObjective in questObjectiveDataList)
                    {
                        string questObjectiveMenuItemName = String.IsNullOrEmpty(questObjective.questId)
                            ? questObjective.name
                            : questObjective.questId;  
                        
                        //string questStepObjectiveMenuPath = $"{questStepMenuPath}/Objectives/{questObjectiveMenuItemName}";
                        string questStepObjectiveMenuPath = $"{questStepMenuPath}/Objectives";
                        
                        
                        string questObjectiveAssetPath = AssetDatabase.GetAssetPath(questObjective);
                        OdinMenuItem questObjectiveMenuItem = new OdinMenuItem(tree, questObjectiveMenuItemName, AssetDatabase.LoadAssetAtPath<QuestStepObjectiveData>(questObjectiveAssetPath));
                    
                        tree.AddMenuItemAtPath(questStepObjectiveMenuPath, questObjectiveMenuItem);
                        AddDragHandles(questObjectiveMenuItem);
                    }
                    
                }
                
                
            }
            
            
            //tree.AddAllAssetsAtPath("", "Assets", typeof(SkillDataContainer), true, true).ForEach(AddDragHandles);
            //tree.EnumerateTree().AddIcons<QuestDataContainer>(x => x.icon);
            
            
            return tree;
        }
        
        private void AddDragHandles(OdinMenuItem menuItem)
        {
            //Debug.Log(menuItem.Value);

            // OdinMenuItem odinMenuItem = new OdinMenuItem();
            //
            // tree.AddMenuItemAtPath("Passive", menuItem);
            
            menuItem.OnDrawItem += x => DragAndDropUtilities.DragZone(menuItem.Rect, menuItem.Value, false, false);
        }
        
        
        protected override void OnBeginDrawEditors()
        {
            var selected = this.MenuTree.Selection.FirstOrDefault();
            var toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;
            
            
            // Draws a toolbar with the name of the currently selected menu item.
            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                if (selected != null)
                {
                    GUILayout.Label(selected.Name);
                }

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Quest")))
                {
                    ScriptableObjectCreator.ShowDialog<QuestDataContainer>("Assets/Scriptable Objects/Quests", obj =>
                    {
                       
                        base.TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
                    });
                }
                
                
                
                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Quest Step")))
                {
                    ScriptableObjectCreator.ShowDialog<QuestStepDataContainer>("Assets/Scriptable Objects/Quests", obj =>
                    {
                       
                        base.TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
                    });
                }
                
                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Quest Step Objective")))
                {
                    ScriptableObjectCreator.ShowDialog<QuestStepObjectiveData>("Assets/Scriptable Objects/Quests", obj =>
                    {
                       
                        base.TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
                    });
                }

                
                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Update")))
                {
                    ForceMenuTreeRebuild();
                    
                    List<QuestListContainer> questListContainers = AssetDatabase.FindAssets("t:QuestListContainer", null).Select(guid => AssetDatabase.LoadAssetAtPath<QuestListContainer>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();


                    foreach (QuestListContainer questListContainer in questListContainers)
                    {
                        questListContainer.UpdateQuest();
                    }
                }
                
            }
            
            SirenixEditorGUI.EndHorizontalToolbar();
            
        }
        
        
    }
}