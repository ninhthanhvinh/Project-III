using RPG.Attributes;
using RPG.Inventories;
using RPG.Saving;
using RPG.Stats;
using System;
using System.Collections;
using UnityEngine;
using RPG.Skills;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using Unity.VisualScripting;

namespace Control
{
    public class PlayerController : MonoBehaviour, IModifierProvider
    {
        Animator animator;
        BaseStats baseStats;
        ActionStore actionStore;
        SkillController skillController;
        Slide slide;
        float dmg;
        bool attackMode = false;

        List<Modifier> environmentModifier;

        private bool canAttack = true;
        public bool CanAttack { get => canAttack; set => canAttack = value; }

        public void UpdateModifier(List<Modifier> modifiers)
        {
            environmentModifier.Clear();
            foreach (var modifier in modifiers)
            {
                environmentModifier.Add(modifier);
            }
        }


        bool isDashing = false;
        Rigidbody rb;
        bool canDash = true;
        [SerializeField] float dashSpeed = 10f;
        [SerializeField] float dashTime = 0.5f;

        [SerializeField] private CinemachineVirtualCamera battleCamera;
        [SerializeField] private CinemachineFreeLook normalCamera;
        [SerializeField] private Volume volume;
        Vignette vignette;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            baseStats = GetComponent<BaseStats>();
            slide = GetComponentInChildren<Slide>();
            actionStore = GetComponent<ActionStore>();
            skillController = GetComponent<SkillController>();
            environmentModifier = new List<Modifier>();
            volume.profile.TryGet(out vignette);

        }

        private void Start()
        {
            EnvironmentRunner.instance.OnWeatherChange.AddListener(UpdateModifier);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                attackMode = !attackMode;
                Debug.Log(attackMode);
                if (attackMode)
                {
                    battleCamera.Priority = 20;
                    normalCamera.Priority = 10;
                }
                else
                {
                    battleCamera.Priority = 10;
                    normalCamera.Priority = 20;
                }
            }    

            if (!attackMode)
            {
                return;
            }
            if (canAttack && Input.GetMouseButtonDown(0))
            {
                dmg = baseStats.GetStats(Stat.Damage);
                NormalAttack(dmg);
            }

            if (Input.GetKeyDown(KeyCode.R) && canDash && !isDashing)
            {
                DoDashing();
            }

            var firstSlot = KeyCode.Alpha1;
            var lastSlot = KeyCode.Alpha3;
            for (var i = firstSlot; i <= lastSlot; i++)
            {
                if (Input.GetKeyDown(i))
                {
                    actionStore.Use(i - firstSlot, gameObject);
                }
            }

            var firstSkill = KeyCode.F;
            var lastSkill = KeyCode.H;
            for (var i = firstSkill; i <= lastSkill; i++)
            {
                if (Input.GetKeyDown(i))
                {
                    skillController.Use(i - firstSkill, this);
                }
            }
        }

        private void ChangeCameraMode(bool attackMode)
        {
            attackMode = !attackMode;
            if (attackMode)
            {
                battleCamera.Priority = 20;
                normalCamera.Priority = 10;
            }
            else
            {
                Debug.Log(Vector3.Distance(Camera.main.transform.position, transform.position));
                battleCamera.Priority = 10;
                normalCamera.Priority = 20;
            }
        }

        private void DoDashing()
        {
            animator.SetTrigger("Rolling");
            StartCoroutine(Dash());    
        }

        private void NormalAttack(float dmg)
        {
            slide.SetTarget(null);
            SoundManager.instance.PlaySound("slash", transform);
            animator.SetTrigger("Attack");
        }

        public void Hit()
        {
            if (slide.GetTarget() == null) return;
            slide.GetTarget().GetComponent<Health>().TakeDamage(gameObject, dmg);
            slide.SetTarget(null);
        }

        private IEnumerator Dash()
        {
            isDashing = true;
            canDash = false;

            Vector3 targetDirection = /*Quaternion.Euler(0f, _mainCamera.transform.eulerAngles.y, 0f) */ transform.forward;
            rb.AddForce(targetDirection.normalized * dashSpeed, ForceMode.VelocityChange);
            yield return new WaitForSeconds(dashTime);

            isDashing = false;
            Invoke(nameof(CooldownCanDash), 1f);
        }

        private void CooldownCanDash()
        {
            canDash = true;
        }


        public void PlayAnim(TypeOfSkill type)
        {
            switch (type)
            {
                case TypeOfSkill.StraightSlash:
                    animator.Play("Slash");
                    break;
                case TypeOfSkill.Casting:
                    animator.Play("casting");
                    break;
                case TypeOfSkill.RoundSlash:
                    animator.Play("round");
                    break;
                default:
                    break;
            }
        }

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            if (environmentModifier == null) yield break;
            foreach (var modifier in environmentModifier)
            {
                if (modifier.stat == stat)
                {
                    yield return modifier.value;
                }
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if (environmentModifier == null) yield break;
            foreach (var modifier in environmentModifier)
            {
                if (modifier.stat == stat)
                {
                    yield return modifier.value;
                }   
            }
        }
        public IEnumerator Damaged()
        {
            Debug.Log(vignette);
            vignette.color.value = Color.red;
            vignette.intensity.value = 0.5f;
            yield return new WaitForSeconds(0.2f);
            vignette.intensity.value = 0.26f;
            vignette.color.value = Color.black;
        }
    }

    public enum TypeOfSkill
    {
        StraightSlash,
        Casting,
        RoundSlash,
    }

    
}