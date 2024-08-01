using UnityEngine;

namespace CodeReverie
{
    public class PartySlot
    {
        public Character character;
        //public CharacterController characterUnitController;


        public void Init()
        {
            // if (characterUnitController != null)
            // {
            //     GameObject.Destroy(characterUnitController.gameObject);
            // }

           
            
            if (character != null)
            {
               int count = 0;
               
               // foreach (Archetype archetype in character.availableArchetypes)
               // {
               //     ArchetypeTree archetypeTree = GameObject.Instantiate(archetype.info.archetypeTree, CanvasManager.Instance.characterMenuManager.treeHolder.transform);
               //     archetypeTree.gameObject.SetActive(false);
               //     archetypeTree.archetype = archetype;
               //     archetypeTree.Init();
               //     CanvasManager.Instance.characterMenuManager.archetypeTrees.Add(archetypeTree);
               // }

            }
        }

        public void SpawnCharactersss(Vector3 transform)
        {
            // characterUnitController = GameObject.Instantiate(character.info.characterUnitPF, transform, Quaternion.identity);
            // characterUnitController.gameObject.SetActive(false);
            // characterUnitController.character = character;
            // characterUnitController.GetComponent<Health>().SetHealth();
        }
        
    }
}