using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float speed;
    private Vector3 direction;
    private Vector3 bounds;
    private bool moving;


    // Start is called before the first frame update
    void Start()
    {
        bounds = new Vector3(0, 0 , -10);
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
        

        if (transform.position.z < bounds.z)
        {
            Destroy(gameObject);
        }
    }

    public void Init(Vector3 direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;
        moving = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("HIT");
            GameManager.Instance.Restart();
        }
    }
}
