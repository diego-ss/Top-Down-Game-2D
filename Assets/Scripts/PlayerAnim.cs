using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Player player;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
        OnRun();
    }

    #region Movement

    void OnMove()
    {
        if (player.Direction.magnitude > 0f)
        {
            animator.SetInteger("transition", 1);
        }
        else
        {
            animator.SetInteger("transition", 0);
        }

        if (player.Direction.x >= 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else
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


}
