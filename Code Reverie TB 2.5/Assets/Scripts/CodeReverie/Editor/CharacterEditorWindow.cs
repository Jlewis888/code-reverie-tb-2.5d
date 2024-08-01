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
    public class CharacterEditorWindow : OdinMenuEditorWindow
    {
        [MenuItem("Tools/Characters/Character Editor")]
        static void Open()
        {
            var window = GetWindow<CharacterEditorWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true);
            tree.DefaultMenuStyle.IconSize = 28.00f;
            tree.Config.DrawSearchToolbar = true;

            
            List<CharacterDataContainer> characterDataContainers = AssetDatabase.FindAssets("t:CharacterDataContainer", null).Select(guid => AssetDatabase.LoadAssetAtPath<CharacterDataContainer>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();

            characterDataContainers.ForEach(characterDataContainer =>
            {
                string menuPath = "";
                
                switch (characterDataContainer.characterType)
                {
                    case CharacterType.Playable:
                        menuPath = "Playable Characters";
                        break;
                    default:
                        menuPath = "Other Characters";
                        break;
                }
                
                
                string assetPath = AssetDatabase.GetAssetPath(characterDataContainer);
                OdinMenuItem odinMenuItem = new OdinMenuItem(tree, characterDataContainer.name, AssetDatabase.LoadAssetAtPath<CharacterDataContainer>(assetPath));
                
                tree.AddMenuItemAtPath(menuPath, odinMenuItem);
                //AddDragHandles(odinMenuItem);

            });
            
            tree.EnumerateTree().AddIcons<CharacterDataContainer>(x => x.characterPortrait);
            
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

                
                
                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Character")))
                {
                    ScriptableObjectCreator.ShowDialog<CharacterDataContainer>("Assets/Prefabs/Characters", obj =>
                    {
                        //obj.Name = obj.name;
                        base.TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
                        
                        // string assetPath = AssetDatabase.GetAssetPath(obj);
                        //
                        // assetPath = assetPath.Replace($"/{obj.name}.asset", "");
                        //
                        // if (!Directory.Exists($"{assetPath}/Skill Nodes"))
                        // {
                        //     AssetDatabase.CreateFolder(assetPath, "Skill Nodes");
                        // }
                        //
                        //
                        //
                        // Object archetypeTreeSource = Resources.Load("Archetype Tree");
                        // GameObject archetypeTreeSourceObjectSource = (GameObject)PrefabUtility.InstantiatePrefab(archetypeTreeSource);
                        // GameObject archetypeTreeSourceObject  = PrefabUtility.SaveAsPrefabAsset(archetypeTreeSourceObjectSource, $"{assetPath}/{archetypeTreeSourceObjectSource}.prefab");
                        //
                        
                       
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
                
            }
            SirenixEditorGUI.EndHorizontalToolbar();
            
        }
    }
}