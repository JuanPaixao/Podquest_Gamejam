using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private Enemy[] enemy;
    public int enemyQuantity, maxEnemyOnRoom, initialRoomQuantity, roomNumber;
    public GameObject[] portals, enemiesObjects;
    public float xMin, yMin, xMax, yMax, xMaxP2, xMinP2, yMinP2, yMaxP2;
    public int caloriesQuantity;
    public int caloriesToGet;
    public UIManager uIManager;
    private SceneManager _sceneManager;
    public RandomizeTile randomizeTile;
    public GameObject finishGamePanel, pressAnythingToPlay, arrowSelectionMenu, skipObject;
    public GameObject[] arrowUI;
    public bool finished, canPlay, canGoToScene;
    public string sceneName;
    public AudioSource audioSource;
    public AudioClip finishMusic;
    public AudioClip cakeHitted, turtleHitted, bossHitted, playerHitted, cakeShoot, fishShoot, turtleShoot, bossShoot, playerShoot,
    cakeDefeated, fishDefeated, turtleDefeated, bossDefeated, playerDefeated, playerGrab, doorSound;
    public string gameMode;
    public int deathCountPlayer;
    public bool isVs, paused;
    public int id;
    public GameObject menuContent, pausePanel;
    private void Start()
    {
        initialRoomQuantity = maxEnemyOnRoom;
        maxEnemyOnRoom = initialRoomQuantity + roomNumber;
        Cursor.visible = false;

        if (this.sceneName == "Menu")
        {
            Invoke("ActivePressToPlay", 4);
        }
        Time.timeScale = 1;
        paused = false;
    }
    private void Update()
    {
        if (this.sceneName == "Menu")
        {
            if (Input.anyKeyDown && canPlay)
            {
                //  introPanel.SetActive(true);
                arrowSelectionMenu.SetActive(true);
                pressAnythingToPlay.SetActive(false);
                Invoke("CanPlay", 4);
            }
        }
        if (this.sceneName == "Introduction")
        {
            if (Input.GetKey(KeyCode.Escape) || (Input.GetButton("Escape")))
            {
                LoadScene("Menu");
            }
        }
        else
        {
            if (Input.GetButtonDown("Pause"))
            {
                Pause();
            }
        }
    }
    public void ActivePressToPlay()
    {
        canPlay = true;
        pressAnythingToPlay.SetActive(true);
    }
    public void CanPlay()
    {
        canGoToScene = true;
        skipObject.SetActive(true);
    }
    public void DeactivePortals()
    {
        foreach (var portal in portals)
        {
            portal.SetActive(false);
        }
    }
    public void UpdateEnemyQuantity()
    {
        enemyQuantity--;
        if (this.enemyQuantity <= 0)
        {
            DeactivePortals();
            int randomDoor = Random.Range(0, 4);
            portals[randomDoor].SetActive(true);
            DoorAnimation doorAnim = portals[randomDoor].GetComponent<DoorAnimation>();
            doorAnim.SetDoorStatus(false);
            Debug.Log("next stage, door number " + doorAnim.name);
            doorAnim = portals[randomDoor].GetComponent<DoorAnimation>();
            doorAnim.SetDoorStatus(true);
            arrowUI[randomDoor].SetActive(true);
        }
    }
    public void FinishGame()
    {
        if (!finished)
        {
            finishGamePanel.SetActive(true);
            audioSource.Stop();
            audioSource.PlayOneShot(finishMusic, 1);
            finished = true;
        }
    }
    public void CreateEnemies()
    {
        GameObject[] splat = GameObject.FindGameObjectsWithTag("Splat");
        foreach (GameObject s in splat)
        {
            Destroy(s);
        }
        randomizeTile.RandomizeGround();
        enemyQuantity = 0;
        roomNumber++;
        foreach (var arrow in arrowUI)
        {
            arrow.SetActive(false);
        }
        if (gameMode == "Single" || deathCountPlayer > 0)
        {
            maxEnemyOnRoom = initialRoomQuantity + roomNumber;
        }
        else if (gameMode == "Co-op")
        {
            maxEnemyOnRoom = initialRoomQuantity + roomNumber * 2;
        }

        if (roomNumber % 5 == 0)
        {
            var tempEnemyObject = Instantiate(enemiesObjects[5], new Vector2(Random.Range(xMin, xMax), (Random.Range(yMin, yMax))), Quaternion.identity);
            tempEnemyObject.GetComponent<Enemy>().createdFromPlayer = id;
        }

        else
        {
            for (int i = 0; i < maxEnemyOnRoom; i++)
            {
                int monsterToCreate = Random.Range(0, 5);
                var tempEnemyObject = Instantiate(enemiesObjects[monsterToCreate], new Vector2(Random.Range(xMin, xMax), (Random.Range(yMin, yMax))), Quaternion.identity);
                tempEnemyObject.GetComponent<Enemy>().createdFromPlayer = id;

            }
        }
        enemy = FindObjectsOfType<Enemy>();

        foreach (Enemy monster in enemy)
        {
            int tempEnemyID = monster.createdFromPlayer;
            if (tempEnemyID == this.id)
            {
                enemyQuantity++;
            }
        }
    }
    public void AddCalories(int calories)
    {
        this.caloriesQuantity += calories;
        uIManager.SetScore(caloriesQuantity);
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
    public void PlaySFX(string audioClip)
    {
        switch (audioClip)
        {
            case "cakeHitted":
                audioSource.PlayOneShot(cakeHitted, 1);
                break;
            case "turtleHitted":
                audioSource.PlayOneShot(turtleHitted, 1);
                break;
            case "bossHitted":
                audioSource.PlayOneShot(bossHitted, 1);
                break;
            case "playerHitted":
                audioSource.PlayOneShot(playerHitted, 1);
                break;
            case "cakeShoot":
                audioSource.PlayOneShot(cakeShoot, 1);
                break;
            case "fishShoot":
                audioSource.PlayOneShot(fishShoot, 1);
                break;
            case "turtleShoot":
                audioSource.PlayOneShot(turtleShoot, 1);
                break;
            case "bossShoot":
                audioSource.PlayOneShot(bossShoot, 1);
                break;
            case "playerShoot":
                audioSource.PlayOneShot(playerShoot, 1);
                break;
            case "cakeDefeated":
                audioSource.PlayOneShot(cakeDefeated, 1);
                break;
            case "fishDefeated":
                audioSource.PlayOneShot(fishDefeated, 1);
                break;
            case "turtleDefeated":
                audioSource.PlayOneShot(turtleDefeated, 1);
                break;
            case "bossDefeated":
                audioSource.PlayOneShot(bossDefeated, 1);
                break;
            case "playerDefeated":
                audioSource.PlayOneShot(playerDefeated, 1);
                break;
            case "playerGrab":
                audioSource.PlayOneShot(playerGrab, 1);
                break;
            case "doorSound":
                audioSource.PlayOneShot(doorSound, 1);
                break;
        }
    }
    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }
    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }
}