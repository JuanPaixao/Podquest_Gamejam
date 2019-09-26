using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public string direction;
    public float projectileSpeed;
    public Vector3 shootDirection;
    private Rigidbody _rb;
    public Transform playerPosition;
    private Vector3 _directionFollow;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Invoke("Destroy", 10);
        _directionFollow = new Vector3(this.playerPosition.position.x, 0, this.playerPosition.position.z) - new Vector3(this.transform.position.x, 0, this.transform.position.z);
    }
    private void Update()
    {
        _rb.velocity = _directionFollow * projectileSpeed * Time.deltaTime;
    }
    public void SetShootDirection(Vector3 direction, float projectileSpeed, Transform playerPosition)
    {
        this.shootDirection = direction;
        this.projectileSpeed = projectileSpeed;
        this.playerPosition = playerPosition;
    }
    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
