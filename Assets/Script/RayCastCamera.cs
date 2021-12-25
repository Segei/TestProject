using Assets.Script.Units;
using UnityEngine;

public class RayCastCamera : MonoBehaviour
{
    public bool ShootingPermit = false;
    
    [SerializeField] private Camera camera;
    [SerializeField] private Player player;
    
    private RaycastHit hit;

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
                    player.ReadyToShot(hit.collider.transform.position);
                }
            }
        }
    }
}
