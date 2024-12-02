using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.VFX;

namespace CodeReverie
{
    public class CharacterUnit : SerializedMonoBehaviour
    {
        public Animator animator;
        public Character character;
        public SpriteRenderer spriteRenderer;
        public bool dissolveTest;
        public VisualEffect visualEffect;
        public float refreshRate;
        public float dissolveRate;


        public int _dissolveAmount = Shader.PropertyToID("_DissolveAmount");
        

        private void Awake()
        {
            refreshRate = 0.025f;
            dissolveRate = 0.0125f;
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (dissolveTest)
            {
                if (Input.GetKeyDown(KeyCode.Y))
                {
                    StartCoroutine(Dissolve());
                }
                
                if (Input.GetKeyDown(KeyCode.T))
                {
                    StartCoroutine(Appear());
                }
            }
        }
        
        
        public IEnumerator Appear()
        {
            
            // Material[] materials = spriteRenderer.materials;
            // materials[0] = MaterialsManager.Instance.dissolveMaterial;
            // spriteRenderer.materials = materials;
            // int dissolveAmount = Shader.PropertyToID("_DissolveAmount");
            // GetComponent<Renderer>().material.SetFloat(dissolveAmount, 1.1f);
            // float elapsedTime = 1.1f;

            // float refreshRate = 0.025f;
            
            Renderer rend = GetComponent<Renderer> ();
            rend.material.shader = Shader.Find("Shader Graphs/Dissolve Shader Graph");

            float counter = 1.1f;

            while (counter > 0)
            {
                counter -= dissolveRate;
                
                
                rend.material.SetFloat("_DissolveAmount", counter);
                //Debug.Log(rend.material.GetFloat("_DissolveAmount"));
                yield return new WaitForSeconds(refreshRate);
            }
            
            //Debug.Log("Appear Complter");

            // while (elapsedTime > dissolveAmount)
            // {
            //     elapsedTime -= Time.deltaTime;
            //
            //
            //     float lerpedDissolve = Mathf.Lerp(1.1f, 0f, elapsedTime);
            //     
            //     
            //     GetComponent<Renderer>().material.SetFloat(dissolveAmount, lerpedDissolve);
            //     
            //     
            //     
            //     yield return null;
            //
            // }

        }


        public IEnumerator Dissolve()
        {
            
            // Material[] materials = spriteRenderer.materials;
            // materials[0] = MaterialsManager.Instance.dissolveMaterial;
            // spriteRenderer.materials = materials;
            // int dissolveAmount = Shader.PropertyToID("_DissolveAmount");
            // GetComponent<Renderer>().material.SetFloat(dissolveAmount, 0);
            // float elapsedTime = 0f;

            if (visualEffect != null)
            {
                visualEffect.Play();
            }
            
            Renderer rend = GetComponent<Renderer> ();
            rend.material.shader = Shader.Find("Shader Graphs/Dissolve Shader Graph");

            float counter = 0f;
            
            
            while (counter < 1.1f)
            {
                counter += dissolveRate;
                
                
                rend.material.SetFloat("_DissolveAmount", counter);
                //Debug.Log(rend.material.GetFloat("_DissolveAmount"));
                yield return new WaitForSeconds(refreshRate);
            }
            
            //Debug.Log("Dissolve Complter");

            // while (elapsedTime < dissolveAmount)
            // {
            //     elapsedTime += Time.deltaTime;
            //
            //
            //     float lerpedDissolve = Mathf.Lerp(0f, 1.1f, elapsedTime);
            //     
            //     
            //     GetComponent<Renderer>().material.SetFloat(dissolveAmount, lerpedDissolve);
            //     
            //     
            //     
            //     yield return null;
            //
            // }
            
        }
        
        
    }
}