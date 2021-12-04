using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("kill");
        if (other.gameObject.TryGetComponent(out HealthMario mario))
        {
            mario.doDamage(1000.0f);
        }
    }
}
