using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public AudioClip hitSound;
    public AudioClip deathSound;
    public ParticleSystem hitEffect;
    private GameObject target;
    public readonly int hashMove = Animator.StringToHash("Move");
    public float damage;
    public float Hp;
    private float deadBodyTimer;
    private float deadBodyDelay = 3.5f;
    private CapsuleCollider capsule;

    private NavMeshAgent agent;
    private Animator animator;
    private AudioSource audioSource;
    private bool isDead;
    private bool attackAble = true;
    
    public Transform scoreUI;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        capsule = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        isDead = false;
        deadBodyTimer = 0f;
        
        target = GameObject.FindGameObjectWithTag("Player");
        scoreUI = GameObject.FindGameObjectWithTag("ScoreUI").transform;
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            deadBodyTimer += Time.deltaTime;
            if(deadBodyTimer > deadBodyDelay)
                Destroy(gameObject);
            return;
        }

        agent.SetDestination(target.transform.position);
        animator.SetFloat(hashMove, agent.velocity.magnitude / agent.speed);
    }

    public void OnDamage(float damage, Vector3 hitPos, Vector3 hitNormal)
    {
        if (isDead)
            return;

        Hp -= damage;
        hitEffect.transform.position = hitPos;
        hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
        hitEffect.Play();
        audioSource.PlayOneShot(hitSound);
        if(Hp  < 0)
        {
            Hp = 0;
            OnDie();
        }
    }

    public void OnDie()
    {
        capsule.enabled = false;
        animator.SetBool("Dead", true);
        audioSource.PlayOneShot(deathSound);
        isDead = true;
        
        scoreUI.GetComponent<ScoreUI>().AddScore(100);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isDead && attackAble && other.CompareTag("Player"))
        {
            StartCoroutine(Attack(other.GetComponent<PlayerHealth>()));
        }
    }
    
    private IEnumerator Attack(PlayerHealth playerHealth)
    {
        if (!playerHealth.IsDead)
        {
            var hitPoint = playerHealth.GetComponent<Collider>().ClosestPoint(transform.position);
            var hitNormal = (playerHealth.transform.position - transform.position).normalized;
            playerHealth.OnDamage(damage, hitPoint, hitNormal);
            attackAble = false;
        }
        yield return new WaitForSeconds(1f);
    
        attackAble = true;
    }

    public void StartSinking()
    {
        
    }
}
