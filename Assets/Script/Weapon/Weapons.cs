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


        [SerializeField] protected float hitRate;
        [SerializeField] protected int _ammo;
        [SerializeField] protected int _ammoInMagazine;
        [SerializeField] protected int magazineSize;

        protected Button _button;
        protected float cooldown;
        protected LabelBarrel barrel;


        protected LabelPool pool = null;


        protected void Start()
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
        public void SetButtonReload(Button button)
        {
            _button = button;
            _button.onClick.AddListener(TryReload);
        }
        protected void Update()
        {
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
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
            Debug.Log("Reload");
            if (_ammo >= _ammoInMagazine - magazineSize)
            {
                _ammo -= magazineSize - _ammoInMagazine;
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