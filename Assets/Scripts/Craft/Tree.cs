using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private float health = 5.0f;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    public void OnHit()
    {
        health--;

        animator.SetTrigger("Hit");

        if(health <= 0)
        {
            // criar o toco e instanciar os drops
            animator.SetTrigger("Cut");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Axe"))
            OnHit();
    }
}
