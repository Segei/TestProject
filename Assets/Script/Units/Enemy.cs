using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Script.Units
{
    public class Enemy : Unit
    {
        [SerializeField] private Collider collider; 
        public UnityAction<Enemy> Died;
        protected override void Active()
        {            
        }

        protected override void Death()
        {
            base.Death();
            animator.enabled = false;
            collider.isTrigger = true;
            Died?.Invoke(this);
        }
    }
}