using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class Footstep : MonoBehaviour
{
    public ParticleSystem system;
    public Material footprintMaterial;
    private Vector3 lastEmit;

    [SerializeField]
    private float delta = 1;
    [SerializeField]
    private float gap = 0.5f;
    private int dir = 1;
    private void Start()
    {
        lastEmit = transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(lastEmit, transform.position) > delta)
        {
            Gizmos.color = Color.green;
            var pos = transform.position + (transform.right * gap * dir);
            dir *= -1;
            ParticleSystem.EmitParams ep = new ParticleSystem.EmitParams
            {
                position = pos,
                rotation = transform.rotation.eulerAngles.y
            };
            system.Emit(ep, 1);
            lastEmit = transform.position;
            Debug.Log("Emitting");
        }
    }
}
