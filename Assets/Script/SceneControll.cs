using Assets.Script.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneControll : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform startPoint;
    void Start()
    {
        Load();
    }
    private void Load()
    {
        GameObject t =  Instantiate(playerPrefab);
        t.transform.position = startPoint.position;
    }
    
}
