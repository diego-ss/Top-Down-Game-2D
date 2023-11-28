using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AnimationControl animationControl;
    [SerializeField] private float playerDetectionDistance;
    [SerializeField] private LayerMask playerLayer;

    public Image healthImage;

    [Header("Stats")]
    public float attack;
    public float health;
    public float totalHealth;
    public bool isDead;

    private Player player;
    private bool playerHit;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        health = totalHealth;
    }

    private void FixedUpdate()
    {
        DetectPlayer();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead && playerHit)
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);

            if (transform.position.x > player.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (Vector2.Distance(transform.position, player.gameObject.transform.position) <= agent.stoppingDistance)
            {
                // chegou no player
                animationControl.PlayAnimation(2);
            }
            else
            {
                // animação de walk
                animationControl.PlayAnimation(1);
            }
        }    
    }

    public void DetectPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, playerDetectionDistance, playerLayer);

        if (hit != null)
            playerHit = true;
        else
        {
            playerHit = false;
            animationControl.PlayAnimation(0);
            agent.isStopped = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, playerDetectionDistance);
    }
}
