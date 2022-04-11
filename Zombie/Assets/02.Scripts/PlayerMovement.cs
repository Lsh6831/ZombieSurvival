using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;// 앞뒤 움직임의 속도
    public float rotateSpeed = 180f; //좌우 회전 속도

    private PlayerInput playerInput;// 플레이어 입력을 알려주는 컴포넌트

    [SerializeField] private Rigidbody playerRigidbody; //플레이어 캐릭터의 리지드 바디
    //↑
    private Animator playerAnimator; //플레이이어 캐릭터의 애니메이터

    // Start is called before the first frame update
    void Start()
       //사용할 컴포넌트들의 참조 가져오기
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    //FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    private void FixedUpdate()
        //물리 갱신 주기마다 움직임,회전,애니메이션 처리 실행
    {
        
    }
    //입력값에 따라 캐릭터를 앞뒤로 움직임
    private void Move()
    {
        
    }
    //입력값에 따라 캐릭터를 좌우로 움직임
    void Rotate()
    {

    }
    
    void Update()
    {
        
    }
}
