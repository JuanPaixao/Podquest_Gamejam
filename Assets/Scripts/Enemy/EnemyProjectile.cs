﻿using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public string direction;
    public float projectileSpeed;
    public Vector3 shootDirection;
    private Rigidbody2D _rb;
    public Transform playerPosition;
    private Vector3 _directionFollow;
    public GameObject particle;
    public int bossProjectileHP;
    public CameraShake cameraShake;

    private void Start()
    {
        cameraShake = FindObjectOfType<CameraShake>();
        _rb = GetComponent<Rigidbody2D>();
        Invoke("Destroy", 10);
        _directionFollow = playerPosition.position - this.transform.position;
        transform.right = _directionFollow;
    }
    private void Update()
    {
        _rb.velocity = _directionFollow.normalized * projectileSpeed * Time.deltaTime;
    }
    public void SetShootDirection(Vector2 direction, float projectileSpeed, Transform playerPosition)
    {
        this.shootDirection = direction;
        this.projectileSpeed = projectileSpeed;
        this.playerPosition = playerPosition;
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
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerProjectile"))
        {
            if (this.bossProjectileHP <= 0)
            {
                Destroy();
                CamShake();
            }
            else
            {
                bossProjectileHP--;
                CamShake();
            }
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            Destroy();
        }
    }
}