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
        //int randomIndex = grid.GetRandomSpawnableCell();

        //dropTraps.transform.position = grid.GetCellPosition(randomIndex);
        //dropTraps.gameObject.SetActive(true);
        //dropTraps.SpawnTrap();
    }

    public void SpawnSpikeTrap()
    {
        Trap tempTrap = null;

        foreach (Trap trap in spikeTraps)
        {
            if (!trap.isDeployed)
            {
                tempTrap = trap;
                break;
            }
        }

        if (tempTrap == null) return;
        Cell randomCell = grid.GetRandomSpawnableCell(true);
        randomCell.cellType = Grid.CellType.WALKABLE;
        Vector3 temPos = randomCell.transform.position;
        temPos = new Vector3(temPos.x, 0.14f, temPos.z);

        tempTrap.transform.position = temPos;
        tempTrap.gameObject.SetActive(true);
        tempTrap.SpawnTrap();
        randomCell.HideFloor();
    }

    public void SpawnBlockTrap()
    {
        GameObject tempTrap = null;

        foreach (GameObject trap in blockTraps)
        {
            if (!trap.activeSelf)
            {
                tempTrap = trap;
                break;
            }
        }

        if (tempTrap == null) return;
        Cell randomCell = grid.GetRandomSpawnableCell(true);
        randomCell.cellType = Grid.CellType.TRAP;

        tempTrap.transform.position = randomCell.transform.position;
        tempTrap.gameObject.SetActive(true);

    }
}
