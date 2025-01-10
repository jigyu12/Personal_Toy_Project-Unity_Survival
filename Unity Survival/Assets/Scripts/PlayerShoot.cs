using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Gun gun;

    private PlayerInput input;
    void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if(input.Fire)
        {
            gun.Fire();
        }
    }
}
