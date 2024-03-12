using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour, ISaveable
{
    float inputX, inputY;


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
            transform.rotation = Quaternion.Euler(0.0f, _targetRotation, 0.0f);

            SoundManager.instance.PlaySound("run", transform);

            Vector3 moveDir = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            transform.position += moveDir * MoveSpeed * Time.deltaTime;
        }

        animator.SetFloat("Speed", _speed);
    }

    private void FixedUpdate()
    {
       
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
