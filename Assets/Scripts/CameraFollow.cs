using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float camSpeed;
    public float minX, maxX, minY, maxY;
    private void Start()
    {
        transform.position = playerTransform.position;
    }
    private void Update()
    {
        if (playerTransform != null)
        {
            float clampedX = Mathf.Clamp(playerTransform.position.x, minX, maxX);
            float clampedY = Mathf.Clamp(playerTransform.position.y, minY, maxY);
            transform.position = Vector2.Lerp(transform.position, new Vector3(clampedX, clampedY, -10f), camSpeed);
        }
    }
}
