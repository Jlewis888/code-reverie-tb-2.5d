using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeReverie
{
    public class BattleAreaManager : SerializedMonoBehaviour
    {
        // public static BattleAreaManager instance;
        // public BattleAreaManagerState battleAreaManagerState;
        // //[Range(1f, 6f)] 
        // public float areaRange;
        // public List<CharacterBattleManager> enemies;
        // public CapsuleCollider areaCollider;
        // public CapsuleCollider startingPositionCollider;
        //
        // [Header("Active Battle")]
        // public List<CharacterBattleManager> allUnits;
        // public List<CharacterBattleManager> playerUnits;
        // public List<CharacterBattleManager> enemyUnits;
        // public Dictionary<CharacterBattleManager, int> orderOfTurnsMap;
        // public bool pause;
        // public CharacterBattleManager selectedPlayerCharacter;
        // public List<CharacterBattleManager> selectedTargets;
        // public List<CharacterBattleManager> selectableTargets;
        // public int targetIndex;
        // public Queue<CharacterBattleManager> combatQueue;
        //
        // private void Awake()
        // {
        //     instance = this;
        //     enemies = GetComponentsInChildren<CharacterBattleManager>().ToList();
        //     //EventManager.Instance.combatEvents.onCombatEnter += SetEnemyPositions;
        //    
        // }
        //
        // private void Start()
        // {
        //     //SetEnemyPositionsTest();
        //     SetPreBattleConfigurations();
        // }
        //
        // private void Update()
        // {
        //     switch (battleAreaManagerState)
        //     {
        //         
        //     }
        // }
        //
        // public void SetPreBattleConfigurations()
        // {
        //     CanvasManager.Instance.screenSpaceCanvasManager.hudManager.combatHudManager.gameObject.SetActive(true);
        //     pause = true;
        //     combatQueue = new Queue<CharacterBattleManager>();
        //     GameManager.Instance.playerInput.controllers.maps.SetAllMapsEnabled(false);
        //     GameManager.Instance.playerInput.controllers.maps.SetMapsEnabled(true, 2);
        //     orderOfTurnsMap = new Dictionary<CharacterBattleManager, int>();
        //     
        //     playerUnits = PlayerManager.Instance.GetCharacterBattleManagers();
        //     allUnits.AddRange(playerUnits);
        //     
        //     enemyUnits = GetComponentsInChildren<CharacterBattleManager>().ToList();
        //     allUnits.AddRange(enemyUnits);
        //     
        //     DetermineOrderOfTurns();
        //     SetOrderOfTurnsLists();
        //     SetInitialCombatPositions();
        //     CanvasManager.Instance.screenSpaceCanvasManager.hudManager.combatHudManager.Init();
        //     battleAreaManagerState = BattleAreaManagerState.PreBattle;
        //     CameraManager.Instance.SetBattleCamera(this);
        // }
        //
        // public void InstantiateEnemyUnits()
        // {
        //     
        // }
        //
        // public void InstantiatePlayerUnits()
        // {
        //     
        // }
        //
        // public void SetAreaMaterial()
        // {
        //     Material[] materials = areaCollider.GetComponent<Renderer>().materials;
        //     Color color = materials[0].GetColor("_IntersectionColor");
        //     //Color color = materials[0].color;
        //     //materials[0].color = new Color(color.r, color.g, color.b, 1);
        //     materials[0].SetColor("_IntersectionColor", new Color(color.r, color.g, color.b, 1));
        // }
        //
        // public void SetInitialCombatPositions()
        // {
        //     foreach (CharacterBattleManager unit in allUnits)
        //     {
        //         float xBound = Random.Range(startingPositionCollider.bounds.min.x, startingPositionCollider.bounds.max.x);
        //         float zBound = Random.Range(startingPositionCollider.bounds.min.z, startingPositionCollider.bounds.max.z);
        //
        //         unit.transform.position = new Vector3(xBound, 0, zBound);
        //     }
        // }
        //
        // public void SetEnemyPositions()
        // {
        //     int count = 0;
        //     foreach (CharacterBattleManager enemy in enemies)
        //     {
        //         //enemy.battlePosition = enemyPositions.battlePositions[count];
        //         enemy.inCombat = true;
        //
        //         count++;
        //     }
        // }
        //
        // public void DetermineOrderOfTurns()
        // {
        //     
        //     foreach (CharacterBattleManager character in allUnits)
        //     {
        //
        //         int randomNum = Random.Range(1, 21);
        //
        //         orderOfTurnsMap.Add(character, randomNum + (int)character.GetComponent<CharacterStatsManager>().GetStat(StatAttribute.Initiative));
        //         character.SetStartingPosition();
        //         character.battleState = CharacterBattleState.MoveToStartingBattlePosition;
        //         character.inCombat = true;
        //         CameraManager.Instance.AddToTargetGroup(character.transform);
        //     }
        //     
        //     // SetOrderOfTurnsLists(orderOfTurnsMap, orderOfTurnsList);
        //     //
        //     //onDetermineOrderOfTurnsComplete();
        // }
        //
        // public void SetOrderOfTurnsLists()
        // {
        //
        //     List<CharacterBattleManager> orderOfTurnsList = new List<CharacterBattleManager>();
        //
        //     int count = 0;
        //     float totalInitiative = orderOfTurnsMap.Values.Sum();
        //     
        //     
        //     foreach (KeyValuePair<CharacterBattleManager, int> character in orderOfTurnsMap.OrderByDescending(key => key.Value))
        //     {
        //         orderOfTurnsList.Add(character.Key);
        //         float relativeInitiative = character.Value / totalInitiative;
        //         character.Key.cooldownTimer = (5f * .8f) - ((5f * .8f) * (1 - relativeInitiative));
        //     }
        //     
        // }
        //
        //
        // // private void OnTriggerEnter(Collider other)
        // // {
        // //     if (other.TryGetComponent(out ComponentTagManager tagManager))
        // //     {
        // //         
        // //         
        // //         if (tagManager.HasTag(ComponentTag.Player))
        // //         {
        // //             BattleManager.Instance.currentBattleArea = this;
        // //         }
        // //     }
        // // }
        // //
        // // private void OnTriggerExit(Collider other)
        // // {
        // //     if (other.TryGetComponent(out ComponentTagManager tagManager))
        // //     {
        // //         
        // //         
        // //         if (tagManager.HasTag(ComponentTag.Player) && BattleManager.Instance.battleManagerState == BattleManagerState.Inactive)
        // //         {
        // //             BattleManager.Instance.currentBattleArea = null;
        // //         }
        // //     }
        // // }
        // //
        // //
        // // private void OnTriggerEnter2D(Collider2D other)
        // // {
        // //     
        // //     if (other.TryGetComponent(out ComponentTagManager tagManager))
        // //     {
        // //         
        // //         
        // //         if (tagManager.HasTag(ComponentTag.Player))
        // //         {
        // //             BattleManager.Instance.currentBattleArea = this;
        // //         }
        // //     }
        // //     
        // // }
        // //
        // //
        // // private void OnTriggerExit2D(Collider2D other)
        // // {
        // //     if (other.TryGetComponent(out ComponentTagManager tagManager))
        // //     {
        // //         
        // //         
        // //         if (tagManager.HasTag(ComponentTag.Player) && BattleManager.Instance.battleManagerState == BattleManagerState.Inactive)
        // //         {
        // //             BattleManager.Instance.currentBattleArea = null;
        // //         }
        // //     }
        // // }
        
    }
}