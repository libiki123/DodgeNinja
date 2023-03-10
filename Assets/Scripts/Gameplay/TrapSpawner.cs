using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class TrapSpawner : MonoBehaviour
{
    [SerializeField] private Grid grid;

    public void SpawnDropTrap()
    {
        Cell cell = grid.GetPlayerCell();

        if (cell == null) return;
        TrapWarning dropTrap = ObjectsPool.instance.GetDropTrap();
        dropTrap.rootParent.SetActive(true);
        dropTrap.rootParent.transform.position = new Vector3(cell.transform.position.x, 0, cell.transform.position.z);
        dropTrap.SpawnTrap();
    }

    public void SpawnSpikeTrap()
    {
        Trap tempTrap = ObjectsPool.instance.GetSpikeTrap();
        Cell randomCell = grid.GetRandomSpawnableCell(1);
        randomCell.cellType = Grid.CellType.WALKABLE;

        tempTrap.transform.position = new Vector3(randomCell.transform.position.x, 0.19f, randomCell.transform.position.z);
        tempTrap.gameObject.SetActive(true);
        tempTrap.SpawnTrap();
        //randomCell.HideFloor();
    }

    public void SpawnBlockTrap()
    {
        GameObject tempTrap = ObjectsPool.instance.GetBlockTrap();
        Cell randomCell = grid.GetRandomSpawnableCell(2);
        randomCell.cellType = Grid.CellType.TRAP;

        tempTrap.transform.position = new Vector3(randomCell.transform.position.x, 0f, randomCell.transform.position.z);
        tempTrap.gameObject.SetActive(true);

    }
}
