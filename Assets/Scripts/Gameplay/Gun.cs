using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject warningMark;
    public float warningTime = 0.5f;
    private Animator animator;

    private GameObject obstaclePrefab;
    private Vector3 direction;
    private float speed;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Init(Vector3 direction, float speed, GameObject obstaclePrefab)
    {
        this.direction = direction;
        this.speed = speed;
        this.obstaclePrefab = obstaclePrefab;
        StartCoroutine(StartWarning());
    }

    IEnumerator StartWarning()
    {
        warningMark.SetActive(true);
        yield return new WaitForSecondsRealtime(warningTime);
        warningMark.SetActive(false);
        animator.SetTrigger("Shoot");
    }

    public void Shoot()
    {
        GameObject a = Instantiate(obstaclePrefab, transform.position + new Vector3(0,0.25f, 0), Quaternion.identity);
        a.GetComponent<Obstacle>().Init(direction, speed);
    }

}