using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class PostProcessingMask : SerializedMonoBehaviour
    {
        public PostProcessingDepthOfFieldState depthOfFieldState;
        public LayerMask layerMask;

        private void Awake()
        {
            layerMask = gameObject.layer;
        }


        private void Update()
        {

            switch (depthOfFieldState)
            {
                case PostProcessingDepthOfFieldState.Close:
                    //layerMask = LayerMask.NameToLayer("No Post Processing");
                    gameObject.layer = LayerMask.NameToLayer("No Post Processing");
                    break;
                
                case PostProcessingDepthOfFieldState.Far:
                    //layerMask = LayerMask.NameToLayer("Post Processing");
                    gameObject.layer = LayerMask.NameToLayer("Post Processing");
                    break;
                
            }
            
            
            
        }
    }
}