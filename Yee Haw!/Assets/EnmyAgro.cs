using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnmyAgro : MonoBehaviour
{

    [SerializeField]
    Transform player;

    [SerializeField]
    float agroRange;

    [SerializeField]
    float moveSpeed;

    Rigidbody2D rb2d;
    

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //how close he is to the player
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        //Debug.Log(distToPlayer);

        if(distToPlayer < agroRange)
        {
            ChasePlayer();
		}

        else
        {
           //StopChasingPlayer();
		}

        
        
        
    void ChasePlayer()
    {
        
        if (transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(moveSpeed,0);
        }
        
        else 
		{
            rb2d.velocity = new Vector2(-moveSpeed,0);
		}
    }
    }
    }
