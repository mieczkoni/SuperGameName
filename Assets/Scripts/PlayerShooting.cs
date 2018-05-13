using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    LayerMask layerMesh;
    public GameObject bullet, granade;
    private GameObject player, text, reloadingText;
    public string gunType;

    List<float> pistolValues, minigunValues;
    private float pistolFrequencyTimer = 0.0f, minigunFrequencyTimer = 0.0f;
    private float pistolFrequency, minigunFrequency;
    private int pistolBullets = 0, minigunBullets = 0;
    private bool pistolReloading = false, minigunReloading = false;
    private float pistolReloadingTime, minigunReloadingTime;
    private float pistolReloadingTimer = 0.0f, minigunReloadingTimer = 0.0f;


    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        text = GameObject.Find("BulletCount");
        reloadingText = GameObject.Find("ReloadingText");
        reloadingText.SetActive(true);
        pistolValues = player.GetComponent<PlayerValues>().GetPistolValues();
        minigunValues = player.GetComponent<PlayerValues>().GetMinigunValues();

        pistolFrequency = pistolValues[3];
        pistolBullets = (int)pistolValues[4];
        pistolReloadingTime = pistolValues[5];

        minigunFrequency = minigunValues[3];
        minigunBullets = (int)minigunValues[4];
        minigunReloadingTime = minigunValues[5];

        SetBulletText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (gunType == "Pistol" && pistolBullets > 0)
            {
                text.GetComponent<TextEdit>().changeText(pistolBullets.ToString());
                pistolFrequencyTimer += Time.deltaTime;
                if (pistolFrequencyTimer > pistolFrequency)
                {
                    NewShot();                    
                    pistolFrequencyTimer = 0.0f;
                }
            }
            else if (pistolBullets == 0 && !pistolReloading)
            {
                pistolReloading = true;
            }
            else if (gunType == "Granade")
            {
                ThrowGranade();
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (gunType == "Minigun" && minigunBullets > 0)
            {
                text.GetComponent<TextEdit>().changeText(minigunBullets.ToString());
                minigunFrequencyTimer += Time.deltaTime;
                if (minigunFrequencyTimer > minigunFrequency)
                {
                    NewShot();
                    minigunFrequencyTimer = 0.0f;
                }
            }
            else if (minigunBullets == 0)
            {
                minigunReloading = true;
                text.GetComponent<TextEdit>().changeText("0");
                reloadingText.GetComponent<TextEdit>().changeText("RELOADING");
                minigunReloadingTimer += Time.deltaTime;
                if (minigunReloadingTimer > minigunReloadingTime)
                {
                    reloadingText.GetComponent<TextEdit>().changeText("");
                    minigunBullets = (int)minigunValues[4];
                    text.GetComponent<TextEdit>().changeText(minigunBullets.ToString());
                    minigunReloadingTimer = 0.0f;
                    minigunReloading = false;
                }
            }
        }
        else if (pistolReloading)
        {
            text.GetComponent<TextEdit>().changeText("0");
            pistolReloading = true;
            reloadingText.GetComponent<TextEdit>().changeText("RELOADING");
            pistolReloadingTimer += Time.deltaTime;
            if (pistolReloadingTimer > pistolReloadingTime)
            {
                reloadingText.GetComponent<TextEdit>().changeText("");
                pistolBullets = (int)pistolValues[4];
                text.GetComponent<TextEdit>().changeText(pistolBullets.ToString());
                pistolReloading = false;
                pistolReloadingTimer = 0.0f;
            }
        } else if (minigunReloading)
        {
            minigunReloadingTimer += Time.deltaTime;
            if (minigunReloadingTimer > minigunReloadingTime)
            {
                reloadingText.GetComponent<TextEdit>().changeText("");
                minigunBullets = (int)minigunValues[4];
                text.GetComponent<TextEdit>().changeText(minigunBullets.ToString());
                minigunReloadingTimer = 0.0f;
                minigunReloading = false;
            }
        }
    }

    private void NewShot()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //print("I'm looking at " + hit.transform.name);
        }
        Vector3 v3_Dir = hit.point - transform.position;

        if (hit.point.z > player.transform.position.z)
        {
            //print("Poziszon: " + transform.position);
            GameObject newBullet = Instantiate(bullet, new Vector3(transform.position.x, 0.5f, transform.position.z), Quaternion.identity);
            float angle = AngleInDeg(newBullet.transform.forward, v3_Dir);
            newBullet.transform.Rotate(0, angle, 0);

            if (gunType == "Pistol")
            {
                newBullet.GetComponent<Shot>().setValues(pistolValues[0], pistolValues[1], pistolValues[2]);
                pistolBullets -= 1;
            }
            else if (gunType == "Minigun")
            {
                newBullet.GetComponent<Shot>().setValues(minigunValues[0], minigunValues[1], minigunValues[2]);
                minigunBullets -= 1;
            }
        }
    }
    
    private void ThrowGranade()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //print("I'm looking at " + hit.transform.name);
        }
        Vector3 v3_Dir = hit.point - transform.position;
        GameObject newGranade = Instantiate(granade, new Vector3(transform.position.x, 0.5f, transform.position.z), Quaternion.identity);
        newGranade.GetComponent<Throw>().destination = hit.point;
        newGranade.GetComponent<Throw>().originalPlayerPosition = transform.position;
        float angle = AngleInDeg(newGranade.transform.forward, v3_Dir);
        newGranade.transform.Rotate(0, angle, 0);
    }

    //This returns the angle in degress
    public static float AngleInDeg(Vector3 vec1, Vector3 vec2)
    {
        return Mathf.Atan2(vec2.x - vec1.x, vec2.z - vec1.z) * 180 / Mathf.PI;
    }

    public void SetBulletText()
    {
        if (gunType == "Pistol")
        {
            text.GetComponent<TextEdit>().changeText(pistolBullets.ToString());
        } else if (gunType == "Minigun")
        {
            text.GetComponent<TextEdit>().changeText(minigunBullets.ToString());
        }
        
    }

    public List<int> GetBulletsCount()
    {
        List<int> bulletsCount = new List<int>();
        bulletsCount.Add(pistolBullets);
        bulletsCount.Add(minigunBullets);
        return bulletsCount;
    }
}


