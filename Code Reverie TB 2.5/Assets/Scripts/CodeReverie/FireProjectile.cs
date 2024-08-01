using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeReverie
{
    public class FireProjectile : MonoBehaviour
    {

        public GameObject firePoint;
        public Projectile projectile;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                FireProjectileShot();
            }
        }


        public void FireProjectileShot()
        {
            Projectile _projectile = Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation);
            
            _projectile.gameObject.SetActive(true);
            
        }

    }
}
