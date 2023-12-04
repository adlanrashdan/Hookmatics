using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isReturning = false;
    public float speed = 50f;
    private Vector3 start;
    void Start()
    {
        start = transform.position;
        SoundEffectManager.instance.PlaySoundEffect("HookThrow");
        
        Destroy(gameObject, 3f);
    }
    public void SetDirection(Vector3 targetPosition)
    {
        //the hook is thrown in the direction of the joystick
        targetPosition.y = transform.position.y;
        Vector3 direction = targetPosition - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
        //draw a gizmos from the hook to the destination
        

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 10f);
    }

    // Update is called once per frame
    void Update()
    {
        // the hook flies forward and then returns to the player
         
    
        if(!isReturning)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        } else {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
            }
        //if the hook is the distance of 10 units away from the player, it returns
        if(Vector3.Distance(transform.position, start) > 10f)
        {
            isReturning = true;
        }
        //if the hook is back at the player, it is destroyed
        if(Vector3.Distance(transform.position, start) < 0.5f && isReturning)
        {
            print("hook destroyed itself");
            Destroy(gameObject);
        }
        transform.GetChild(0).GetComponent<LineRenderer>().SetPosition(1, transform.GetChild(0).transform.InverseTransformPoint(start.x, start.y, start.z));
    }
    
    private void OnCollisionEnter(Collision other)
    {
        //when the hook collides with an object, it is destroyed
        if(other.gameObject.tag != "Hookable")
        {
            Destroy(gameObject);
            SoundEffectManager.instance.PlaySoundEffect("Bounce");
            Instantiate(Resources.Load("Prefabs/FX/CFX3_Hit_SmokePuff"), transform.position, Quaternion.identity);
        }
    }
    
}
