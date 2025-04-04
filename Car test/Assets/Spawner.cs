using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    public GameObject PowerupPrefab;
    [SerializeField] private float timer;

    void Update()

    {
        timer += Time.deltaTime; 

        if(timer >= 3)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-500, 510 ), 5, Random.Range(-500, 510));
            Instantiate(PowerupPrefab, randomSpawnPosition, Quaternion.identity);
            timer = 0;
        }
    }
}