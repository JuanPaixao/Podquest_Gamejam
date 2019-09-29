using System.Collections;
using System.Collections.Generic;
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
    public Slider slider;

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
    }
    public void SetScore(int text)
    {
        this.text.text = text.ToString() + " kcal";
    }
    public void SetHP(int hp)
    {
        slider.value = hp;
    }
}
