using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using CodeReverie;

using UnityEngine;

[DefaultExecutionOrder(-115)]
public class CameraManager : ManagerSingleton<CameraManager>
{


    public Camera mainCamera;
    public Camera mapCamera;
    public List<CinemachineVirtualCamera> virtualCameras = new List<CinemachineVirtualCamera>();
    public CinemachineVirtualCamera mainVirtualCamera;
    public CinemachineVirtualCamera inventoryVirtualCamera;
    public CinemachineVirtualCamera skillVirtualCamera;
    public CinemachineVirtualCamera dialogueVirtualCamera;
    public CinemachineVirtualCamera battleVirtualCamera;
    private CinemachineImpulseSource impulseSource;
    public float impulseForce = 1f;
    public bool screenShake = false;
    public float screenShakeTimer;
    
    
    
    
    protected override void Awake()
    {
        base.Awake();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }


    private void OnEnable()
    {
        EventManager.Instance.generalEvents.toggleInventory += ToggleInventoryCamera;
        EventManager.Instance.combatEvents.onEnemyDamageTaken += ScreenShake;
    }

    private void OnDisable()
    {
        EventManager.Instance.generalEvents.toggleInventory -= ToggleInventoryCamera;
        EventManager.Instance.combatEvents.onEnemyDamageTaken -= ScreenShake;
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (screenShakeTimer >= 0)
        {
            screenShakeTimer -= Time.deltaTime;
        }
        else
        {
            screenShake = false;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            ResetMapCamera();
        }
    }

    public void SetPriorityCamera(CinemachineVirtualCamera camera)
    {
        foreach (CinemachineVirtualCamera virtualCamera in virtualCameras)
        {
            if (camera == virtualCamera)
            {
                virtualCamera.m_Priority = 10;
            }
            else
            {
                virtualCamera.m_Priority = 1;
            }
        }
    }

    public void SetBattleCamera(BattleArea battleArea)
    {
        // battleVirtualCamera.transform.localPosition = mainVirtualCamera.transform.localPosition;
        // battleVirtualCamera.m_Follow = battleArea.transform;
        // battleVirtualCamera.LookAt = battleArea.transform;
        // battleVirtualCamera.m_Priority = 10;

        //mainVirtualCamera.m_Priority = 1;
        //battleVirtualCamera.transform.localPosition = mainVirtualCamera.transform.localPosition;
        mainVirtualCamera.m_Follow = battleArea.transform;
        mainVirtualCamera.LookAt = battleArea.transform;
        //mainVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset.y = -2.34f;

    }
    
    public void UnsetBattleCamera()
    {
        
        // battleVirtualCamera.m_Follow = null;
        // battleVirtualCamera.LookAt = null;
        // battleVirtualCamera.m_Priority = 1;
        //mainVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset.y = 0f;
        mainVirtualCamera.m_Priority = 10;

    }

    public void ToggleMainCamera()
    {
        SetPriorityCamera(mainVirtualCamera);
    }
    

    public void ToggleInventoryCamera(bool open)
    {
        //if(!open) return;
        SetPriorityCamera(inventoryVirtualCamera);
    }

    public void ToggleSkillCamera()
    {
        SetPriorityCamera(skillVirtualCamera);
    }

    public void ToggleDialogueCamera()
    {
        SetPriorityCamera(dialogueVirtualCamera);
    }
    
    
    public void SetCameraConfiner(PolygonCollider2D collider2D)
    {
        mainVirtualCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = collider2D;
    }

    public void UpdateCamera(Transform transform)
    {
        mainVirtualCamera.m_Follow = transform;
        mainVirtualCamera.LookAt = transform;
        
        inventoryVirtualCamera.m_Follow = transform;
        inventoryVirtualCamera.LookAt = transform;
        
        skillVirtualCamera.m_Follow = transform;
        skillVirtualCamera.LookAt = transform;
        
        dialogueVirtualCamera.m_Follow = transform;
        dialogueVirtualCamera.LookAt = transform;
    }

    public void ScreenShake()
    {
        if (!screenShake)
        {
            impulseSource.GenerateImpulseWithForce(impulseForce);
            screenShake = true;
            screenShakeTimer = impulseSource.m_ImpulseDefinition.m_ImpulseDuration;
        }
    }
    
    public void ScreenShake2()
    {
        impulseSource.GenerateImpulseWithForce(3f);
        screenShake = true;
        screenShakeTimer = impulseSource.m_ImpulseDefinition.m_ImpulseDuration;
    }
    
    public void ScreenShake(DamageProfile damageProfile)
    {
       ScreenShake();
    }

    public void ResetMapCamera()
    {
        mapCamera.transform.localPosition = Vector3.zero;
        mapCamera.orthographicSize = 12f;
    }
}
