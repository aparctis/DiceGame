using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarModel : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void Death()
    {
        animator?.SetBool("isDead", true);
    }

    public void Alive()
    {
        animator?.SetBool("isDead", false);

    }
}
