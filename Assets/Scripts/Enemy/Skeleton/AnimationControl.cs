using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAnimation(int value)
    {
        animator.SetInteger("transition", value);
    }

    public void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, playerLayer);
        
        if(hit != null )
        {
            // tira vida do player
            Debug.Log("Acertou o player");
        } else
        {
            // não acertou o player
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
