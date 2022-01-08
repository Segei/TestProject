using Assets.Script.Weapon;
using UnityEngine;

namespace Assets.Script.Units
{
    public class Player : Unit
    {


        [SerializeField] private Weapons _weapon;
        [SerializeField] private Transform SlotToWeapon;
        [SerializeField] private GameObject PlaerMesh;

        public Weapons Weapons => _weapon;

        public void SetWeapon(GameObject weapon) {
            if (_weapon != null) {
               Destroy(_weapon.gameObject);
            }
            GameObject t = Instantiate(weapon);
            t.transform.parent = SlotToWeapon;
            t.transform.localPosition = Vector3.zero;
            t.transform.localRotation = Quaternion.Euler(0, 0, 0);
            t.transform.localScale = Vector3.one;
           
            _weapon = t.GetComponent<Weapons>();
        }
        public void WeaponReload()
        {
            _weapon.TryReload();
        }
        protected override void Active()
        {

        }
        public void ReadyToShot(Vector3 pointShot)
        {
            PlaerMesh.transform.LookAt(new Vector3(pointShot.x, PlaerMesh.transform.position.y, pointShot.z));
            _weapon.Shoot(pointShot);
        }
    }
}