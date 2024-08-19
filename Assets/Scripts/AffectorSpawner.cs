using System.Collections;
using UnityEngine;

public class AffectorSpawner : MonoBehaviour
{
    [SerializeField] private Affector[] prefabs;
    [SerializeField] private float spawnCooldown;

    [SerializeField] private Transform firstPoint;
    [SerializeField] private Transform secondPoint;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        var randPrefab = prefabs[Random.Range(0, prefabs.Length)];
        var randPosition = Vector2.Lerp(firstPoint.position, secondPoint.position, Random.Range(0f, 1f));
        Instantiate(randPrefab, randPosition, Quaternion.identity);
        yield return new WaitForSeconds(spawnCooldown);
        StartCoroutine(SpawnRoutine());
    }
}
