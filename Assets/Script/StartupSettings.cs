using Assets.Script.Units;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartupSettings : MonoBehaviour
{
    [SerializeField] private MovebleController moveble;
    [SerializeField] private Player player;
    [SerializeField] private Button buttonReload;
    [SerializeField] private ViewAmmo viewAmmo;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private Transform startPoint;

    private void Start()
    {
        GameObject t = Instantiate(playerPrefab);
        t.transform.position = startPoint.position;
        player = t.GetComponent<Player>();
        moveble.SetPlayer(player);
        TryGetCam();
        TryGetNav();
        player.SetWeapon(weaponPrefab);
        player.Weapons.SetButtonReload(buttonReload);
        viewAmmo.SetWeapon(player.Weapons);

    }
    private void Update()
    {
        if (GetDistance(player.transform.position, moveble.WayPoints.Last().position) == 0)
        {
            SceneManager.LoadScene(0);
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
    private float GetDistance(Vector3 A, Vector3 B)
    {
        float result = 0;
        result = Mathf.Abs(A.x - B.x) + Mathf.Abs(A.z - B.z);
        return result;
    }
}
