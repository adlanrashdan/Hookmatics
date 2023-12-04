using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;
    private bool isBlocked = false;
    public bool isHooking = false;
    private bool dashonCooldown = false;
    public float dashCooldown = 0.5f;
    public float dashSpeed = 10f;
    public Transform hookspawn;
    private Rigidbody rb;

    private Animator animator;
    public GameObject Cursor;
   
    private bool isMoving = false;
    
    private bool isHit = false;
    private FixedJoystick joystick;
    private Vector3 hookTargetPosition;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animator.SetBool("isMoving", false);
        animator.SetBool("isHook", false);
        animator.SetBool("isHit", false);
        
    }
    void Start()
    {
        joystick = FixedJoystick.instance;
    }

    void Update()
    {
        if (isHooking == false && isBlocked == false)
        {
            // when the player presses spacebar, he gains speed for a short amount of time
            //CheckDash();
            CheckMovement();    
            //CheckRotation();
            CheckHook();
            
        } else{
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            isMoving = false;
            animator.SetBool("isMoving", false);
        }
        CheckAnimationState();
    }


    // Update is called once per frame
    void CheckMovement()
    {
        // rotate the player to the direction of the joystick in respect to the world

        if(joystick.Horizontal != 0 || joystick.Vertical != 0){
            float angle = Mathf.Atan2(-joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle, 0);
            print("joystick direction: " + joystick.Direction);
            print("joystick horizontal: " + joystick.Horizontal);
            print("joystick vertical: " + joystick.Vertical);
            // add speed depending on how  far the joystick is from the center
            rb.AddForce(transform.forward * joystick.Direction.magnitude * speed * Time.deltaTime);
            isMoving = true;
            animator.SetBool("isMoving", true);
        } else {
            isMoving = false;
            animator.SetBool("isMoving", false);
        }
        
    }
    void CheckHook(){
        // if(Input.GetMouseButtonDown(0) && isHooking == false)
        // {
        //     isHooking = true;
        //     TriggerHookAnimation();
        //     Invoke("SpawnHook", 0.1f);
        //     Invoke("cancelHook", 0.5f);
        // }
        //if there is a touch on to the ground and the player is not hooking, then hook

        
        if(Input.touchCount == 0 || isHooking || Input.GetTouch(0).phase == TouchPhase.Ended){
            
            return;
        }
        if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) ){
            if(Input.touchCount <= 1){
                ;
                return;
            }
            if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(1).fingerId)){
                
                return;
            }
            
        }
        if(Input.touchCount > 1){
               
        if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(1).fingerId) || Input.GetTouch(1).phase == TouchPhase.Ended){
                
                return;
            }
        }
        

        
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(Input.touchCount>1 ? 1 : 0).position);
        RaycastHit hit;
        //if the raycast hits something
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            //if the thing the raycast hits is a floor
            if (hit.collider.tag == "Floor")
            {
                //get the position of the thing the raycast hits
                Vector3 targetPosition = hit.point;
                Cursor.transform.position = new Vector3(targetPosition.x, Cursor.transform.position.y, targetPosition.z);
                //set the y position of the target position to the y position of the player
                targetPosition.y = transform.position.y;
                //get the direction from the player to the target position
                Vector3 direction = targetPosition - transform.position;
                //rotate the player to face the target position
                transform.rotation = Quaternion.LookRotation(direction);
                isHooking = true;
                TriggerHookAnimation();
                hookTargetPosition = targetPosition;
                Invoke("SpawnHook", 0.1f);
                Invoke("cancelHook", 0.5f);
            }
        }
        
            



    }
    void SpawnHook()
    {
        // calculate the direction of the hook
        
        GameObject hook = Instantiate(Player.instance.hook, hookspawn.position , Quaternion.identity);
        hook.GetComponent<Hook>().SetDirection(hookTargetPosition);
    }


    void CheckRotation()
    {
        //raycast from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //if the raycast hits something
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            //if the thing the raycast hits is a floor
            if (hit.collider.tag == "Floor")
            {
                //get the position of the thing the raycast hits
                Vector3 targetPosition = hit.point;
                Cursor.transform.position = new Vector3(targetPosition.x, Cursor.transform.position.y, targetPosition.z);
                //set the y position of the target position to the y position of the player
                targetPosition.y = transform.position.y;
                //get the direction from the player to the target position
                Vector3 direction = targetPosition - transform.position;
                //rotate the player to face the target position
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
    void CheckDash()
    {
        if(dashonCooldown)
            return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            speed = dashSpeed*speed;
            SoundEffectManager.instance.PlaySoundEffect("Dash");
            dashonCooldown = true;
            Invoke("StopDashCooldown", dashCooldown);
            Invoke("ResetSpeed", 0.1f);
        }
    }
    public void Dash()
    {
        if(dashonCooldown)
            return;
        speed = dashSpeed*speed;
        SoundEffectManager.instance.PlaySoundEffect("Dash");
        dashonCooldown = true;
        Invoke("StopDashCooldown", dashCooldown);
        Invoke("ResetSpeed", 0.1f);
    }
    void cancelHook()
    {
        isHooking = false;
    }
    void StopDashCooldown()
    {
        dashonCooldown = false;
    }
    void ResetSpeed()
    {
        speed = 1000f;
    }
    public void BlockMovement()
    {
        //the palyer cant move for 2 seconds
        animator.SetBool("isMoving", false);
        animator.Play("Happy Idle", -1, 0f);
        isBlocked = true;
        Invoke("FreeMovement", HookablesManager.instance.spawnTime*3);

    }
    void FreeMovement()
    {
        //the player can move again
        isBlocked = false;
    }

     void TriggerHookAnimation()
    {
        // instantly trigger the animation state and stop any other animations
        //animator.Play("Throwing", -1, 0f);
        animator.CrossFade("Throwing", 0f);
        animator.SetBool("isHook", isHooking);

    }

    void TriggerDieAnimation()
    {
        isHit = true;
        animator.SetBool("isHit", isHit);

    }
    void CheckAnimationState()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Throwing"))
        {
            // Wait for the animation to complete
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                 // Reset the flag
                animator.SetBool("isHook", false);  // Inform the Animator
            }
        }

        
    }
}
