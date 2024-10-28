using System;
using CodeReverie;
using Unity.Behavior;
using UnityEngine;
using Modifier = Unity.Behavior.Modifier;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "On End", description: "Ends current dialogue ",  category: "Events", id: "f1530bb182cbd40e6fa4232ae426b745")]
public partial class OnEndModifier : Modifier
{

    protected override Status OnStart()
    {
        EventManager.Instance.generalEvents.OpenMenuManager(CanvasManager.Instance.screenSpaceCanvasManager.hudManager);
        //EventManager.Instance.playerEvents.OnDialogueEnd(speaker);
        return Status.Waiting;
        CameraManager.Instance.UpdateCamera(PlayerManager.Instance.currentParty[0].characterController.transform);
        CameraManager.Instance.ToggleMainCamera();
        return Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

