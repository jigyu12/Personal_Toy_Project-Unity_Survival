using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public readonly int hashMove = Animator.StringToHash("Move");

    public float speed;

    private Animator animator;
    private Rigidbody rb;
    private PlayerInput input;
    public VirtualJoyStick joystick;
    public VirtualJoyStickAttack JoyStickAttack;
    
    private Camera mainCamera;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        var pos = transform.position;

#if UNITY_STANDALONE || UNITY_EDITOR
        Vector3 moveVec = new Vector3(input.VMove, 0f, input.HMove);
#elif UNITY_ANDROID || UNITY_IOS
        Vector3 moveVec = new Vector3(joystick.Input.x, 0f, joystick.Input.y);
#endif
        if(moveVec.magnitude > 1f)
            moveVec.Normalize();
        
        rb.velocity = moveVec * speed * Time.deltaTime;
        
#if UNITY_STANDALONE || UNITY_EDITOR
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 point = ray.GetPoint(distance);
        
            Vector3 direction = new Vector3(point.x, transform.position.y, point.z) - transform.position;
        
            if (direction != Vector3.zero)
            {
                rb.MoveRotation(Quaternion.LookRotation(direction));
            }
        }
#elif UNITY_ANDROID || UNITY_IOS
        Vector3 directionAttack = Vector3.zero;
        directionAttack.x = JoyStickAttack.Input.x;
        directionAttack.z = JoyStickAttack.Input.y;
            
         if (directionAttack != Vector3.zero)
         {
             rb.MoveRotation(Quaternion.LookRotation(directionAttack));
         }
#endif
        animator.SetFloat(hashMove, rb.velocity.magnitude / 10f);
    }
}