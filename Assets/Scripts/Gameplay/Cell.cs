using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Grid.CellType cellType;
    public bool havePlayer;
    [HideInInspector] public GameObject floor;

    private void Start()
    {
        cellType = Grid.CellType.NONE;
        havePlayer = false;
    }

    public void HideFloor()
    {
        floor.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            havePlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            havePlayer = false;
        }
    }
}
