using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public const float MinWalkDistance = 0.1f;
    public const float DeadTime = 2f;
    public const float AttackTime = 0.5f;
    public const float UseMedicineTime = 1f;
    public const float UseGrenadeTime = 1f;
    private Animator _animator;
    private void Start()
    {
        TryGetAnimator();
    }
    private void TryGetAnimator()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
    }
    public void SetAnimatorSpeed(float speed)
    {
        if (speed > 0)
        {
            TryGetAnimator();
            _animator.speed = speed;
        }
    }
    /// <summary>
    /// Judge whether to play the walk animation
    /// </summary>
    /// <param name="originalPosition"></param>
    /// <param name="newPosition"></param>
    public void WalkTo(Vector3 originalPosition, Vector3 newPosition)
    {
        TryGetAnimator();

        if (Vector3.Distance(originalPosition, newPosition) > MinWalkDistance * Record.RecordInfo.FrameTime)
            _animator.SetBool("Running", true);
        else
            _animator.SetBool("Running", false);
    }

    /// <summary>
    /// Play the dead animation
    /// </summary>
    private void SetNotDead()
    {
        TryGetAnimator();

        _animator.SetBool("IsDead", false);
    }
    public void SetDead()
    {
        TryGetAnimator();

        _animator.SetBool("IsDead", true);
        Invoke(nameof(SetNotDead), DeadTime);
    }

    public void SetNotFiring()
    {
        TryGetAnimator();
        _animator.SetBool("Firing", false);
    }

    public void SetFiring()
    {
        TryGetAnimator();
        _animator.SetBool("Firing", true);
        Invoke(nameof(SetNotFiring), AttackTime);
    }

    private void SetNotDrinking()
    {
        TryGetAnimator();
        _animator.SetBool("Drinking", false);
    }

    public void SetDrinking()
    {
        TryGetAnimator();
        _animator.SetBool("Drinking", true);
        Invoke(nameof(SetNotDrinking), UseMedicineTime);
    }
}
