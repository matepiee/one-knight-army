using UnityEngine;

public class Player_Guard : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
    [HideInInspector] public bool isShieldActive = false;

    public void ActivateGuard()
    {
        isShieldActive = true;
        anim.SetBool("IsGuarding", true);
    }

    public void DeactivateGuard()
    {
        isShieldActive = false;
        anim.SetBool("IsGuarding", false);
    }
}