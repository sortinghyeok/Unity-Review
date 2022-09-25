using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallShooter : MonoBehaviour
{
    public CamFollow cam;
    public Rigidbody ball;
    
    //ball �� �����Ǵ� ��ġ
    public Transform firePos;
    public Slider powerSlider;
    public AudioSource ShooterAudio; //�߻��
    public AudioClip fireClip; // ���� ����
    public AudioClip chargingClip; //������

    public float minForce = 15f;
    public float maxForce = 30f;

    public float chargingTime = 0.75f;

    private float currentForce;
    private float chargeSpeed;
    private bool fired; //�̹� �߻�Ǿ��°�?
    private void OnEnable() //Active�Ǹ� �Ź� ����
    {
        currentForce = minForce;
        powerSlider.value = minForce;
        fired = false;
    }

    private void Start()
    {
        chargeSpeed = (maxForce - minForce) / chargingTime;
    }

    private void Update()
    {
        if(fired == true)
        {
            return;
        }

        powerSlider.value = minForce;

        if(currentForce >= maxForce && !fired)
        {
            currentForce = maxForce;
            fire();
            //�߻�ó��
        }
        else if(Input.GetButtonDown("Fire1")) //��ư ��������
        {
            //fired = false; //���� ����

            currentForce = minForce;
            ShooterAudio.clip = chargingClip;
            ShooterAudio.Play();
        }
        else if(Input.GetButton("Fire1") && !fired) //��ư ������ �ִ� �߿�
        {
            currentForce = currentForce + chargeSpeed*Time.deltaTime;
            powerSlider.value = currentForce;
        }
        else if(Input.GetButtonUp("Fire1") && !fired) // ��ư���� ���� �� ��
        {
            fire();
        }
    }

    private void fire()
    {
        fired = true;
        Rigidbody ballInstacne = Instantiate(ball, firePos.position, firePos.rotation);
        ballInstacne.velocity = currentForce*firePos.forward;//������ �����־� vector 3�� ��ȯ��
        ShooterAudio.clip = fireClip;
        ShooterAudio.Play();

        currentForce = minForce;

        cam.SetTarget(ballInstacne.transform, CamFollow.State.Tracking);
    }
}
