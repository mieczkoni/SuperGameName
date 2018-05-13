using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScripts : MonoBehaviour {

    private GameObject weaponEnd;

    public void SetPistol()
    {
        weaponEnd = GameObject.Find("WeaponEnd");
        weaponEnd.GetComponent<PlayerShooting>().gunType = "Pistol";
        weaponEnd.GetComponent<PlayerShooting>().SetBulletText();
    }

    public void SetMinigun()
    {
        weaponEnd = GameObject.Find("WeaponEnd");
        weaponEnd.GetComponent<PlayerShooting>().gunType = "Minigun";
        weaponEnd.GetComponent<PlayerShooting>().SetBulletText();
    }
}
