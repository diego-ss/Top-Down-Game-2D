using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float speed;
    public List<Transform> targets = new List<Transform>();
    
    private int index = 0;
    private float initialSpeed;
    private Animator anim;

    private void Start()
    {
        initialSpeed = speed;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueControl.instance.IsShowing)
        {
            speed = 0;
            anim.SetBool("IsWalking", false);
        }
        else
        {
            speed = initialSpeed;
            anim.SetBool("IsWalking", true);
        }

        transform.position = Vector3.MoveTowards(transform.position, targets[index].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targets[index].position) < 0.1f)
        {
            if (index < targets.Count - 1)
                index = Random.Range(0, targets.Count - 1);
            else
                index = 0;
        }

        Vector2 direction = targets[index].position - transform.position;
        if (direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        } else if (direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

}
