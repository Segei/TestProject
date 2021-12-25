using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script.Units
{
    public abstract class Unit : MonoBehaviour
    { 
        [SerializeField] protected float timeToDestroyDefaultValue = 5;
        [SerializeField] protected float healsPoint = 5;
        
        protected NavMeshAgent nav;
        protected Animator animator;
        protected bool died = false;
        protected float timeToDestroy;
       

        protected virtual void Start()
        {
            gameObject.TryGetComponent(out nav);
            gameObject.TryGetComponent(out animator);
            if (nav == null) Debug.LogError(gameObject.name + " no component 'NavMeshAgent'");
            if (animator == null) Debug.LogError(gameObject.name + " no component 'Animator'");
        }
        public void TryCatchDamage(float Damage)
        {
            if (!died)
            {
                healsPoint -= Damage;
                if (healsPoint <= 0)
                {
                    healsPoint = 0;
                    Death();
                }
            }
        }

        protected void Update()
        {
            animator.SetFloat(name = "Speed", nav.velocity.magnitude);
            if (died && timeToDestroy < 0)
            {
                Destroy(gameObject);
            }
            if(died && timeToDestroy > 0)
            {
                timeToDestroy -= Time.deltaTime;
            }
            if (!died)
                Active();
        }
        protected abstract void Active();
        protected virtual void Death()
        {
            died = true;
            timeToDestroy = timeToDestroyDefaultValue;
        }

    }
}