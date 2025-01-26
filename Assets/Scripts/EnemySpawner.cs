using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform playerTransform;
    public float spawnDelay = 2f;
    public float moveSpeed = 5f;
    public float shootDelay = 1f;
    public float bulletSpeed = 10f;
    public GameObject bulletPrefab;
    public int enemiesToSpawn = 10;
    public int enemiesToDestroy = 5;
    public EnemyFire enemyFire;
    public TextMeshProUGUI timerText;

    private Camera mainCamera;
    private int enemiesDestroyed = 0;
    public float timeRemaining = 15f;

    private void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector3 randomViewportPosition = new Vector3(Random.Range(0f, 1f), 1f, mainCamera.nearClipPlane);
            Vector3 spawnPosition = mainCamera.ViewportToWorldPoint(randomViewportPosition);
            spawnPosition.y += 1f;

            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void Update()
    { 
        timeRemaining -= Time.deltaTime;
        timerText.text = "00 :  "  + Mathf.RoundToInt(timeRemaining);

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
            

            if (mainCamera.WorldToViewportPoint(enemy.transform.position).y > 0)
            {
                if (Time.time % shootDelay == 0)
                {
                    Vector3 direction = (playerTransform.position - enemy.transform.position).normalized;
                    GameObject bullet = Instantiate(bulletPrefab, enemy.transform.position + direction, Quaternion.identity);
                    bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
                }
            }
        }

        if (timeRemaining <= 0)
        {
            SceneManager.LoadScene("Retry");
        }
    }

    public void EnemyDestroyed()
    {
        enemiesDestroyed++;
        Debug.Log("Enemies Destroyed" + enemiesDestroyed);
        Debug.Log(enemiesToSpawn);

        if (enemiesDestroyed == enemiesToSpawn)
        {
            SceneManager.LoadScene("WIN");
        }
    }
}
