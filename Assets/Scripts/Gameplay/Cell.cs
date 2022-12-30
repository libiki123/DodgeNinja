using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Grid.CellType cellType = Grid.CellType.NONE;
    public bool havePlayer;
    [HideInInspector] public GameObject floor;

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
