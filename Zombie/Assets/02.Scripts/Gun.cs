using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum State
    {
        //enum = �⺻ ���̽��� ���(������ �ʴ¼�) 

        //���� ���¸� ǥ���ϴ� �� ����� Ÿ�� ����
        Ready, //�߻� �غ��
        Empty, // źâ�� ��
        Reloading, // ������ ��
    }
    public State state { get; private set; } //���� ���� ����

    public Transform fieTransform; //ź���� �߻�� ��ġ

    public ParticleSystem muzzleFlashEffext; // �ѱ� ȭ�� ȿ��
    public ParticleSystem shellEjectEffext; // ź�� ���� ȿ��

    private LineRenderer bulletLineRenderer; // ź�� ������ �׸��� ���� ������

    private AudioSource gunAudioPlayer; // �� �Ҹ� ����� 

    public GunData gunData;// ���� ���� ������

    private float fireDistance = 50f; // �����Ÿ�

    public int ammoRemain = 100; // ���� ��ü ź��

    public int magammo; // ���� źâ�� ���� �ִ� ź��

    private float lastFireTime; // ���� ���������� �߻��� ����

    private void Awake()
        //����� ������Ʈ�� ���� ��������
    {
        bulletLineRenderer = GetComponent < LineRenderer>();
        gunAudioPlayer = GetComponent<AudioSource>();

        //����� ���� �ΰ��� ����
        bulletLineRenderer.positionCount = 2;
        // ���� �������� ��Ȱ��ȭ
        bulletLineRenderer.enabled = false;
        //*���۳�Ʈ �̴ϱ� �ο��̺�� ��Ȱ��ȭ = �ο��̺��� �ҹ��ڴϱ� �ȿ���
        // set active �� �޼���� gun������ ��Ȱ��ȭ ��ų�����
    }

    private void OnEnable()
        // �� ���� �ʱ�ȭ
    {
        // ��ü ���� ź�� ���� �ʱ�ȭ
        ammoRemain = gunData.startAmmoRemain;
        // ���� źâ�� ���� ä���
        magammo = gunData.magCapacity;

        // ���� ���� ���¸� ���� �� �غ� �� ���·� ����
        state = State.Ready;
        // ���������� ���� �� ������ �ʱ�ȭ
        lastFireTime = 0;
    }

    public void Fire()
        //�߻� �õ�
    {

    }

    private void Shot()
        //���� �߻� ó��
    {

    }

    //�߻� ����Ʈ�� �Ҹ��� ����ϰ� ź�� ������ �׸�
    private IEnumerator ShotEffect(Vector3 hitPosition)
    //IEnumerator(�ڸ�ƾ�޼���) �κ�ũ��� ���� ����� ���� �ϴܿ� ÷��
    {
        // �ѱ� ȭ�� ȿ�� ���
        muzzleFlashEffext.Play();

        // ź�� ���� ȿ�� ���
        shellEjectEffext.Play();

        // �Ѱ� �Ҹ� ���
        gunAudioPlayer.PlayOneShot(gunData.shotClip);
        //�÷��̾� �¼��� ���鸣 ��ø�� ����

        // ���� �������� �ѱ��� ��ġ
        bulletLineRenderer.SetPosition(0, fieTransform.position);

        // ���� ������ �Է����� ���� �浹 ��ġ
        bulletLineRenderer.SetPosition(1, hitPosition);

        // ���� �������� Ȱ��ȭ�Ͽ� ź�� ������ �׸�
        bulletLineRenderer.enabled = true;

        // 0.03�ʵ��� ��� ó���� ���
        yield return new WaitForSeconds(0.03f);

        // ���� �������� ��Ȱ��ȭ�Ͽ� ź�� ������ ����
        bulletLineRenderer.enabled = false;
        //���� �������� Ȱ��ȭ�Ͽ� ź�� ������ �׸�
        bulletLineRenderer.enabled = true;

        //0.03�� ���� ��� ó���� ���
        yield return new WaitForSeconds(0.03f);

        // ���� �������� ��Ȱ��ȭ�Ͽ� ź�� ������ ����
        bulletLineRenderer.enabled = false;
    }
    // �κ�ũ ����
    // ����Ƽ���� �κ�ũ ����� ���� �ʱ� ����
    //private void Start()
    //{
    //    Invoke("Test", 3f);
    //}

    //public void Test()
    //{
    //    Debug.Log("!");
    //}
    // ������ �õ�
    public bool Reload()
    {
        return false;
    }

    // ���� ������ ó���� ����
    private IEnumerator ReloadRountine()
        //�̳� ������ = �ڸ�ƾ ���� ->�޼��� ���࿡ ���ð��� �ټ� �ִ°� 
    {
        //���� ���¸� ������ �� ���·� ��ȯ
        state = State.Reloading;
        
        // ������ �ҿ� �ð���ŭ ó�� ����
        yield return new WaitForSeconds(gunData.reloadTime);

        // ���� ���� ���¸� �߻� �غ�� ���·� ����
        state = State.Ready;
    }
}
