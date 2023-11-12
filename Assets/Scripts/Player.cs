using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 300.0f;
    [SerializeField]
    private float runSpeed = 450.0f;

    private Rigidbody2D rigidbody;
    
    private float initialSpeed;
    private bool _isRunning;
    public bool IsRunning { get => _isRunning; set => _isRunning = value; }

    private Vector2 _direction;
    public Vector2 Direction { get => _direction; set => _direction = value; }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        initialSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        OnInput();
        OnRun();
    }

    private void FixedUpdate()
    {
        OnMove();
    }

    #region Movement
    
    void OnInput()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void OnMove()
    {
        rigidbody.velocity = _direction.normalized * speed * Time.fixedDeltaTime;
    }

    void OnRun()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            IsRunning = true;
            speed = runSpeed;
        } else
        {
            IsRunning = false;
            speed = initialSpeed;
        }
    }

    #endregion
}
