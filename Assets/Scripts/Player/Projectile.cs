﻿using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string direction;
    public float projectileSpeed;
    public SpriteRenderer spriteRenderer;
    public Transform tempTransform;
    public GameObject particle;
    public CameraShake cameraShake;

    private void Start()
    {
        cameraShake = FindObjectOfType<CameraShake>();
        this.spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Invoke("Destroy", 1.15f);
        this.transform.Rotate(0, 0, 0);
    }
    public void CreateProjectile(string direction, float projectileSpeed)
    {
        this.direction = direction;
        this.projectileSpeed = projectileSpeed;

        if (direction == "right")
        {
            this.spriteRenderer.flipX = false;
        }
        if (direction == "left")
        {
            this.spriteRenderer.flipX = true;
        }
        if (direction == "up")
        {
            tempTransform.eulerAngles = new Vector3(0, 0, 90);
        }
        if (direction == "down")
        {
            tempTransform.eulerAngles = new Vector3(0, 0, 270);
        }
    }
    private void Update()
    {
        switch (direction)
        {
            case "right":
                transform.Translate(new Vector2(projectileSpeed * Time.deltaTime, 0));
                break;
            case "left":
                transform.Translate(new Vector2(-projectileSpeed * Time.deltaTime, 0));
                break;
            case "up":
                transform.Translate(new Vector2(0, projectileSpeed * Time.deltaTime));
                break;
            case "down":
                transform.Translate(new Vector2(0, -projectileSpeed * Time.deltaTime));
                break;

        }
    }
    private void Destroy()
    {
        Instantiate(particle, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    private void CamShake()
    {
        cameraShake.Shake(0.2f, 0.15f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyShoot"))
        {
            Destroy();
            CamShake();
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            Destroy();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!other.gameObject.GetComponent<Enemy>().isDefeated)
            {
                Destroy();
                CamShake();
            }
        }
    }
}

