using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float speed;
    private Vector3 direction;
    public Vector3 minBound = new Vector3(-10, 0, -10);
    public Vector3 maxBound = new Vector3(10, 0, 10);
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
        //if (other.gameObject.tag == "Player")
        if (other.gameObject.tag == "")
        {
            other.GetComponent<Player>().TrigerDieAnim();
            gameObject.SetActive(false);
        }
    }

}
