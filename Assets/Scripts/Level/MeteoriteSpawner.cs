using System.Collections;
using UnityEngine;

public class MeteoriteSpawner : MonoBehaviour
{
    [SerializeField] Transform terrain;
    [SerializeField] GameObject meteoriteTargetPrefab;
    [SerializeField] GameObject meteoritePrefab;
    [SerializeField] float spawnPeriod;

    public void StartSpawn()
    {
        StartCoroutine(SpawnMeteorite());
    }

    IEnumerator SpawnMeteorite()
    {
        yield return new WaitForSeconds(spawnPeriod);

        float rx = Random.Range(-40f, 40f);
        float rz = Random.Range(-80f, 80f);

        GameObject meteoriteTarget = GameObject.Instantiate(meteoriteTargetPrefab, transform.position + new Vector3(rx, 0f, rz), Quaternion.Euler(0f, 0f, 0f), transform);
        GameObject meteorite = GameObject.Instantiate(meteoritePrefab, transform.position + new Vector3(rx, 0f, rz), Quaternion.Euler(0f, 0f, 0f), transform);
        meteorite.GetComponent<Meteorite>().terrain = terrain;
        meteorite.GetComponent<Meteorite>().targetHint = meteoriteTarget;

        float randomForceMagnitude = Random.Range(0f, 10f);
        Vector3 randomForceDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

        Rigidbody meteoriteRigidbody = meteorite.GetComponent<Rigidbody>();

        meteoriteRigidbody.AddForce(randomForceDirection * randomForceMagnitude);
        meteoriteRigidbody.angularVelocity = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        StartCoroutine(SpawnMeteorite());
    }
}
