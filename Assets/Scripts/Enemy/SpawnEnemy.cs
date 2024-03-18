using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The prefab to spawn.")]
    private GameObject enemyPrefab;

    [SerializeField]
    [Tooltip("The player's transform.")]
    private Transform playerTransform;

    [SerializeField]
    [Tooltip("The interval between spawns.")]
    private float spawnInterval = 3.0f;

    [SerializeField]
    [Tooltip("The spawn points.")]
    private Transform[] spawnPoints; // Array to hold the spawn points.

    private int enemyCount = 0; // Counter for naming enemies.
    public int enemyTotalNumber;

    private bool gameOver = false;

    private TaskController _taskController;

    private Dictionary<Transform, ParticleSystem> SpawnPointToparticleSystem;

    private AudioSource _enemyAppearingAudio = null;
    private void Start()
    {
        _enemyAppearingAudio = gameObject.GetComponent<AudioSource>();
        SpawnPointToparticleSystem = new Dictionary<Transform, ParticleSystem>();

        foreach (Transform spawnPoint in spawnPoints)
        {
            SpawnPointToparticleSystem[spawnPoint] = spawnPoint.gameObject.GetComponent<ParticleSystem>();
        }
        ResetSpawningPoints(Color.blue);

        _taskController = GameObject.FindGameObjectWithTag("Task").GetComponent<TaskController>();
        if (_taskController != null)
        {
            enemyTotalNumber = _taskController.nTrials;
        }
    }

    private void Update()
    {
        if (!gameOver)
        {
            // If the system is spawning
            if (enemyCount == enemyTotalNumber)
            {
                Debug.Log("End of the wave");
                // When all enemies are spawned
                StopSpawning();
                gameOver = true;
            }
        }
    }

    public void StartSpawning()
    {
        enemyCount = 0;
        gameOver = false;
        // If the task controller is set, trigger the start task event.
        if(_taskController != null)
        {
            _taskController.TaskTriggerEntered("StartTask");
        }
        // Start spawning enemies at regular intervals.
        InvokeRepeating("Spawn", 0f, spawnInterval);
    }

    public void StopSpawning()
    {
        CancelInvoke("Spawn");
    }

    public void OnSpawnButtonClick()
    {
        StopSpawning();
        CleanupEnemies();
        ResetSpawningPoints(Color.red);
        StartSpawning();
    }

    private void Spawn()
    {
        // Randomly select one of the three spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Transform randomSpawnPoint = spawnPoints[spawnPointIndex];

        StartCoroutine(changeSpawnPointColor(SpawnPointToparticleSystem[randomSpawnPoint], Color.red));

        // Instantiate the enemy from the enemyPrefab at the selected spawn point.
        EnemyController enemy = Instantiate(enemyPrefab, randomSpawnPoint.position, Quaternion.identity).GetComponent<EnemyController>();
        enemy.CueId = spawnPointIndex + 1;

        // Set the name of the spawned enemy.
        enemy.name = "Enemy " + enemyCount;
        enemyCount++;

        // Make the enemy face towards the player's position.
        Vector3 direction = playerTransform.position - enemy.transform.position;
        enemy.transform.rotation = Quaternion.LookRotation(direction);

        // You can add more initialization or behavior setup for the enemy here.
        //enemy.moveSpeed = 2.0f;
        _enemyAppearingAudio.Play();
    }

    public void SpawnTraining(float trainingSpeed)
    {
        Transform randomSpawnPoint = spawnPoints[1];
        StartCoroutine(changeSpawnPointColor(SpawnPointToparticleSystem[randomSpawnPoint], Color.red));

        // Instantiate the enemy from the enemyPrefab at the selected spawn point.
        EnemyController enemy = Instantiate(enemyPrefab, randomSpawnPoint.position, Quaternion.identity).GetComponent<EnemyController>();
        enemy.CueId = 0;
        // Make the enemy face towards the player's position.
        Vector3 direction = playerTransform.position - enemy.transform.position;
        enemy.transform.rotation = Quaternion.LookRotation(direction);

        enemy.moveSpeed = trainingSpeed;
        _enemyAppearingAudio.Play();
    }

    public void CleanupEnemies()
    {
        // Destroy all enemies.
        ScoreManager.Instance.ResetScore();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    public IEnumerator ResetSpawningPoints(Color color)
    {
        foreach (KeyValuePair<Transform, ParticleSystem> spawnPoint in SpawnPointToparticleSystem)
        {
            ParticleSystem ps = spawnPoint.Value;
            var Main = ps.main;
            Main.startColor = color;
        }
        yield return new WaitForSeconds(1);
    }

    IEnumerator changeSpawnPointColor(ParticleSystem ps, Color color)
    {
        // Find back the original color
        var Main = ps.main;
        
        // Set the gate to red color
        Main.startColor = color;

        // Wait for 1 seconds
        yield return new WaitForSeconds(1);

        // Change back the color
        Main.startColor = Color.blue;
    }
}
