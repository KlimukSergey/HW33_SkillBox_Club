using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targets : MonoBehaviour
{
    private List<GameObject> danceTarget;
    private List<GameObject> barTarget;
    private List<GameObject> loungeTarget;
    public List<GameObject> AllTargets;
    public GameObject[] arrBarTarget;
    public GameObject[] arrLoungeTarget;
    public GameObject[] arrDanceTarget;

    // Start is called before the first frame update
    void Start()
    {
        arrDanceTarget = GameObject.FindGameObjectsWithTag("DanceTarget");
        arrBarTarget = GameObject.FindGameObjectsWithTag("BarTarget");
        arrLoungeTarget = GameObject.FindGameObjectsWithTag("LoungeTarget");
        ListsForm();

    }

    /// <summary>
    /// формирование коллекций
    /// </summary>
    private void ListsForm()
    {
        danceTarget = new List<GameObject>();
        barTarget = new List<GameObject>();
        loungeTarget = new List<GameObject>();
        AllTargets = new List<GameObject>();
        foreach (GameObject obj in arrDanceTarget)
        {
            danceTarget.Add(obj);
            AllTargets.Add(obj);
        }
        foreach (GameObject obj in arrBarTarget)
        {
            barTarget.Add(obj);
            AllTargets.Add(obj);
        }
        foreach (GameObject obj in arrLoungeTarget)
        {
            loungeTarget.Add(obj);
            AllTargets.Add(obj);
        }
    }

    public GameObject ChangeTarget()
    {
        GameObject target = null;
        int rnd = Random.Range(0, AllTargets.Count - 1);
        target = AllTargets[rnd];
        AllTargets.RemoveAt(rnd);
        return target;
    }
    public void AddTarget(GameObject target)
    {
        AllTargets.Add(target);
    }

}
