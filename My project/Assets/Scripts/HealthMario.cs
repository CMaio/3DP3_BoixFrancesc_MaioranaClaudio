using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthMario : MonoBehaviour
{
    public UnityEvent<float, float> healthChanged;
    float totalHealth = 100.0f;
    float currentHealth = 100.0f;

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
        healthChanged.Invoke(currentHealth, totalHealth);
    }
}
