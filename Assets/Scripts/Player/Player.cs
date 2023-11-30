using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 300.0f;
    [SerializeField]
    private float runSpeed = 650.0f;

    [Header("Stats")]
    public float attack;
    public float health;

    private Rigidbody2D rigidbody;

    private PlayerItems playerItems;

    private float initialSpeed;

    public bool CanMove { get; set; } = true;

    private bool _isRunning;
    public bool IsRunning { get => _isRunning; set => _isRunning = value; }

    private bool _isRolling;
    public bool IsRolling { get => _isRolling; set => _isRolling = value; }

    private bool _isCutting;
    public bool IsCutting { get => _isCutting; set => _isCutting = value; }

    private bool _isDigging;
    public bool IsDigging { get => _isDigging; set => _isDigging = value; }

    private bool _isWatering;
    public bool IsWatering { get => _isWatering; set => _isWatering = value; }

    private Vector2 _direction;
    public Vector2 Direction { get => _direction; set => _direction = value; }

    public Weapon weapon = Weapon.Axe;

    public enum Weapon
    {
        Axe = 1,
        Shovel = 2,
        WaterBucket = 3
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerItems = GetComponent<PlayerItems>();
        initialSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
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
                case Weapon.WaterBucket:
                    OnWatering();
                    break;
                default:
                    IsCutting = false;
                    IsDigging = false;
                    IsWatering = false;
                    break;

            }
        }   
    }

    private void FixedUpdate()
    {
        if (CanMove)
        {
            OnMove();
        }
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
            ClearActions();
            weapon = Weapon.Axe;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ClearActions();
            weapon = Weapon.Shovel;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ClearActions();
            weapon = Weapon.WaterBucket;
        }
    }

    void ClearActions()
    {
        IsDigging = false;
        IsWatering = false;
        IsCutting = false;
    }

    void OnDigging()
    {
        if (Input.GetMouseButtonDown(0) && !IsDigging)
            IsDigging = true;

        if (Input.GetMouseButtonUp(0) && IsDigging)
        {
            IsDigging = false;
            speed = initialSpeed;
        }

        if (IsDigging)
            speed = 0f;

    }

    void OnWatering()
    {
        if (playerItems.TotalWater > 0)
            if (Input.GetMouseButtonDown(0) && !IsWatering)
                IsWatering = true;



        if ((Input.GetMouseButtonUp(0) && IsWatering) || playerItems.TotalWater == 0)
        {
            IsWatering = false;
            speed = initialSpeed;
        }

        if (IsWatering)
        {
            speed = 0f;
            playerItems.TotalWater = Mathf.Clamp(playerItems.TotalWater -= 10 * Time.deltaTime, 0, playerItems.TotalWater);
        }

    }

    void OnCutting()
    {
        if(Input.GetMouseButtonDown(0) && !IsCutting)
            IsCutting = true;
        
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
