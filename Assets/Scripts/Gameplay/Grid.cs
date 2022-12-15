using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public enum CellType { PLAYER, TRAP, REWARD, NONE}

    [SerializeField] List<Cell> cells = new List<Cell>();

    public int GetCellCount()
    {
        return cells.Count;
    }

    public CellType GetCellStatus(int index)
    {
        return cells[index].cellType;
    }

    public Vector3 GetCellPosition(int index)
    {
        return cells[index].transform.position;
    }

    public bool IsCellAvailable(int index)
    {
        return cells[index].cellType == CellType.NONE;
    }

    public void SetCellType(int index, CellType type)
    {
        cells[index].cellType = type;
    }
}
