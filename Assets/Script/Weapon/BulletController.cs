using Assets.Script.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Weapon
{
    public abstract class BulletController : MonoBehaviour
    {
        [SerializeField] protected float defoltTimeToDestruction = 1;        
        [SerializeField] protected float damage = 1;
        [SerializeField] protected float speed = 1;
        [SerializeField] protected Rigidbody rb;


        protected float timeToDestruction;
        protected Unit unit;
        protected Vector3 _pointToMovement;
        public void Initialize(Vector3 pointToMovement)
        {
            _pointToMovement = pointToMovement;
            timeToDestruction = defoltTimeToDestruction;
            gameObject.transform.LookAt(_pointToMovement);
            rb.AddForce(gameObject.transform.forward * speed, ForceMode.Impulse);
        }

        protected void OnCollisionEnter(Collision collision)
        {
            Debug.Log(collision.gameObject.name);
            if (collision.transform.gameObject.TryGetComponent(out unit))
            {
                unit.TryCatchDamage(damage);
            }
            Disable();
        }
        void Update()
        {
            if (timeToDestruction <= 0)
            {
                Disable();
            }
            timeToDestruction -= Time.deltaTime;
        }
        private void Disable()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            foreach (GameObject t in gameObject.scene.GetRootGameObjects())
            {
                if (t.TryGetComponent<LabelPool>(out var marcer))
                {
                    gameObject.transform.parent = marcer.gameObject.transform;
                    gameObject.transform.localPosition = Vector3.zero;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}