using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScripts : MonoBehaviour {

    private GameObject weaponEnd;
    public string weaponID;
    public Sprite pistol, minigun, empty;
    
    public void SetPistol()
    {
        weaponEnd = GameObject.Find("WeaponEnd");
        weaponEnd.GetComponent<PlayerShooting>().gunType = "Pistol";
        //weaponEnd.GetComponent<PlayerShooting>().SetBulletText();
        GameObject.Find(weaponID).GetComponent<Image>().sprite = pistol;
        GameObject.Find(weaponID).GetComponent<WeaponsController>().weaponName = "Pistol";
        GameObject.Find("ChoosePistol").SetActive(false);
        GameObject.Find("ChooseWeapon").SetActive(false);
    }

    public void SetMinigun()
    {
        weaponEnd = GameObject.Find("WeaponEnd");
        weaponEnd.GetComponent<PlayerShooting>().gunType = "Minigun";
        //weaponEnd.GetComponent<PlayerShooting>().SetBulletText();
        GameObject.Find(weaponID).GetComponent<Image>().sprite = minigun;
        GameObject.Find(weaponID).GetComponent<WeaponsController>().weaponName = "Minigun";
        GameObject.Find("ChooseMinigun").SetActive(false);
        GameObject.Find("ChooseWeapon").SetActive(false);
    }

    public void WeaponChooseExit()
    {
        GameObject.Find("ChooseWeapon").SetActive(false);
    }
}
