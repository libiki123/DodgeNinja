using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    [SerializeField] private Grid grid;

    [Header("Traps")]
    [SerializeField] private List<Trap> spikeTraps = new List<Trap>();
    [SerializeField] private List<GameObject> blockTraps = new List<GameObject>();

    public void SpawnDropTrap()
    {
        Cell cell = grid.GetPlayerCell();

        if (cell == null) return;
        TrapWarning dropTrap = ObjectsPool.Instance.GetDropTrap();
        dropTrap.rootParent.SetActive(true);
        dropTrap.rootParent.transform.position = new Vector3(cell.transform.position.x, 0, cell.transform.position.z);
        dropTrap.SpawnTrap();
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

        tempTrap.transform.position = new Vector3(randomCell.transform.position.x, 0.19f, randomCell.transform.position.z);
        tempTrap.gameObject.SetActive(true);
        tempTrap.SpawnTrap();
        //randomCell.HideFloor();
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

        tempTrap.transform.position = new Vector3(randomCell.transform.position.x, 0f, randomCell.transform.position.z);
        tempTrap.gameObject.SetActive(true);

    }
}
