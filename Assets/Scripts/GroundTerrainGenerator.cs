using UnityEngine;
using System.Collections.Generic;

public class GroundTerrainGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject grassPrefab;
    [SerializeField] private List<GameObject> stonePrefabs;  // List of stone prefabs
    [SerializeField] private Transform groundTransform;
    [SerializeField] private float groundLength = 30f;
    [SerializeField] private GameObject grassParent;

    [Header("Spawn Settings")]
    [SerializeField] private int grassCount = 50;
    [SerializeField] private Vector3 Offset = Vector3.zero;
    [SerializeField] private float grassMaxSize = 1.2f;
    [SerializeField] private float grassMinSize = 0.8f;
    [SerializeField] private float stoneMaxSize = 1f;
    [SerializeField] private float stoneMinSize = 0.7f;
    [Range(0f, 1f)]
    [SerializeField] private float grassProbability = 0.7f;  // % chance to spawn grass, rest will be stones

    private void Start()
    {
        SpawnGrassAndStones();
    }

    void SpawnGrassAndStones()
    {
        Vector3 groundSize = groundTransform.localScale;
        Vector3 groundPos = groundTransform.position;

        float groundWidth = groundSize.x;
        float groundLength = groundSize.z;

        for (int i = 0; i < 3; i++)
        {
            Vector3 grassParentSpawnPosition = new Vector3(
                groundTransform.position.x,
                0,
                groundTransform.position.z + (i * this.groundLength)
            );

            GameObject grassParentInstance = Instantiate(grassParent, grassParentSpawnPosition, Quaternion.identity, transform);

            for (int j = 0; j < grassCount; j++)
            {
                float xPos = Random.Range(-groundWidth / 2, groundWidth / 2);
                float zPos = Random.Range(-groundLength / 2, groundLength / 2);

                Vector3 spawnPosition = new Vector3(
                    groundPos.x + xPos,
                    groundPos.y + 0.01f,
                    groundPos.z + zPos
                ) + Offset + grassParentInstance.transform.position;
                GameObject toSpawn;

                // Choose whether to spawn grass or stone
                bool isGrass = Random.value < grassProbability;

                if (isGrass)
                {
                    toSpawn = grassPrefab;
                }
                else if (stonePrefabs.Count > 0)
                {
                    toSpawn = stonePrefabs[Random.Range(0, stonePrefabs.Count)];
                }
                else
                {
                    toSpawn = grassPrefab; // fallback
                }

                GameObject instance = Instantiate(toSpawn, spawnPosition, Quaternion.identity, grassParentInstance.transform);

                // Apply different randomization for grass and stones
                if (isGrass)
                {
                    // Random scale for grass
                    instance.transform.localScale *= Random.Range(grassMinSize, grassMaxSize);
                }
                else
                {
                    // Random uniform scale for stones (you can tweak these values)
                    float stoneScale = Random.Range(stoneMinSize, stoneMaxSize);
                    instance.transform.localScale = Vector3.one * stoneScale;

                    // Random Y-axis rotation + optional tilt
                    Vector3 randomRotation = new Vector3(
                        Random.Range(-5f, 5f),      // slight tilt on X
                        Random.Range(0f, 360f),     // full rotation on Y
                        Random.Range(-5f, 5f)       // slight tilt on Z
                    );
                    instance.transform.eulerAngles = randomRotation;
                }
            }
        }
    }
}
