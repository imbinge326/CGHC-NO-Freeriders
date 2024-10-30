using System.Collections;
using UnityEngine;

public class SpawnFireballs : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;
    private float interval = 5f;

    public void Start()
    {
        StartCoroutine(SpawnFireball());
    }

    public IEnumerator SpawnFireball()
    {
        while (true)
        {
            FireFireball();
            yield return new WaitForSeconds(interval);
        }
    }
    private void FireFireball()
    {
        Instantiate(fireballPrefab, transform.position,  Quaternion.Euler(0,0,-90));
    }
}
