using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    public void MaxFund()
    {
        MainMenu.instance.totalCoin = 9999;
        MainMenu.instance.totalScroll = 999;
        DataPersistenceManager.instance.SaveGame();
        DataPersistenceManager.instance.LoadGame();
    }

    public void DeleteSave()
    {
        DataPersistenceManager.instance.ResetSave();
        DataPersistenceManager.instance.LoadGame();
        Shop.instance.RefreshShop();
    }
}
