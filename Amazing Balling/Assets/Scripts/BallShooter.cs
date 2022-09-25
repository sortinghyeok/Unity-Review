using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallShooter : MonoBehaviour
{
    public CamFollow cam;
    public Rigidbody ball;
    
    //ball 이 생성되는 위치
    public Transform firePos;
    public Slider powerSlider;
    public AudioSource ShooterAudio; //발사시
    public AudioClip fireClip; // 실행 음악
    public AudioClip chargingClip; //충전시

    public float minForce = 15f;
    public float maxForce = 30f;

    public float chargingTime = 0.75f;

    private float currentForce;
    private float chargeSpeed;
    private bool fired; //이미 발사되었는가?
    private void OnEnable() //Active되면 매번 시작
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
            //발사처리
        }
        else if(Input.GetButtonDown("Fire1")) //버튼 눌렀을때
        {
            //fired = false; //연사 가능

            currentForce = minForce;
            ShooterAudio.clip = chargingClip;
            ShooterAudio.Play();
        }
        else if(Input.GetButton("Fire1") && !fired) //버튼 누르고 있는 중에
        {
            currentForce = currentForce + chargeSpeed*Time.deltaTime;
            powerSlider.value = currentForce;
        }
        else if(Input.GetButtonUp("Fire1") && !fired) // 버튼에서 손을 뗄 때
        {
            fire();
        }
    }

    private void fire()
    {
        fired = true;
        Rigidbody ballInstacne = Instantiate(ball, firePos.position, firePos.rotation);
        ballInstacne.velocity = currentForce*firePos.forward;//방향을 곱해주어 vector 3가 반환됨
        ShooterAudio.clip = fireClip;
        ShooterAudio.Play();

        currentForce = minForce;

        cam.SetTarget(ballInstacne.transform, CamFollow.State.Tracking);
    }
}
