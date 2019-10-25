using System.Collections;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour
{
    public int deathCountPlayer, player1Score, player2Score;
    public bool player1Dead, player2Dead, p1Win, p2Win;
    public GameObject deathPlayer1, deathPlayer2, dialog;
    private Dialog _dialog;
    private void Awake()
    {
        _dialog = dialog.GetComponent<Dialog>();
    }
    public void AddDeathCount(int playerID, int playerScore)
    {
        deathCountPlayer++;

        if (playerID == 1)
        {
            player1Dead = true;
            player1Score = playerScore;
            deathPlayer1.SetActive(true);
        }
        if (playerID == 2)
        {
            player2Dead = true;
            player2Score = playerScore;
            deathPlayer2.SetActive(true);
        }
        if (deathCountPlayer >= 2)
        {
            if (player1Score > player2Score)
            {
                p1Win = true;
                _dialog.index = 0;
            }
            if (player1Score < player2Score)
            {
                p2Win = true;
                _dialog.index = 1;
            }
            if (player1Score == player2Score)
            {
                p1Win = true;
                p2Win = true;
                _dialog.index = 2;
            }
            dialog.SetActive(true);
            StartCoroutine(RestartVs());
        }
    }
    private IEnumerator RestartVs()
    {
        yield return new WaitForSeconds(10f);
        FindObjectOfType<GameManager>().LoadScene("DungeonVs");
    }
}
