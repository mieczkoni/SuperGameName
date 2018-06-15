using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsController : MonoBehaviour {

    public string skillName;

    public GameObject shieldPrefab;
    private GameObject shield;

    public bool shieldActive = false, shieldDone = false, shieldInstantiated = false;
    private float skillTimer = 0.0f, fillTimer = 0.0f;

    private PlayerValues playerVal;

    public Image skillFill;

	// Use this for initialization
	void Start () {
        playerVal = GetComponent<PlayerValues>();
        skillFill.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (shieldActive)
        {
            skillTimer += Time.deltaTime;
            if (skillTimer >= playerVal.shieldTime)
            {
                shieldActive = false;
                shieldDone = true;
                Destroy(shield);
                shieldInstantiated = false;
            } else
            {
                RotateShield();
            }
        } else if (!shieldActive && shieldDone)
        {
            fillTimer += Time.deltaTime;
            UpdateImageFill(fillTimer);
        }
	}

    private void RotateShield()
    {
        if (shieldInstantiated == false)
        {
            shield = Instantiate(shieldPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            shield.transform.SetParent(this.transform, false);
            shieldInstantiated = true;
        }
        shield.transform.Rotate(0, 8, 0);
    }

    private void UpdateImageFill(float timer)
    {
        skillFill.enabled = true;
        if (skillFill.fillAmount <= 0.0f)
        {
            shieldDone = false;
            skillFill.enabled = false;
            fillTimer = 0.0f;
        }
        skillFill.fillAmount = 1 - (timer / 3);
    }

    public void UseSkill()
    {
        if (skillName == "Shield")
        {
            UseShield();
        }
    }

    void UseShield()
    {
        skillTimer = 0.0f;
        skillFill.fillAmount = 1.0f;
        shieldActive = true;
    }
}
