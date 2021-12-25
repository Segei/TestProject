using Assets.Script.Units;
using UnityEngine;

public class MyRayCastCamera : MonoBehaviour
{
    public bool ShootingPermit = false;

    [SerializeField] private Camera camera;
    [SerializeField] private Player _player;

    private RaycastHit hit;

    public void SetPlayer(Player player)
    {
        _player = player;
    }
    void Start()
    {
        if (camera == null)
            gameObject.TryGetComponent(out camera);
    }
    void Update()
    {
        if (ShootingPermit)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    _player.ReadyToShot(hit.point);
                }
            }
        }
    }
}
