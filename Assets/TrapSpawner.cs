using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    [SerializeField] private Grid grid;

    [Header("Traps")]
    [SerializeField] private List<Trap> spikeTraps = new List<Trap>();
    [SerializeField] private List<GameObject> blockTraps = new List<GameObject>();
    [SerializeField] private TrapWarning dropTraps;

    public void SpawnDropTrap()
    {
        int randomIndex = grid.GetRandomSpawnableCellIndex();

        dropTraps.transform.position = grid.GetCellPosition(randomIndex);
        dropTraps.gameObject.SetActive(true);
        dropTraps.SpawnTrap();
    }

    public void SpawnSpikeTrap()
    {
        Trap tempTrap = null;

        foreach (Trap trap in spikeTraps)
        {
            if (!trap.isDeployed)
            {
                tempTrap = trap;
            }
        }

        if (tempTrap == null) return;
        int randomIndex = grid.GetRandomSpawnableCellIndex();

        Vector3 temPos = grid.GetCellPosition(randomIndex);
        temPos = new Vector3(temPos.x, 0.127f, temPos.z);

        tempTrap.transform.position = temPos;
        tempTrap.gameObject.SetActive(true);
        tempTrap.SpawnTrap();
        grid.HideCellFloor(randomIndex);
    }

    public void SpawnBlockTrap()
    {
        //Trap tempTrap = null;

        //foreach (Trap trap in spikeTraps)
        //{
        //    if (!trap.isDeployed)
        //    {
        //        tempTrap = trap;
        //    }
        //}

        //if (tempTrap == null)
        //{
        //    maxSpikeTrapReached = true;
        //    return;
        //}
        //int randomIndex = grid.GetRandomSpawnableCellIndex();

        //tempTrap.transform.position = grid.GetCellPosition(randomIndex);
        //tempTrap.gameObject.SetActive(true);
        //tempTrap.SpawnTrap();
        //grid.HideCellFloor(randomIndex);
    }
}
