using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigid;

    public int jumpPower;
    private bool isJumping;
    private bool isRunning;
    public int moveSpeed;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        isJumping = false;
        isRunning = false;
    }
    
    void Update()
    {
        Move();
        Jump();
    }

    void Move()     // 이동
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
            isRunning = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            isRunning = false;

        if(isRunning)   // 달리기
            transform.Translate((new Vector3(h, 0, v) * moveSpeed * 2) * Time.deltaTime);
        else            // 걷기
            transform.Translate((new Vector3(h, 0, v) * moveSpeed) * Time.deltaTime);
        
    }
    void Jump()     // 점프
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isJumping = false;
    }
}
