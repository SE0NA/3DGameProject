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

    public int playerPos;   // 플레이어가 위치하는 방 번호

    public GameObject _doorInfoPanel;

    private DoorMove touchDoor = null;
    private bool istouchDoor = false;

    StageInfo _stageInfo;
    CanvasManager _canvasManager;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   // 마우스 커서 고정
        Cursor.visible = false;

        rigid = GetComponent<Rigidbody>();
        _stageInfo = FindObjectOfType<StageInfo>();
        _canvasManager = FindObjectOfType<CanvasManager>();
        _doorInfoPanel = FindObjectOfType<GameManager>()._doorInfoImage;
        isJumping = false;
        isRunning = false;
    }
    
    void Update()
    {
        Move();
        Jump();

        CameraRotation();
        CharacterRotation();

        MouseClick();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Room"))
        {
            playerPos = other.GetComponent<RoomInfo>().roomNum;
            _canvasManager.SetScanner(_stageInfo.roomList[playerPos - 1].aroundBomb);
        }
        else if (other.gameObject.CompareTag("Door"))
        {
            istouchDoor = true;
            touchDoor = other.gameObject.GetComponent<DoorMove>();
            _doorInfoPanel.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            istouchDoor = false;
            touchDoor = null;
            _doorInfoPanel.SetActive(false);
        }
    }

    private void MouseClick()
    {
        // 문열기 - 왼쪽
        if (Input.GetMouseButtonDown(0) && istouchDoor && touchDoor)
        {
            _doorInfoPanel.SetActive(false);
            int behindRoomNum;
            touchDoor.DoorOpen();
            
            if (touchDoor.roomNum1 == playerPos)
                behindRoomNum = touchDoor.roomNum2;
            else
                behindRoomNum = touchDoor.roomNum1;
            
            _stageInfo.roomList[behindRoomNum - 1].Open();
            _canvasManager.SetBombCnt(_stageInfo.currentBombCnt);

           // 폭탄이 있는 방을 열었을 때
            if (_stageInfo.roomList[behindRoomNum - 1].hasBomb)
            {

            }
            touchDoor = null;
        }

        // 플래그 표시 -오른쪽
        else if (Input.GetMouseButtonDown(1) && istouchDoor && touchDoor)
        {
            _doorInfoPanel.SetActive(false);
            int behindRoomNum;
            if (touchDoor.roomNum1 == playerPos)
                behindRoomNum = touchDoor.roomNum2;
            else
                behindRoomNum = touchDoor.roomNum1;

            _stageInfo.roomList[behindRoomNum - 1].Flag();
        }
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
