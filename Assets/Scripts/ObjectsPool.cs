using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    public static ObjectsPool Instance;

    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField]  private int amountOfBulletPool = 20;

    [Header("Drop Trap")]
    [SerializeField] private GameObject dropTrapPrefab;
    [SerializeField] private int amountOfDropTrapPool = 4;

    private List<GameObject> bulletList = new List<GameObject>();
    private List<GameObject> dropTrapList = new List<GameObject>();
    

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    private void Start()
    {
        for(int i = 0; i < amountOfBulletPool; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            bulletList.Add(obj);
        }

        for (int i = 0; i < amountOfDropTrapPool; i++)
        {
            GameObject obj = Instantiate(dropTrapPrefab);
            obj.SetActive(false);
            dropTrapList.Add(obj);
        }
    }

    public GameObject GetBullet()
    {
        foreach(var bullet in bulletList)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }

        return null;
    }

    public TrapWarning GetDropTrap()
    {
        foreach (var trap in dropTrapList)
        {
            if (!trap.activeInHierarchy)
            {
                return trap.GetComponentInChildren<TrapWarning>();
            }
        }

        return null;
    }
}
