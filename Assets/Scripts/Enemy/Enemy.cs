using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP;
    public string id;
    private GameManager _gameManager;
    private Transform _player;
    public float lookDistance, speed, offset;
    private Rigidbody2D _rb;
    public GameObject enemyProjectile;
    public float shootRechargeTime, shootCooldown, projectileSpeed;
    private float _angle;
    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _player = FindObjectOfType<PlayerTopDown>().GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        shootRechargeTime = shootCooldown;
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(_player.position, this.transform.position);

        shootRechargeTime += Time.deltaTime;
        _angle = Mathf.Atan2(_player.position.y, _player.position.x) * Mathf.Rad2Deg;
        LookPosition();
        if (distanceToPlayer <= lookDistance && distanceToPlayer > 0.5f)
        {


            switch (id)
            {

                case "Fish":

                    //   Vector3 direction = _player.position - this.transform.position;
                    // this.transform.Translate(direction * speed * Time.deltaTime);
                    if (shootRechargeTime >= shootCooldown)
                    {
                        Shoot();
                    }
                    break;

                case "Cupcake":

                    Vector2 directionFollow = new Vector2(this._player.position.x, this._player.position.y) - new Vector2(this.transform.position.x, this.transform.position.y);
                    _rb.velocity = directionFollow * speed * Time.deltaTime;
                    if (shootRechargeTime >= shootCooldown)
                    {
                        Shoot();
                    }

                    break;

                case "Turtle":

                    Vector2 directionRetreat = new Vector2(this.transform.position.x, this.transform.position.y) - new Vector2(this._player.position.x, this._player.position.y);
                    _rb.velocity = directionRetreat * speed * Time.deltaTime;
                    if (shootRechargeTime >= shootCooldown)
                    {
                        Shoot();

                    }
                    break;
            }
        }
        else
        {
            _rb.velocity = Vector3.zero;
        }

    }
    public void Shoot()
    {
        Vector2 directionShoot = new Vector2(this._player.position.x, this._player.position.y) - new Vector2(this.transform.position.x, this.transform.position.y);
        GameObject enemyShoot = Instantiate(enemyProjectile, this.transform.position, Quaternion.identity);
        enemyShoot.GetComponent<EnemyProjectile>().SetShootDirection(directionShoot, projectileSpeed, _player);

        shootRechargeTime = 0;
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerProjectile"))
        {
            Destroy(other.gameObject);
            EnemyTakeDamage();
        }
    }
    public void LookPosition()
    {
        Vector3 lookPosition = _player.transform.position;
        Vector2 direction = new Vector2(lookPosition.x - transform.position.x, lookPosition.y - transform.position.y).normalized;
        transform.right = -direction;
    }
}
