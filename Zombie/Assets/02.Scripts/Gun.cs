using System.Collections;
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

    public Transform fieTransform; //탄알이 발사될 위치

    public ParticleSystem muzzleFlashEffext; // 총구 화염 효과
    public ParticleSystem shellEjectEffext; // 탄피 배출 효과

    private LineRenderer bulletLineRenderer; // 탄알 궤적을 그리기 위한 랜더러

    private AudioSource gunAudioPlayer; // 총 소리 재생기 

    public GunData gunData;// 총의 현재 데이터

    private float fireDistance = 50f; // 사정거리

    public int ammoRemain = 100; // 남의 전체 탄알

    public int magammo; // 현재 탄창에 남아 있는 탄알

    private float lastFireTime; // 총을 마지막으로 발사한 시점

    private void Awake()
        //사용할 컴포넌트의 참조 가져오기
    {
        bulletLineRenderer = GetComponent < LineRenderer>();
        gunAudioPlayer = GetComponent<AudioSource>();

        //사용할 점을 두개로 변경
        bulletLineRenderer.positionCount = 2;
        // 라인 렌더러를 비활성화
        bulletLineRenderer.enabled = false;
        //*콤퍼넌트 이니까 인에이블로 비활성화 = 인에이블은 소문자니까 안에서
        // set active 는 메서드로 gun같은걸 비활성화 시킬때사용
    }

    private void OnEnable()
        // 총 상태 초기화
    {
        // 전체 예비 탄알 양을 초기화
        ammoRemain = gunData.startAmmoRemain;
        // 현재 탄창을 가득 채우기
        magammo = gunData.magCapacity;

        // 총의 현재 상태를 총을 쏠 준비가 된 상태로 변경
        state = State.Ready;
        // 마지막으로 총을 쏜 시점을 초기화
        lastFireTime = 0;
    }

    public void Fire()
        //발사 시도
    {

    }

    private void Shot()
        //실제 발사 처리
    {

    }

    //발사 이펙트와 소리를 재생하고 탄알 궤적을 그림
    private IEnumerator ShotEffect(Vector3 hitPosition)
    //IEnumerator(코르틴메서드) 인보크라는 지연 방법도 존재 하단에 첨부
    {
        // 총구 화염 효과 재생
        muzzleFlashEffext.Play();

        // 탄피 배출 효과 재생
        shellEjectEffext.Play();

        // 총격 소리 재생
        gunAudioPlayer.PlayOneShot(gunData.shotClip);
        //플레이어 온샷은 사운들르 중첩이 가능

        // 선의 시작점은 총구의 위치
        bulletLineRenderer.SetPosition(0, fieTransform.position);

        // 선의 끝점은 입력으로 들어온 충돌 위치
        bulletLineRenderer.SetPosition(1, hitPosition);

        // 라인 렌더러를 활성화하여 탄알 궤적을 그림
        bulletLineRenderer.enabled = true;

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
        return false;
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
