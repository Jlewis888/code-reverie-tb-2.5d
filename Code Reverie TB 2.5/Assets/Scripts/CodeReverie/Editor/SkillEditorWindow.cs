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
    public class SkillEditorWindow : OdinMenuEditorWindow
    {
        //private OdinMenuTree tree;
        
        [MenuItem("Tools/Skills/Skill Editor")]
        static void Open()
        {
            var window = GetWindow<SkillEditorWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true);
            tree.DefaultMenuStyle.IconSize = 28.00f;
            tree.Config.DrawSearchToolbar = true;

            List<SkillDataContainer> skillDataContainers = AssetDatabase.FindAssets("t:SkillDataContainer", null).Select(guid => AssetDatabase.LoadAssetAtPath<SkillDataContainer>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();
            
            //HashSet<OdinMenuItem> result = new HashSet<OdinMenuItem>();
            foreach (SkillDataContainer skillDataContainer in skillDataContainers)
            {
                
                string menuPath = "";
                
                switch (skillDataContainer.skillType)
                {
                    case SkillType.Passive:
                        menuPath = "Passive Skills";
                        break;
                    case SkillType.Basic:
                        menuPath = "Basic Attack Skills";
                        break;
                    case SkillType.Action:
                        menuPath = "Core(Action) Skills";
                        break;
                    
                    case SkillType.Dodge:
                        menuPath = "Movement Skills";
                        break;
                    case SkillType.AlchemicBurst:
                        menuPath = "Alchemic Burst Skills";
                        break;
                    case SkillType.None:
                        menuPath = "Unassigned Skilltype Skills";
                        break;
                    default:
                        menuPath = "Other Skills";
                        break;
                }
                
                string assetPath = AssetDatabase.GetAssetPath(skillDataContainer);
                
                // string[] splitTag = Path.GetDirectoryName(assetPath).Split("\\");
                //
                // Debug.Log(splitTag.LastOrDefault());
                
                string menuItemName = Regex.Replace(skillDataContainer.name, "(?<!^)_?([A-Z])", " $1");  
                
                OdinMenuItem odinMenuItem = new OdinMenuItem(tree, menuItemName, AssetDatabase.LoadAssetAtPath<SkillDataContainer>(assetPath));
                
                tree.AddMenuItemAtPath(menuPath, odinMenuItem);
                AddDragHandles(odinMenuItem);
            }
            
            
            //tree.AddAllAssetsAtPath("", "Assets", typeof(SkillDataContainer), true, true).ForEach(AddDragHandles);
            tree.EnumerateTree().AddIcons<SkillDataContainer>(x => x.icon);
            
            
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

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Skill")))
                {
                    ScriptableObjectCreator.ShowDialog<SkillDataContainer>("Assets/Scriptable Objects/Skills", obj =>
                    {
                        //obj.Name = obj.name;
                        base.TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
                        
                        
                        List<SkillDataContainerList> skillDataContainerLists = AssetDatabase.FindAssets("t:SkillDataContainerList", null).Select(guid => AssetDatabase.LoadAssetAtPath<SkillDataContainerList>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();


                        foreach (SkillDataContainerList skillDataContainerList in skillDataContainerLists)
                        {
                            skillDataContainerList.UpdateArchetypeDataContainers();
                        }
                    });
                }

                
                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Update")))
                {
                    ForceMenuTreeRebuild();
                    
                    List<SkillDataContainerList> skillDataContainerLists = AssetDatabase.FindAssets("t:SkillDataContainerList", null).Select(guid => AssetDatabase.LoadAssetAtPath<SkillDataContainerList>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();


                    foreach (SkillDataContainerList skillDataContainerList in skillDataContainerLists)
                    {
                        skillDataContainerList.UpdateArchetypeDataContainers();
                    }
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