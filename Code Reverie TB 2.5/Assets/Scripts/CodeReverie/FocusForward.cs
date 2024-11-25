using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;
//using UnityEngine.Rendering.Universal;

namespace CodeReverie
{
    public class FocusForward : SerializedMonoBehaviour
    {
        // [Header("Options")] public float minDistance = 0.1f;
        // public float maxDistance = 15f;
        // public float fousSpeed = 0.15f;
        // public bool useSphereCast = true;
        // public float sphereCastRadius = 0.5f;
        //
        //
        // private Volume _volume;
        // private DepthOfField _depthOfField;
        // private Coroutine _focusCoroutine;
        // private float _targetDistance;
        //
        // private Volume Volume => GetPostProcessVolume();
        // private DepthOfField DepthOfField;
        //
        // private void Awake()
        // {
        //     if (_depthOfField == null)
        //     {
        //         Volume.profile.TryGet<DepthOfField>(out var depthOfField);
        //         _depthOfField = depthOfField;
        //     }
        //
        //     DepthOfField = _depthOfField;
        // }
        //
        //
        // private void Update()
        // {
        //     if (Volume == null)
        //     {
        //         return;
        //     }
        //
        //     CheckFocus();
        // }
        //
        // private void CheckFocus()
        // {
        //     var hitInfo = CalculateHit();
        //     
        //     if (!hitInfo.HasValue)
        //     {
        //         SwitchFocus(maxDistance);
        //         return;
        //     }
        //
        //     var focusDistance = CalculateFocusDistance(hitInfo.Value);
        //     SwitchFocus(focusDistance);
        // }
        //
        // private void SwitchFocus(float newTargetDistance)
        // {
        //     if(Mathf.Approximately(DepthOfField.focusDistance.value, newTargetDistance)) return;
        //     if(Mathf.Approximately(_targetDistance, newTargetDistance)) return;
        //
        //     _targetDistance = newTargetDistance;
        //
        //     if (_focusCoroutine!= null)
        //     {
        //         return;
        //     }
        //
        //     _focusCoroutine = StartCoroutine(SetFocusDistance());
        // }
        //
        // private IEnumerator SetFocusDistance()
        // {
        //     var elapsed = 0f;
        //     var startValue = DepthOfField.focusDistance.value;
        //     while (elapsed < fousSpeed)
        //     {
        //         DepthOfField.focusDistance.value = Mathf.Lerp(startValue, _targetDistance, elapsed / fousSpeed);
        //         elapsed += Time.deltaTime;
        //
        //         yield return null;
        //     }
        //
        //     _focusCoroutine = null;
        //
        // }
        //
        // private float CalculateFocusDistance(RaycastHit hitInfo)
        // {
        //     var distance = Vector3.Distance(transform.position, hitInfo.point);
        //     return Mathf.Clamp(distance, minDistance, maxDistance);
        // }
        //
        // private RaycastHit? CalculateHit()
        // {
        //     var thisTransform = transform;
        //
        //     var hit = useSphereCast 
        //         ? Physics.SphereCast(thisTransform.position, sphereCastRadius, thisTransform.forward, out var hitInfo, maxDistance)
        //         : Physics.Raycast(thisTransform.position, thisTransform.forward, out hitInfo, maxDistance);
        //
        //     return hit ? hitInfo : null;
        //
        //
        //
        // }
        //
        // private Volume GetPostProcessVolume()
        // {
        //     if (_volume != null) return _volume;
        //
        //     var volumes = FindObjectsOfType<Volume>();
        //
        //     if (volumes.Length == 0)
        //     {
        //         Debug.Log("No Post Process Volumes found in scene");
        //         return null;
        //     }
        //
        //     _volume = volumes[0];
        //     return _volume;
        //
        // }
        
        
        
    }
}