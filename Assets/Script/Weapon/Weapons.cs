using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Script.Weapon
{
    public abstract class Weapons : MonoBehaviour
    {
        public int Ammo => _ammo;
        public int AmmoInMagazine => _ammoInMagazine;
        public UnityAction changeAmmo;
        public Button button = null;
        
        [SerializeField] protected float hitRate;
        [SerializeField] protected int _ammo;
        [SerializeField] protected int _ammoInMagazine;
        [SerializeField] protected int magazineSize;
        
        protected float cooldown;
        protected LabelBarrel barrel;
        
        
        protected LabelPool pool = null;
        protected UnityAction action;
        

        protected void Start()
        {
            action += TryReload;
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
                if (t.TryGetComponent(out pool))
                {
                    break;
                }
            }
            if (pool == null)
            {
                Debug.LogError("No pool on scene.");
            }
        }
        private void OnDisable()
        {
            button.onClick.RemoveListener(action);
            action -= TryReload;
        }

        protected void Update()
        {
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
            if(button != null)
            {
                button.onClick.AddListener(action);
            }
        }
        public void Shoot(Vector3 pointToShoot)
        {
            if (cooldown <= 0 && _ammoInMagazine > 0)
            {
                _ammoInMagazine--;
                BulletController bullet = GetBullet();
                if (bullet != null)
                {
                    bullet.gameObject.SetActive(true);
                    bullet.gameObject.transform.parent = null;
                    bullet.gameObject.transform.position = barrel.transform.position;
                    bullet.gameObject.transform.LookAt(pointToShoot);
                    bullet.Initialize(pointToShoot);
                }
                cooldown = 1 / hitRate;
            }
            if (_ammoInMagazine == 0)
            {
                Debug.Log("Need reload");
            }
            changeAmmo?.Invoke();
        }
        protected abstract BulletController GetBullet();
        public void TryReload()
        {
            if (_ammo >= _ammoInMagazine - magazineSize)
            {
                _ammo -= magazineSize - _ammoInMagazine ;
                _ammoInMagazine = magazineSize;
            }
            else if (_ammo != 0)
            {
                _ammoInMagazine += _ammo;
                _ammo -= _ammo;
            }
            changeAmmo?.Invoke();
        }
    }
}