using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Script.Weapon
{
    public class Weapons : MonoBehaviour
    {
        public int TotalAmmo => _totalAmmo;
        public int AmmoInMagazine => _ammoInMagazine;
        public UnityAction changeAmmo;
        public GameObject Bullet;

        [SerializeField] protected int _totalAmmo;
        [SerializeField] protected float _hitRate;
        protected int _ammoInMagazine;
        [SerializeField] protected int _magazineSize;

        protected Button _button;
        protected float _cooldown;
        protected LabelBarrel _barrel;


        protected void Start()
        {
            foreach (Transform t in gameObject.GetComponentsInChildren<Transform>())
            {
                t.gameObject.TryGetComponent(out _barrel);
            }
            if (_barrel == null)
            {
                Debug.LogError(gameObject.name + " doesn't have script 'MarcerBarrel' on barrel point");
            }
            _cooldown = 1 / _hitRate;
        }
        public void SetButtonReload(Button button)
        {
            _button = button;
            _button.onClick.AddListener(TryReload);
        }
        protected void Update()
        {
            if (_cooldown > 0)
            {
                _cooldown -= Time.deltaTime;
            }

        }
        public void Shoot(Vector3 pointToShoot)
        {
            if (_ammoInMagazine == 0)
            {
                Debug.Log("Need reload");
                return;
            }
            if (_cooldown <= 0)
            {
                _ammoInMagazine--;
                BulletController bullet = Pool.PoolInitialize.GetBullet(Bullet);
                if (bullet != null)
                {
                    bullet.gameObject.SetActive(true);
                    bullet.gameObject.transform.parent = null;
                    bullet.gameObject.transform.position = _barrel.transform.position;
                    bullet.gameObject.transform.rotation = _barrel.transform.rotation;
                    bullet.Shoot();
                }
                
            }
            changeAmmo?.Invoke();
        }
        public void TryReload()
        {
            if (_totalAmmo >= _ammoInMagazine - _magazineSize)
            {
                _totalAmmo -= _magazineSize - _ammoInMagazine;
                _ammoInMagazine = _magazineSize;
            }
            else if (_totalAmmo != 0)
            {
                _ammoInMagazine += _totalAmmo;
                _totalAmmo -= _totalAmmo;
            }
            changeAmmo?.Invoke();
        }
    }
}