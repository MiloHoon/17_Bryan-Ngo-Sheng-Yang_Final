using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject EnemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawner(5));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator EnemySpawner(float WaitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(WaitTime);
            Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
        }
    }
}
