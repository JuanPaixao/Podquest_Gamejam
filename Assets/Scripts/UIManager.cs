using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Transform minPoint, maxPoint;
    public float dayTime;
    private Vector3 startPos, finishPos;
    public GameObject pointerDay;
    public float timeSpeed;
    public TextMeshProUGUI text;
    public Slider sliderP1, sliderP2;
    public GameManager gameManager;
    public GameObject[] deathPanel;
    public Animator foodBagAnimator;

    private void Start()
    {
        startPos = new Vector3(minPoint.position.x, minPoint.position.y, minPoint.position.z);
        finishPos = new Vector3(maxPoint.position.x, maxPoint.position.y, maxPoint.position.z);
    }
    private void Update()
    {
        if (dayTime < 1)
        {
            dayTime += Time.deltaTime * timeSpeed;
            pointerDay.transform.position = Vector3.Lerp(startPos, finishPos, dayTime);
        }
        else
        {
            gameManager.FinishGame();
        }
    }
    public void SetScore(int text)
    {
        this.text.text = text.ToString() + " kcal";
        if (text >= 2000 && text < 5000)
        {
            foodBagAnimator.SetInteger("stage", 1);
        }
        if (text >= 5000)
        {
            foodBagAnimator.SetInteger("stage", 2);
        }
    }
    public void SetHP(int hp, int player)
    {
        if (player == 1)
        {
            sliderP1.value = hp;
        }
        if (player == 2)
        {
            sliderP2.value = hp;
        }
    }
    public void DeathPanel(int player)
    {
        if (player == 1)
        {
            deathPanel[0].SetActive(true);
        }
        if (player == 2)
        {
            deathPanel[1].SetActive(true);
        }
    }
}