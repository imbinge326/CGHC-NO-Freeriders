using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLATwo : MonoBehaviour
{
    public List<GameObject> spawnPoints;
    [SerializeField] private GameObject LATwo;
    private float interval;
    private GameObject point;
    private int pointIndex = 0;

        public void Start()
    {
        StartCoroutine(SpawnLAONE());
    }

    public IEnumerator SpawnLAONE()
    {
        while (true)
        {
            GetPoints();
            point = spawnPoints[pointIndex];
            interval = Random.Range(0.5f, 2);
            yield return new WaitForSeconds(interval);
            FireLATWO();
        }
    }
    private void FireLATWO()
    {
        Instantiate(LATwo, point.gameObject.transform.position, Quaternion.Euler(0,0,-90));
    }

    private int GetPoints()
    {
        if (pointIndex < spawnPoints.Count - 1)
        {
            return pointIndex++;
        }
        else
        {
            return pointIndex = 0;
        }
    }

}
