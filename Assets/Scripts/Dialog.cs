using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public int index;
    public float typingSpeed;
    public string sceneName;
    private GameManager _gameManager;
    public string sceneToLoad;
    private SelectMode _selectMode;
    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();

        if (this.sceneName == "Game" && !_gameManager.isVs)
        {
            if (_gameManager.caloriesQuantity > _gameManager.caloriesToGet)
            {
                index = 0;
            }
            else
            {
                index = 1;
            }
        }
        if (this.sceneName == "Menu")
        {
            _selectMode = FindObjectOfType<SelectMode>();
            this.sceneToLoad = _selectMode.sceneToLoad;
        }
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
            string thisScene = SceneManager.GetActiveScene().name;
            _gameManager.LoadScene(thisScene);
        }
        if (this.sceneName == "Menu")
        {
            yield return new WaitForSeconds(32.5f);
            _gameManager.LoadScene(sceneToLoad);
        }

    }
}
