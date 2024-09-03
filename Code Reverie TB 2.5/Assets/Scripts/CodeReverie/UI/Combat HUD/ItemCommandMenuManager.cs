using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class ItemCommandMenuManager : CommandMenuManager
    {
        public GameObject commandMenuNavigationButtonHolder;
        public ItemCommandMenuNavigationButton itemCommandMenuNavigationButtonPF;
        
        private void Awake()
        {
            if (commandMenuNavigation == null)
            {
                SetCommandNavigation();
            }

        }
        
        private void OnEnable()
        {
            SetItemButtons();
            commandMenuNavigation.SetFirstItem();
            //EventManager.Instance.combatEvents.OnPlayerSelectTarget(SelectedNavigationButton.GetComponent<TargetCommandMenuNavigationButton>().characterBattleManager);
        }
        
        private void Update()
        {
            
            if (GameManager.Instance.playerInput.GetButtonDown("Confirm Action"))
            {
                ConfirmAction();
            }
            
            if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
            {
                CanvasManager.Instance.hudManager.commandMenu.combatCommandMenu.ToggleCommandAction();
            }
            
            commandMenuNavigation.NavigationInputUpdate();
        }
        
        
        public void SetCommandNavigation()
        {
            commandMenuNavigation = new CommandMenuNavigation();
        }
        
        public void Clear()
        {
            foreach (Transform child in commandMenuNavigationButtonHolder.transform)
            {
                Destroy(child.gameObject);
            }
        }
        
        
        public void SetItemButtons()
        {
            
            if (commandMenuNavigation == null)
            {
                SetCommandNavigation();
            }
            
            commandMenuNavigation.ClearNavigationList();
            
            Clear();
            
            
            foreach (Item item in PlayerManager.Instance.inventory.items)
            {
                
                if (item.info.combatItem)
                {
                    ItemCommandMenuNavigationButton itemCommandMenuNavigationButton =
                        Instantiate(itemCommandMenuNavigationButtonPF, commandMenuNavigationButtonHolder.transform);


                    itemCommandMenuNavigationButton.item = item;
                    itemCommandMenuNavigationButton.nameText.text = item.info.itemName;
                    itemCommandMenuNavigationButton.itemCountText.text = item.amount.ToString();
                    commandMenuNavigation.Add(itemCommandMenuNavigationButton);
                }
                
            }
        }
        
        
        public void ConfirmAction()
        {

            CombatManager.Instance.SetSelectableTargets();
            CombatManager.Instance.selectedPlayerCharacter.selectedItem = commandMenuNavigation.SelectedNavigationButton.GetComponent<ItemCommandMenuNavigationButton>().item;
            CanvasManager.Instance.hudManager.commandMenu.combatCommandMenu.ToggleTargetMenu(this);
        }
        
    }
}