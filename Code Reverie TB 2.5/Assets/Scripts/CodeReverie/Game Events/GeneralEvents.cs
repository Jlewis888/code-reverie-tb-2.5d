using System;

namespace CodeReverie
{
    public class GeneralEvents
    {
        public Action<bool> onGamePause;
        public Action openInventory;
        public Action closeInventory;
        public Action<bool> toggleInventory;
        public Action<MenuManager, bool> toggleMenuManager;
        public Action<MenuManager> openMenuManager;
        public Action onGameRestart;
        public Action<bool> toggleCharacterSidePanelUI;
        public Action<string> onStartCutscene;


        public void OnGamePause(bool pause)
        {
            onGamePause?.Invoke(pause);
        }
        
        public void OpenInventory()
        {
            openInventory?.Invoke();
        }
        
        public void CloseInventory()
        {
            closeInventory?.Invoke();
        }
        
        public void ToggleInventory(bool open)
        {
            toggleInventory?.Invoke(open);
        }

        public Action<Character, string> characterUnitManagerReceiver;

        public void CharacterUnitReceiver(Character character, string message)
        {
            characterUnitManagerReceiver.Invoke(character, message);
        }


        public void ToggleMenuManager(MenuManager menuManager, bool open)
        {
            toggleMenuManager?.Invoke(menuManager, open);
        }
        
        
        
        public void OpenMenuManager(MenuManager menuManager)
        {
            openMenuManager?.Invoke(menuManager);
        }

        public Action onLoad;

        public void OnLoad()
        {
            onLoad?.Invoke();
        }


        public Action<TooltipData> openToolTip;
        public Action closeToolTip;

        public void OpenToolTip(TooltipData tooltipData)
        {
            openToolTip?.Invoke(tooltipData);
        }

        public void CloseToolTip()
        {
            closeToolTip?.Invoke();
        }
        
        public void OnGameRestart()
        {
            onGameRestart?.Invoke();
        }

        public Action<Character> onPauseMenuCharacterSwap;

        public void OnPauseMenuCharacterSwap(Character partySlot)
        {
            onPauseMenuCharacterSwap?.Invoke(partySlot);
        }
        
        public Action<PauseMenuNavigationState> onPauseMenuNavigationStateChange;

        public void OnPauseMenuNavigationStateChange(PauseMenuNavigationState pauseMenuNavigationState)
        {
            onPauseMenuNavigationStateChange?.Invoke(pauseMenuNavigationState);
        }
        
        
        public Action<PauseMenuSubNavigationState> onPauseMenuSubNavigationStateChange;

        public void OnPauseMenuSubNavigationStateChange(PauseMenuSubNavigationState pauseMenuNavigationState)
        {
            onPauseMenuSubNavigationStateChange?.Invoke(pauseMenuNavigationState);
        }

        public void ToggleCharacterSidePanelUI(bool setActive)
        {
            toggleCharacterSidePanelUI?.Invoke(setActive);
        }

        public Action<SkillSlotUI> onSkillSlotSelect;

        public void OnSkillSlotSelect(SkillSlotUI skillSlotUI)
        {
            onSkillSlotSelect?.Invoke(skillSlotUI);
        }

        public void OnStartCutscene(string cutsceneName)
        {
            onStartCutscene?.Invoke(cutsceneName);
        }
        
    }
}