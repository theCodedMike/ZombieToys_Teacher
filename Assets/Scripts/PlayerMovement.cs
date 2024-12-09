using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");

    [Header("移动速度")]
    public float moveSpeed;
    [Header("移动时的遮罩")]
    public LayerMask mask;

    private Rigidbody rb;
    private Vector3 movement;
    private Camera mainCamera;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.GameOver)
            return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Move(h, v);
        Turn();
        Walking(h, v);
    }

    // 移动
    void Move(float h, float v)
    {
        movement.Set(h, 0, v);
        movement = movement.normalized * Time.fixedDeltaTime * moveSpeed;
        rb.MovePosition(rb.position + movement);
    }

    // 转向
    void Turn()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mask))
        {
            Vector3 dir = hit.point - transform.position;
            dir.y = 0;
            rb.MoveRotation(Quaternion.LookRotation(dir));
        }
    }

    void Walking(float h, float v)
    {
        // ReSharper disable once ComplexConditionExpression
        var isWalking = h != 0 || v != 0;
        animator.SetBool(IsWalking, isWalking);
    }
}
