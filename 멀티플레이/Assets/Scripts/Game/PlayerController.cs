﻿using System.Collections;
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
    private bool isWalking;
    public int moveSpeed;

    public int playerPos;   // 플레이어가 위치하는 방 번호

    GameObject _doorInfoPanel;

    private DoorMove touchDoor = null;
    private bool istouchDoor = false;

    private bool isMapActive = false;
    private bool isEnd = false;

    StageInfo _stageInfo;
    CanvasManager _canvasManager;

    Animator playerAnim;
    AudioSource playerAudioSource;
    public AudioClip startClip;
    public AudioClip walkClip;
    public AudioClip jumpClip;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   // 마우스 커서 고정
        Cursor.visible = false;

        rigid = GetComponent<Rigidbody>();
        _stageInfo = FindObjectOfType<StageInfo>();
        _canvasManager = FindObjectOfType<CanvasManager>();
        _doorInfoPanel = FindObjectOfType<GameManager>()._doorInfoImage;
        playerAnim = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
        playerAnim.Play("Idle");

        isJumping = false;
        isRunning = false;
        isWalking = false;

        playerAudioSource.PlayOneShot(startClip);
    }
    
    void Update()
    {
        if (!isEnd)
        {
            // 플레이어 이동
            Move();
            Jump();

            if (!isMapActive)
            {
                // 플레이어-카메라 회전
                CameraRotation();
                CharacterRotation();

                // 마우스 클릭 이벤트(문열기/플래그)
                MouseClick();
            }
            // 미니맵 열기
            OpenMap();
        }
    }

    void Move()     // 이동
    {
        isWalking = false;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0 || v != 0) isWalking = true;
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
            isRunning = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            isRunning = false;

        if (isRunning && isWalking)  // 달리기
        {
            if (!isJumping)
            {
                playerAudioSource.pitch = 1.5f;
                if (!playerAudioSource.isPlaying)
                    playerAudioSource.PlayOneShot(walkClip);
                playerAnim.Play("Run");
            }
            transform.Translate((new Vector3(h, 0, v) * moveSpeed * 2) * Time.deltaTime);
        }
        else if(isWalking)            // 걷기
        {
            if (!isJumping)
            {
                playerAudioSource.pitch = 1f;
                if (!playerAudioSource.isPlaying)
                    playerAudioSource.PlayOneShot(walkClip);
                playerAnim.Play("Walk");
            }
            transform.Translate((new Vector3(h, 0, v) * moveSpeed) * Time.deltaTime);
        }
    }
    void Jump()     // 점프
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            playerAudioSource.pitch = 1f;
            isJumping = true;
            playerAudioSource.Stop();
            playerAudioSource.PlayOneShot(jumpClip);

            playerAnim.Play("Jump");
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

            PlayerOpenDoor(behindRoomNum - 1, _stageInfo.roomList[behindRoomNum-1].isOpened);
            if (_stageInfo.CheckAllRoom())
                Clear();
        }

        // 플래그 표시 - 오른쪽
        else if (Input.GetMouseButtonDown(1) && istouchDoor && touchDoor)
        {
            _doorInfoPanel.SetActive(false);
            int behindRoomNum;
            if (touchDoor.roomNum1 == playerPos)
                behindRoomNum = touchDoor.roomNum2;
            else
                behindRoomNum = touchDoor.roomNum1;

            // 열리지 않은 방에만 플래그 표시 가능
            if (!_stageInfo.roomList[behindRoomNum - 1].isOpened)
            {
                _stageInfo.roomList[behindRoomNum - 1].Flag();
            }
        }
    }
    public void PlayerOpenDoor(int roomIndex, bool behindStateisOpen)
    {
        _stageInfo.roomList[roomIndex].Open(behindStateisOpen);
        // 폭탄이 있는 방을 열었을 때
        if (_stageInfo.roomList[roomIndex].hasBomb)
        {
            isEnd = true;
            Invoke("Die", 0.5f);
        }
        touchDoor = null;
    }

    public void OpenMap()
    {
        // 미니맵 열기
        if (!isMapActive && Input.GetKeyDown(KeyCode.M))
        {
            Cursor.lockState = CursorLockMode.None;   // 마우스 커서 고정 풀기
            Cursor.visible = true;
            _canvasManager.ActiveMap();
            isMapActive = true;
        }
        // 미니맵 닫기
        else if(isMapActive && Input.GetKeyDown(KeyCode.M))
        {
            CloseMap();
        }
    }
    public void CloseMap()
    {
        Cursor.lockState = CursorLockMode.Locked;   // 마우스 커서 고정
        Cursor.visible = false;
        _canvasManager.CloseMap();
        isMapActive = false;
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

    private void Die()
    {
        _canvasManager.isEnd = true;
        
        myCamera.GetComponent<Animation>().Play();
        playerAnim.Play("Dead");

        Cursor.lockState = CursorLockMode.None;   // 마우스 커서 고정 풀기
        Cursor.visible = true;

        _canvasManager.PopDeadPanel();
    }
    private void Clear()
    {
        isEnd = true;
        myCamera.GetComponent<Animation>().Play();

        Cursor.lockState = CursorLockMode.None;   // 마우스 커서 고정 풀기
        Cursor.visible = true;

        _canvasManager.PopClearPanel();
    }
}
