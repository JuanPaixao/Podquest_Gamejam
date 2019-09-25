using UnityEngine;

public class GameManager : MonoBehaviour
{

    private Enemy[] enemy;
    public int enemyQuantity, maxEnemyOnRoom, initialRoomQuantity, roomNumber;
    public GameObject[] portals, enemiesObjects;
    public float xMin, zMin, xMax, zMax;

    private void Start()
    {
        initialRoomQuantity = maxEnemyOnRoom;
        maxEnemyOnRoom = initialRoomQuantity + roomNumber;

        for (int i = 0; i < maxEnemyOnRoom; i++)
        {
            int monsterToCreate = Random.Range(0, 3);
            Instantiate(enemiesObjects[monsterToCreate], new Vector3(Random.Range(xMin, xMax), 0.5f, (Random.Range(zMin, zMax))), Quaternion.Euler(90, 0, 0));
        }
        enemy = FindObjectsOfType<Enemy>();
        foreach (Enemy monster in enemy)
        {
            enemyQuantity++;
        }

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
            Instantiate(enemiesObjects[3], new Vector3(Random.Range(xMin, xMax), 0.5f, (Random.Range(zMin, zMax))), Quaternion.Euler(90, 0, 0));
        }
        else
        {
            for (int i = 0; i < maxEnemyOnRoom; i++)
            {
                int monsterToCreate = Random.Range(0, 3);
                Instantiate(enemiesObjects[monsterToCreate], new Vector3(Random.Range(xMin, xMax), 0.5f, (Random.Range(zMin, zMax))), Quaternion.Euler(90, 0, 0));
            }
        }
        enemy = FindObjectsOfType<Enemy>();
        foreach (Enemy monster in enemy)
        {
            enemyQuantity++;
        }
    }
}
