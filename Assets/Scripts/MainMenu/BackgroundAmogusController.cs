using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class BackgroundAmogusController : MonoBehaviour
{
    [SerializeField] GameObject amogusPrefab;
    [SerializeField] int maxCount;

    List<GameObject> amoguses;

    private void Start()
    {
        amoguses = new List<GameObject>();
        StartCoroutine(SpawnController());
    }

    IEnumerator SpawnController()
    {
        yield return new WaitForSeconds(2f);

        if (Random.Range(1, 6) < 2)
        {
            if (amoguses.Count < maxCount)
            {
                float startX = 0f, startY = 0f;
                float forceX = 0f, forceY = 0f;
                int side = Random.Range(1, 5);

                switch(side)
                {
                    case 1:
                        startX = -28f;
                        startY = Random.Range(-10f, 10f);
                        forceX = 1f;
                        forceY = Random.Range(-1f, 1f);
                        break;
                    case 2:
                        startX = Random.Range(-20f, 20f);
                        startY = 14f;
                        forceX = Random.Range(-1f, 1f);
                        forceY = -1f;
                        break;
                    case 3:
                        startX = 28f;
                        startY = Random.Range(-10f, 10f);
                        forceX = -1f;
                        forceY = Random.Range(-1f, 1f);
                        break;
                    case 4:
                        startX = Random.Range(-20f, 20f);
                        startY = -14f;
                        forceX = Random.Range(-1f, 1f);
                        forceY = 1f;
                        break;
                }

                GameObject newAmogus = Instantiate(
                    amogusPrefab, 
                    new Vector3(startX, startY, 0f), 
                    Quaternion.Euler(0f, 180f, Random.Range(-180f, 180f))
                );
                Rigidbody amogusRB = newAmogus.GetComponent<Rigidbody>();
                amogusRB.linearVelocity = new Vector3(forceX, forceY, 0f);
                amogusRB.angularVelocity = new Vector3(0f, 0f, Random.Range(-1f, 1f));

                amoguses.Add(newAmogus);
            }
        }

        StartCoroutine(SpawnController());
    }

    private void OnTriggerExit(Collider other)
    {
        if (amoguses.Contains(other.gameObject))
        {
            amoguses.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
    }
}
