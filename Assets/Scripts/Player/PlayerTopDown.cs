using UnityEngine;

public class PlayerTopDown : MonoBehaviour
{
    public float movSpeed, shootSpeed;
    public int HP, maxHP;
    [SerializeField] private float _movHor, _movVer, _movHorRot, _movVerRot;
    [SerializeField] private Animator _animator;
    public bool isMoving;
    public string movingDirection;
    public GameObject projectile, fade;
    private GameManager _gameManager;
    private UIManager _uiManager;
    public bool isDead;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public float cooldownToShoot, rechargeTime;
    public string sceneToLoad;
    public int playerNumber;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _uiManager = FindObjectOfType<UIManager>();
    }
    private void Start()
    {
        cooldownToShoot = rechargeTime;
        HP = maxHP;
        _animator.SetFloat("Horizontal", 0);
        _animator.SetFloat("Vertical", 1);
        movingDirection = "up";
        _animator.SetInteger("Direction", 1);
    }
    private void Update()
    {
        if (!_gameManager.finished)
        {
            if (!isDead)
            {
                if (playerNumber == 1)
                {
                    _movHor = Input.GetAxisRaw("HorizontalKeyboard");
                    _movVer = Input.GetAxisRaw("VerticalKeyboard");

                    _movHorRot = Input.GetAxisRaw("HorizontalRot");
                    _movVerRot = Input.GetAxisRaw("VerticalRot");
                }
                if (playerNumber == 2)
                {
                    _movHor = Input.GetAxis("HorizontalJoystick");
                    _movVer = Input.GetAxis("VerticalJoystick");

                    _movHorRot = Input.GetAxis("RightRotHorJoystick");
                    _movVerRot = Input.GetAxis("RightRoVertJoystick");
                }

                Vector2 movement = new Vector2(_movHor, _movVer);
                transform.Translate(movement * movSpeed * Time.deltaTime);
                _animator.SetFloat("Speed", Mathf.Abs(movement.magnitude));
                cooldownToShoot -= Time.deltaTime;

                if (this.transform.position.x <= -12.79f)
                {
                    this.transform.position = new Vector2(-12.79f, this.transform.position.y);
                }
                if (this.transform.position.x >= 9.76)
                {
                    this.transform.position = new Vector2(9.76f, this.transform.position.y);
                }
                if (this.transform.position.y >= 6.89f)
                {
                    this.transform.position = new Vector2(this.transform.position.x, 6.89f);
                }
                if (this.transform.position.y <= -5.55f)
                {
                    this.transform.position = new Vector2(this.transform.position.x, -5.55f);
                }
                //
                if (movement.magnitude != 0)
                {
                    isMoving = true;
                }
                else
                {
                    isMoving = false;
                }
                //


                if (_movHorRot > 0.9)
                {
                    movingDirection = "right";
                    _animator.SetInteger("Direction", 0);
                }
                if (_movHorRot < -0.9)
                {
                    movingDirection = "left";
                    _animator.SetInteger("Direction", 2);
                }
                if (_movVerRot > 0.9)
                {
                    movingDirection = "up";
                    _animator.SetInteger("Direction", 1);
                }
                if (_movVerRot < -0.9)
                {
                    movingDirection = "down";
                    _animator.SetInteger("Direction", 3);
                }


                if (movingDirection == "right")
                {
                    _animator.SetFloat("Horizontal", 1);
                    _animator.SetFloat("Vertical", 0);
                }
                if (movingDirection == "left")
                {
                    _animator.SetFloat("Horizontal", -1);
                    _animator.SetFloat("Vertical", 0);
                }
                if (movingDirection == "up")
                {
                    _animator.SetFloat("Horizontal", 0);
                    _animator.SetFloat("Vertical", 1);
                }
                if (movingDirection == "down")
                {
                    _animator.SetFloat("Horizontal", 0);
                    _animator.SetFloat("Vertical", -1);
                }

                if (cooldownToShoot <= 0)
                {
                    if (playerNumber == 1)
                    {
                        if (Input.GetButton("Jump"))
                        {
                            GameObject shoot = Instantiate(projectile, this.transform.position, Quaternion.identity);
                            shoot.GetComponent<Projectile>().CreateProjectile(this.movingDirection, this.shootSpeed);
                            _animator.SetTrigger("Shoot");
                            _gameManager.PlaySFX("playerShoot");
                            cooldownToShoot = rechargeTime;
                        }
                    }
                    if (playerNumber == 2)
                    {
                        if (Input.GetAxisRaw("JoystickShoot_L") != 0)
                        {
                            GameObject shoot = Instantiate(projectile, this.transform.position, Quaternion.identity);
                            shoot.GetComponent<Projectile>().CreateProjectile(this.movingDirection, this.shootSpeed);
                            _animator.SetTrigger("Shoot");
                            _gameManager.PlaySFX("playerShoot");
                            cooldownToShoot = rechargeTime;
                        }
                    }
                }
            }
            else if (isDead)
            {
                _animator.SetTrigger("Dead");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_gameManager.enemyQuantity <= 0)
        {
            if (other.gameObject.CompareTag("Portal"))
            {
                var portal = other.GetComponent<Portal>();

                if (portal.portalSide == "left")
                {

                }
                if (portal.portalSide == "right")
                {

                }
                if (portal.portalSide == "down")
                {

                }
                if (portal.portalSide == "up")
                {

                }
                fade.SetActive(true);
                _gameManager.PlaySFX("doorSound");
            }
        }
        if (other.gameObject.CompareTag("EnemyShoot"))
        {
            TakeDamage();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemyTemp = other.GetComponent<Enemy>();
            if (!enemyTemp.isDefeated)
            {
                TakeDamage();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy tempEnemy = other.GetComponent<Enemy>();
            Debug.Log(tempEnemy.name);
            if (tempEnemy.isDefeated)
            {
                if (playerNumber == 1)
                {
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        _gameManager.AddCalories(tempEnemy.calories);
                        _animator.SetTrigger("Catch");
                        Destroy(other.gameObject);
                        _gameManager.UpdateEnemyQuantity();
                        _gameManager.PlaySFX("playerGrab");
                    }
                }

                if (playerNumber == 2)
                {
                    if (Input.GetButton("Xbox_A"))
                    {
                        _gameManager.AddCalories(tempEnemy.calories);
                        _animator.SetTrigger("Catch");
                        Destroy(other.gameObject);
                        _gameManager.UpdateEnemyQuantity();
                        _gameManager.PlaySFX("playerGrab");
                    }
                }
                //  else if (Input.GetKeyDown(KeyCode.LeftControl))
                //  {
                //    Recover();
                //      Destroy(other.gameObject);
                //      _gameManager.UpdateEnemyQuantity();
                //  }
            }
        }
    }

    private void TakeDamage()
    {
        HP--;
        if (playerNumber == 1)
        {
            _uiManager.SetHP(HP, 1);
        }
        if (playerNumber == 2)
        {
            _uiManager.SetHP(HP, 2);
        }
        if (HP <= 0)
        {
            if (_gameManager.gameMode == "Single")
            {
                isDead = true;
                _gameManager.PlaySFX("playerDefeated");
                Invoke("Defeated", 3f);
            }
            else if (_gameManager.gameMode == "Co-op")
            {
                isDead = true;
                _gameManager.PlaySFX("playerDefeated");
                _uiManager.DeathPanel(this.playerNumber);
                _gameManager.deathCountPlayer++;
                this.GetComponent<CapsuleCollider2D>().enabled = false;
                if (_gameManager.deathCountPlayer >= 2)
                {
                    Invoke("Defeated", 3f);
                }
            }
        }
        if (HP > 0)
        {
            _spriteRenderer.enabled = false;
            _gameManager.PlaySFX("playerHitted");
            Invoke("Restore", 0.15f);
        }
    }
    public void Restore()
    {
        _spriteRenderer.enabled = true;
    }
    public void Recover()
    {
        HP += 2;
        if (HP > maxHP)
        {
            HP = maxHP;
        }
    }
    public void Defeated()
    {
        _gameManager.LoadScene(sceneToLoad);
    }
}