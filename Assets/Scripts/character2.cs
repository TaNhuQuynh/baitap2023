using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character2 : MonoBehaviour
{
    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] float speed = 5;
   
    public float direction=-1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

   
    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        Vector3 position = transform.position;
        if (direction == 1 && position.x > 29)
        {
            SetDirecton(dir: -1);
        }
        if (direction == -1 && position.x < 12)
        {
            SetDirecton(dir: 1);
        }

        position.x += direction * speed * Time.deltaTime;
        transform.position = position;
    }
    private void SetDirecton(int dir)
    {
        direction = dir;
        playerSprite.flipX = dir == 1;
    }
}
