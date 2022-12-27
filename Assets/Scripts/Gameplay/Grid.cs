using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public enum CellType { PLAYER, WALKABLE, TRAP, NONE}

    [SerializeField] List<Cell> cellList = new List<Cell>();
    [SerializeField] List<GameObject> cellFloor = new List<GameObject>();


    private void Start()
    {
        for(int i = 0; i < cellList.Count; i++)
        {
            cellList[i].floor = cellFloor[i];
        }

    }

    public Cell GetRandomSpawnableCell(bool onlyEmpty = false, Cell excludingCell = null)
    {
        List<Cell> availableCells = GetAvailableCells(onlyEmpty);
        Debug.Log(availableCells.Count);
        int maxIndex = availableCells.Count - 1;
        int randomIndex = Random.Range(1, maxIndex);

        if(excludingCell != null)
        {
            while (availableCells[randomIndex] == excludingCell)
            {
                randomIndex = Random.Range(1, maxIndex);
            }
        }

        return availableCells[randomIndex];
    }

    private List<Cell> GetAvailableCells(bool onlyEmpty = false)
    {
        List<Cell> tempCellList = new List<Cell>();

        foreach (var cell in cellList)
        {
            if (onlyEmpty)
            {
                if (cell.cellType == CellType.NONE && !cell.havePlayer)
                {
                    tempCellList.Add(cell);
                }
            }
            else
            {
                if ((cell.cellType == CellType.NONE || cell.cellType == CellType.WALKABLE) && !cell.havePlayer)
                {
                    tempCellList.Add(cell);
                }
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
