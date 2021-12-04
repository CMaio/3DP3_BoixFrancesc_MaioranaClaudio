using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour, ILifeManager, IRestartGame
{
    [SerializeField] float currentHealth;
    [SerializeField] float totalHealth;
    [SerializeField] GameManager gm;
    public event LifeChanged lifeChangedDelegate;
    void Awake()
    {
        DependencyContainer.AddDependency<ILifeManager>(this);
    }
    private void Start()
    {
        gm.addRestartListener(this);
    }


    private void OnDestroy()
    {
        gm.removeRestartListener(this);
    }
    public void addLife(float health)
    {
        this.currentHealth += health;
        lifeChangedDelegate?.Invoke(this);
    }
    public void doDamage(float health)
    {
        this.currentHealth -= health;
        lifeChangedDelegate?.Invoke(this);
    }
    public float getLife() { return currentHealth / totalHealth; }
    public bool haveAllHealth() { return currentHealth == totalHealth; }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            doDamage(1);
        }
    }
    public void RestartGame()
    {
        currentHealth = totalHealth;
        lifeChangedDelegate?.Invoke(this);
    }
    public void Die() { }
}