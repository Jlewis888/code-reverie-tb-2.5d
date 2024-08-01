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
using Object = UnityEngine.Object;

namespace CodeReverie
{
    public class ArchetypeEditorWindow : OdinMenuEditorWindow
    {
        [MenuItem("Tools/Archetype/Archetype Editor")]
        static void Open()
        {
            var window = GetWindow<ArchetypeEditorWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true);
            tree.DefaultMenuStyle.IconSize = 28.00f;
            tree.Config.DrawSearchToolbar = true;

            
            List<ArchetypeDataContainer> archetypeDataContainers = AssetDatabase.FindAssets("t:ArchetypeDataContainer", null).Select(guid => AssetDatabase.LoadAssetAtPath<ArchetypeDataContainer>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();

            archetypeDataContainers.ForEach(archetypeDataContainer =>
            {
                string menuPath = Regex.Replace(archetypeDataContainer.name, "(?<!^)_?([A-Z])", " $1");
                
                string assetPath = AssetDatabase.GetAssetPath(archetypeDataContainer);
                OdinMenuItem odinMenuItem = new OdinMenuItem(tree, archetypeDataContainer.name, AssetDatabase.LoadAssetAtPath<ArchetypeDataContainer>(assetPath));

                if (archetypeDataContainer.icon != null)
                {
                    tree.Add(menuPath, null, archetypeDataContainer.icon);
                }
                
                tree.AddMenuItemAtPath(menuPath, odinMenuItem);
                //AddDragHandles(odinMenuItem);

                
                string assetPathClean = assetPath.Replace($"/{archetypeDataContainer.name}.asset", "");
                List<ArchetypeSkillNodeDataContainer> archetypeSkillNodeDataContainers = AssetDatabase.FindAssets("t:ArchetypeSkillNodeDataContainer", new []{assetPathClean}).Select(guid => AssetDatabase.LoadAssetAtPath<ArchetypeSkillNodeDataContainer>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();

                string skillNodeMenuPath = $"{menuPath}/Skill Nodes";
                
                archetypeSkillNodeDataContainers.ForEach(archetypeSkillNodeDataContainer =>
                {


                    string menuItemName = String.IsNullOrEmpty(archetypeSkillNodeDataContainer.id)
                        ? archetypeSkillNodeDataContainer.name
                        : archetypeSkillNodeDataContainer.id;  
                    
                    string skillNodeAssetPath = AssetDatabase.GetAssetPath(archetypeSkillNodeDataContainer);
                    OdinMenuItem skillNodeMenuItem = new OdinMenuItem(tree, menuItemName, AssetDatabase.LoadAssetAtPath<ArchetypeSkillNodeDataContainer>(skillNodeAssetPath));
                    
                    tree.AddMenuItemAtPath(skillNodeMenuPath, skillNodeMenuItem);
                    AddDragHandles(skillNodeMenuItem);
                });
                
                AddDragHandles(odinMenuItem);

            });
            
            tree.EnumerateTree().AddIcons<ArchetypeSkillNodeDataContainer>(x =>
            {
                if (x.skillDataContainer != null)
                {
                    return x.skillDataContainer.icon;
                }

                return null;
            });

            tree.AddAllAssetsAtPath("", "Assets", typeof(ArchetypeDataContainer), true);
            // tree.AddAllAssetsAtPath("", "Assets", typeof(ArchetypeSkillNodeDataContainer), true);
            //


            // tree.EnumerateTree().ForEach(item =>
            // {
            //     Debug.Log(item.Parent.Value);
            // });
            
            return tree;
        }
        
        private void AddDragHandles(OdinMenuItem menuItem)
        {
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

                
                
                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Archetype")))
                {
                    ScriptableObjectCreator.ShowDialog<ArchetypeDataContainer>("Assets/Archetypes", obj =>
                    {
                        //obj.Name = obj.name;
                        base.TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
                        
                        string assetPath = AssetDatabase.GetAssetPath(obj);
                        
                        assetPath = assetPath.Replace($"/{obj.name}.asset", "");

                        if (!Directory.Exists($"{assetPath}/Skill Nodes"))
                        {
                            AssetDatabase.CreateFolder(assetPath, "Skill Nodes");
                        }
                        
                        
                        
                        Object archetypeTreeSource = Resources.Load("Archetype Tree");
                        GameObject archetypeTreeSourceObjectSource = (GameObject)PrefabUtility.InstantiatePrefab(archetypeTreeSource);
                        GameObject archetypeTreeSourceObject  = PrefabUtility.SaveAsPrefabAsset(archetypeTreeSourceObjectSource, $"{assetPath}/{archetypeTreeSourceObjectSource}.prefab");
                        
                        
                        // List<SkillDataContainerList> skillDataContainerLists = AssetDatabase.FindAssets("t:SkillDataContainerList", null).Select(guid => AssetDatabase.LoadAssetAtPath<SkillDataContainerList>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();
                        //
                        //
                        // foreach (SkillDataContainerList skillDataContainerList in skillDataContainerLists)
                        // {
                        //     skillDataContainerList.UpdateArchetypeDataContainers();
                        // }
                    });
                }
                
                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Archetype Skill Node")))
                {
                    ScriptableObjectCreator.ShowDialog<ArchetypeSkillNodeDataContainer>("Assets/Archetypes", obj =>
                    {
                        //obj.Name = obj.name;
                        base.TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
                        
                    });
                }
                
                
                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Update")))
                {
                    ForceMenuTreeRebuild();
                    
                    // List<SkillDataContainerList> skillDataContainerLists = AssetDatabase.FindAssets("t:SkillDataContainerList", null).Select(guid => AssetDatabase.LoadAssetAtPath<SkillDataContainerList>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();
                    //
                    //
                    // foreach (SkillDataContainerList skillDataContainerList in skillDataContainerLists)
                    // {
                    //     skillDataContainerList.UpdateArchetypeDataContainers();
                    // }
                }
                

                // if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Character")))
                // {
                //     ScriptableObjectCreator.ShowDialog<Character>("Assets/Plugins/Sirenix/Demos/Sample - RPG Editor/Character", obj =>
                //     {
                //         obj.Name = obj.name;
                //         base.TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
                //     });
                // }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
            
        }
    }
}