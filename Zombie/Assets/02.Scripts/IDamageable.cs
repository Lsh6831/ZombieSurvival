using UnityEngine;

public interface IDamageable
{
    void OnDamage(float damage, Vector3 hitpoint, Vector3 hitnormal);
    //       ������ ���°�  �浹�� ��ġ���� ���°�   �浹�� ����
}
