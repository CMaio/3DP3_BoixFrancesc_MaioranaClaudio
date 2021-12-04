using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] Life life;
    [SerializeField] LifeManager lifeManager;
    private void OnTriggerEnter(Collider other)
    {
        if (!lifeManager.haveAllHealth() && life != null)
        {
            life.life();
            Destroy(gameObject);
        }

    }
}

