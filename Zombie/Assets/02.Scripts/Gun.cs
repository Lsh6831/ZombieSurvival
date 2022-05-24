﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum State
    {
        //enum = 기본 베이스는 상수(변하지 않는수) 

        //총의 상태를 표현하는 데 사용할 타입 선언
        Ready, //발사 준비됨
        Empty, // 탄창이 빔
        Reloading, // 재장전 중
    }
    public State state { get; private set; } //현재 총의 상태

    public Transform fireTransform; //탄알이 발사될 위치

    public ParticleSystem muzzleFlashEffext; // 총구 화염 효과
    public ParticleSystem shellEjectEffext; // 탄피 배출 효과

    private LineRenderer bulletLineRenderer; // 탄알 궤적을 그리기 위한 랜더러

    private AudioSource gunAudioPlayer; // 총 소리 재생기 

    public GunData gunData;// 총의 현재 데이터

    private float fireDistance = 50f; // 사정거리

    public int ammoRemain = 100; // 남의 전체 탄알

    public int magAmmo; // 현재 탄창에 남아 있는 탄알

    private float lastFireTime; // 총을 마지막으로 발사한 시점

    

    private void Awake()
        //사용할 컴포넌트의 참조 가져오기
    {
        bulletLineRenderer = GetComponent < LineRenderer>();
        //GetComponet=가져오기
        //bulletLineRenderer = GetComponentInChildren<LineRenderer>() = 자식의 안에 있는걸 가죠온다
        //LineRenderer[] bulletLInerenderlers = GetComponent<LineRenderer>() //다수를 가져온다
        //LineRenderer[] bulletLInerenderlers = GetComponentInChildren<LineRenderer>() //자식의 다수를 가져온다

        gunAudioPlayer = GetComponent<AudioSource>();

        //사용할 점을 두개로 변경
        bulletLineRenderer.positionCount = 2;
        // 라인 렌더러를 비활성화
        bulletLineRenderer.enabled = false;
        //*콤퍼넌트 이니까 인에이블로 비활성화 = 인에이블은 소문자니까 안에서
        // set active 는 메서드로 gun같은걸 비활성화 시킬때사용
    }

    private void OnEnable()
        //컴포넌트 실행시 1번
        //private void OnDisable()
        ////컴포넌트 종료시 1번
        
        // 총 상태 초기화
    {
        // 전체 예비 탄알 양을 초기화
        ammoRemain = gunData.startAmmoRemain;
        // 현재 탄창을 가득 채우기
        magAmmo = gunData.magCapacity;

        // 총의 현재 상태를 총을 쏠 준비가 된 상태로 변경
        state = State.Ready;
        // 마지막으로 총을 쏜 시점을 초기화
        lastFireTime = 0;
    }

    public void Fire()
        //발사 시도
    {
        // 현재상태가 발사 가능한 상태
        // &&마지막 총 발사 시점에서 gunData.timeBetFire 이상의 시간이 지남
        if(state==State.Ready&&Time.time>=lastFireTime+gunData.timeBetFire)
        {
            //마지막 총 발사 시점 갱신
            lastFireTime = Time.time;
            // 실제 발사 처리 싱행
            Shot();
        }
    }
    

    private void Shot()
        //실제 발사 처리
    {
        // 레이캐스트에 의한 충돌 정보를 저장하는 컨데이너
        RaycastHit hit;

        // 탄알이 맞은 곳을 저장할 변수
        Vector3 hitPosition = Vector3.zero;

        //레이캐스트(시작 지접,방향,충돌 정보 컨테이너,사정거리
        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance))
        {//레일케스트는 무조건 아웃 으로 표현해야한다 로 생각하자
            //레이가 어떤 물체와 충돌한 경우

            //충돌한 상대방으로부터 IDamageable 오브젝트 가져오기 시도
            IDamageable target = hit.collider.GetComponent<IDamageable>();

            if (target != null)
            {
                //상대방의 OnDamage 함수를 실행시켜 상대방에 대미지 주기
                target.OnDamage(gunData.dagame, hit.point, hit.normal);
                //                               위치       방향
            }
            //레이가 충돌한 위치 저장
            hitPosition = hit.point;
        }
        else
        {
            //레이가 다른 물체와 충돌하지 않았다면
            //탄알이 최대 사정거리까지 날아깟을 때의 위치를 충돌 위치로 사용
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;

        }
            // 발사 이펙트 재생시작
            StartCoroutine(shotEffect(hitPosition));
            //지연시간

            //남은 탄알 수를 -1
            magAmmo--;
            if (magAmmo <= 0)
            {
                // 탄창에 남은 탄알이 없다면 총의 현재 상태를 Empty로 갱신
                state = State.Empty;
            }
        
    }

    //발사 이펙트와 소리를 재생하고 탄알 궤적을 그림
    private IEnumerator shotEffect(Vector3 hitPosition)
    //IEnumerator(코르틴메서드) 인보크라는 지연 방법도 존재 하단에 첨부
    {
        // 총구 화염 효과 재생
        muzzleFlashEffext.Play();

        // 탄피 배출 효과 재생
        shellEjectEffext.Play();

        // 총격 소리 재생
        gunAudioPlayer.PlayOneShot(gunData.shotClip);
        //플레이어 온샷은 사운드를 중첩이 가능

        // 선의 시작점은 총구의 위치
        bulletLineRenderer.SetPosition(0, fireTransform.position);

        // 선의 끝점은 입력으로 들어온 충돌 위치
        bulletLineRenderer.SetPosition(1, hitPosition);

        // 라인 렌더러를 활성화하여 탄알 궤적을 그림
        bulletLineRenderer.enabled = true;
        //게임 오브젝트는 Setactive
        //컴포넌트는 enabled

        // 0.03초동안 잠시 처리를 대기
        yield return new WaitForSeconds(0.03f);

        // 라인 렌더러를 비활성화하여 탄알 궤적을 지움
        bulletLineRenderer.enabled = false;
        //라인 랜더러를 활성화하여 탄알 궤적을 그림
        bulletLineRenderer.enabled = true;

        //0.03초 동안 잠시 처리를 대기
        yield return new WaitForSeconds(0.03f);

        // 라인 렌더러를 비활성화하여 탄알 궤적을 지움
        bulletLineRenderer.enabled = false;
    }
    // 인보크 사용법
    // 유니티에서 인보크 사용을 하지 않길 원함
    //private void Start()
    //{
    //    Invoke("Test", 3f);
    //}

    //public void Test()
    //{
    //    Debug.Log("!");
    //}
    // 재장전 시도
    public bool Reload()
    {
        if (state == State.Reloading || ammoRemain <= 0 || magAmmo >= gunData.magCapacity)
        {
            // 이미 재장전 중이거나 남은 탈알이 없거나
            //탄창에 탄알이 이미 가득한 경우 재장전할 수 없음
            return false;
        }
        StartCoroutine(ReloadRountine());
        return true;    
    }

    // 실제 재장전 처리를 진행
    private IEnumerator ReloadRountine()
        //이놈 레이터 = 코르틴 영역 ->메서드 실행에 대기시간을 줄수 있는것 
    {
        //현재 상태를 재장전 중 상태로 전환
        state = State.Reloading;
        
        // 재장전 소요 시간만큼 처리 쉬기
        yield return new WaitForSeconds(gunData.reloadTime);

        // 총의 현재 상태를 발사 준비된 상태로 변경
        state = State.Ready;
    }
}
