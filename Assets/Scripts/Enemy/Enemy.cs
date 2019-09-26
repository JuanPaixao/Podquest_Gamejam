using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP;
    public string id;
    private GameManager _gameManager;
    private Transform _player;
    public float lookDistance, speed;
    private Rigidbody _rb;
    public GameObject enemyProjectile;
    public float shootRechargeTime, shootCooldown, projectileSpeed;
    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _player = FindObjectOfType<PlayerTopDown>().GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
        shootRechargeTime = shootCooldown;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(_player.position, this.transform.position);
        shootRechargeTime += Time.deltaTime;
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

                    Vector3 directionFollow = new Vector3(this._player.position.x, 0, this._player.position.z) - new Vector3(this.transform.position.x, 0, this.transform.position.z);
                    _rb.velocity = directionFollow * speed * Time.deltaTime;
                    transform.LookAt(_player.position);
                    if (shootRechargeTime >= shootCooldown)
                    {
                        Shoot();
                    }

                    break;
                case "Turtle":
                    Vector3 directionRetreat = new Vector3(this.transform.position.x, 0, this.transform.position.z) - new Vector3(this._player.position.x, 0, this._player.position.z);
                    _rb.velocity = directionRetreat * speed * Time.deltaTime;
                    transform.LookAt(_player.position);
                    if (shootRechargeTime >= shootCooldown)
                    {
                        Shoot();

                    }
                    break;
            }
        }
        else
        {
            Debug.Log("cant see");
        }

    }
    public void Shoot()
    {
        Vector3 directionShoot = new Vector3(this._player.position.x, 0.5f, this._player.position.z) - new Vector3(this.transform.position.x, 0, this.transform.position.z);
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerProjectile"))
        {
            Destroy(other.gameObject);
            EnemyTakeDamage();
        }
    }
}
