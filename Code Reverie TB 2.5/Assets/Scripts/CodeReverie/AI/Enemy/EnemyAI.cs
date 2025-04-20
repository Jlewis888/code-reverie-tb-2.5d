using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeReverie
{
    public class EnemyAI : SerializedMonoBehaviour
    {

        public CharacterDataContainer characterInfo;
        public EnemyActiveWeapon activeWeapon;
        public Projectile projectile;
        public GameObject firePoint;
        public bool debugging;
        public AttackIndicatorsManager attackIndicatorsManager;
        public GameObject attackCirclePF;
        public Vector3 battlePosition;
        public bool inCombat;
        public List<CharacterDataContainer> enemyList;
        
        public enum State
        {
            Roaming,
            Dead
        }

        public State state;
        //private EnemyPathfinding enemyPathfinding;

        private void Awake()
        {

            // if (debugging)
            // {
            //     
            //     CharacterController characterUnitController = Instantiate(characterInfo.characterUnitPF, transform.position, Quaternion.identity);
            //     Destroy(gameObject);
            //     
            //     characterUnitController.gameObject.SetActive(true);
            //     characterUnitController.character = new Character(characterInfo);
            //     
            //     
            //     return;
            // }
            
            
            
            EventManager.Instance.combatEvents.onEnemyDeath += OnEnemyDeath;
            
            
            state = State.Roaming;
            
        }
        
        // private void OnEnable()
        // {
        //     if (!debugging)
        //     {
        //         Debug.Log("fhdkjdf fdjhkfklajfld afhjdklajsfhal fdhaksljfhdasklj fadskjlhafkljd");
        //         EventManager.Instance.combatEvents.onEnemyDeath += OnEnemyDeath;
        //     }
        //
        //     
        // }
        //
        // private void OnDisable()
        // {
        //     EventManager.Instance.combatEvents.onEnemyDeath -= OnEnemyDeath;
        // }

        private void OnDestroy()
        {
            EventManager.Instance.combatEvents.onEnemyDeath -= OnEnemyDeath;
        }


        public Vector2 GetRoamingPosition()
        {
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }

        public void OnEnemyDeath(CharacterUnitController characterController)
        {
            
            if (characterController == GetComponent<CharacterUnitController>())
            {
                Debug.Log("On Enemy Death");
                //attackIndicatorsManager.CloseAllIndicators();
            }
        }
        
    }
}