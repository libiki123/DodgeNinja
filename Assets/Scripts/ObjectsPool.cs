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

    [Header("Spike Trap")]
    [SerializeField] private GameObject SpikeTrapPrefab;
    [SerializeField] private int amountOfSpikeTrapPool = 10;

    [Header("Block Trap")]
    [SerializeField] private GameObject blockTrapPrefab;
    [SerializeField] private int amountOfBlockTrapPool = 5;

    private List<GameObject> bulletList = new List<GameObject>();
    private List<GameObject> dropTrapList = new List<GameObject>();
    private List<GameObject> spikeTrapList = new List<GameObject>();
    private List<GameObject> blockTrapList = new List<GameObject>();
    

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
            GameObject obj = Instantiate(bulletPrefab, transform);
            obj.SetActive(false);
            bulletList.Add(obj);
        }

        for (int i = 0; i < amountOfDropTrapPool; i++)
        {
            GameObject obj = Instantiate(dropTrapPrefab, transform);
            obj.SetActive(false);
            dropTrapList.Add(obj);
        }

        for (int i = 0; i < amountOfSpikeTrapPool; i++)
        {
            GameObject obj = Instantiate(SpikeTrapPrefab, transform);
            obj.SetActive(false);
            spikeTrapList.Add(obj);
        }

        for (int i = 0; i < amountOfBlockTrapPool; i++)
        {
            GameObject obj = Instantiate(blockTrapPrefab, transform);
            obj.SetActive(false);
            blockTrapList.Add(obj);
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

    public Trap GetSpikeTrap()
    {
        foreach (var trap in spikeTrapList)
        {
            if (!trap.activeInHierarchy)
            {
                return trap.GetComponent<Trap>();
            }
        }

        return null;
    }

    public GameObject GetBlockTrap()
    {
        foreach (var trap in blockTrapList)
        {
            if (!trap.activeInHierarchy)
            {
                return trap;
            }
        }

        return null;
    }
}
