using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    [SerializeField] private AudioSource bulletSound;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
        }

        void Shoot()
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bulletSound.Play();

        }
    }
}
