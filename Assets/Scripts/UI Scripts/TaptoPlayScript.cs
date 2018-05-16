using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TaptoPlayScript : MonoBehaviour {

    private Text myText;
    private float lerpStrength = 1f;
    private bool increasing = false;
    private GameObject shopButton, shopContent;

    private void Start()
    {
        SetShopValues();
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerWalking>().playerSpeed = 0;
        GameObject.Find("WeaponEnd").GetComponent<PlayerShooting>().enabled = false;
        shopContent = GameObject.Find("ShopContent");
        shopContent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!increasing)
        {
            lerpStrength -= 0.02f;
            if (lerpStrength <= 0.1f)
                increasing = true;
        }
        else
        {
            lerpStrength += 0.02f;
            if (lerpStrength >= 0.9f)
                increasing = false;
        }
        if (GameObject.Find("TapToPlay") != null)
        {
            myText = GameObject.Find("TapToPlay").GetComponent<Text>();
            Color lerpColor = myText.color;
            lerpColor.a = Mathf.Lerp(1, 0, lerpStrength);
            myText.color = lerpColor;
        } 
        
    }

    public void OnClick()
    {
        GameObject.Find("ContentToHideInGame").SetActive(false);
        GameObject.Find("Player").GetComponent<PlayerWalking>().playerSpeed = 0.05f;
        GameObject.Find("WeaponEnd").GetComponent<PlayerShooting>().enabled = true;
    }

    public void ShopOnClick()
    {
        shopContent.SetActive(true);
    }

    public void ShopExit()
    {
        shopContent.SetActive(false);
    }

    public void ShopButtonMouseEntered()
    {
        shopButton = GameObject.Find("ShopButton");
        shopButton.transform.position = new Vector3(shopButton.transform.position.x, shopButton.transform.position.y + 30, shopButton.transform.position.z);
    }

    public void ShopButtonMouseExited()
    {
        shopButton = GameObject.Find("ShopButton");
        shopButton.transform.position = new Vector3(shopButton.transform.position.x, shopButton.transform.position.y - 30, shopButton.transform.position.z);
    }

    private void SetShopValues()
    {
        GameObject[] goArray = SceneManager.GetSceneByBuildIndex(0).GetRootGameObjects();
    }

}
