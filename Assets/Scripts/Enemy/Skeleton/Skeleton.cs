using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AnimationControl animationControl;
    public Image healthImage;

    [Header("Stats")]
    public float attack;
    public float health;
    public float totalHealth;
    public bool isDead;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        health = totalHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
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
}
