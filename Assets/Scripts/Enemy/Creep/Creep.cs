
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.Enemy.Creep
{
    //public class Creep : Enemy
    //{
    //    Animator animator;


    //    float attackCD = 0f;

    //    private void Awake()
    //    {
    //        animator = GetComponent<Animator>();
    //        stateMachine = new StateMachine(this);
    //        stateMachine.RegisterState(new Attack());
    //        stateMachine.RegisterState(new Chase());
    //        stateMachine.RegisterState(new Patrol());
    //    }

    //    // Start is called before the first frame update
    //    void Start()
    //    {
    //        stateMachine.ChangeState(StateID.Patrol);
    //    }

    //    private void OnTriggerEnter(Collider other)
    //    {
    //        if (other.gameObject.CompareTag("Player"))
    //        {
    //            target = other.gameObject;                
    //            stateMachine.ChangeState(StateID.Chase);
    //        }
    //    }

        
    //    public override void Attack()
    //    {
    //        if (attackCD > 0)
    //        {
    //            attackCD -= Time.deltaTime;
    //            return;
    //        }
    //        Debug.Log("Attack");
    //        attackCD = 1f;
    //        animator.SetTrigger("Attack");
    //    }

    //    private void OnTriggerExit(Collider other)
    //    {
    //        if (other.gameObject.CompareTag("Player"))
    //        {
    //            target = null;
    //            stateMachine.ChangeState(StateID.Patrol);
    //        }
    //    }

    //    private void Update()
    //    {
    //        stateMachine.Update();
    //    }
    //}    
}

