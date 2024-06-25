using RPG.Attributes;
using RPG.Inventories;
using RPG.Stats;
using System.Collections;
using UnityEngine;
using RPG.Skills;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

namespace Control
{
    public class PlayerController : MonoBehaviour, IModifierProvider
    {
        #region private variables
        Animator animator; // Kiểm soát các animation
        BaseStats baseStats; // Chứa các thông số cơ bản của nhân vật
        ActionStore actionStore; // Điều khiển việc sử dụng các vật phẩm
        SkillController skillController; // Điều khiển sử dụng kĩ năng
        Rigidbody rb; // Component quản lý vật lý của nhân vật
        Slide slide; // Component gắn với vũ khí để xác định mục tiêu bị tấn công
        float dmg; // Sát thương của nhân vật
        bool attackMode = false; // Chế độ tấn công
        private bool canAttack = true; // Có thể tấn công
        List<Modifier> environmentModifier; // Danh sách các modifier từ môi trường ảnh hưởng đến nhân vật
        bool canDash = true; // Có thể Dash
        bool isDashing = false; // Đang Dash
        Vignette vignette; // Hiệu ứng khi nhân vật bị tấn công
        #endregion

        #region serialized fields
        [SerializeField] float dashSpeed = 10f; // Tốc độ khi dash
        [SerializeField] float dashTime = 0.5f; // Thời gian dash

        [SerializeField] private CinemachineVirtualCamera battleCamera; // Camera trong chế độ tấn công
        [SerializeField] private CinemachineFreeLook normalCamera; // Camera trong chế độ khám phá
        [SerializeField] private Volume volume; // Volume chứa các hiệu ứng 
        #endregion

        public bool CanAttack { get => canAttack; set => canAttack = value; }        
        public void UpdateModifier(List<Modifier> modifiers)
        {
            environmentModifier.Clear();
            foreach (var modifier in modifiers)
            {
                environmentModifier.Add(modifier);
            }
        }

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
            if (Input.GetKeyDown(KeyCode.Z))
                GetComponent<Health>().TakeDamage(gameObject, 10000);
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                attackMode = !attackMode;
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