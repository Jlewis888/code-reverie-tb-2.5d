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
    public class ItemEditorWindow : OdinMenuEditorWindow
    {
        [MenuItem("Tools/Items/Item Editor")]
        static void Open()
        {
            var window = GetWindow<ItemEditorWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true);
            tree.DefaultMenuStyle.IconSize = 28.00f;
            tree.Config.DrawSearchToolbar = true;
            
            List<ItemInfo> itemInfoList = AssetDatabase.FindAssets("t:ItemInfo", null).Select(guid => AssetDatabase.LoadAssetAtPath<ItemInfo>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();



            foreach (ItemInfo itemInfo in itemInfoList)
            {

                string menuPath = "";

                switch (itemInfo.itemType)
                {
                    case ItemType.Consumable:
                        menuPath = "Consumables";
                        break;
                    case ItemType.Material:
                        menuPath = "Materials";
                        break;

                    default:
                        menuPath = "Other Items";
                        break;
                }
                
                string assetPath = AssetDatabase.GetAssetPath(itemInfo);
                
                string menuItemName = Regex.Replace(itemInfo.name, "(?<!^)_?([A-Z])", " $1");  
                
                OdinMenuItem odinMenuItem = new OdinMenuItem(tree, menuItemName, AssetDatabase.LoadAssetAtPath<ItemInfo>(assetPath));
                
                tree.AddMenuItemAtPath(menuPath, odinMenuItem);
                AddDragHandles(odinMenuItem);
                
            }
            
            tree.EnumerateTree().AddIcons<ItemInfo>(x => x.uiIcon);
            
            
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

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Item")))
                {
                    ScriptableObjectCreator.ShowDialog<ItemInfo>("Assets/Scriptable Objects/Items", obj =>
                    {
                        //obj.Name = obj.name;
                        base.TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor


                        System.Collections.Generic.List<BaseItemDetailsListContainer> baseItemDetailsListContainers = AssetDatabase.FindAssets("t:BaseItemDetailsListContainer", null).Select(guid => AssetDatabase.LoadAssetAtPath<BaseItemDetailsListContainer>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();


                        foreach (BaseItemDetailsListContainer baseItemDetailsListContainer in baseItemDetailsListContainers)
                        {
                            baseItemDetailsListContainer.Update();
                        }
                    });
                }

                
                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Update")))
                {
                    ForceMenuTreeRebuild();
                    
                    System.Collections.Generic.List<BaseItemDetailsListContainer> baseItemDetailsListContainers = AssetDatabase.FindAssets("t:BaseItemDetailsListContainer", null).Select(guid => AssetDatabase.LoadAssetAtPath<BaseItemDetailsListContainer>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();


                    foreach (BaseItemDetailsListContainer baseItemDetailsListContainer in baseItemDetailsListContainers)
                    {
                        baseItemDetailsListContainer.Update();
                    }
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
            
        }
        
    }
}