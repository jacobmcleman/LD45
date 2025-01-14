﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Shotgun : MonoBehaviour, IWeapon
{
    public GameObject projectilePrefab;

    public int maxAmmo = 6;
    public int curAmmo;

    public int projectilesPerShot = 12;

    public float shotCooldownTime = 0.2f;
    private bool reloading;
    public float reloadTime = 2.0f;
    private float cooldownClock;

    /*
    * Width in degrees of the cone of bullet spread
    */
    public float spreadAngle = 10;

    private void Awake() 
    {
        curAmmo = maxAmmo;

        cooldownClock = 0;
        reloading = false;
    }

    public string GetDisplayName()
    {
        return "Shotgun";
    }

    public string GetInternalName()
    {
        return "Shotgun";
    }

    public float GetProjectileSpeed()
    {
        return projectilePrefab.GetComponent<Projectile>().speed;
    }

    public float GetCurrentAmmoRatio()
    {
        return (float)curAmmo / maxAmmo;
    }

    public float GetCurrentAmmo()
    {
        return curAmmo;
    }

    public float GetMaxAmmo()
    {
        return maxAmmo;
    }

    public float GetReloadProgress()
    {
        if (reloading)
        {
            return cooldownClock/reloadTime;
        }
        else
        {
            return 1;
        }
    }

    public bool PrimaryFire(WeaponWielder firer, bool tryAuto)
    {
        if (!tryAuto && cooldownClock >= shotCooldownTime && !reloading)
        {
            if(curAmmo == 0) 
            {
                Reload(firer);
                return false;
            }

            float spreadLinearMax = Mathf.Sin(Mathf.Deg2Rad * (spreadAngle / 2));

            for(int i = 0; i < projectilesPerShot; ++i)
            {
                float spreadLinear = Random.Range(0, spreadLinearMax);
                Vector3 fireVec = Quaternion.AngleAxis(Random.Range(0, 360), transform.forward) * (transform.forward + (transform.up * spreadLinear));

                GameObject projectile = GameObject.Instantiate(projectilePrefab);
                Projectile proj = projectile.GetComponent<Projectile>();
                if(proj == null) Debug.LogError("Tried to shoot not a projectile");

                projectile.transform.position = transform.position + transform.forward;
                proj.direction = fireVec;
            }

            curAmmo--;

            cooldownClock = 0;
            
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Reload(WeaponWielder firer)
    {
        if(curAmmo == maxAmmo || reloading) return false;

        reloading = true;
        cooldownClock = 0;

        Debug.Log("Reloading...");
        curAmmo = maxAmmo;
        return true;
    }

    private void Update()
    {
        if(cooldownClock < shotCooldownTime || (reloading && cooldownClock < reloadTime))
        {
            cooldownClock += Time.deltaTime;
        }
        else if (reloading && cooldownClock >= reloadTime)
        {
            reloading = false;
        }
    }
}
