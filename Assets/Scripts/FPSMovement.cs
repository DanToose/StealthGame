using UnityEngine;

// THIS PLAYER MOVE CLASS WILL ALLOW THE GAMEOBJECT TO MOVE BASED ON CHARACTERCONTROLLER

public class FPSMovement : MonoBehaviour
{
    // VARS
    public UnityEngine.CharacterController m_charControler;
    public float m_movementSpeed = 12f;
    public float m_runSpeed = 1.5f;

    public float m_gravity = -9.81f;
    public float m_jumpHeight = 3f;
    private Vector3 m_velocity;

    public Transform m_groundCheckPoint;
    public float m_groundDistance = 0.4f;
    public LayerMask m_groundMask;
    private bool m_isGrounded; 

    public KeyCode m_forward;
    public KeyCode m_back;
    public KeyCode m_left;
    public KeyCode m_right;
    public KeyCode m_sprint;
    public KeyCode m_jump;

    private float m_finalSpeed = 0;

    // Start is called before the first frame update
    void Awake()
    {
        m_finalSpeed = m_movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        m_isGrounded = HitGroundCheck(); // CHecks touching the ground every frame
        MoveInputCheck();
    }

    // Check if a button is pressed
    void MoveInputCheck()
    {
        float x = Input.GetAxis("Horizontal"); // Gets the x input value for the Gameobject vector
        float z = Input.GetAxis("Vertical"); // Gets the z input value for the Gameobject vector

        Vector3 move = Vector3.zero;

        if (Input.GetKey(m_forward) || Input.GetKey(m_back) || Input.GetKey(m_left) || Input.GetKey(m_right))
        {
            move = transform.right * x + transform.forward * z; // calculate the move vector (direction)          
        }

        MovePlayer(move); // Run the MovePlayer function with the vector3 value move
        RunCheck(); // Checks the input for run
        JumpCheck(); // Checks if we can jump
    }

    // MovePlayer
    void MovePlayer(Vector3 move)
    {
        m_charControler.Move(move * m_finalSpeed * Time.deltaTime); // Moves the Gameobject using the Character Controller

        m_velocity.y += m_gravity * Time.deltaTime; // Gravity affects the jump velocity
        m_charControler.Move(m_velocity * Time.deltaTime); //Actually move the player up
    }

    // Player run
    void RunCheck()
    {
        if (Input.GetKeyDown(m_sprint)) // if key is down, sprint
        {
            m_finalSpeed = m_movementSpeed * m_runSpeed;
        } 
        else if (Input.GetKeyUp(m_sprint)) // if key is uo, don't sprint
        {
            m_finalSpeed = m_movementSpeed;
        }
    }

    // Ground check
    bool HitGroundCheck()
    {
        bool isGrounded = Physics.CheckSphere(m_groundCheckPoint.position, m_groundDistance, m_groundMask);

        //Gravity
        if (isGrounded && m_velocity.y < 0)
        {
            m_velocity.y = -2f;
        }

        return isGrounded;
    }

    // Jump Check
    void JumpCheck()
    {
        if (Input.GetKeyDown(m_jump))
        {
            if (m_isGrounded == true)
            {
                m_velocity.y = Mathf.Sqrt(m_jumpHeight * -2f * m_gravity);
            }
        }

    }
}
