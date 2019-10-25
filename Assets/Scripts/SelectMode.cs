using UnityEngine;

public class SelectMode : MonoBehaviour
{
    public int state;
    private Animator _animator;
    private Dialog _dialog;
    public string sceneToLoad;
    public GameObject introPanel;
    private bool moved;
    public float cooldownStick, actualStickTime;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _dialog = FindObjectOfType<Dialog>();

    }
    private void Start()
    {
        actualStickTime = cooldownStick;
    }
    private void Update()
    {
        actualStickTime += Time.deltaTime;
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
                sceneToLoad = "Dungeon";
            }
            if (state == 1)
            {
                sceneToLoad = "Dungeon2P";
            }
            if (state == 2)
            {
                sceneToLoad = "DungeonVs";
            }
            introPanel.SetActive(true);
        }
    }
}
