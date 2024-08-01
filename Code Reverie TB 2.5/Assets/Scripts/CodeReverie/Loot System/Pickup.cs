using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeReverie
{
    public class Pickup : SerializedMonoBehaviour
    {
        public string itemId;
        public Item item;
        public SpriteRenderer spriteRenderer;
        public float duration = 1f;
        public AnimationCurve animationCurve;
        public float heightY = 3;
        public CharacterUnit characterUnit;
        public float radius;
        public TMP_Text itemName;
        public bool inRange;
        

        private void Start()
        {
            itemName.text = item.info.itemName;
            itemName.color = ItemManager.Instance.itemRarityMap[item.info.itemRarity].color;
            spriteRenderer.sprite = item.info.lootIcon;
            Vector3 target = transform.position + (Vector3)(radius * Random.insideUnitCircle);
            StartCoroutine(LootDropAnimation(transform.position, target));
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Vector3 target = transform.position + (Vector3)(radius * Random.insideUnitCircle);
                //StartCoroutine(DropLoot(transform.position, target));
                
                StartCoroutine(LootDropAnimation(transform.position, target));
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (item.info.isInteractivePickup && inRange)
                {
                    ItemPickup();
                }
            }
            
        }


        IEnumerator LootDropAnimation(Vector3 startPosition, Vector3 endPosition)
        {
            float timePassed = 0f;
            while (timePassed < duration)
            {
                timePassed += Time.deltaTime;
                float linearT = timePassed / duration;
                float heightT = animationCurve.Evaluate(linearT);
                float height = Mathf.Lerp(0f, heightY, heightT);

                transform.position = Vector2.Lerp(startPosition, endPosition, linearT) + new Vector2(0f, height);

                yield return null;
            }
        }


        public void ItemPickup()
        {
            PlayerManager.Instance.inventory.AddItem(item);
            EventManager.Instance.playerEvents.OnItemPickup(item.info.id, 1);
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag.Equals("Player"))
            {
                //TODO Uncomment and fix
                // if (ItemManager.Instance.GetItemById(itemId) is EquippableItem)
                // {
                //     EquippableItem item = new EquippableItem(ItemManager.Instance.GetItemById(itemId).itemData as EquippableItemDataContainer);
                //     EventManager.Instance.inventoryEvents.OnItemPickup(item);
                // } else if (ItemManager.Instance.GetItemById(itemId) is ConsumableItem)
                // {
                //     ConsumableItem item = new ConsumableItem(ItemManager.Instance.GetItemById(itemId).itemData as ConsumableItemDataContainer);
                //     EventManager.Instance.inventoryEvents.OnItemPickup(item);
                // }


                if (item.info.isInteractivePickup)
                {
                    inRange = true;
                }
                else
                {
                   ItemPickup();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag.Equals("Player"))
            {
                if (item.info.isInteractivePickup)
                {
                    inRange = false;
                } 
            }
        }
    }
}