using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeReverie
{
    [DefaultExecutionOrder(-115)]
    public class CanvasManager : ManagerSingleton<CanvasManager>
    {
        //public GameObject gameMenu;
        public List<MenuManager> menuManagers = new List<MenuManager>();
        public HudManager hudManager;
        public DialogueManager dialogueManager;
        public VendorMenuManager vendorMenuManager;
        public SystemMenuManager systemMenuManager;
        public MapMenuManager mapMenuManager;
        public PauseMenuManager pauseMenuManager;

        public UIFade uiFade;
        public GameOverUIManager gameOverUIManager;

        public List<Tooltip> tooltips = new List<Tooltip>();

        public Tooltip itemTooltip;
        public Tooltip skillTooltip;

        public LoadDataConfirmationPopup loadDataConfirmationPopup;
        public SaveDataConfirmationPopup saveDataConfirmationPopup;
        
        
       // public bool inventoryActive;
        public bool skillMenuActive;
        public ItemMouseFollow itemMouseFollow;

        public Button journalMenuButton;
        public Button mapMenuButton;
        public Button characterMenuButton;
        public Button systemMenuButton;
        public Button craftingMenuButton;


        public LevelUpPopUp levelUpPopUp;
        
        
        protected override void Awake()
        {
            base.Awake();

            menuManagers = GetComponentsInChildren<MenuManager>(true).ToList();

            tooltips = new List<Tooltip>();
            
            tooltips.Add(itemTooltip);
            tooltips.Add(skillTooltip);
            
            foreach (MenuManager menuManager in menuManagers)
            {
                menuManager.SetListeners();
            }
            
            hudManager.partyHudPanelManager.SetListeners();
            loadDataConfirmationPopup.gameObject.SetActive(false);
            saveDataConfirmationPopup.gameObject.SetActive(false);
            
            
            
            // mapMenuButton.onClick.AddListener(() =>
            // {
            //     EventManager.Instance.generalEvents.OpenMenuManager(mapMenuManager);
            // });
            //
            //
            //
            // systemMenuButton.onClick.AddListener(() =>
            // {
            //     EventManager.Instance.generalEvents.OpenMenuManager(systemMenuManager);
            // });
            
           
            
            
            levelUpPopUp.gameObject.SetActive(false);
            
        }

        private void OnEnable()
        {
            EventManager.Instance.generalEvents.openToolTip += OpenToolTip;
            EventManager.Instance.generalEvents.closeToolTip += CloseToolTips;
            EventManager.Instance.playerEvents.onLevelUp += OpenLevelUpPopup;
            EventManager.Instance.generalEvents.onGamePause += TogglePauseMenu;
        }

        private void OnDisable()
        {
            EventManager.Instance.generalEvents.openToolTip -= OpenToolTip;
            EventManager.Instance.generalEvents.closeToolTip -= CloseToolTips;
            EventManager.Instance.playerEvents.onLevelUp -= OpenLevelUpPopup;
            
            EventManager.Instance.generalEvents.onGamePause -= TogglePauseMenu;
        }

        private void Start()
        {

            if (SceneManager.GetActiveScene().name == "Title Screen")
            {
                //OpenStartMenu();
            }
            else
            {
                //EventManager.Instance.generalEvents.OpenMenuManager(hudManager);
            }

            
        }

        private void OnDestroy()
        {
            foreach (MenuManager menuManager in menuManagers)
            {
                menuManager.UnsetListeners();
            }
        }


        private void Update()
        {
            if (SceneManager.GetActiveScene().name != "Title Screen")
            {
                if (GameManager.Instance.playerInput.GetButtonDown("Inventory"))
                {
                    SetInventoryMenuManager();

                }
                // else if (GameManager.Instance.playerInput.GetButtonDown("Skills Menu"))
                // {
                //     ToggleSkillsMenu();
                //
                // }
                // else if (GameManager.Instance.playerInput.GetButtonDown("Pause"))
                // {
                //     TogglePauseMenu();
                // } 
                else if (GameManager.Instance.playerInput.GetButtonDown("Map"))
                {
                    ToggleMap();
                }
            }


        }
        
        public void SetInventoryMenuManager()
        {
            
        }
        

        public void OpenVendorMenu()
        {
            if (!vendorMenuManager.gameObject.activeInHierarchy)
            {
                EventManager.Instance.generalEvents.OpenMenuManager(vendorMenuManager);
            }
        }

        public void OpenStartMenu()
        {
            //EventManager.Instance.generalEvents.OpenMenuManager(startMenuManager);
        }

        // public void ToggleStartMenu()
        // {
        //     
        //     if (!startMenuManager.gameObject.activeInHierarchy)
        //     {
        //         EventManager.Instance.generalEvents.OpenMenuManager(startMenuManager);
        //     } else
        //     {
        //         //EventManager.Instance.generalEvents.OpenMenuManager(hudManager);
        //    
        //     }
        //    
        // }
        

        public void TogglePauseMenu(bool isPaused)
        {

            if (isPaused)
            {
                EventManager.Instance.generalEvents.OpenMenuManager(pauseMenuManager);
            }
            else
            {
                EventManager.Instance.generalEvents.OpenMenuManager(hudManager);
            }
        }

        public void OpenToolTip(TooltipData tooltipData)
        {
            switch (tooltipData.toolTipType)
            {
                case "skill":
                    SetToolTip(skillTooltip, tooltipData);
                    break;
                case "item":
                    SetToolTip(itemTooltip, tooltipData);
                    break;
            }
        }

        public void SetToolTip(Tooltip tooltip, TooltipData tooltipData)
        {
            foreach (var tooltipObject in tooltips)
            {
                if (tooltipObject == tooltip)
                {
                    tooltipObject.gameObject.SetActive(true);
                    tooltipObject.SetToolTipData(tooltipData);
                }
                else
                {
                    tooltipObject.gameObject.SetActive(false);
                }
            }
        }

        public void CloseToolTips()
        {
            foreach (var tooltipObject in tooltips)
            {
                tooltipObject.gameObject.SetActive(false);
            }
        }

        public void ToggleMap()
        {
            
            if (!mapMenuManager.gameObject.activeInHierarchy)
            {
                EventManager.Instance.generalEvents.OpenMenuManager(mapMenuManager);
            } else
            {
                //EventManager.Instance.generalEvents.OpenMenuManager(hudManager);
           
            }
        }
        
        

        public void OpenLevelUpPopup()
        {

            if (hudManager.gameObject.activeInHierarchy)
            {
                levelUpPopUp.gameObject.SetActive(true);
            }
            
            
        }
        
        
        
        
        // public void SetActiveUiPanel()
        // {
        //     if (inventoryActive)
        //     {
        //         inventory.SetActive(true);
        //         hudManager.gameObject.SetActive(false);
        //     }
        //     else
        //     {
        //         inventory.SetActive(false);
        //         hudManager.gameObject.SetActive(true);
        //     }
        // }
        
        
    }
}