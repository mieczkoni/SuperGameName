using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsController : MonoBehaviour {

    private GameObject weaponChoose, weaponsBckg, hideContent, weaponEnd;
    public string weaponName;

    public void Start()
    {
        weaponChoose = GameObject.Find("ChooseWeapon");
        weaponsBckg = GameObject.Find("WeaponsBackground");
        hideContent = GameObject.Find("ContentToHideInGame");
        weaponEnd = GameObject.Find("WeaponEnd");
    }

    public void OnButtonClick()
    {
        if (hideContent.activeSelf) {
            weaponChoose.SetActive(true);
            weaponsBckg.GetComponent<UIScripts>().weaponID = transform.name;
        } else
        {
            if (weaponName == "Pistol")
            {
                weaponEnd = GameObject.Find("WeaponEnd");
                weaponEnd.GetComponent<PlayerShooting>().gunType = "Pistol";
                weaponEnd.GetComponent<PlayerShooting>().SetBulletText();
            } else if (weaponName == "Minigun")
            {
                weaponEnd = GameObject.Find("WeaponEnd");
                weaponEnd.GetComponent<PlayerShooting>().gunType = "Minigun";
                weaponEnd.GetComponent<PlayerShooting>().SetBulletText();
            }
        }
    }

    
}
