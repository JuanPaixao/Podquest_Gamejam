using UnityEngine;

public class DungeonCreator : MonoBehaviour
{
    public DungeonWall[] floors;
    public GameObject door;
    public int roomQuantity;
    public DungeonDoor[] doors;
    public void RoomConstructed()
    {
        roomQuantity--;
        if (roomQuantity == 0)
        {
            floors = FindObjectsOfType<DungeonWall>();

            foreach (var floor in floors)
            {
                floor.CheckBorders();
            }

            foreach (var floor in floors)
            {
                if (floor.left)
                {
                    Instantiate(door, new Vector3(floor.transform.position.x - 1.52f, floor.transform.position.y, floor.transform.position.z), Quaternion.identity, floor.transform);
                }
                if (floor.right)
                {
                    Instantiate(door, new Vector3(floor.transform.position.x + 1.52f, floor.transform.position.y, floor.transform.position.z), Quaternion.identity, floor.transform);
                }
                if (floor.up)
                {
                    Instantiate(door, new Vector3(floor.transform.position.x, floor.transform.position.y + 1.52f, floor.transform.position.z), Quaternion.identity, floor.transform);
                }
                if (floor.down)
                {
                    Instantiate(door, new Vector3(floor.transform.position.x, floor.transform.position.y - 1.52f, floor.transform.position.z), Quaternion.identity, floor.transform);
                }
            }
            doors = FindObjectsOfType<DungeonDoor>();

            foreach (var door in doors)
            {
                door.VerifyDoorContact();
            }
        }
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
