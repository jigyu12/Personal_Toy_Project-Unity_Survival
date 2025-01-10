using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;

    public AudioClip deathSound;
    public AudioClip hitSound;

    private AudioSource audioSource;
    private Animator animator;
    private PlayerMove playerMovement;
    private PlayerShoot playerShooter;

    public Transform hitPanel;
    
    public float maxHp = 100f;

    public float Hp {  get; private set; }
    public bool IsDead { get; private set; }

    public event Action onDeath;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMove>();
        playerShooter = GetComponent<PlayerShoot>();
    }

    private void Start()
    {
        hitPanel.gameObject.SetActive(false);
    }

    protected void OnEnable()
    {
        IsDead = false;
        Hp = maxHp;
        
        healthSlider.gameObject.SetActive(true);

        healthSlider.maxValue = maxHp;
        healthSlider.minValue = 0f;
        healthSlider.value = Hp;

        playerMovement.enabled = true;
        playerShooter.enabled = true;
    }

    public void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        Hp -= damage;

        if (Hp <= 0)
        {
            Hp = 0;
            Die();
        }

        healthSlider.value = Hp;

        if (!IsDead)
        {
            StartCoroutine(ShowHitPanel());
            audioSource.PlayOneShot(hitSound);
        }
    }

    public void Die()
    {
        onDeath?.Invoke();
        IsDead = true;
        Hp = 0;

        healthSlider.gameObject.SetActive(false);
        animator.SetTrigger("Die");

        audioSource.PlayOneShot(deathSound);

        playerMovement.enabled = false;
        playerShooter.enabled = false;
    }

    public void AddHp(float add)
    {
        if (IsDead)
            return;

        Hp += add;
        if (Hp > maxHp)
        {
            Hp = maxHp;
        }

        healthSlider.value = Hp;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnDamage(20, Vector3.zero, Vector3.zero);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddHp(20);
        }
    }

    IEnumerator ShowHitPanel()
    {
        hitPanel.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(0.05f);
        
        hitPanel.gameObject.SetActive(false);
    }
}