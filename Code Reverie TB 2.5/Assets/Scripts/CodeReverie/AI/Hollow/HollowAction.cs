using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace CodeReverie
{
    public class HollowAction : Action
    {
        protected HollowAIController hollowAIController;
        protected Animator animator;


        public override void OnAwake()
        {
            hollowAIController = GetComponent<HollowAIController>();
            //playerCharacterUnit = GetComponent<PlayerCharacterUnit>();
            animator = GetComponent<Animator>();
        }




        public void FaceTarget()
        {
            if (hollowAIController.target != null)
            {
                if (transform.position.x > hollowAIController.target.transform.position.x)
                {
                    //transform.localScale =  new Vector3(-1,1,1);
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1f, transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    //transform.localScale = new Vector3(1,1,1);
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * 1f, transform.localScale.y, transform.localScale.z);
                }
            }
        }
        

        public void FaceFollowTarget()
        {
            if (transform.position.x > PlayerManager.Instance.currentParty[0].characterController.transform.position.x)
            {
                //transform.localScale =  new Vector3(-1,1,1);
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1f, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                //transform.localScale = new Vector3(1,1,1);
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * 1f, transform.localScale.y, transform.localScale.z);

            }
        }
    }
}