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
    public CinemachineVirtualCamera combatVirtualCamera;
    public CinemachineTargetGroup combatTargetGroup;
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
        // EventManager.Instance.generalEvents.toggleInventory += ToggleInventoryCamera;
        // EventManager.Instance.combatEvents.onEnemyDamageTaken += ScreenShake;
    }

    private void OnDisable()
    {
        // EventManager.Instance.generalEvents.toggleInventory -= ToggleInventoryCamera;
        // EventManager.Instance.combatEvents.onEnemyDamageTaken -= ScreenShake;
        
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
        combatVirtualCamera.m_Priority = 11;
        mainVirtualCamera.m_Priority = 1;
    }

    public void SetTargetGroup(List<CharacterBattleManager> characterBattleManager)
    {
        
    }

    public void AddToTargetGroup(Transform targetTransform, float weight = 1f, float radius = 2f)
    {
        combatTargetGroup.AddMember(targetTransform, weight, radius);
        //combatTargetGroup.m_Targets[0].
    }


    public void SetSelectedPlayerWeight(CharacterBattleManager characterBattleManager, float weight, float radius = 2f)
    {
        
        for (int i = 0; i < combatTargetGroup.m_Targets.Length; i++)
        {
            if (combatTargetGroup.m_Targets[i].target == characterBattleManager.transform)
            {
                
                //int member = combatTargetGroup.FindMember(characterBattleManager.transform);
                combatTargetGroup.m_Targets[i].weight = weight;
                combatTargetGroup.m_Targets[i].radius = radius;
            }
            else
            {
                combatTargetGroup.m_Targets[i].weight = 1f;
                combatTargetGroup.m_Targets[i].radius = 2f;
            }
        }
    }

    public void SetCombatFollowTarget(Transform transform)
    {
        combatVirtualCamera.m_Follow = transform;
        combatVirtualCamera.LookAt = transform;
    }
    
    public void ResetCombatFollowTarget()
    {
        combatVirtualCamera.m_Follow = combatTargetGroup.transform;
        combatVirtualCamera.LookAt = combatTargetGroup.transform;
    }

    public void SetCharacterWeight(CharacterBattleManager characterBattleManager, float weight, float radius = 1f)
    {
        int member = combatTargetGroup.FindMember(characterBattleManager.transform);
        combatTargetGroup.m_Targets[member].weight = weight;
        combatTargetGroup.m_Targets[member].radius = radius;
    }
    
    
    
    public void ResetTargetGroupSetting()
    {
        for (int i = 0; i < combatTargetGroup.m_Targets.Length; i++)
        {
            combatTargetGroup.m_Targets[i].weight = 1f;
            combatTargetGroup.m_Targets[i].radius = 1f;
        }
    }

    public void ClearTargetGroup()
    {
        combatTargetGroup.m_Targets = new CinemachineTargetGroup.Target[] { };
    }
    
    
    public void UnsetBattleCamera()
    {
        
        // battleVirtualCamera.m_Follow = null;
        // battleVirtualCamera.LookAt = null;
        // battleVirtualCamera.m_Priority = 1;
        //mainVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset.y = 0f;
        mainVirtualCamera.m_Priority = 10;
        combatVirtualCamera.m_Priority = 1;
        ClearTargetGroup();

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
    
    public void SetCameraConfiner(Collider collider)
    {
        mainVirtualCamera.GetComponent<CinemachineConfiner>().m_BoundingVolume = collider;
    }

    public void UpdateCamera(Transform transform)
    {
        mainVirtualCamera.m_Follow = transform;
        mainVirtualCamera.LookAt = null;
        //
        // inventoryVirtualCamera.m_Follow = transform;
        // inventoryVirtualCamera.LookAt = transform;
        //
        // skillVirtualCamera.m_Follow = transform;
        // skillVirtualCamera.LookAt = transform;
        //
        // dialogueVirtualCamera.m_Follow = transform;
        // dialogueVirtualCamera.LookAt = transform;
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
