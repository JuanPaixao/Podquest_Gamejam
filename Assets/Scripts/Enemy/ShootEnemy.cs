using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    private Enemy _enemy;
    private GameManager _gameManager;
    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
        _gameManager = FindObjectOfType<GameManager>();
    }
    public void Shoot()
    {
        if (this._enemy.id == "Boss")
        {
            _gameManager.PlaySFX("bossShoot");
        }
        if (this._enemy.id == "Cupcake")
        {
            _gameManager.PlaySFX("cakeShoot");
        }
        if (this._enemy.id == "Fish")
        {
            _gameManager.PlaySFX("fishShoot");
        }
        if (this._enemy.id == "Turtle")
        {
            _gameManager.PlaySFX("turtleShoot");
        }
        _enemy.Shoot();
    }
}
