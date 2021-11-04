using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throw_script : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "player")
            collision.collider.GetComponent<player>().Ragdoll(transform.forward * 10);
    }
}
