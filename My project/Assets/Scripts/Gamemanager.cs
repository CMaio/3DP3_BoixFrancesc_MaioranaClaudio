using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<IRestartGame> listeners = new List<IRestartGame>();
    bool died = false;
    public void RestartGame()
    {
        foreach(IRestartGame l in listeners)
        {
            l.RestartGame();
        }
        died = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && died)
        {
            RestartGame();
        }
    }
    public void addRestartListener(IRestartGame listener)
    {
        listeners.Add(listener);
    }
    public void removeRestartListener(IRestartGame listener)
    {
        listeners.Remove(listener);
    }

    public void playerDie()
    {
        
        foreach (IRestartGame l in listeners)
        {
            l.Die();
        }
        died = true;
    }
}
