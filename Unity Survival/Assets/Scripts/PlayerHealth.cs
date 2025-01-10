using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using MouseButton = UnityEngine.UIElements.MouseButton;
using Slider = UnityEngine.UI.Slider;

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
    public Transform settingPanel;
    
    public float maxHp = 100f;

    public float Hp {  get; private set; }
    public bool IsDead { get; private set; }

    public event Action onDeath;
    
    public Transform gameOverPanel;
    bool isGameOver;
    
    public bool isSettingPanelOpen;

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
        settingPanel.gameObject.SetActive(false);
        
        Time.timeScale = 1f;
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
        
        StartCoroutine(ShowGameOverPanel());
    }

    IEnumerator ShowGameOverPanel()
    {
        yield return new WaitForSeconds(3f);
        
        Time.timeScale = 0f;
        gameOverPanel.gameObject.SetActive(true);
        isGameOver = true;
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
        if (isGameOver && Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingPanel();
        }
    }

    IEnumerator ShowHitPanel()
    {
        hitPanel.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(0.05f);
        
        hitPanel.gameObject.SetActive(false);
    }

    public void SettingPanel()
    {
        if (!isSettingPanelOpen)
        {
            Time.timeScale = 0f;
                
            settingPanel.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
                
            settingPanel.gameObject.SetActive(false);
        }
            
        isSettingPanelOpen = !isSettingPanelOpen;
    }

    public void GameByeBye()
    {
        Application.Quit();
    }
    
    public void RestartLevel()
    {
        
    }
}