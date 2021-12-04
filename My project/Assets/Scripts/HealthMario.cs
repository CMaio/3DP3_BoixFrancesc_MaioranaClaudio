using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthMario : MonoBehaviour,IRestartGame
{
    public UnityEvent<float, float> healthChanged;
    float totalHealth = 100.0f;
    float currentHealth = 100.0f;
    [SerializeField] GameManager gm;

    private void Start()
    {
        gm.addRestartListener(this);
    }


    private void OnDestroy()
    {
        gm.removeRestartListener(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            doDamage(totalHealth/8);
        }
    }
    private void doDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0.0f)
        {
            gm.playerDie();
        }
        healthChanged.Invoke(currentHealth, totalHealth);
    }

    public void RestartGame()
    {
        currentHealth = totalHealth;
        healthChanged.Invoke(currentHealth, totalHealth);
    }

    public void Die(){}
}
