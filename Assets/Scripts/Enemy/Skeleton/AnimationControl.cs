using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private float attack;

    private Animator animator;
    private PlayerAnim player;
    private Skeleton skeleton;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerAnim>();
        skeleton = GetComponentInParent<Skeleton>();
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
        if(!skeleton.isDead)
        {
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, playerLayer);

            if (hit != null)
            {
                // tira vida do player
                player.TakeDamage(attack);
            }
            else
            {
                // não acertou o player
            }
        }
    }

    public void TakeDamage(float value)
    {     
        if(!skeleton.isDead)
        {
            skeleton.health -= value;
            
            if(skeleton.health <= 0)
            {
                // morreu
                skeleton.isDead = true;
                animator.SetBool("death", true);
                Destroy(skeleton.gameObject, 1.0f);
            } else
            {
                animator.SetTrigger("hit");
                skeleton.healthImage.fillAmount = skeleton.health / skeleton.totalHealth;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
