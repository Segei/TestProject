using Assets.Script.Units;
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
        public void Shoot()
        {
            timeToDestruction = defoltTimeToDestruction;
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
        private void Update()
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
            gameObject.transform.SetParent(Pool.PoolInitialize.gameObject.transform);
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.SetActive(false);
        }
        public override bool Equals(object other)
        {
            return this.GetType() == other.GetType();
        }
    }
}