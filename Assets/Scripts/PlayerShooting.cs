using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    LayerMask layerMesh;
    public GameObject bullet, granade;
    private GameObject player;
    public string gunType = "Pistol";

    List<float> pistolValues, minigunValues;
    private float pistolFrequencyTimer = 0.0f, minigunFrequencyTimer = 0.0f;
    private float pistolFrequency, minigunFrequency;
    private bool mouseUp = false;

    private Vector3 referenceVector;
    private PlayerValues playerValues;


    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        playerValues = player.GetComponent<PlayerValues>();
        pistolValues = playerValues.GetPistolValues();
        minigunValues = playerValues.GetMinigunValues();

        pistolFrequency = 1.0f / pistolValues[4];
        minigunFrequency = 1.0f / minigunValues[4];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (gunType == "Pistol")
            {
                pistolFrequencyTimer += Time.deltaTime;
                if (pistolFrequencyTimer > pistolFrequency)
                {
                    NewShot();
                    pistolFrequencyTimer = 0.0f;
                }
            }
            if (gunType == "Minigun")
            {
                minigunFrequencyTimer += Time.deltaTime;
                if (minigunFrequencyTimer > minigunFrequency)
                {
                    NewShot();
                    minigunFrequencyTimer = 0.0f;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0) && mouseUp == true)
        {
            mouseUp = false;
        }
    }

    private void NewShot()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //print("I'm looking at " + hit.transform.name);
            //print("HIT: " + hit.point);
        }

        if (mouseUp == false)
        {
            referenceVector = new Vector3(hit.point.x, hit.point.y, player.transform.position.z - 2);
            mouseUp = true;
        }
        referenceVector = new Vector3(referenceVector.x, referenceVector.y, player.transform.position.z - 4);
        Vector3 v3_Dir = hit.point - referenceVector;

        if (hit.point.z > player.transform.position.z)
        {
            GameObject newBullet = Instantiate(bullet, new Vector3(transform.position.x, 0.5f, transform.position.z), Quaternion.identity);
            float angle = AngleInDeg(newBullet.transform.forward, v3_Dir);
            newBullet.transform.Rotate(0, angle, 0);

            if (gunType == "Pistol")
            {
                pistolValues = playerValues.GetPistolValues();
                newBullet.GetComponent<Shot>().setValues(pistolValues[0], pistolValues[1], pistolValues[2]);
            }
            else if (gunType == "Minigun")
            {
                minigunValues = playerValues.GetMinigunValues();
                newBullet.GetComponent<Shot>().setValues(minigunValues[0], minigunValues[1], minigunValues[2]);
            }
        }
    }

    public static float AngleInDeg(Vector3 vec1, Vector3 vec2)
    {
        return Mathf.Atan2(vec2.x - vec1.x, vec2.z - vec1.z) * 180 / Mathf.PI;
    }
}