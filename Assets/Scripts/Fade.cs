using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public GameManager gameManager;
    public void FinishedFade()
    {
        this.gameObject.SetActive(false);
        gameManager.CreateEnemies();
    }
}
