using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    private Enemy _enemy;
    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }
    public void Shoot()
    {
        _enemy.Shoot();
    }
}
