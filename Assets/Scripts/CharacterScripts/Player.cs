using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    public static Player instance;


    [Header("Settings")]

    [Header("Data")] 
    [SerializeField] float speed;

    Animator animator;
    Shooter shooter;
    CharacterMover characterMover;
    Health Hp;

    private PlayerInputActions controls;
    private Vector2 move;

    public string ScenePassword;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        shooter = GetComponent<Shooter>();
        characterMover = GetComponent<CharacterMover>();
        Hp = GetComponent<Health>();
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        controls = new PlayerInputActions();

        controls.GamePlay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.GamePlay.Move.canceled += ctx => move = Vector2.zero;

        controls.GamePlay.Shoot.started += ctx => Aim();

    }

    void OnEnable()
    {
        controls.GamePlay.Enable();
    }

    void Ondisable()
    {
        controls.GamePlay.Disable();
    }

    void Update()
    {
        Move();
        if (Input.GetMouseButtonDown(0))
            AimByOldInput();

        if (Hp.CurrentHealth <= 0)
        {
            GameManager.PlayerDied();
        }
    }

    void Move()
    {

        // face towards
        if (move.x > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (move.x < 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }

        characterMover.Move(move.normalized * speed);

        // animate
        animator.SetBool("isMoving", move != Vector2.zero);
    }

    void Aim()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorldPos.z = 0;
        Vector3 direction = mouseWorldPos - transform.position;

        shooter.Shoot(direction);

    }

    void AimByOldInput()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        Vector3 direction = mouseWorldPos - transform.position;

        shooter.Shoot(direction);

    }



}
