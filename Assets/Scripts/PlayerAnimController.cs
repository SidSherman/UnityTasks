using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetAnimState(int value)
    {
        _animator.SetInteger("AnimState", value);
    }
    public void SetVelocity(float value)
    {
        _animator.SetFloat("Velocity", value);
    }
    public void SetTalk(bool value)
    {
        _animator.SetBool("Talk", value);
    }
   
}
