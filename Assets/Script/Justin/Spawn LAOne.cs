using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpawnLAOne : MonoBehaviour
{
    public List<GameObject> spawnPoints;
    [SerializeField] private GameObject LAOne;
    [SerializeField] private GameObject warningSign;
    private float interval = 3f;
    private GameObject point;

        public void Start()
    {
        StartCoroutine(SpawnLAONE());
    }

    public IEnumerator SpawnLAONE()
    {
        while (true)
        {
            point = spawnPoints[Random.Range(0, spawnPoints.Count)];
            SpawnWarning();
            yield return new WaitForSeconds(interval);
            FireLAONE();
        }
    }
    private void FireLAONE()
    {
        Instantiate(LAOne, point.gameObject.transform.position, Quaternion.identity);
    }

    private void SpawnWarning()
    {
        var warning = Instantiate(warningSign, point.gameObject.transform.position, Quaternion.identity);
        Destroy(warning, interval);
    }
}
