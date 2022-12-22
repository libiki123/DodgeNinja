using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Grid.CellType cellType;
    public bool havePlayer;

    private void Start()
    {
        cellType = Grid.CellType.NONE;
        havePlayer = false;
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
