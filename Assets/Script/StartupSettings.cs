using Assets.Script.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class StartupSettings : MonoBehaviour
{
    [SerializeField] private MovebleController moveble;
    [SerializeField] private Player player;
    [SerializeField] private Button buttonReload;
    [SerializeField] private ViewAmmo viewAmmo;


    void Update()
    {
        if (player == null) TryGetPlayer();
        if (player!= null && moveble.Nav == null) TryGetNav();
        if (player != null && moveble.Camera == null) TryGetCam();
        if (player != null && player.weapon.button == null) player.weapon.button = buttonReload;
        if (player != null && viewAmmo.Weapons == null) viewAmmo.SetWeapon(player.weapon);
    }
    private void TryGetPlayer()
    {
        foreach (GameObject t in gameObject.scene.GetRootGameObjects())
        {
            if (t.TryGetComponent(out player))
            {
                moveble.SetPlayer(player);
                break;
            }
        }
    }
    private void TryGetCam()
    {
        foreach (Transform t in player.GetComponentsInChildren(typeof(Transform), true))
        {
            if (t.TryGetComponent<MyRayCastCamera>(out var c))
            {
                moveble.SetCamera(c);
                c.SetPlayer(player);
                break;
            }
        }
    }    
    private void TryGetNav()
    {
        foreach (Transform t in player.GetComponentsInChildren(typeof(Transform), true))
        {
            if (t.TryGetComponent<NavMeshAgent>(out var n))
            {                
                moveble.SetNav(n);
                break;
            }
        }
    }
}
