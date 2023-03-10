using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Vector3 minBound = new Vector3(-10, 0, -10);
    [SerializeField] private Vector3 maxBound = new Vector3(10, 0, 10);

    private float speed;
    private Vector3 direction;
    private bool moving;

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            transform.position += direction * speed * Time.deltaTime;
        }

        if (transform.position.x > maxBound.x || transform.position.x < minBound.x || transform.position.z > maxBound.z || transform.position.z < minBound.z)
        {
            gameObject.SetActive(false);
        }
    }

    public void Init(Vector3 direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;
        transform.rotation = Quaternion.LookRotation(direction);
        moving = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        //if (other.gameObject.tag == "")
        {
            Vector3 tmpContactPoint = other.ClosestPoint(transform.position);
            other.GetComponent<Player>().KillPLayer(tmpContactPoint);
            gameObject.SetActive(false);
        }
    }

}
