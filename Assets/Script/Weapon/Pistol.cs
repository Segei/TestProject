using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Weapon
{
    public class Pistol : Weapons
    {
        protected override BulletController GetBullet()
        {
            PistolBullet bullet = null;
            foreach (Transform t in pool.GetComponentsInChildren(typeof(Transform), true))
            {
                if (t.TryGetComponent(out bullet))
                {
                    break;
                }
            }
            if (bullet == null)
            {
                Debug.LogError("No bullet in pool");
                return null;
            }
            return bullet;
        }
    }
}