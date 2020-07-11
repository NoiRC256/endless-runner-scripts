//NoiR_CCC

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    Text scoreText;
    Text distanceText;
    Text energyText;
    RectTransform skillFiller;
    Image skillCooldown;

    void Start ()
    {
        UpdateTextObjects();
        energyText = GameObject.Find("EnergyText").GetComponent<Text>();
        skillFiller = GameObject.Find("SkillFiller").GetComponent<RectTransform>();
        skillCooldown = GameObject.Find("CooldownBar").GetComponent<Image>();
    }

    public void UpdateScore(int currentScore)
    {
        scoreText.text = "" + currentScore;
    }

    public void UpdateDistance(int currentDistance)
    {
        distanceText.text = currentDistance + "m";       
    }

    public void UpdateEnergy (float currentEnergy)
    {       
        float targetValue = Mathf.Clamp(Mathf.FloorToInt(currentEnergy * 20), 0, 100);        
        energyText.text = targetValue + "%";

        float targetHeight = currentEnergy * 30;
        var position = skillFiller.transform.localPosition;
        position.y = Mathf.Lerp(position.y, targetHeight, 2 * Time.deltaTime);
        skillFiller.transform.localPosition = position;
    }

    public void UpdateCooldown (float currentCooldown)
    {
        skillCooldown.fillAmount = Mathf.Lerp(skillCooldown.fillAmount, currentCooldown, 15 * Time.deltaTime);
    }

    public void UpdateTextObjects()
    {
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        distanceText = GameObject.FindGameObjectWithTag("DistanceText").GetComponent<Text>();
    }
}
