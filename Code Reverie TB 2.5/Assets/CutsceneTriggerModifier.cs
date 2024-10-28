using System;
using CodeReverie;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.Playables;
using Modifier = Unity.Behavior.Modifier;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Cutscene Trigger", story: "Play [Cutscene]", category: "Events", id: "7914b69ee02eab851fa85eb078db1362")]
public partial class CutsceneTriggerModifier : Modifier
{
    [SerializeReference] public BlackboardVariable<PlayableDirector> Cutscene;

    protected override Status OnStart()
    {

        GameObject timelineObject = GameObject.Find("Timelines");
        PlayableDirector playableDirector = GameObject.Instantiate(Cutscene, timelineObject.transform) as PlayableDirector;
        
        EventManager.Instance.generalEvents.OpenMenuManager(CanvasManager.Instance.screenSpaceCanvasManager.hudManager);
        //EventManager.Instance.playerEvents.OnDialogueEnd(speaker);
            
        // CameraManager.Instance.UpdateCamera(PlayerManager.Instance.currentParty[0].characterController.transform);
        // CameraManager.Instance.ToggleMainCamera();
        
        playableDirector.Play();
        
        
        
        
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

