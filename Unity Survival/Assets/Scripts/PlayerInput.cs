using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static readonly string moveVAxisName = "Vertical";
    public static readonly string moveHAxisName = "Horizontal";
    public static readonly string fireAxisName = "Fire1";

    public float HMove {  get; private set; }
    public float VMove {  get; private set; }
    public bool Fire {  get; private set; }
    private void Update()
    {
        HMove = Input.GetAxis(moveVAxisName);
        VMove = Input.GetAxis(moveHAxisName);
        Fire = Input.GetButton(fireAxisName);
    }
}