using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public string sceneName;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(TypeRoutine());
    }
    private IEnumerator TypeRoutine()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        if (this.sceneName == "Game")
        {
            yield return new WaitForSeconds(20f);
            _gameManager.LoadScene("Dungeon");
        }
        else if (this.sceneName == "Menu")
        {
            yield return new WaitForSeconds(46.5f);
            _gameManager.LoadScene("Dungeon");
        }
    }
}
