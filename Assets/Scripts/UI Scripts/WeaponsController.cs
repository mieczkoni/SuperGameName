using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsController : MonoBehaviour {

    private GameObject weaponChoose, weaponsBckg, startCanvas, weaponEnd, gameOverCanvas;
    private CanvasController controller;
    public string weaponName;

    public void Start()
    {
        controller = GameObject.Find("MainCanvas").GetComponent<CanvasController>();
        weaponChoose = GameObject.Find("ChooseWeapon");
        weaponsBckg = GameObject.Find("WeaponsBackground");
        startCanvas = GameObject.Find("StartCanvas");
        gameOverCanvas = GameObject.Find("GameOverCanvas");
        weaponEnd = GameObject.Find("WeaponEnd");
    }

    public void OnButtonClick()
    {
        if (startCanvas.GetComponent<Canvas>().enabled == true|| gameOverCanvas.GetComponent<Canvas>().enabled == true) {
            controller.ShowWeaponChooseCanvas();
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
