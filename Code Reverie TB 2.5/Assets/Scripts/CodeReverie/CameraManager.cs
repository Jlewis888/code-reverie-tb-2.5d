using System;
using System.Collections;
using System.Collections.Generic;
using CodeReverie;
using Unity.Cinemachine;
using UnityEngine;

[DefaultExecutionOrder(-115)]
public class CameraManager : ManagerSingleton<CameraManager>
{


    public Camera mainCamera;
    public Camera mapCamera;
    public List<CinemachineCamera> virtualCameras = new List<CinemachineCamera>();
    public CinemachineCamera mainVirtualCamera;
    public CinemachineCamera inventoryVirtualCamera;
    public CinemachineCamera skillVirtualCamera;
    public CinemachineCamera dialogueVirtualCamera;
    public CinemachineCamera combatVirtualCamera;
    public CinemachineTargetGroup combatTargetGroup;
    private CinemachineImpulseSource impulseSource;
    public float impulseForce = 1f;
    public bool screenShake = false;
    public float screenShakeTimer;
    
    public float refreshRate;
    public float rotationRate;
    
    protected override void Awake()
    {
        refreshRate = 0.025f;
        rotationRate = 1.5f;
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
            UpdateCamera(PlayerManager.Instance.currentParty[0].characterController
                .transform);
            ToggleMainCamera();
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleSkillCamera();
        }
    }

    public void SetPriorityCamera(CinemachineCamera camera)
    {
        foreach (CinemachineCamera virtualCamera in virtualCameras)
        {
            if (camera == virtualCamera)
            {
                virtualCamera.Priority = 10;
            }
            else
            {
                virtualCamera.Priority = 1;
            }
        }
    }
    
    public void SetBattleCamera()
    {
        SetPriorityCamera(combatVirtualCamera);
        // combatVirtualCamera.Priority = 11;
        // mainVirtualCamera.Priority = 1;
    }

    public void SetBattleCamera(BattleAreaManager battleArea)
    {
        combatVirtualCamera.Priority = 11;
        mainVirtualCamera.Priority = 1;
    }

    public void SetTargetGroup(List<CharacterBattleManager> characterBattleManager)
    {
        
    }

    public void AddToTargetGroup(Transform targetTransform, float weight = 2f, float radius = 2f)
    {
        combatTargetGroup.AddMember(targetTransform, weight, radius);
        //combatTargetGroup.m_Targets[0].
    }


    public void SetSelectedPlayerWeight(CharacterBattleManager characterBattleManager, float weight, float radius = 2f)
    {
        
        for (int i = 0; i < combatTargetGroup.m_Targets.Length; i++)
        {
            if (combatTargetGroup.Targets[i].Object == characterBattleManager.transform)
            {
                
                //int member = combatTargetGroup.FindMember(characterBattleManager.transform);
                combatTargetGroup.Targets[i].Weight = weight;
                combatTargetGroup.Targets[i].Radius = radius;
            }
            else
            {
                combatTargetGroup.Targets[i].Weight = 1f;
                combatTargetGroup.Targets[i].Radius = 2f;
            }
        }
    }

    public void SetCombatFollowTarget(Transform transform)
    {
        //combatVirtualCamera.m_Follow = transform;
        combatVirtualCamera.LookAt = transform;
    }
    
    public void ResetCombatFollowTarget()
    {
        //combatVirtualCamera.m_Follow = combatTargetGroup.transform;
        combatVirtualCamera.LookAt = combatTargetGroup.transform;
    }

    public void SetCharacterWeight(CharacterBattleManager characterBattleManager, float weight, float radius = 1f)
    {
        int member = combatTargetGroup.FindMember(characterBattleManager.transform);
        combatTargetGroup.Targets[member].Weight = weight;
        combatTargetGroup.Targets[member].Radius = radius;
    }
    
    
    
    public void ResetTargetGroupSetting()
    {
        for (int i = 0; i < combatTargetGroup.m_Targets.Length; i++)
        {
            combatTargetGroup.Targets[i].Weight= 1f;
            combatTargetGroup.Targets[i].Radius = 2f;
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
        // battleVirtualCamera.Priority = 1;
        //mainVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset.y = 0f;
        mainVirtualCamera.Priority = 10;
        combatVirtualCamera.Priority = 1;
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
        skillVirtualCamera.gameObject.SetActive(false);
        
        SetPriorityCamera(skillVirtualCamera);
        skillVirtualCamera.gameObject.SetActive(true);
        // skillVirtualCamera.GetComponent<CinemachineOrbitalFollow>().HorizontalAxis.Value = -90;
        //
        // skillVirtualCamera.Follow = CombatManager.Instance.selectedSkillPlayerCharacter
        //     .transform;
        // skillVirtualCamera.LookAt = CombatManager.Instance.selectedSkillPlayerCharacter
        //     .transform;

        //StartCoroutine(RotateSkillCamera(() => {}));
    }

    public void SetSkillCameraFollow(GameObject skillGameObject)
    {
        skillVirtualCamera.GetComponent<CinemachineFollow>().enabled = false;
        skillVirtualCamera.GetComponent<CinemachineRotationComposer>().enabled = false;
        skillVirtualCamera.Follow = skillGameObject.transform;
        skillVirtualCamera.LookAt = skillGameObject.transform;

        
        skillVirtualCamera.GetComponent<CinemachineFollow>().enabled = true;
        skillVirtualCamera.GetComponent<CinemachineRotationComposer>().enabled = true;
    }

    public IEnumerator RotateSkillCamera(Action onComplete)
    {
        float counter = skillVirtualCamera.GetComponent<CinemachineOrbitalFollow>().HorizontalAxis.Value;
       
        yield return new WaitForSeconds(0.5f);

        if (counter == 90f)
        {
            while (counter > 0)
            {

                counter -= rotationRate;
                skillVirtualCamera.GetComponent<CinemachineOrbitalFollow>().HorizontalAxis.Value = counter;
            
                yield return new WaitForSeconds(refreshRate);
            }
        }
        else
        {
            while (counter < 0)
            {

                counter += rotationRate;
                skillVirtualCamera.GetComponent<CinemachineOrbitalFollow>().HorizontalAxis.Value = counter;
            
                yield return new WaitForSeconds(refreshRate);
            }
        }

        onComplete();



    }
    

    public void ToggleDialogueCamera()
    {
        SetPriorityCamera(dialogueVirtualCamera);
    }
    
    
    public void SetCameraConfiner(PolygonCollider2D collider2D)
    {
        mainVirtualCamera.GetComponent<CinemachineConfiner2D>().BoundingShape2D = collider2D;
    }
    
    public void SetCameraConfiner(Collider collider)
    {
        mainVirtualCamera.GetComponent<CinemachineConfiner3D>().BoundingVolume = collider;
    }

    public void UpdateCamera(Transform transform)
    {
        mainVirtualCamera.Follow = transform;
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
            screenShakeTimer = impulseSource.ImpulseDefinition.ImpulseDuration;
        }
    }
    
    public void ScreenShake2()
    {
        impulseSource.GenerateImpulseWithForce(3f);
        screenShake = true;
        screenShakeTimer = impulseSource.ImpulseDefinition.ImpulseDuration;
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
