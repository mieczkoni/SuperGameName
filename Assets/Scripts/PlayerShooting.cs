using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    LayerMask layerMesh;
    public GameObject bullet, granade;
    private GameObject player;
    public string gunType;

    List<float> pistolValues, minigunValues;
    private float pistolFrequencyTimer = 0.0f, minigunFrequencyTimer = 0.0f;
    private float pistolFrequency, minigunFrequency;
    private bool mouseUp = false;

    private Vector3 referenceVector;


    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        pistolValues = player.GetComponent<PlayerValues>().GetPistolValues();
        minigunValues = player.GetComponent<PlayerValues>().GetMinigunValues();

        pistolFrequency = 1.0f / pistolValues[4];

        minigunFrequency = 1.0f / minigunValues[4];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (gunType == "Granade")
            {
                ThrowGranade();
            }
        }
        else if (Input.GetMouseButton(0))
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
        //print("REF: " + referenceVector);
        //Vector3 v3_Dir = hit.point - transform.position;

        if (hit.point.z > player.transform.position.z)
        {
            //print("Poziszon: " + transform.position);
            GameObject newBullet = Instantiate(bullet, new Vector3(transform.position.x, 0.5f, transform.position.z), Quaternion.identity);
            float angle = AngleInDeg(newBullet.transform.forward, v3_Dir);
            newBullet.transform.Rotate(0, angle, 0);

            if (gunType == "Pistol")
            {
                pistolValues = player.GetComponent<PlayerValues>().GetPistolValues();
                newBullet.GetComponent<Shot>().setValues(pistolValues[0], pistolValues[1], pistolValues[2]);
            }
            else if (gunType == "Minigun")
            {
                minigunValues = player.GetComponent<PlayerValues>().GetMinigunValues();
                newBullet.GetComponent<Shot>().setValues(minigunValues[0], minigunValues[1], minigunValues[2]);
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
}