using System.Collections;
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
            animator.SetFloat("Speed", nav.velocity.magnitude);
            if (!died)
                Active();
        }
        protected abstract void Active();
        protected virtual void Death()
        {
            died = true;
            StartCoroutine(DeleteBody(timeToDestroyDefaultValue));
        }
        protected virtual IEnumerator DeleteBody(float timeToDestroyBody)
        {
            yield return new WaitForSeconds(timeToDestroyBody);
            Destroy(gameObject);
            yield return null;
        }

    }
}