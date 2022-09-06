using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] public float movementSpeed, currentMovementSpeed, upperBodyRotationSpeed;
    [SerializeField] private Transform upperBody;
    public Animator animator;
    public Vector3 fwd, r;
    bool walk = false;
    float dist;
    public Vector3 velocity;
    public float angle;
    Vector3 targetPoint;
    float value1, value2;
    public bool isWASDDisableActivatet=true;
    public static PlayerMovement instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentMovementSpeed = movementSpeed;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(calculateVelocity());
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isWASDDisableActivatet==false)
        {
            MovePlayer(-Input.GetAxis("Vertical"), -Input.GetAxis("Horizontal"));
        }
        else
        {
            MovePlayer(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        }
    }

    void MovePlayer(float v, float h)
    {
        rb.MovePosition(rb.position + (v * Vector3.forward + Vector3.right * h) * currentMovementSpeed * Time.fixedDeltaTime);
       
    }

    IEnumerator calculateVelocity()
    {
        while (true)
        {
            var x1 = rb.position;
            yield return new WaitForFixedUpdate();
            var x2 = rb.position;

            velocity.x = -(x2.x - x1.x);
            velocity.z = -(x2.z - x1.z);
            /*if (velocity.x > 0)
            {
                velocity.x = 1;
            }
            if (velocity.z > 0)
            {
                velocity.z = 1;
            }
            if (velocity.x < 0)
            {
                velocity.x = -1;
            }
            if (velocity.z < 0)
            {
                velocity.z = -1;
            }*/
        }
    }

    private void Update()
    {
        GameObject.Find("PlayerCanvas").transform.LookAt(Camera.main.transform);

        fwd = upperBody.transform.forward;
        r = upperBody.transform.right;
        angle = Vector3.Angle(velocity, velocity - fwd);
        var angle2 = Vector3.Angle(velocity, velocity - r);

        value1 = 20 * (angle > 0 && angle < 90 ? 1 - angle / 90 : (angle > 90 && angle < 180 ? -1 + (180 - angle) / 90 : Mathf.Lerp(value1, 0, Time.deltaTime)));
        value2 = (angle2 > 0 && angle2 < 90 ? 1 - angle2 / 90 : (angle2 > 90 && angle2 < 180 ? -1 - (angle2 - 180) / 90 : Mathf.Lerp(value2, 0, Time.deltaTime))) * 20;
        if (Input.GetAxis("Horizontal") < .1f && Input.GetAxis("Vertical") < .1f)
        {
            value1 = Mathf.Lerp(value1, 0, Time.deltaTime);
            value2 = Mathf.Lerp(value2, 0, Time.deltaTime);
        }
        else
        {

        }


        animator.SetFloat("Vertical", value1 * currentMovementSpeed / movementSpeed);
        animator.SetFloat("Horizontal", value2 * currentMovementSpeed / movementSpeed);
        UpperBodyMovement();
    }

    void UpperBodyMovement()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            targetPoint = new Vector3(hit.point.x, upperBody.transform.position.y, hit.point.z);
            dist = Vector3.Distance(targetPoint, transform.position);

            if (dist > 2)
            {
                upperBody.LookAt(targetPoint);
                walk = true;
            }
            else
            {
                var dir = (targetPoint - transform.position).normalized;
                dir.y = 0;
                upperBody.LookAt(targetPoint + dir);
                walk = false;
            }
        }

    }
}
