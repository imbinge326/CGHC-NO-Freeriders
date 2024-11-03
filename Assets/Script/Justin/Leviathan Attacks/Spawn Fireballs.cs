using System.Collections;
using UnityEngine;

public class SpawnFireballs : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;
    private float interval;

    public void Start()
    {
        StartCoroutine(SpawnFireball());
    }

    public IEnumerator SpawnFireball()
    {
        while (true)
        {
            FireFireball();
            interval = Random.Range(0.2f, 3);
            yield return new WaitForSeconds(interval);
        }
    }
    private void FireFireball()
    {
        Instantiate(fireballPrefab, transform.position,  Quaternion.Euler(0,0,-90));
    }
}
