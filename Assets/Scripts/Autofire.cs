/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autofire : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform firePoint;
    public float fireRate = 1f;
    private float timeSinceLastFire = 0f;
    public AudioSource audioSource;
    private List<GameObject> bulletPool = new List<GameObject>();

    void Start()
    {      
        for (int i = 0; i < 10; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            audioSource.Play();
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    void Update()
    {
        
        if (timeSinceLastFire > 1f / fireRate)
        {
            
            GameObject bullet = null;
            for (int i = 0; i < bulletPool.Count; i++)
            {
                if (!bulletPool[i].activeInHierarchy)
                {
                    bullet = bulletPool[i];
                    break;
                }
            }
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
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autofire : MonoBehaviour
{
    public GameObject[] bulletPrefabs; 
    public Transform firePoint;
    public float fireRate = 1f;
    private float timeSinceLastFire = 0f;
    public AudioSource audioSource;
    private List<GameObject>[] bulletPools;
    private int currentBulletIndex = 0; 

    void Start()
    {
        bulletPools = new List<GameObject>[bulletPrefabs.Length]; 

        for (int i = 0; i < bulletPrefabs.Length; i++)
        {
            bulletPools[i] = new List<GameObject>();

            for (int j = 0; j < 10; j++)
            {
                GameObject bullet = Instantiate(bulletPrefabs[i]);
                audioSource.Play();
                bullet.SetActive(false);
                bulletPools[i].Add(bullet); 
            }
        }
    }

    void Update()
    {
        if (timeSinceLastFire > 1f / fireRate)
        {
            GameObject bullet = null;

            for (int i = 0; i < bulletPools[currentBulletIndex].Count; i++)
            {
                if (!bulletPools[currentBulletIndex][i].activeInHierarchy)
                {
                    bullet = bulletPools[currentBulletIndex][i];
                    break;
                }
            }
            if (bullet == null)
            {
                bullet = Instantiate(bulletPrefabs[currentBulletIndex]); 
                bulletPools[currentBulletIndex].Add(bullet);
            }

            bullet.SetActive(true);
            bullet.transform.position = firePoint.position;

            currentBulletIndex = (currentBulletIndex + 1) % bulletPrefabs.Length;

            timeSinceLastFire = 0f;
        }

        timeSinceLastFire += Time.deltaTime;
    }
}

