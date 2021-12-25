using Assets.Script.Weapon;
using UnityEngine;

namespace Assets.Script.Units
{
    public class Player : Unit
    {
        public Weapons weapon;
        
        [SerializeField] private GameObject PlaerMesh;
        

        protected override void Start()
        {
            base.Start();
            foreach (Transform t in gameObject.GetComponentsInChildren(typeof(Transform), true))
            {
                if (t.TryGetComponent(out weapon))
                {
                    break;
                }
            }
        }

        public void WeaponReload()
        {
            weapon.TryReload();
        }
        protected override void Active()
        {

        }
        public void ReadyToShot(Vector3 pointShot)
        {
            PlaerMesh.transform.LookAt(new Vector3(pointShot.x, PlaerMesh.transform.position.y, pointShot.z));

            weapon.Shoot(pointShot);
        }
    }
}