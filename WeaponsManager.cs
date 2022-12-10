using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    public int minDamage,maxDamage;
    public Camera playerCamera;
    public float range = 200f;
    public ParticleSystem gunPlay;
    public GameObject impactEffect;
    
  
 

    private EnemyManager enemyManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    void Update()
    {
       
        if (Input.GetButtonDown("Fire1"))
        {
          
            Fire();
            gunPlay.Play();
           
        }
    }
    void Fire()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            
            Debug.Log(hit.transform.name);
            enemyManager=hit.transform.GetComponent<EnemyManager>();
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            if (enemyManager != null)
            {
                enemyManager.EnemyTakeDamage(Random.Range(minDamage, maxDamage));
            }
            
        }
    

    }
}
