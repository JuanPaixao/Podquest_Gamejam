﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public int state;
    private Animator _animator;
    public GameObject pausePanel;
    private bool moved;
    public float cooldownStick, actualStickTime;
    private GameManager _gameManager;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _gameManager = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        actualStickTime = cooldownStick;
    }
    private void Update()
    {
        actualStickTime += Time.unscaledDeltaTime;
        float horInputJoystick = Input.GetAxisRaw("VerticalJoystick");
        if (moved)
        {
            horInputJoystick = 0;
        }
        if (actualStickTime > cooldownStick)
        {
            moved = false;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || horInputJoystick <= -0.8f)
        {
            state++;
            horInputJoystick = 0;
            if (state > 2)
            {
                state = 0;
            }
            _animator.SetInteger("State", state);
            moved = true;
            actualStickTime = 0;

        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || horInputJoystick >= 0.8f)
        {
            state--;
            horInputJoystick = 0;
            if (state < 0)
            {
                state = 2;
            }
            _animator.SetInteger("State", state);
            moved = true;
            actualStickTime = 0;
        }
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Return) || Input.GetButton("Xbox_A"))
        {
            if (state == 0)
            {
                _gameManager.Resume();
            }
            if (state == 1)
            {
                _gameManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (state == 2)
            {
                _gameManager.LoadScene("Menu");
            }
        }
    }
}
