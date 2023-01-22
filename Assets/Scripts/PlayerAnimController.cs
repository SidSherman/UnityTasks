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
   
}
