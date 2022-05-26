using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // AI, 내이게이션 시스템 관련 코드 가져오기


// 좀비 AI 구현
public class Zombie : LivingEntity
{

    public LayerMask whatISTarget; // 추적 대상 레이어
    
    private LivingEntity targetEntity; // 추적 대상
    private NavMeshAgent navMeshAgent; // 경로 계산 AI 에이전트

    public ParticleSystem hitEffect; // 피격 시 재생할 파티클 효과
    public AudioClip deaathSound;// 사망 시 재생할 소리
    public AudioClip hitSound;// 피격 시 재 생할 소리

    private Animator zombieAnimator; // 애니메이터 컴포넌트
    private AudioSource zombieAudioPlayer; // 오디오 소스 컴퍼난ㅌ,
    private Renderer zombieRenderer; //렌더러 컴포넌트
    
    public float famage = 20f; // 공격력
    public float timeBetAttack = 0.5f; // 공격 간격
    private float lastAttackTime; // 마지막 공격 시점

    // 추저ㅏㄱ할 대상이 존재하는지 알려주는 포로퍼티
    private bool hasTasrget
    {

        get
        {
            //추적할 대상이 존재하고, 대상이 사망하지 않았다면 true
            if(targetEntity!=null&&!targetEntity.dead)
            {
                return true;
            }
            // 그렇지 않다면 false
            return false;
        }
        
        
    }
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
