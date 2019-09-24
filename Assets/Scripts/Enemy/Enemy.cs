using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP;
    public int id;
    private GameManager _gameManager;
    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {

    }
    public void EnemyTakeDamage()
    {
        HP--;
        if (HP <= 0)
        {
            _gameManager.UpdateEnemyQuantity();
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerProjectile"))
        {
            Destroy(other.gameObject);
            EnemyTakeDamage();
        }
    }
}
