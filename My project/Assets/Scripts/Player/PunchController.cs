using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchController : MonoBehaviour
{
    [SerializeField] private SphereCollider punchCollider;

    public void setPunch(bool change)
    {
        punchCollider.enabled = change;
    }
}
