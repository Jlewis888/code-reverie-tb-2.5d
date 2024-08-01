using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace CodeReverie
{
    public class SquadAction : Action
    {
        // protected PlayerAIController playerAIController;
        // protected PlayerCharacterUnit playerCharacterUnit;
        // protected Animator animator;
        //
        //
        // public override void OnAwake()
        // {
        //     playerAIController = GetComponent<PlayerAIController>();
        //     playerCharacterUnit = GetComponent<PlayerCharacterUnit>();
        //     animator = playerCharacterUnit.animator;
        // }
        //
        //
        //
        //
        // public void FaceTarget()
        // {
        //     if (playerAIController.target != null)
        //     {
        //         if (playerCharacterUnit.transform.position.x > playerAIController.target.transform.position.x)
        //         {
        //             playerCharacterUnit.activeUnitSprite.transform.localScale =  new Vector3(-1,1,1);
        //         }
        //         else
        //         {
        //             playerCharacterUnit.activeUnitSprite.transform.localScale = new Vector3(1,1,1);
        //         }
        //     }
        // }
        //
        //
        // public void FaceFollowTarget()
        // {
        //     if (playerCharacterUnit.transform.position.x > PlayerManager.Instance.currentParty[0].characterUnit.transform.position.x)
        //     {
        //         playerCharacterUnit.activeUnitSprite.transform.localScale =  new Vector3(-1,1,1);
        //     }
        //     else
        //     {
        //         playerCharacterUnit.activeUnitSprite.transform.localScale = new Vector3(1,1,1);
        //     }
        // }
    }
}