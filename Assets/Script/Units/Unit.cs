using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script.Units
{
    public class Unit : MonoBehaviour
    {
        protected NavMeshAgent nav;
        protected Animator animator;
        [SerializeField] protected float _hitPoint;
        private bool died;
        private float timeToDestroy;

        private void Start()
        {
            gameObject.TryGetComponent(out nav);
            gameObject.TryGetComponent(out animator);
            if (nav == null) Debug.LogError(gameObject.name + " no component 'NavMeshAgent'");
            if (animator == null) Debug.LogError(gameObject.name + " no component 'Animator'");
        }
        public void TryCatchDamage(float Damage)
        {

        }

        private void Update()
        {
            animator.SetFloat(name = "Speed", nav.velocity.magnitude);
        }
        protected virtual void Death()
        {
            
        }

    }
}