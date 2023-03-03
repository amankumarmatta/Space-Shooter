using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyFire : MonoBehaviour
{
    public float moveSpeed;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float Health = 10f;
    private float timeSinceLastFire = 0f;
    private List<GameObject> bulletPool = new List<GameObject>();
    public GameObject enemyBlast;
    private EnemySpawner enemySpawner;


    float barSize = 1f;
    float Damage = 1f;

    void Start()
    {
        enemySpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>();

        for (int i = 0; i < 10; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }

        Damage = barSize / Health;
    }

    void Update()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);

        if (timeSinceLastFire > 1f / fireRate)
        {

            GameObject bullet = null;

            if (bullet == null)
            {
                bullet = Instantiate(bulletPrefab);
                bulletPool.Add(bullet);
            }


            bullet.SetActive(true);
            bullet.transform.position = firePoint.position;


            timeSinceLastFire = 0f;
        }


        timeSinceLastFire += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player Bullet")
        {
            DamageHealth();
            if (Health <= 0)
            {
                enemySpawner.EnemyDestroyed();
                Destroy(gameObject);
                GameObject enemyExplode = Instantiate(enemyBlast, transform.position, Quaternion.identity);
                Destroy(enemyExplode, 0.4f);
            }
            collision.gameObject.SetActive(false); // Disable the player bullet prefab
        }

        if (collision.tag == "Stop")
        {
            SceneManager.LoadScene("Retry");
        }
    }


    void DamageHealth()
    {
        if (Health > 0)
        {
            Health -= 1;
            barSize = barSize - Damage;
        }
    }
}
