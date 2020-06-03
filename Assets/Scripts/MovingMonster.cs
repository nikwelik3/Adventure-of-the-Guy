using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingMonster : MonoBehaviour
{
    float dirX;
    public float speed;
    public float distance;

    bool movingUp = true;
    
    void Update()
    {
        
        if (transform.position.y > distance)
        {
            movingUp = false;
        }
        else if (transform.position.y < -distance)
        {
            movingUp = true;
        }

        if (movingUp)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
        }
    }
    /*
    public Transform pos1, pos2;
    public float speed;
    public Transform startPos;

    Vector3 nextPos;

    void Start()
    {
        nextPos = startPos.position;
    }

    void Update()
    {
        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }
        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }
    */
}
