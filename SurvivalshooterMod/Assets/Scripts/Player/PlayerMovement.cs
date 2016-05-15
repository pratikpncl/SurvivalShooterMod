using UnityEngine;
using CnControls;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public bool AndroidInput = true;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    void Awake()
    {
        //Find components
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //Get input from joystick
        Vector2 Joystickmovement = new Vector2(CnInputManager.GetAxis("HorizontalMovement"), CnInputManager.GetAxis("VerticalMovement"));

        //use either joystick or keyboard (W,A,S,D) inputs to move
        Move(Joystickmovement.x, Joystickmovement.y);
        Turning();
        Animating(Joystickmovement.x, Joystickmovement.y);

    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        //movement with real time.
        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        if (!AndroidInput)
        {
            //use keyboard if android input is false
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit floorHit;

            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {
                Vector3 playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0f;

                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
                playerRigidbody.MoveRotation(newRotation);
            }
        }
        
        if (AndroidInput)
        {
            //Use the onscreen joystick for directions if aandroid input is true
            Vector3 turnDir = new Vector3(CnInputManager.GetAxis("HorizontalDirection"), 0f, CnInputManager.GetAxis("VerticalDirection"));

            if (turnDir != Vector3.zero)
            {
                // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                Vector3 playerToMouse = (transform.position + turnDir) - transform.position;

                // Ensure the vector is entirely along the floor plane.
                playerToMouse.y = 0f;

                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

                // Set the player's rotation to this new rotation.
                playerRigidbody.MoveRotation(newRotatation);
            }
        }
        
    }

    void Animating(float h, float v)
    {
        //set animation if bool is true
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }
}
