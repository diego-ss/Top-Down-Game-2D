using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 300.0f;
    [SerializeField]
    private float runSpeed = 650.0f;

    private Rigidbody2D rigidbody;

    private float initialSpeed;

    private bool _isRunning;
    public bool IsRunning { get => _isRunning; set => _isRunning = value; }

    private bool _isRolling;
    public bool IsRolling { get => _isRolling; set => _isRolling = value; }

    private bool _isCutting;
    public bool IsCutting { get => _isCutting; set => _isCutting = value; }

    private bool _isDigging;
    public bool IsDigging { get => _isDigging; set => _isDigging = value; }

    private Vector2 _direction;
    public Vector2 Direction { get => _direction; set => _direction = value; }

    private Weapon weapon = Weapon.Axe;

    public enum Weapon
    {
        Axe = 1,
        Shovel = 2
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        initialSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {

        CheckSwitchWeapon();

        OnInput();
        OnRun();
        OnRoll();

        switch (weapon)
        {
            case Weapon.Axe:
                OnCutting();
                break;
            case Weapon.Shovel:
                OnDigging();
                break;
        }   
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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            IsRunning = true;
            speed = runSpeed;
        }
        else
        {
            IsRunning = false;
            speed = initialSpeed;
        }
    }

    void OnRoll()
    {
        if (Input.GetMouseButtonDown(1) && !IsRolling)
            IsRolling = true;
       else 
            IsRolling = false;
    }

    #endregion

    #region Combat
    void CheckSwitchWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapon = Weapon.Axe;
            Debug.Log("Weapon: Machado");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapon = Weapon.Shovel;
            Debug.Log("Weapon: Pá");
        }
    }

    void OnDigging()
    {
        if (Input.GetMouseButtonDown(0) && !IsDigging)
        {
            IsDigging = true;
        }

        if (Input.GetMouseButtonUp(0) && IsDigging)
        {
            IsDigging = false;
            speed = initialSpeed;
        }

        if (IsDigging)
            speed = 0f;

    }

    void OnCutting()
    {
        if(Input.GetMouseButtonDown(0) && !IsCutting)
        {
            IsCutting = true;
        }
        
        if(Input.GetMouseButtonUp(0) && IsCutting)
        {
            IsCutting = false;
            speed = initialSpeed;
        }

        if(IsCutting)
            speed = 0f;
    }
    #endregion
}
