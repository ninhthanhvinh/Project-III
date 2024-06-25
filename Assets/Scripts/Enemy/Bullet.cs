using RPG.Attributes;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private float damage = 10f;
    private Vector3 direction = Vector3.zero;
    public float Damage { set { damage = value; } }
    public Vector3 Direction { set { direction = value; } }

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.position += direction * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health>().TakeDamage(gameObject, damage);
            Destroy(gameObject);
        }
    }
}
