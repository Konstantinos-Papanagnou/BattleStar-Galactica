using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] asteroids;  // Initialized in the inspector
    private Vector3 origin = Vector3.zero;

    float minSize = 0.2f;
    float maxSize = 1.5f;

    int minCount = 50;
    int maxCount = 100;

    float minDistance = 10.0f;
    float maxDistance = 35.0f;

    void Start()
    {
        origin = transform.position;
        GenerateAsteroids(Random.Range(minCount, maxCount));
    }

    void GenerateAsteroids(int count)
    {
        for (var i = 0; i < count; i++)
        {
            var size = Random.Range(minSize, maxSize);
            var prefab = asteroids[Random.Range(0, asteroids.Length)];

            var pos = Random.insideUnitSphere * (minDistance + (maxDistance - minDistance) * Random.value);

            for (var j = 0; j < 100; j++)
            {
                pos += origin;
                if (!Physics.CheckSphere(pos, (float)(size / 2.0)))
                {
                    break;
                }
            }
            var go = Instantiate(prefab, pos, Random.rotation);
            go.transform.localScale = new Vector3(size, size, size);
        }
    }

}
