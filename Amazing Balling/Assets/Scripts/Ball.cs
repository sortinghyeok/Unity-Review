using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public LayerMask whatIsProp;
    public ParticleSystem explosionParticle;
    public AudioSource explosionAudio;

    public float maxDamage = 100f;
    public float explosionForce = 1000f;
    public float lifeTime = 10f;
    public float explosionRadius = 10f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    { 
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, whatIsProp); //layer�� �ش��ϴ� �͸� ������

        Debug.Log("Ʈ���� ����");
        // �浹�� �θ�� �и�
     

        for(int i = 0; i<colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();
            targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            Prop targetProp = colliders[i].GetComponent<Prop>();
            float damage = CalculateDamage(colliders[i].transform.position);

            targetProp.TakeDamage(damage);
        }
        
        explosionParticle.transform.parent = null;

        explosionParticle.Play();
        explosionAudio.Play();

        Destroy(explosionParticle.gameObject, explosionParticle.duration);
        Destroy(gameObject);
        Debug.Log("Ʈ���� ����");

    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;

        float distance = explosionToTarget.magnitude; //Vector�� ����
        float edgeToCenterDistance = explosionRadius - distance;
        float percentatge = edgeToCenterDistance / explosionRadius;

        float damage = maxDamage * percentatge;
        damage = Mathf.Max(damage, 0);

        return damage;

    }
}
