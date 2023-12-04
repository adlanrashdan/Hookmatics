using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // the wall moves forwards
        transform.Translate(Vector3.back * Time.deltaTime * speed);
        //if the wall is the distance of 10 units away from the player, it is destroyed
        if(Vector3.Distance(transform.position, Player.instance.transform.position) > 25f)
        {
            Destroy(gameObject);
        }

    }
}
