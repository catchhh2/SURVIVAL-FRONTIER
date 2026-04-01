using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float shootRate = 2f;
    private float timer = 0;
    private Light light;
    private ParticleSystem particleSystem;
    private LineRenderer lineRenderer;
    private AudioSource audio;
    public float attack = 30f;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponentInChildren<Light>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1/shootRate)
        {
            timer -= 1/shootRate;
            Shoot();
        }
    }
    void Shoot()
    {
        light.enabled = true;
        particleSystem.Play();
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            lineRenderer.SetPosition(1, hitInfo.point);
            // ÅÐ¶ÏÉä»÷ÊÇ·ñÅö×²µ½µÐÈË
            if (hitInfo.collider.tag == "Enemy")
            {
                hitInfo.collider.GetComponent<EnemyHealth>().TakeDamage(attack,hitInfo.point);
            }
        }
        else 
        {
            lineRenderer.SetPosition(1, transform.position + transform.forward * 100);
        }

        audio.Play();
        
              
        Invoke("ClearEffects", 0.05f);
    }
    void ClearEffects()
    {
        light.enabled = false;
        lineRenderer.enabled = false;
    }
}
