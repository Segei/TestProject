using Assets.Script.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MovebleController : MonoBehaviour
{
    [SerializeField] private Button BMove;
    [SerializeField] private List<Transform> wayPoints;
    [SerializeField] private List<Transform> enemyPoints;
    [SerializeField] private NavMeshAgent nav;
    [SerializeField] private float rotationSpeed = 1;
    [SerializeField] private bool move;
    [SerializeField] private Player player;
    [SerializeField] private RayCastCamera camera;
    [SerializeField] private Button buttonReload;
    [SerializeField] private ViewAmmo viewAmmo;
    
    private int countWayPoint = 1;
    private int countEnemyPoint = 0;

    void Start()
    {
        BMove.gameObject.SetActive(true);
    }

    public void OnClickButtonMove()
    {
        BMove.gameObject.SetActive(false);
        nav.SetDestination(wayPoints[countWayPoint++].position);
        if (countWayPoint != 0 && countWayPoint != wayPoints.Count - 1) countEnemyPoint++;
    }
    void Update()
    {
        if (nav == null) TryGetNav();
        if (nav != null && camera == null) TryGetCam();
        if (player != null && player.weapon.button == null) player.weapon.button = buttonReload;
        if (player != null && viewAmmo.Weapons == null)
        {
            viewAmmo.SetWeapon(player.weapon);
        }
        if (move && nav.velocity.magnitude == 0)
        {
            if (countWayPoint > 1 && countWayPoint != wayPoints.Count - 1)
            {
                player.gameObject.transform.LookAt(new Vector3(enemyPoints[countEnemyPoint - 1].position.x, player.gameObject.transform.position.y, enemyPoints[countEnemyPoint - 1].position.z));
                camera.ShootingPermit = true;
            }
        }
        else
        {
            move = true;
            camera.ShootingPermit = false;
        }
    }
    private void TryGetCam()
    {
        foreach (Transform t in nav.gameObject.GetComponentsInChildren(typeof(Transform), true))
        {
            if (t.TryGetComponent(out camera))
            {
                break;
            }
        }
    }
    private void TryGetNav()
    {
        foreach (GameObject t in gameObject.scene.GetRootGameObjects())
        {
            if (t.TryGetComponent(out player))
            {
                player.gameObject.TryGetComponent(out nav);
                break;
            }
        }
    }
    private void Rotate(Transform target)
    {
        Vector3 dir = Vector3.Cross(target.position, target.forward) - nav.gameObject.transform.forward;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir);
        nav.gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
    }
}
