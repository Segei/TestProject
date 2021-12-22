using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected float hitRate;
        [SerializeField] protected int _ammo;
        [SerializeField] protected int _ammoInMagazine;
        [SerializeField] protected int magazineSize;
        private float cooldown;
        private LabelBarrel barrel;
        public int Ammo => _ammo;
        public int AmmoInMagazine => _ammoInMagazine;
        LabelPool pool;

        private void Start()
        {
            foreach (Transform t in gameObject.GetComponentsInChildren<Transform>())
            {
                t.gameObject.TryGetComponent(out barrel);
            }
            if (barrel == null)
            {
                Debug.LogError(gameObject.name + " doesn't have script 'MarcerBarrel' on barrel point");
            }
            foreach (GameObject t in gameObject.scene.GetRootGameObjects())
            {
                t.TryGetComponent(out pool);
            }
            if(pool == null)
            {
                Debug.LogError("No pool on scene.");
            }
        }

        private void Update()
        {
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
        }
        public void Shoot(Vector3 pointToShoot)
        {
            if (cooldown <= 0)
            {
                BulletController bullet = null;
                foreach (Transform t in pool.GetComponentsInChildren<Transform>())
                {
                    if (t.TryGetComponent(out bullet))
                    {
                        break;
                    }
                }
                if (bullet == null) {
                    Debug.LogError("No bullet in pool");
                    return;
                }
                bullet.gameObject.SetActive(true);
                bullet.gameObject.transform.parent = null;
                bullet.gameObject.transform.position = barrel.transform.position;
                bullet.gameObject.transform.LookAt(pointToShoot);
                bullet.Initialize(pointToShoot);
            }
        }
        public void TryReload()
        {
            if (_ammo >= _ammoInMagazine - magazineSize)
            {
                _ammo -= _ammoInMagazine - magazineSize;
                _ammoInMagazine = magazineSize;
            }
            else if (_ammo != 0)
            {
                _ammoInMagazine += _ammo;
                _ammo -= _ammo;
            }
        }
    }
}