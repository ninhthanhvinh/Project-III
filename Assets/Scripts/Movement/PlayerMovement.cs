using Control;
using RPG.Saving;
using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour, ISaveable
{
    float inputX, inputY;

    PlayerController playerController;

    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;

    [Tooltip("Move speed of the character in m/s")]
    public float MoveSpeed = 2.0f;


    private Animator animator;

    private Vector3 targetDirection;
    private float _rotationVelocity;
    private float _speed;
    private float _targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        Vector3 inputDirection = new Vector3(inputX, 0.0f, inputY).normalized;
        
        _speed = Mathf.Lerp(_speed, inputDirection.magnitude, Time.deltaTime * 10.0f);
        
        
        if (inputDirection != Vector3.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
            if (playerController.AttackMode)
                transform.rotation = Quaternion.Euler(0.0f, _targetRotation, 0.0f);
            else
            {
                transform.rotation = Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y, 0f);
            }

            SoundManager.instance.PlaySound("run", transform);

            Vector3 moveDir = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * GetCameraFront();
            MoveSpeed = GetComponent<BaseStats>().GetStats(Stat.Speed);

            transform.position += MoveSpeed * Time.deltaTime * moveDir;
        }
        animator.SetFloat("Speed", _speed);
    }

    public Vector3 GetCameraFront()
    {
        Vector3 fwd = Camera.main.transform.forward.normalized;
        fwd = new Vector3(fwd.x, 0, fwd.z);
        return fwd;
    }


    public object CaptureState()
    {
        return new SerializableVector3(transform.position);
    }

    public void RestoreState(object state)
    {
        SerializableVector3 position = (SerializableVector3)state;
        //GetComponent<NavMeshAgent>().enabled = false;
        transform.position = position.ToVector();
        //GetComponent<NavMeshAgent>().enabled = true;
    }
}
