using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string moveAxisName = "Vertical"; //�յ� �������� ���� �Է��� �̸�
    public string rotareAxisName = "Horizontal"; //�¿� ȸ���� ���� �Է��� �̸�
    public string fireButtonName = "Fire1"; // �߻縦 ���� �Է� ��ư �̸�
    public string reloadButtonName = "Reload"; // �������� ���� �Է� ��ư �̸�

    public float move { get; private set; } // ������ ������ �Է°�
    public float rotate { get; private set; }// ������ ȸ�� �Է°�
    public bool fire { get; private set; }// ������ �߻� �Է°�
    public bool reload { get; private set; }// ������ ������ �Է°�


    // Update is called once per frame
    void Update()
    {
        //���ӿ��� ���¿����� ����� �Է��� �������� ����
    if(Gamemanager.instance!=null && Gamemanager.instance.isGameover)
        {
            move = 0;
            rotate = 0;
            fire = false;
            reload = false;
            return;
        }
    // ������Ƽ**
        //move�� ���� �Է� ����
        move = Input.GetAxis(moveAxisName);
        //rotate �� ���� �Է°���
        rotate = Input.GetAxis(rotareAxisName);
        //fire �� ���� �Է°���
        fire = Input.GetButton(fireButtonName);
        //reload�� ���� �Է°���
        reload = Input.GetButtonDown(reloadButtonName);
    }

}
