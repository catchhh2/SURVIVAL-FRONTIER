using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public enum PickupType { BuffAttack, BuffRate, Heal }

public class Pickup : MonoBehaviour
{
    public PickupType type;
    [Tooltip("Buff 数值：伤害+5，攻速+0.5，回血+10")]
    public float value = 5f;
    public float rotate_speed = 30f;//设置旋转速度

    void Update()
    {
        transform.Rotate(Vector3.down * rotate_speed * Time.deltaTime);//物体自转
        //Vector3=Vector（0，1，0）是一个向上的向量,代表Y轴
        //Time.deltaTime每帧调用一次
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 应用效果
            var shoot = other.GetComponentInChildren<PlayerShoot>();
            var health = other.GetComponent<PlayerHealth>();
            string msg = "";

            switch (type)
            {
                case PickupType.BuffAttack:
                    shoot.attack += value;
                    msg = $"ATTACK UP";
                    break;
                case PickupType.BuffRate:
                    float rateValue=value*0.1f;
                    shoot.shootRate += rateValue;
                    msg = $"SPEED UP";
                    break;
                case PickupType.Heal:
                    float healthValue=value*2f;
                    health.Heal(healthValue);
                    msg = $"HEAL UP";
                    break;
            }

            // 通知 UIManager 显示提示
            UIManager.Instance.ShowPickupText(msg);

            // 销毁自己
            Destroy(gameObject);
        }

            

    }
}
