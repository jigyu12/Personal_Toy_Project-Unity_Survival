using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform firePosition;
    
    public float fireDistance = 50f;
    
    private LineRenderer lineRenderer;
    
    public ParticleSystem muzzleEffect;
    
    public float lastFireTime;
    
    public float fireRate = 0.02f;

    public float damage = 20f;
    
    private AudioSource audio;
    public AudioClip clip;
    
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
        
        audio = GetComponent<AudioSource>();
    }
    
    public void Fire()
    {
        if(lastFireTime + fireRate < Time.time)
        {
            lastFireTime = Time.time;
            var endPos = Vector3.zero;
            
            Ray ray = new Ray(firePosition.position, firePosition.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, fireDistance))
            {
                endPos = hit.point;

                var damagable = hit.collider.GetComponent<Monster>();
                if (damagable != null)
                {
                    damagable.OnDamage(damage, hit.point, hit.normal);
                }
            }
            else
            {
                endPos = firePosition.position + firePosition.forward * fireDistance;
            }
            
            StartCoroutine(ShotEffect(endPos));
        }
    }
    
    private IEnumerator ShotEffect(Vector3 hitPoint)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePosition.position);
        lineRenderer.SetPosition(1, hitPoint);
        
        audio.PlayOneShot(clip);
        muzzleEffect.Play();

        yield return new WaitForSeconds(0.003f);

        lineRenderer.enabled = false;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(firePosition.position, firePosition.position + firePosition.forward * fireDistance);
    }
}