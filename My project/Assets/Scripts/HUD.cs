using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour, IRestartGame
{
    public Text score;
    public Image healthUI;
    public GameObject die;
    [SerializeField] Color deadColor, healthyColor;
    [SerializeField] GameManager gm;
    private void Start()
    {
        DependencyContainer.GetDependency<IScoreManager>().scoreChangedDelegate += updateScore;
        gm.addRestartListener(this);
    }
    private void OnDestroy()
    {
        DependencyContainer.GetDependency<IScoreManager>().scoreChangedDelegate -= updateScore;
        gm.removeRestartListener(this);
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

    void IRestartGame.RestartGame()
    {
        die.SetActive(false);
    }

    void IRestartGame.Die()
    {
        Debug.Log("activa");
        die.SetActive(true);
    }
}