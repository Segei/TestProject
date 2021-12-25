using Assets.Script.Units;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class WaveEnemy : MonoBehaviour
{
    public UnityAction SpawnWave;
    public UnityAction EndWave;

    [SerializeField] private GameObject enemyPrefab = null;
    [SerializeField] private List<Transform> enemySpawnPoint = null;
    [SerializeField] private List<Enemy> enemyInWave = new List<Enemy>();
    [SerializeField] private int countEnemyInSpawn = 3;

    private int curetnWaveSpawn = 0;


    public void SpawnNext()
    {
        for (int i = 0; i < countEnemyInSpawn; i++)
        {
            GameObject t = Instantiate(enemyPrefab);
            t.transform.position = enemySpawnPoint[curetnWaveSpawn].position + new Vector3(0, 0, i);
            t.transform.rotation = enemySpawnPoint[curetnWaveSpawn].rotation;
            enemyInWave.Add(t.GetComponent<Enemy>());
            enemyInWave.Last().Died += EnemyDied;
        }
        SpawnWave?.Invoke();
        curetnWaveSpawn++;
    }
    private void EnemyDied(Enemy e)
    {
        enemyInWave.Remove(e);
        if (enemyInWave.Count == 0) EndWave?.Invoke();
    }

}
