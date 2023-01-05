using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public enum CellType { WALKABLE, TRAP, NONE}

    [SerializeField] List<Cell> cellList = new List<Cell>();
    [SerializeField] List<GameObject> cellFloor = new List<GameObject>();


    private void Start()
    {
        for(int i = 0; i < cellList.Count; i++)
        {
            cellList[i].floor = cellFloor[i];
        }

    }

    public Cell GetRandomSpawnableCell(int caseIndex)
    {
        List<Cell> availableCells = GetAvailableCells(caseIndex);

        int maxIndex = availableCells.Count - 1;
        int randomIndex = Random.Range(1, maxIndex);

        return availableCells[randomIndex];
    }

    private List<Cell> GetAvailableCells(int caseIndex)
    {
        List<Cell> tempCellList = new List<Cell>();

        foreach (var cell in cellList)
        {
            switch (caseIndex)
            {
                case 1:
                    if (cell.cellType == CellType.NONE && !cell.havePlayer)
                    {
                        tempCellList.Add(cell);
                    }
                    break;
                case 2:
                    if (cell.cellType == CellType.NONE && !cell.havePlayer && !cell.haveReward)
                    {
                        tempCellList.Add(cell);
                    }
                    break;
                case 3:
                    if ((cell.cellType == CellType.NONE || cell.cellType == CellType.WALKABLE) && !cell.havePlayer && !cell.haveReward)
                    {
                        tempCellList.Add(cell);
                    }
                    break;
            }
        }

        return tempCellList;
    }

    public Cell GetPlayerCell()
    {
        Cell playerCell = null;

        foreach (var cell in cellList)
        {
            if (cell.havePlayer)
            {
                playerCell = cell;
            }
        }

        return playerCell;
    }

}
