using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Targets targets;
    [SerializeField] private GameObject[] prefab;
    [SerializeField] private int pedsCount;
    [SerializeField] private Transform[] spawnPoint;
    
    public bool music = false;
    void Start()
    {
        targets = GetComponent<Targets>();
        SpawnPeds();
    }
    private void SpawnPeds()
    {
        List<GameObject> TargetPoints = targets.AllTargets;

        if (TargetPoints.Count >= pedsCount)
        {
            StartCoroutine(Wait());
        }
        else Debug.LogError($"More Peds[{pedsCount}]" +
            $" then spawnPoints[{TargetPoints.Count}]");

    }
    IEnumerator Wait()
    {
            for (int i = 0; i < pedsCount; i++)
            {
                int rnd = Random.Range(0,2);
                Instantiate(prefab[Random.Range(0, prefab.Length - 1)],
                     spawnPoint[rnd].position,
                     transform.rotation);
            yield return new WaitForSeconds(0.5f);
            }

    }
    public void Signal()
    {
        music = true;
    }
}
