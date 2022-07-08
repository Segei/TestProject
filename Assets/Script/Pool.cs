using Assets.Script.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pool : MonoBehaviour
{

    public static Pool PoolInitialize;

    private void Start()
    {
        PoolInitialize = this;
    }
    public BulletController GetBullet(GameObject bulletPrefab)
    {
        BulletController bullet = bulletPrefab.GetComponent<BulletController>();
        foreach (var item in gameObject.transform.GetComponentsInChildren<BulletController>(true))
        {
            if(item.Equals(bullet))
                return item;
        }
        GameObject temp = Instantiate(bulletPrefab);
        bullet = temp.GetComponent<BulletController>();
        return bullet;
    }
}
