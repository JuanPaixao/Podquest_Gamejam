using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string direction;
    public float projectileSpeed;

    private void Start()
    {
        Invoke("Destroy", 4);
        this.transform.Rotate(0, 0, 0);
    }
    public void CreateProjectile(string direction, float projectileSpeed)
    {
        this.direction = direction;
        this.projectileSpeed = projectileSpeed;
        if (direction == "right")
        {
            // this.transform.Rotate(90, 180, 90);
        }
        if (direction == "left")
        {
            //   this.transform.Rotate(90, 180, 270);
        }
        if (direction == "up")
        {
            //   this.transform.Rotate(90, 180, 180);
        }
        if (direction == "down")
        {
            //  this.transform.Rotate(90, 180, 0);
        }
    }
    private void Update()
    {
        switch (direction)
        {

            case "right":
                transform.Translate(new Vector3(projectileSpeed * Time.deltaTime, 0, 0));
                break;
            case "left":
                transform.Translate(new Vector3(-projectileSpeed * Time.deltaTime, 0, 0));
                break;
            case "up":
                transform.Translate(new Vector3(0, 0, projectileSpeed * Time.deltaTime));
                break;
            case "down":
                transform.Translate(new Vector3(0, 0, -projectileSpeed * Time.deltaTime));
                break;

        }
    }
    private void Destroy()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Room"))
        {
            Destroy();
        }
    }
}
