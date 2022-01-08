using Assets.Script.Units;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MovebleController : MonoBehaviour
{
    public NavMeshAgent Nav => _nav;
    public MyRayCastCamera Camera => _camera;
    public Player Player => _player;
    public List<Transform> WayPoints => _wayPoints;

    [SerializeField] private Button BMove;
    [SerializeField] private List<Transform> _wayPoints = null;
    [SerializeField] private List<Transform> enemyPoints = null;
    [SerializeField] private float rotationSpeed = 1;
    [SerializeField] private WaveEnemy waveEnemy = null;

    private MyRayCastCamera _camera;
    private bool move = false;
    private NavMeshAgent _nav;
    private int countWayPoint = 0;
    private int countEnemyPoint = 0;
    [SerializeField] private Player _player;
    private bool look;

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    public void SetNav(NavMeshAgent nav)
    {
        _nav = nav;
    }
    public void SetCamera(MyRayCastCamera camera)
    {
        _camera = camera;
    }


    private void Start()
    {
        BMove.gameObject.SetActive(true);
        BMove.onClick.AddListener(OnClickButtonMove);
        waveEnemy.EndWave += ReadyFoMove;
    }

    private void OnClickButtonMove()
    {
        BMove.gameObject.SetActive(false);
        _nav.SetDestination(_wayPoints[++countWayPoint].position);
        if (countWayPoint > 0 && countWayPoint != _wayPoints.Count - 1)
        {
            countEnemyPoint++;
            waveEnemy.SpawnNext();
        }
        look = false;
        move = true;

    }
    private void ReadyFoMove()
    {
        BMove.gameObject.SetActive(true);
    }
    private void Update()
    {
        if (!move)
        {
            if (countWayPoint > 0 && countWayPoint < _wayPoints.Count - 1 && !look)
            {
                _player.gameObject.transform.LookAt(new Vector3(enemyPoints[countEnemyPoint - 1].position.x, _player.gameObject.transform.position.y, enemyPoints[countEnemyPoint - 1].position.z));
                _camera.ShootingPermit = true;
                look = true;
            }
        }
        else
        {
            _camera.ShootingPermit = false;
        }
        if (GetDistance(_player.gameObject.transform.position, _wayPoints[countWayPoint].position) == 0 && move)
        {
            move = false;
        }
    }
    private float GetDistance(Vector3 A, Vector3 B)
    {
        float result = 0;
        result = Mathf.Abs(A.x - B.x) + Mathf.Abs(A.z - B.z);
        return result;
    }
}
