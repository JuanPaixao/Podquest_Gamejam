using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP;
    public string id;
    private GameManager _gameManager;
    [SerializeField] private Transform _player, _player2, _target;
    public float lookDistance, speed, offset;
    private Rigidbody2D _rb;
    public GameObject enemyProjectile;
    public float shootRechargeTime, shootCooldown, projectileSpeed;
    private float _angle;
    public int calories;
    public bool isDefeated, startMoving;
    public float delayOffset;
    private Animator _animator;
    private Animator _blinkAnimator;
    private SpriteRenderer _spriteRenderer;
    private float _followPlayerDistance;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        if (_gameManager.gameMode == "Single")
        {
            _player = FindObjectOfType<PlayerTopDown>().GetComponent<Transform>();
        }
        if (_gameManager.gameMode == "Co-op" || _gameManager.gameMode == "Vs")
        {
            PlayerTopDown[] players = FindObjectsOfType<PlayerTopDown>();
            _player = players[0].GetComponent<Transform>();
            _player2 = players[1].GetComponent<Transform>();
        }
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        shootRechargeTime = shootCooldown;
        Invoke("StartMoving", delayOffset);
        _blinkAnimator = GetComponent<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (this.id == "Boss")
        {
            if (_gameManager.gameMode == "Co-op" && _gameManager.deathCountPlayer == 0)
            {
                this.HP += 25;
            }
        }
    }

    private void Update()
    {
        if (_gameManager.finished)
        {
            this.gameObject.SetActive(false);
        }
        if (!isDefeated)
        {
            if (startMoving)
            {
                if (_gameManager.gameMode == "Single")
                {
                    _target = _player;
                }

                if (_gameManager.gameMode == "Co-op" || _gameManager.gameMode == "Vs")
                {
                    float distanceToPlayer = Vector2.Distance(_player.position, this.transform.position);
                    float distanceToPlayer2 = Vector2.Distance(_player2.position, this.transform.position);
                    PlayerTopDown P1 = _player.GetComponent<PlayerTopDown>();
                    PlayerTopDown P2 = _player2.GetComponent<PlayerTopDown>();
                    if (P1.isDead)
                    {
                        _target = _player2;
                    }
                    else if (P2.isDead)
                    {
                        _target = _player;
                    }
                    else
                    {
                        if (distanceToPlayer < distanceToPlayer2)
                        {
                            _target = _player;
                        }
                        else
                        {
                            _target = _player2;
                        }
                    }
                }

                _followPlayerDistance = Vector2.Distance(_target.position, this.transform.position);
                shootRechargeTime += Time.deltaTime;
                _angle = Mathf.Atan2(_target.position.y, _target.position.x) * Mathf.Rad2Deg;
                LookPosition();

                if (_followPlayerDistance <= lookDistance && _followPlayerDistance > 0.5f)
                {
                    switch (id)
                    {

                        case "Fish":

                            //   Vector3 direction = _player.position - this.transform.position;
                            // this.transform.Translate(direction * speed * Time.deltaTime);
                            if (shootRechargeTime >= shootCooldown)
                            {
                                // Shoot();
                                _animator.SetBool("canAttack", true);
                            }
                            break;

                        case "Cupcake":

                            Vector2 directionFollow = new Vector2(this._target.position.x, this._target.position.y) - new Vector2(this.transform.position.x, this.transform.position.y);
                            _rb.velocity = directionFollow * speed * Time.deltaTime;
                            if (shootRechargeTime >= shootCooldown)
                            {
                                //    Shoot();
                                _animator.SetBool("canAttack", true);
                            }

                            break;

                        case "Boss":

                            Vector2 directionFollowBoss = new Vector2(this._target.position.x, this._target.position.y) - new Vector2(this.transform.position.x, this.transform.position.y);
                            _rb.velocity = directionFollowBoss * speed * Time.deltaTime;
                            if (shootRechargeTime >= shootCooldown)
                            {
                                //    Shoot();
                            }

                            break;

                        case "Turtle":

                            Vector2 directionRetreat = new Vector2(this.transform.position.x, this.transform.position.y) - new Vector2(this._target.position.x, this._target.position.y);
                            _rb.velocity = directionRetreat * speed * Time.deltaTime;
                            if (shootRechargeTime >= shootCooldown)
                            {
                                //     Shoot();
                                _animator.SetBool("canAttack", true);
                            }
                            break;
                    }
                }
                else
                {
                    _rb.velocity = Vector3.zero;
                }
            }
        }
        else
        {
            _rb.velocity = Vector3.zero;
            _rb.isKinematic = true;
            _animator.SetBool("isDead", true);
        }
    }
    public void Shoot()
    {
        Vector2 directionShoot = new Vector2(this._target.position.x, this._target.position.y) - new Vector2(this.transform.position.x, this.transform.position.y);
        GameObject enemyShoot = Instantiate(enemyProjectile, this.transform.position, Quaternion.identity);
        enemyShoot.GetComponent<EnemyProjectile>().SetShootDirection(directionShoot, projectileSpeed, _target);

        shootRechargeTime = 0;
    }
    public void EnemyTakeDamage()
    {
        HP--;
        if (HP <= 0)
        {
            // _gameManager.UpdateEnemyQuantity();
            // Destroy(this.gameObject);
            isDefeated = true;

            if (this.id == "Boss")
            {
                _gameManager.PlaySFX("bossDefeated");
            }
            if (this.id == "Cupcake")
            {
                _gameManager.PlaySFX("cakeDefeated");
            }
            if (this.id == "Turtle")
            {
                _gameManager.PlaySFX("turtleDefeated");
            }
        }
        if (!isDefeated)
        {
            TakeDamage();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDefeated)
        {
            if (other.CompareTag("PlayerProjectile"))
            {

                Destroy(other.gameObject);
                EnemyTakeDamage();
            }
        }
    }
    public void LookPosition()
    {
        Vector3 lookPosition = _target.transform.position;
        Vector2 direction = new Vector2(lookPosition.x - transform.position.x, lookPosition.y - transform.position.y).normalized;
        if (this.id != "Boss")
        {
            transform.right = -direction;
        }
    }
    public void StartMoving()
    {
        startMoving = true;
    }
    public void TakeDamage()
    {
        _spriteRenderer.enabled = false;
        Invoke("Restore", 0.15f);

        if (this.id == "Boss")
        {
            _gameManager.PlaySFX("bossHitted");
        }
        if (this.id == "Cupcake")
        {
            _gameManager.PlaySFX("cakeHitted");
        }
        if (this.id == "Fish")
        {
            _gameManager.PlaySFX("fishDefeated");
        }
        if (this.id == "Turtle")
        {
            _gameManager.PlaySFX("turtleHitted");
        }
    }
    public void Restore()
    {
        _spriteRenderer.enabled = true;
    }
}