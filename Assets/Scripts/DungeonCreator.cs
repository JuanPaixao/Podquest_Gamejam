using UnityEngine;

public class DungeonCreator : MonoBehaviour
{
    public DungeonWall wall;
    public GameObject dungeonDoor;
    public int roomQuantity;
    private void Start()
    {
        // GenerateDungeonWall();
        // roomQuantity = Random.Range(15, 31);
        //wall.OnCreate();
    }
    public void RoomConstructed()
    {
        roomQuantity--;
    }
    public void UnlockDungeon()
    {
        DungeonWall[] rooms = FindObjectsOfType<DungeonWall>();
        foreach (var room in rooms)
        {
            if (!room.locked)
            {
                room.UnlockRoom();
            }
        }
    }
}
