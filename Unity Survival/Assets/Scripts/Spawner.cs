using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Monster[] prefabs;

    public Transform[] spawnPoints;

    private bool spawnAble;

    private void Start()
    {
        spawnAble = true;
    }

    private void Update()
    {
        if(spawnAble)
        {
            StartCoroutine(CreateMonster());
        }
    }

    private IEnumerator CreateMonster()
    {
        for (int i = 0; i < 3; i++)
        {
            spawnAble = false;
            var prefab = prefabs[Random.Range(0, prefabs.Length)];
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        }
        yield return new WaitForSeconds(3f);

        spawnAble = true;
    }
}
