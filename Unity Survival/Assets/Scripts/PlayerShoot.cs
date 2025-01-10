using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Gun gun;

    private PlayerInput input;
    private PlayerHealth playerHealth;
    void Awake()
    {
        input = GetComponent<PlayerInput>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if(input.Fire && !playerHealth.isSettingPanelOpen)
        {
            gun.Fire();
        }
    }
}