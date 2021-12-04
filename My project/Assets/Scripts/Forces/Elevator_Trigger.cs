using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_Trigger : MonoBehaviour
{
    [SerializeField] float maxAttachingAngle = 20;
    GameObject attachedMario;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MarioPlayerController>() != null)
        {
            Debug.Log("Entradp");
            attachMario(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<MarioPlayerController>() != null)
        {
            Debug.Log("feura");
            dettachMario();
        }
    }

    private void Update()
    {
        if (180.0f > Vector3.Angle(transform.up, Vector3.forward) && Vector3.Angle(transform.up, Vector3.forward) > maxAttachingAngle)
        {
            float angle = Vector3.Angle(transform.up, Vector3.up);
            Debug.DrawRay(transform.position, Vector3.forward, Color.green);
            Debug.DrawRay(transform.position, transform.up, Color.red);
            Debug.Log("feura2" + angle);
            dettachMario();
        }

    }
    private void attachMario(GameObject mario)
    {
        Debug.Log("atachear");
        attachedMario = mario;
        mario.transform.parent = transform;
    }

    private void dettachMario()
    {
        if (attachedMario != null)
        {
            attachedMario.transform.parent = null;
            attachedMario = null;
        }

    }
}
