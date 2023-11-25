using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Player player;
    private Animator animator;

    [SerializeField] private float recoveryTime;

    [Header("Combat")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;

    private CastingArea castingArea;

    private bool isHurt = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();

        castingArea = FindObjectOfType<CastingArea>();
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
        OnRun();
        OnCutting();
        OnDigging();
        OnWatering();
    }

    public void OnCastingStarted()
    {
        animator.SetTrigger("casting");
        player.CanMove = false;
    }

    // Called by animation event
    public void OnCastingEnded()
    {
        castingArea.OnCasting();
        player.CanMove = true;
    }


    public void OnHammeringStarted()
    {
        animator.SetBool("building", true);
        player.CanMove = false;
    }

    public void OnHammeringEnded()
    {
        animator.SetBool("building", false);
        player.CanMove = true;
    }

    #region Movement

    void OnMove()
    {
        if (player.Direction.magnitude > 0f)
        {
            if (player.IsRolling)
                animator.SetTrigger("roll");
            else
                animator.SetInteger("transition", 1);
        }
        else
        {
            animator.SetInteger("transition", 0);
        }

        if (player.Direction.x > 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (player.Direction.x < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
    }

    void OnRun()
    {
        if (player.IsRunning && player.Direction.magnitude > 0f)
        {
            animator.SetInteger("transition", 2);
        }
    }

    #endregion

    #region Weapons
    void OnCutting()
    {
        if (player.IsCutting)
            animator.SetInteger("transition", 3);
    }

    void OnDigging()
    {
        if (player.IsDigging)
            animator.SetInteger("transition", 4);
    }

    void OnWatering()
    {
        if (player.IsWatering)
            animator.SetInteger("transition", 5);
    }
    #endregion


    #region Combat
    public void TakeDamage(float value)
    {
        if(!isHurt)
        {
            animator.SetTrigger("hurt");
            isHurt = true;
            StartCoroutine(Invulnerable());
        }
    }

    private IEnumerator Invulnerable()
    {
        yield return new WaitForSeconds(recoveryTime);
        isHurt = false;
    }

    public void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, enemyLayer);
        
        if(hit != null)
        {
            // atacou o inimigo
            Debug.Log("Acertou o inimigo");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
    #endregion
}
