using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float Health = 20f;
    float barFillAmount = 1f;
    float damage = 0;

    public GameObject particleBlast;
    public PlayerHealth playerHealth;

    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 dragStartPosition;

    public AudioSource audioSource;
    public AudioClip damageSound;

    private void Start()
    {
        mainCamera = Camera.main;
        damage = barFillAmount / Health;
    }

    private void Update()
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
        viewportPosition.x = Mathf.Clamp01(viewportPosition.x);
        viewportPosition.y = Mathf.Clamp01(viewportPosition.y);
        transform.position = mainCamera.ViewportToWorldPoint(viewportPosition);

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isDragging = true;
                    dragStartPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    isDragging = false;
                    break;
            }
        }

        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            isDragging = true;
            dragStartPosition = Input.mousePosition;
        }
        
        else if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 swipeDelta = Vector3.zero;

            if (Input.touchCount > 0)
            {
                swipeDelta = (Vector3)Input.GetTouch(0).deltaPosition;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector3)Input.mousePosition - dragStartPosition;
            }

            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                transform.Translate(swipeDelta.x * moveSpeed * Time.deltaTime * Vector3.right);
            }

            dragStartPosition = Input.mousePosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            DamagePlayerHealth();
            Destroy(collision.gameObject);
            if (Health <= 0)
            {
                audioSource.PlayOneShot(damageSound, 0.5f);
                Destroy(gameObject);
                GameObject Blast = Instantiate(particleBlast, transform.position, Quaternion.identity);
                Destroy(Blast, 2f);
            }
        }
    }

    void DamagePlayerHealth()
    {
        if (Health > 0)
        {
            Health -= 1;
            barFillAmount = barFillAmount - damage;
            playerHealth.SetAmount(barFillAmount);
        }

        if (Health == 0)
        {
            SceneManager.LoadScene("Retry");
        }
    }
}





