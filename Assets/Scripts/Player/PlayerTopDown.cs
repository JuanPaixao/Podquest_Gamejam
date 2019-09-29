using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTopDown : MonoBehaviour
{
    public float movSpeed, shootSpeed;
    public int HP, maxHP;
    private float _movHor, _movVer;
    [SerializeField] private Animator _animator;
    public bool isMoving;
    public string movingDirection;
    public GameObject projectile, fade;
    private GameManager _gameManager;
    private UIManager _uiManager;
    public bool isDead;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _uiManager = FindObjectOfType<UIManager>();
    }
    private void Start()
    {
        HP = maxHP;
        _animator.SetFloat("Horizontal", 1);
        _animator.SetFloat("Vertical", 0);
        movingDirection = "right";
        _animator.SetInteger("Direction", 0);
    }
    private void Update()
    {
        _movHor = Input.GetAxisRaw("Horizontal");
        _movVer = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(_movHor, _movVer);
        transform.Translate(movement * movSpeed * Time.deltaTime);
        _animator.SetFloat("Speed", Mathf.Abs(movement.magnitude));

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
        if (isMoving)
        {
            _animator.SetFloat("Horizontal", _movHor);
            _animator.SetFloat("Vertical", _movVer);


            if (_movHor > 0)
            {
                movingDirection = "right";
                _animator.SetInteger("Direction", 0);
            }
            else if (_movHor < 0)
            {
                movingDirection = "left";
                _animator.SetInteger("Direction", 2);
            }
            else if (_movVer > 0)
            {
                movingDirection = "up";
                _animator.SetInteger("Direction", 1);
            }
            else if (_movVer < 0)
            {
                movingDirection = "down";
                _animator.SetInteger("Direction", 3);
            }
        }
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject shoot = Instantiate(projectile, this.transform.position, Quaternion.identity);
            shoot.GetComponent<Projectile>().CreateProjectile(this.movingDirection, this.shootSpeed);
            _animator.SetTrigger("Shoot");
        }
    }
    public void FinishShoot()
    {

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
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _gameManager.AddCalories(tempEnemy.calories);
                    _animator.SetTrigger("Catch");
                    Destroy(other.gameObject);
                    _gameManager.UpdateEnemyQuantity();
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
        _uiManager.SetHP(HP);
        if (HP <= 0)
        {
            isDead = true;
            Invoke("Defeated", 2f);
        }
        if (HP > 0)
        {
            _spriteRenderer.enabled = false;
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
        _gameManager.LoadScene("Dungeon");
    }
}
