using UnityEngine;
using UnityEngine.Serialization;

public class VerticalSpawner : MonoBehaviour
{
    [SerializeField] private float yRange;
    [SerializeField] private Vector2 spawnTimeRange;
    [SerializeField] private GameObject prefab;
    private float t;
    
    private void Update()
    {
        t -= Time.deltaTime;
        
        if (t <= 0)
        {
            SpawnPrefab();
            
            t = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
        }
    }

    private void SpawnPrefab()
    {
        GameObject newObj = Instantiate(prefab, new Vector3(transform.position.x, Random.Range(transform.position.y - yRange, transform.position.y + yRange), transform.position.z), Quaternion.identity, transform);
    }
}
