using UnityEngine;

public class GameManager : MonoBehaviour
{

    private Enemy[] enemy;
    public int enemyQuantity, maxEnemyOnRoom, initialRoomQuantity, roomNumber;
    public GameObject[] portals, enemiesObjects;
    public float xMin, yMin, xMax, yMax;
    public int caloriesQuantity;
    public int caloriesToGet;

    private void Start()
    {
        initialRoomQuantity = maxEnemyOnRoom;
        maxEnemyOnRoom = initialRoomQuantity + roomNumber;
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
            Debug.Log("next stage, door number " + randomDoor);
            portals[randomDoor].SetActive(true);
        }
    }
    public void CreateEnemies()
    {
        roomNumber++;
        maxEnemyOnRoom = initialRoomQuantity + roomNumber;

        if (roomNumber % 5 == 0)
        {
            Instantiate(enemiesObjects[3], new Vector2(Random.Range(xMin, xMax), (Random.Range(yMin, yMax))), Quaternion.identity);
        }
        else
        {
            for (int i = 0; i < maxEnemyOnRoom; i++)
            {
                int monsterToCreate = Random.Range(0, 3);
                Instantiate(enemiesObjects[monsterToCreate], new Vector2(Random.Range(xMin, xMax), (Random.Range(yMin, yMax))), Quaternion.identity);
            }
        }
        enemy = FindObjectsOfType<Enemy>();
        foreach (Enemy monster in enemy)
        {
            enemyQuantity++;
        }
    }
    public void AddCalories(int calories)
    {
        this.caloriesQuantity += calories;
    }
}
