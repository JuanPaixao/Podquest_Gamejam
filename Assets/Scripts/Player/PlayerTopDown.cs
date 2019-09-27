using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTopDown : MonoBehaviour
{
    public float movSpeed, shootSpeed;
    public int HP;
    private float _movHor, _movVer;
    private Animator _animator;
    public bool isMoving;
    public string movingDirection;
    public GameObject projectile, fade;
    private GameManager _gameManager;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _gameManager = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
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
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
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
    }
}
