using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text score;
    public Image healthUI;
    [SerializeField] Color deadColor, healthyColor;
    private void Start()
    {
        DependencyContainer.GetDependency<IScoreManager>().scoreChangedDelegate += updateScore;
    }
    private void OnDestroy()
    {
        DependencyContainer.GetDependency<IScoreManager>().scoreChangedDelegate -= updateScore;
    }
    public void updateScore(IScoreManager scoreManager)
    {
        score.text = "Score: "+scoreManager.getPoints().ToString("0");
    }
    public void changeHealth(float currentHealth, float totalHealth)
    {
        healthUI.fillAmount = currentHealth/totalHealth;
        healthUI.color = Color.Lerp(deadColor, healthyColor, healthUI.fillAmount);
    }
}