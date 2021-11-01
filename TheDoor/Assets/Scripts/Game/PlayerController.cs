using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigid;
    public Camera myCamera;
    public float cameraRotationLimit;
    private float currentCameraRotationX;
    public float sensitivity; // 감도


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

        CameraRotation();
        CharacterRotation();
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

    private void CameraRotation()   // 1인칭 카메라 회전(마우스-상하)
    {
        float x = Input.GetAxisRaw("Mouse Y");  // x축으로 회전
        float cameraRotationX = x * sensitivity;
        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        myCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }
    private void CharacterRotation()    // 캐릭터 회전(마우스-좌우)
    {
        float y = Input.GetAxisRaw("Mouse X");  // y축으로 회전
        Vector3 characterRotationY = new Vector3(0f, y, 0f) * sensitivity;
        gameObject.transform.rotation *= Quaternion.Euler(characterRotationY);
    }
}
