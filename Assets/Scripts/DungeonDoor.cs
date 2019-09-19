using UnityEngine;

public class DungeonDoor : MonoBehaviour
{
    public Transform[] raycastLocations;
    public bool doorRight, doorLeft, doorBottom, doorUp;
    public LayerMask layerMask;
    public void VerifyDoorContact()
    {
        doorRight = Physics2D.Raycast(raycastLocations[0].position, Vector2.right, 0, layerMask);
        doorUp = Physics2D.Raycast(raycastLocations[1].position, Vector2.up, 0, layerMask);
        doorLeft = Physics2D.Raycast(raycastLocations[2].position, Vector2.left, 0, layerMask);
        doorBottom = Physics2D.Raycast(raycastLocations[3].position, Vector2.down, 0, layerMask);

        if (doorRight || doorUp || doorLeft || doorBottom)
        {
            var room = GetComponentInParent<DungeonWall>();
            room.isAcessibleNextRoom = true;
        }
    }
}
