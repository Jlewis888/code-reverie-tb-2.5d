using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class SpriteBillboard : SerializedMonoBehaviour
    {
        private bool freezeXZAxis = true;
        
        

        private void Update()
        {
            if (freezeXZAxis)
            {
                transform.rotation = Quaternion.Euler(0f, CameraManager.Instance.mainCamera.transform.rotation.eulerAngles.y, 0f);
            }
            else
            {
                transform.rotation = CameraManager.Instance.mainCamera.transform.rotation;
            }
        }
    }
}