using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;


    [Header("Attributes")]
    [SerializeField] private int defaultEnemyNum = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float waveInterval = 5f;
    [SerializeField] private float diff_Factor = 0.75f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int waveNum = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeft;
    private bool spawning = false;

    private void Awake() {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start() {
        StartCoroutine(StartWave());
    }

    private void Update() {

        timeSinceLastSpawn += Time.deltaTime;

        if (!spawning) return;

        if ((timeSinceLastSpawn >= (1f / enemiesPerSecond)) && (enemiesLeft > 0)) {
            SpawnEnemy();
            enemiesLeft --;
            enemiesAlive ++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeft == 0) {
            EndWave();
        }
    }

    private void EndWave() {
        spawning = false;
        timeSinceLastSpawn = 0f;
        waveNum++;
        StartCoroutine(StartWave());
    }

    private void EnemyDestroyed() {
        enemiesAlive--;
    }

    private void SpawnEnemy() {
        GameObject prefabToSpawn = enemyPrefabs[0];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);

    }

    private IEnumerator StartWave() {
        yield return new WaitForSeconds(waveInterval);
        spawning = true;
        enemiesLeft = EnemiesPerWave();
    }

    private int EnemiesPerWave() {
        return Mathf.RoundToInt(defaultEnemyNum * Mathf.Pow(waveNum, diff_Factor));
    }
}
