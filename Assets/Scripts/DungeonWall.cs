using UnityEngine;

public class DungeonWall : MonoBehaviour
{
    public bool isBoss;
    public Transform[] raycastLocations;
    public RaycastHit2D hitRight, hitUp, hitLeft, hitDown;
    public LayerMask layerMask;
    public int distance;
    public bool right, up, left, down, center, created;
    public GameObject room;
    public DungeonCreator dungeonCreator;
    public float latOffset, longOffset;
    public int randomPos;
    public bool locked;
    public bool isAcessibleNextRoom;
    public int numberOfAdjacentRooms;
    public void Start()
    {
        CheckBorders();
        CreateRoom();
    }
    public void CreateRoom()
    {
        if (!created)
        {
            if (dungeonCreator.roomQuantity > 0)
            {
                if (!right || !up || !left || !down)
                {
                    Verify();
                }
                else
                {
                    locked = true;
                    dungeonCreator.UnlockDungeon();
                }
            }
        }
    }
    public void CheckBorders()
    {
        hitRight = Physics2D.Raycast(raycastLocations[0].position, Vector2.right, distance, layerMask);
        hitUp = Physics2D.Raycast(raycastLocations[1].position, Vector2.up, distance, layerMask);
        hitLeft = Physics2D.Raycast(raycastLocations[2].position, Vector2.left, distance, layerMask);
        hitDown = Physics2D.Raycast(raycastLocations[3].position, Vector2.down, distance, layerMask);


        if (hitRight.collider == null)
        {
            right = false;
        }
        else
        {
            right = true;
        }
        /*----*/
        if (hitUp.collider == null)
        {
            up = false;
        }
        else
        {
            up = true;
        }
        /*----*/
        if (hitDown.collider == null)
        {
            down = false;
        }
        else
        {
            down = true;
        }

        /*----*/
        if (hitLeft.collider == null)
        {
            left = false;
        }
        else
        {
            left = true;
        }
        if (left && right && down && up)
        {
            locked = true;
        }
        numberOfAdjacentRooms = 0;
        if (left)
        {
            numberOfAdjacentRooms++;
        }
        if (right)
        {
            numberOfAdjacentRooms++;
        }

        if (up)
        {
            numberOfAdjacentRooms++;
        }

        if (down)
        {
            numberOfAdjacentRooms++;
        }


    }

    public void UnlockRoom()
    {
        if (dungeonCreator.roomQuantity > 0)
        {
            if (!right || !up || !left || !down)
            {
                Verify();
            }
        }
    }
    public void Verify()
    {
        CheckBorders();
        if (!locked)
        {
            if (dungeonCreator.roomQuantity > 0)
            {
                randomPos = Random.Range(0, 4);
                switch (randomPos)
                {
                    case 0:
                        if (!right)
                        {
                            DungeonWall tempDW = room.GetComponentInParent<DungeonWall>();
                            Instantiate(room, new Vector3(raycastLocations[0].position.x + longOffset, raycastLocations[0].position.y, raycastLocations[0].position.z), Quaternion.identity);
                            dungeonCreator.RoomConstructed();
                            created = true;
                        }
                        else
                        {
                            Verify();
                        }
                        break;


                    case 1:
                        if (!up)
                        {
                            DungeonWall tempDW = room.GetComponentInParent<DungeonWall>();
                            Instantiate(room, new Vector3(raycastLocations[1].position.x, raycastLocations[1].position.y + latOffset, raycastLocations[1].position.z), Quaternion.identity);
                            dungeonCreator.RoomConstructed();
                            created = true;
                        }
                        else
                        {
                            Verify();
                        }
                        break;

                    case 2:
                        if (!left)
                        {
                            DungeonWall tempDW = room.GetComponentInParent<DungeonWall>();
                            Instantiate(room, new Vector3(raycastLocations[2].position.x - longOffset, raycastLocations[2].position.y, raycastLocations[2].position.z), Quaternion.identity);
                            dungeonCreator.RoomConstructed();
                            created = true;
                        }
                        else
                        {
                            Verify();
                        }
                        break;

                    case 3:
                        if (!down)
                        {
                            DungeonWall tempDW = room.GetComponentInParent<DungeonWall>();
                            Instantiate(room, new Vector3(raycastLocations[3].position.x, raycastLocations[3].position.y - latOffset, raycastLocations[3].position.z), Quaternion.identity);
                            dungeonCreator.RoomConstructed();
                            created = true;
                        }
                        else
                        {
                            Verify();
                        }
                        break;
                }
            }
        }
        else
        {
            dungeonCreator.UnlockDungeon();
        }
    }
    public void DestroyRoom()
    {
        Destroy(this.gameObject);
    }
}
