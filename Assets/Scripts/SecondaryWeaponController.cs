using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondaryWeaponController : MonoBehaviour {

    public string weaponType;

    public GameObject granadePrefab;

    public Image secondaryWeaponFill;
    private bool isFilled = true;
    private float timer = 0.0f;

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        secondaryWeaponFill.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (!isFilled)
        {
            timer += Time.deltaTime;
            UpdateImageFill(timer);
        }
	}

    private void UpdateImageFill(float timer)
    {
        if (1 - (timer / 3) <= 0.0f)
        {
            isFilled = true;
            secondaryWeaponFill.enabled = false;
        }
        secondaryWeaponFill.fillAmount = 1 - (timer / 3);
    }

    public void ThrowSomething()
    {
        if (weaponType == "Granade")
        {
            ThrowGranade();
        }
    }

    private void ThrowGranade()
    {
        Instantiate(granadePrefab, player.transform.position, Quaternion.identity);
        timer = 0.0f;
        secondaryWeaponFill.enabled = true;
        isFilled = false;
    }
}
