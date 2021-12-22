using Assets.Script.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Weapon
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float timeToDestruction;
        private Vector3 _pointToMovement;
        [SerializeField] private float _damage;
        [SerializeField] private float _speed;
        private Unit unit;
        public void Initialize(Vector3 pointToMovement)
        {
            _pointToMovement = pointToMovement;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.gameObject.TryGetComponent(out unit))
            {
                unit.TryCatchDamage(_damage);
            }
            Disable();
        }
        void Update()
        {
            if (timeToDestruction > 0)
            {
                gameObject.transform.Translate(_pointToMovement * _speed * Time.deltaTime);
            }
            timeToDestruction -= Time.deltaTime;
        }
        private void Disable()
        {
            foreach (GameObject t in gameObject.scene.GetRootGameObjects())
            {
                if (t.TryGetComponent<LabelPool>(out var marcer))
                {
                    gameObject.transform.parent = marcer.gameObject.transform.parent;
                    gameObject.transform.localPosition = Vector3.zero;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}