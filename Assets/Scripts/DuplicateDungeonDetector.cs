using UnityEngine;

public class DuplicateDungeonDetector : MonoBehaviour
{
    public DungeonCreator dungeonCreator;
    public DungeonWall dungeonWall;


    private void OnTriggerEnter2D(Collider2D other)
    {
        dungeonWall.center = true;
        if (other.gameObject.name != this.gameObject.name)
        {
            DungeonWall objTemp = other.GetComponentInParent<DungeonWall>();
            if (objTemp.created)
            {
                Destroy(other.gameObject);
                dungeonCreator.roomQuantity++;
            }
        }
    }

}
