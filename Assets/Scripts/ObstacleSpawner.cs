using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance;

    [SerializeField] private Transform player;
    [SerializeField] private float spawnZ = 50f;
    [SerializeField] private int obstaclesPerRow = 25;
    [SerializeField] private int minObstacleSpacing = 0;
    [SerializeField] private int maxObstacleSpacing = 5;
    [SerializeField] private float platformWidth = 10f;
    [SerializeField] private LayerMask obstacleLayerMask; // Layer mask for obstacles to avoid collisions with them
    [SerializeField] private bool setToOffset;
    [SerializeField] private Vector3 Offset;
    [SerializeField] private float maxtimeGapBtwEachSpawn = 3f; // Time gap between each spawn
    [SerializeField] private int maxObstracleToBeCleared = 15;
    [SerializeField] private float spawnZPosition = 30f; // Fixed spawn Z position



    [SerializeField] private ObstracleTypeDataContainer[] obstracleTypeDataArray;


    private float timeGapBtwEachSpawn = 0f;
    private float timer = 0f;
    private float difficultyIncreaseInterval = 15f;
    private int obstacleSpacing;
    private GameObject obstacle;
    private List<GameObject> spawnedObstacles = new List<GameObject>();
    //private float nextSpawnZ = 0f;


    private HashSet<Vector2> usedPositions = new HashSet<Vector2>(); // Stores used (x, z) positions per row


    [Serializable]
    public struct ObstracleTypeDataContainer
    {
        


        public enum ObstracleTypeEnum
        {
            Obstracle_1,
            Obstracle_2,
            Obstracle_3,
            Obstracle_4,
            Obstracle_5,
            Obstracle_6,
            Obstracle_7,
            Obstracle_8,
            Obstracle_9,
            Obstracle_10,
        }
        public ObstracleTypeEnum obstracleType;

        public Vector3 obstracleScale;

        public GameObject obstraclePrefab;



    }

    private void Awake()
    {
        Instance = this;
    }





    void Update()
    {
        timeGapBtwEachSpawn += Time.deltaTime;
        timer += Time.deltaTime;

        if (timeGapBtwEachSpawn >= maxtimeGapBtwEachSpawn)
        {
            SpawnObstacleRow(spawnZPosition); // Always spawn at fixed Z
            timeGapBtwEachSpawn = 0f;
        }

        
    }

    


    void SpawnObstacleRow(float zPosition)
    {
        usedPositions.Clear(); // Reset for this row

        obstacleSpacing = UnityEngine.Random.Range(minObstacleSpacing, maxObstacleSpacing);
        int safeIndex = UnityEngine.Random.Range(0, obstaclesPerRow);

        float tileWidth = platformWidth / obstaclesPerRow;
        float startX = -(platformWidth / 2f) + tileWidth / 2;


        for (int i = 0; i < obstaclesPerRow; i++)
        {
            if (i == safeIndex) continue; // Leave one lane clear

            float xPosition = startX + i * tileWidth;
            Vector2 gridPos = new Vector2(xPosition, zPosition);

            if (usedPositions.Contains(gridPos)) continue; // Already used

            // Pick a random obstacle type
            ObstracleTypeDataContainer selectedData = obstracleTypeDataArray[UnityEngine.Random.Range(0, obstracleTypeDataArray.Length)];

            //  Check if position is already occupied (just in case — this can be improved with overlap check later)
            Vector3 spawnPos = new Vector3(xPosition, 0.5f, zPosition);

            // Layer-based collision check before spawning
            if (Physics.CheckBox(spawnPos, selectedData.obstracleScale * 0.5f, Quaternion.identity, obstacleLayerMask))   
                continue;

            

            // Spawn correct prefab
            if (setToOffset)
            {
                obstacle = Instantiate(selectedData.obstraclePrefab, spawnPos + Offset, selectedData.obstraclePrefab.transform.rotation);
            }
            else
            {
                obstacle = Instantiate(selectedData.obstraclePrefab, spawnPos, selectedData.obstraclePrefab.transform.rotation);
            }

            

            obstacle.name = selectedData.obstracleType.ToString();

            usedPositions.Add(gridPos); // Mark as used

            spawnedObstacles.Add(obstacle);

        }

        
    }

    public float GetMaxTimeGapBtwEachSpawn()
    {
        return maxtimeGapBtwEachSpawn;
    }

    public void SetMaxTimeGapBtwEachSpawn(float value)
    {
        maxtimeGapBtwEachSpawn = value;
    }

    public int GetObstraclePerRow()
    {
        return obstaclesPerRow;
    }

    public void SetObstraclePerRow(int value)
    {
        obstaclesPerRow = value;
    }







}
