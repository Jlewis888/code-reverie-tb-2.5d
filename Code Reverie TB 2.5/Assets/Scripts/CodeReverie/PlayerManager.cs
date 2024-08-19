using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeReverie
{
    [DefaultExecutionOrder(-118)]
    public class PlayerManager : ManagerSingleton<PlayerManager>, IDataPersistence
    {
        //public PlayerController playerControllerPF;

        public List<Character> availableCharacters = new List<Character>();
        public List<Character> currentParty;
        
        
        //public Party activeParty;

        public PlayerInventory inventory;

        public PlayerExperienceMap playerExperienceMap;
        public int currentLevel = 1;
        public float currentExp = 0;
        public int skillPoints = 0;

        public List<TeamStatModifier> teamStatsModifiers = new List<TeamStatModifier>();

        public bool acceptDataLoad;
        
        public bool debugging;
        
        //todo move or refactor later
        public Dictionary<int, GearSlot> combatItemSlots = new Dictionary<int, GearSlot>();
        public List<Interactable> interactables = new List<Interactable>();
        public Dictionary<int, int> accountExperienceMap= new Dictionary<int, int>();
        protected override void Awake()
        {
            base.Awake();
            teamStatsModifiers = new List<TeamStatModifier>();
            
            //todo move or refactor later
            combatItemSlots.Add(1, new GearSlot());
            combatItemSlots.Add(2, new GearSlot());
            combatItemSlots.Add(3, new GearSlot());
            combatItemSlots.Add(4, new GearSlot());
            combatItemSlots.Add(5, new GearSlot());
            
            
            accountExperienceMap.Add(1, 6);

            for (int i = 1; i < 99; i++)
            {
                accountExperienceMap.Add(i + 1, (int)(accountExperienceMap[i] * 1.7));
            }
           
        }

        private void Start()
        {
            
        }

        private void OnEnable()
        {
            EventManager.Instance.generalEvents.onLoad += Init;
            EventManager.Instance.generalEvents.onGameRestart += ResetParty;
            EventManager.Instance.combatEvents.onEnemyDeath += GiveExperienceOnEnemyDeath;
            EventManager.Instance.combatEvents.onPlayerDeath += AllPartyMembersDeathCheck;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }


        private void OnDisable()
        {
            EventManager.Instance.generalEvents.onLoad -= Init;
            EventManager.Instance.generalEvents.onGameRestart -= ResetParty;
            EventManager.Instance.combatEvents.onEnemyDeath -= GiveExperienceOnEnemyDeath;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }


        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (DataPersistenceManager.Instance.debugging)
            {
                SetNewGameData();
                Init();
                DataPersistenceManager.Instance.debugging = false;
            }
            if (GameSceneManager.Instance.fromLoadedData)
            {
                Init();
            }
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                //Experience = 10000;
                // AddCharacterToAvailablePartyPool("Cecil");
                // AddCharacterToActiveParty("Cecil");
                //
                // AddCharacterToAvailablePartyPool("Arcalia");
                // AddCharacterToActiveParty("Arcalia");
                //
                // Init();
                
                // ResetParty();
                
                // activeParty.ActivePartySlot.character.characterSkills.EquipActionSkill(SkillsManager.Instance.GetSkillById("FireBlastSkill"), 0);
                // activeParty.ActivePartySlot.character.characterSkills.EquipActionSkill(SkillsManager.Instance.GetSkillById("ArcStrikeSkill"), 1);
                // activeParty.ActivePartySlot.character.characterSkills.EquipActionSkill(SkillsManager.Instance.GetSkillById("RevolverSkill"), 2);
                // activeParty.ActivePartySlot.character.characterSkills.EquipActionSkill(SkillsManager.Instance.GetSkillById("FireballSkill"), 3);
                //
                // activeParty.ActivePartySlot.character.characterSkills.LearnSkill(SkillsManager.Instance.GetSkillById("FireBlastSkill"));
                // activeParty.ActivePartySlot.character.characterSkills.LearnSkill(SkillsManager.Instance.GetSkillById("ArcStrikeSkill"));
                // activeParty.ActivePartySlot.character.characterSkills.LearnSkill(SkillsManager.Instance.GetSkillById("RevolverSkill"));
                // activeParty.ActivePartySlot.character.characterSkills.LearnSkill(SkillsManager.Instance.GetSkillById("FireballSkill"));
                
                // activeParty.ActivePartySlot.character.EquipSkill(SkillsManager.Instance.GetSkillById("FlameCycloneSkill"), 4);
                // activeParty.ActivePartySlot.character.EquipSkill(SkillsManager.Instance.GetSkillById("FlameWardSkill"), 5);
                
                
                // currentParty[0].characterSkills.EquipActionSkill(SkillsManager.Instance.GetSkillById("FireBlastSkill"), 0);
                // currentParty[0].characterSkills.EquipActionSkill(SkillsManager.Instance.GetSkillById("ArcStrikeSkill"), 1);
                // currentParty[0].characterSkills.EquipActionSkill(SkillsManager.Instance.GetSkillById("RevolverSkill"), 2);
                // currentParty[0].characterSkills.EquipActionSkill(SkillsManager.Instance.GetSkillById("FireballSkill"), 3);
                //
                // currentParty[0].characterSkills.LearnSkill(SkillsManager.Instance.GetSkillById("FireBlastSkill"));
                // currentParty[0].characterSkills.LearnSkill(SkillsManager.Instance.GetSkillById("ArcStrikeSkill"));
                // currentParty[0].characterSkills.LearnSkill(SkillsManager.Instance.GetSkillById("RevolverSkill"));
                // currentParty[0].characterSkills.LearnSkill(SkillsManager.Instance.GetSkillById("FireballSkill"));
            }
            
            if (GameManager.Instance.playerInput.GetButtonDown("Interact"))
            {
                if (interactables.Count > 0)
                {
                    
                    if (interactables[0] != null)
                    {
                        if (interactables[0].interactableQueue.Count > 0)
                        {
                            interactables[0].interactableQueue.Peek().InteractOnPress(() =>
                            {
                                interactables[0].interactableQueue.Dequeue();

                                if (interactables[0].interactableQueue.Count < 1)
                                {
                                    interactables[0].Deactivate();
                                    interactables.Remove(interactables[0]);
                                }
                            });
                            
                        }
                    }
                }
            } 
            else if (GameManager.Instance.playerInput.GetButton("Interact"))
            {
                if (interactables.Count > 0)
                {
                    if (interactables[0] != null)
                    {
                        if (interactables[0].interactableQueue.Count > 0)
                        {
                            interactables[0].interactableQueue.Peek().InteractOnHold(() =>
                            {
                                interactables[0].interactableQueue.Dequeue();
                                
                                
                                if (interactables[0].interactableQueue.Count < 1)
                                {
                                    interactables[0].Deactivate();
                                    interactables.Remove(interactables[0]);
                                }
                            });
                            
                        }
                    }
                } 
            } 
            else if (GameManager.Instance.playerInput.GetButtonUp("Interact"))
            {
                if (interactables.Count > 0)
                {
                    if (interactables[0] != null)
                    {
                        if (interactables[0].interactableQueue.Count > 0)
                        {
                            interactables[0].interactableQueue.Peek().InteractOnPressUp(() => { });
                            
                        }
                    }
                } 
            }
        }

        public void Init()
        {
            InitializeParty();
            SetPartyUnits();
        }
        
        public Character GetAvailableCharacterPartySlot(string id)
        {
            return availableCharacters.Find(x => x.info.id == id);
        }

        public void InitializeParty()
        {
            
            foreach (Character partySlot in availableCharacters)
            {
                partySlot.Init();
            }
        }

        public void SetPartyUnits()
        {
            
            if (currentParty != null)
            {
                int count = 0;
                
                foreach (Character character in currentParty)
                {
                    character.SpawnCharacter(AreaManager.instance.defaultAreaSpawnPoint != null ? AreaManager.instance.defaultAreaSpawnPoint.position : Vector3.zero);
                    
                    
                    if (character == currentParty[0])
                    {
                        character.characterController.GetComponent<PlayerAIMovementController>().enabled = false;
                        character.characterController.GetComponent<PlayerMovementController>().enabled = true;
                        CameraManager.Instance.UpdateCamera(character.characterController.transform);
                    }
                    else
                    {
                        
                        character.characterController.GetComponent<PlayerMovementController>().enabled = false;
                        character.characterController.GetComponent<PlayerAIMovementController>().followTarget =
                            currentParty[count - 1].characterController.gameObject;
                    }
                    
                    character.characterController.gameObject.SetActive(true);
                    count++;
                }
                
                currentParty[0].characterSkills.EquipActionSkill(SkillsManager.Instance.GetSkillById("FireBlastSkill"), 0);
                currentParty[0].characterSkills.EquipActionSkill(SkillsManager.Instance.GetSkillById("ArcStrikeSkill"), 1);
                currentParty[0].characterSkills.EquipActionSkill(SkillsManager.Instance.GetSkillById("RevolverSkill"), 2);
                currentParty[0].characterSkills.EquipActionSkill(SkillsManager.Instance.GetSkillById("FireballSkill"), 3);
                
                currentParty[0].characterSkills.LearnSkill(SkillsManager.Instance.GetSkillById("FireBlastSkill"));
                currentParty[0].characterSkills.LearnSkill(SkillsManager.Instance.GetSkillById("ArcStrikeSkill"));
                currentParty[0].characterSkills.LearnSkill(SkillsManager.Instance.GetSkillById("RevolverSkill"));
                currentParty[0].characterSkills.LearnSkill(SkillsManager.Instance.GetSkillById("FireballSkill"));
            }
        }

        public void EnableCombatMode()
        {
            
        }


        public Vector3 GetCurrentCharacterPosition()
        {
            return currentParty[0].characterController.transform.position;
        }

        public void SetPlayerBattleMode()
        {
            foreach (Character character in currentParty)
            {
            
                //currentParty[i].characterController.GetComponent<CharacterBattleManager>().battlePosition = BattleManager.Instance.currentBattleArea.playerPositions.battlePositions[i];
                character.characterController.GetComponent<PlayerCombatController>().characterCombatState = CharacterCombatState.Moving;
                character.characterController.GetComponent<PlayerMovementController>().enabled = false;
                character.characterController.GetComponent<PlayerAIMovementController>().enabled = false;
            }
        }
        

        public void UnsetBattle()
        {
            int count = 0;
            
            foreach (Character character in currentParty)
            {
                  
                if (character == currentParty[0])
                {
                    character.characterController.GetComponent<PlayerAIMovementController>().enabled = false;
                    character.characterController.GetComponent<PlayerMovementController>().enabled = true;
                    CameraManager.Instance.UpdateCamera(character.characterController.transform);
                    
                }
                else
                {
                        
                    character.characterController.GetComponent<PlayerMovementController>().enabled = false;
                    character.characterController.GetComponent<PlayerAIMovementController>().enabled = true;
                    character.characterController.GetComponent<PlayerAIMovementController>().followTarget =
                        currentParty[count - 1].characterController.gameObject;
                }

                character.characterController.GetComponent<CharacterBattleManager>().characterTimelineGaugeState =
                    CharacterTimelineGaugeState.WaitPhase;
                
                count++;
            }
        }
        
        public List<CharacterBattleManager> GetCharacterBattleManagers()
        {

            List<CharacterBattleManager> party = new List<CharacterBattleManager>();

            foreach (Character character in currentParty)
            {
                party.Add(character.characterController.GetComponent<CharacterBattleManager>());
            }

            return party;
        }
        
        public void AllPartyMembersDeathCheck(CharacterUnitController enemy)
        {
            // if (GameOverDeathCheck())
            // {
            //     Debug.Log("All Party Members have died");
            //     CanvasManager.Instance.gameOverUIManager.gameObject.SetActive(true);
            // }
        }
        
        public bool GameOverDeathCheck()
        {
            for (int i = 0; i < currentParty.Count; i++)
            {
                if (currentParty[i].characterController.GetComponent<Health>().CurrentHealth >= 1)
                {
                    return false;
                }
            }
            
            return true;
        }
        
        public void GiveExperienceOnEnemyDeath(CharacterUnitController enemy)
        {
            //Experience = enemy.character.info.experienceToGive;

            foreach (Character character in currentParty)
            {
                character.Experience = enemy.character.info.experienceToGive;
            }
            
            
            CanvasManager.Instance.hudManager.notificationCenter.NotificationTrigger($"{enemy.character.info.experienceToGive} gained");
        }
        
        public void AddCharacterToActiveParty(string characterID)
        {
            if (availableCharacters.Count > 0)
            {
                if (availableCharacters.Any(
                        x =>
                        {
                            if (x != null)
                            {
                                return x.info.characterID == characterID;
                            }

                            return false;
                        }
                    )
                   )
                {
                    Character partySlot = availableCharacters.Find(x => x.info.characterID == characterID);
                    //activeParty.team.Add(partySlot);
                    currentParty.Add(partySlot);
                    partySlot.Init();
                    EventManager.Instance.playerEvents.OnPartyUpdate();
                }
            }
        }
        
        public void AddCharacterToAvailablePartyPool(string characterID)
        {
            CharacterDataContainer characterDataContainer = CharacterManager.Instance.GetCharacterByCharacterId(characterID);

            if (characterDataContainer == null)
            {
                Debug.Log("Character Container is Null");
                return;
            }


            if (availableCharacters == null)
            {
                availableCharacters = new List<Character>();
            }


            if (availableCharacters.Count > 0)
            {
                if (availableCharacters.Any(
                        x =>
                        {
                            if (x != null)
                            {
                                Debug.Log("Test");
                                return x.info.characterID == characterID;
                            }

                            return false;
                        }
                    )
                   )
                {
                    
                    Debug.Log("Character already in party");
                    return;
                }
            }


            PartySlot partySlot = new PartySlot();
            //partySlot.character = new Character(CharacterManager.Instance.GetCharacterByCharacterId(characterID));
            Character character = new Character(CharacterManager.Instance.GetCharacterByCharacterId(characterID));
            availableCharacters.Add(character);
        }
        
        public void ResetParty()
        {
            foreach (Character character in currentParty)
            {
                character.characterController.GetComponent<Health>().SetHealth();
                character.characterState = CharacterState.Alive;
            }
            
        }
        
        

        public void LoadData(string dataSlot)
        {
            if (ES3.FileExists(dataSlot))
            {
                Debug.Log("yo");
                if (ES3.KeyExists("availableCharacters", dataSlot))
                {
                    Debug.Log("Available Charactes exist");
                    availableCharacters = new List<Character>();
                    //ES3.LoadInto("availableCharacters", dataSlot, availableCharacters);
                    availableCharacters = ES3.Load<List<Character>>("availableCharacters", dataSlot);
                }
                else
                {
                    AddCharacterToAvailablePartyPool("Fullbody");
                    AddCharacterToAvailablePartyPool("Cecil");
                    AddCharacterToAvailablePartyPool("Arcalia");
                    
                    
                    // availableCharacters = new List<PartySlot>();
                    //
                    // availableCharacters.Add(new PartySlot());
                    // availableCharacters.Add(new PartySlot());
                    // availableCharacters.Add(new PartySlot());
                    // availableCharacters.Add(new PartySlot());
                    //
                    // availableCharacters[0].character = new Character(CharacterManager.Instance.GetCharacterByCharacterId("Fullbody"));
                    // availableCharacters[1].character = new Character(CharacterManager.Instance.GetCharacterByCharacterId("Rue"));
                    // availableCharacters[2].character = new Character(CharacterManager.Instance.GetCharacterByCharacterId("Arcalia"));
                    // availableCharacters[3].character = new Character(CharacterManager.Instance.GetCharacterByCharacterId("Cain"));
                }


                if (ES3.KeyExists("inventory", dataSlot))
                {
                    inventory = ES3.Load<PlayerInventory>("inventory", dataSlot);
                }
                else
                {
                    inventory = new PlayerInventory();
                }

                


                // if (ES3.KeyExists("parties", dataSlot))
                // {
                //     parties = ES3.Load<Dictionary<int, Party>>("parties", dataSlot);
                //
                //     foreach (Party party in parties.Values)
                //     {
                //         party.InitFromLoad();
                //     }
                // }
                // else
                // {
                //     parties = new Dictionary<int, Party>();
                //     parties.Add(1, new Party());
                //     parties.Add(2, new Party());
                //     parties.Add(3, new Party());
                //     parties.Add(4, new Party());
                // }

                if (ES3.KeyExists("currentParty", dataSlot))
                {
                    currentParty = ES3.Load<List<Character>>("currentParty", dataSlot);
                    
                    //todo Check on what this breaks
                    //currentParty.InitFromLoad();
                }
                else
                {
                    //activeParty = new Party();

                    currentParty = new List<Character>();
                    
                    //activeParty.team.Add(availableCharacters[0]);
                    currentParty.Add(availableCharacters[0]);
                    // currentParty.Add(availableCharacters[1]);
                    // currentParty.Add(availableCharacters[2]);
                    // activeParty.team.Add(availableCharacters[1]);
                    // activeParty.team.Add(availableCharacters[2]);
                    // activeParty.team.Add(availableCharacters[3]);

                    // activeParty.team[0].character.characterSkills.equippedActionSkills[1].skill = SkillsManager.Instance.CreateSkill("FireSkill");
                }

                if (ES3.KeyExists("currentLevel", dataSlot))
                {
                    currentLevel = ES3.Load<int>("currentLevel", dataSlot);
                }
                else
                {
                    currentLevel = 1;
                }

                if (ES3.KeyExists("currentExp", dataSlot))
                {
                    currentExp = ES3.Load<float>("currentExp", dataSlot);
                }
                else
                {
                    currentExp = 0;
                }

                if (ES3.KeyExists("skillPoints", dataSlot))
                {
                    skillPoints = ES3.Load<int>("skillPoints", dataSlot);
                }
                else
                {
                    skillPoints = 0;
                }
            }
            else
            {
                SetNewGameData();
            }
        }

        public void SaveData(string dataSlot)
        {
            ES3.Save("availableCharacters", availableCharacters, dataSlot);
            //ES3.Save("parties", parties, dataSlot);
            ES3.Save("currentParty", currentParty, dataSlot);
            ES3.Save("inventory", inventory, dataSlot);
            //ES3.Save("activeParty", activeParty, dataSlot);
            ES3.Save("currentLevel", currentLevel, dataSlot);
            ES3.Save("currentExp", currentExp, dataSlot);
            ES3.Save("skillPoints", skillPoints, dataSlot);
        }

        public void SetNewGameData()
        {
            Debug.Log("Set New Game Data");
            availableCharacters = new List<Character>();
            AddCharacterToAvailablePartyPool("Fullbody");
             AddCharacterToAvailablePartyPool("Cecil");
             AddCharacterToAvailablePartyPool("Arcalia");
            AddCharacterToAvailablePartyPool("Cecil");
            AddCharacterToAvailablePartyPool("Arcalia");
            inventory = new PlayerInventory();
            
            //activeParty = new Party();
            currentParty = new List<Character>();
            
            currentParty.Add(availableCharacters[0]);
            currentParty.Add(availableCharacters[1]);
            currentParty.Add(availableCharacters[2]);
            
            currentLevel = 1;
            currentExp = 0;
            skillPoints = 0;
        }

        
    }
}