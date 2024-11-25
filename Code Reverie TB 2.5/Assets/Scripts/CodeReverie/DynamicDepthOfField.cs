using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;
//using UnityEngine.Rendering.Universal;

namespace CodeReverie
{
    public class DynamicDepthOfField : SerializedMonoBehaviour
    {
        // public Camera mainCamera;
        // public Volume postProcessingVolume;
        // private DepthOfField depthOfField;
        // private DepthOfField DepthOfField;
        //
        // private void Awake()
        // {
        //     postProcessingVolume = FindObjectOfType<Volume>();
        //     
        //     
        //     if (depthOfField == null)
        //     {
        //         postProcessingVolume.profile.TryGet<DepthOfField>(out var depthOfField);
        //         this.depthOfField = depthOfField;
        //     }
        //
        //     DepthOfField = depthOfField;
        // }
        //
        // void Start()
        // {
        //     // Retrieve the DepthOfField component from the volume profile
        //     if (postProcessingVolume.profile.TryGet<DepthOfField>(out depthOfField))
        //     {
        //         depthOfField.active = true;
        //     }
        // }
        //
        // void Update()
        // {
        //     // Cast a ray from the camera forward
        //     RaycastHit hit;
        //     if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit))
        //     {
        //         // Adjust the focus distance based on the hit distance
        //         depthOfField.focusDistance.value = hit.distance;
        //     }
        //     else
        //     {
        //         // Optionally, set a default focus distance if no object is hit
        //         depthOfField.focusDistance.value = 10f;
        //     }
        // }
    }
}