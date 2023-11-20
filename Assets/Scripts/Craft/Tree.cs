using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private float health = 5.0f;
    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private ParticleSystem leafsParticles;
    [SerializeField] private int maxWoods;

    private bool Destroyed = false;
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
        leafsParticles.Play();

        if(health <= 0)
        {
            for(int i = 0; i < maxWoods; i++)
            {
                // criar o toco e instanciar os drops
                Instantiate(woodPrefab,
                    transform.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-0.5f, 0.5f), 0f),
                    Quaternion.identity);
            }

            animator.SetTrigger("Cut");
            Destroyed = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Axe") && !Destroyed)
            OnHit();
    }
}
