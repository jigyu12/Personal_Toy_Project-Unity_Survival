using UnityEngine;
using UnityEngine.Audio;

public class PlayerShoot : MonoBehaviour
{
    public Gun gun;

    private PlayerInput input;
    private PlayerHealth playerHealth;
    
    public VirtualJoyStickAttack joyStick;
    
    void Awake()
    {
        input = GetComponent<PlayerInput>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        if(input.Fire && !playerHealth.isSettingPanelOpen)
        {
            gun.Fire();
        }
#elif UNITY_ANDROID || UNITY_IOS
        if(joyStick.Input != Vector2.zero && !playerHealth.isSettingPanelOpen)
        {
            gun.Fire();
        }
#endif
    }
}