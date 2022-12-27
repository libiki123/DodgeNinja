using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Trap : MonoBehaviour
{
    public bool isDeployed = false;

    private Collider coll;

    private void Start()
    {
        coll = GetComponent<Collider>();
    }

    virtual public void SpawnTrap()
    {
        isDeployed = true;
    }

    public void EnableCollider()
    {
        coll.enabled = true;
    }

    public void DisableCollider()
    {
        coll.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("HIT");
            UIManager.Instance.ShowEndGameMenu();
        }
    }
}
