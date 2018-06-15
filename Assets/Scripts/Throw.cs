using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {

    public float movementSpeed, range, damage;
    public Vector3 destination, originalPosition;
    private Vector3 forward;
    private float originalDistance, actualDistance, tempFloat;
    private PlayerValues playerVal;

    private float halfDistance, halfZpoint, angle;

    private bool topReached = false;

    private void Start()
    {
        playerVal = GameObject.Find("Player").GetComponent<PlayerValues>();
        originalPosition = transform.position;
        destination = new Vector3(Random.Range(-2.0f, 2.0f), transform.position.y, transform.position.z + 20);
        angle = AngleInDeg(originalPosition, destination);
        halfDistance = calculateDistance(originalPosition, destination) / 2;
        halfZpoint = originalPosition.z + halfDistance;
        tempFloat = 0.1f;
        transform.Rotate(0, angle/3, 0);
    }

    private void Update()
    {
        if ((transform.position.z - originalPosition.z) > 0.0f)
        {
            if (transform.position.z >= halfZpoint && topReached == false)
            {
                topReached = true;
            }

            if (transform.position.y <= 0.01f)
            {
                ExplosionDamage(new Vector3(transform.position.x, 1, transform.position.z), playerVal.granadeRange);
                Destroy(this.gameObject);
            }

            if (!topReached)
            {
                tempFloat = transform.position.y + (0.2f / (transform.position.z - originalPosition.z));
            } else if (topReached)
            {
                tempFloat = transform.position.y - (0.2f / (halfDistance - (transform.position.z - halfZpoint)));
            }
        }
        transform.position = new Vector3(transform.position.x, tempFloat, transform.position.z - 0.8f) + transform.forward;
    }

    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].CompareTag("Enemy"))
            {
                hitColliders[i].GetComponent<WeakEnemyAI>().decreaseHealth(playerVal.granadeDamage);
            }            
            i++;
        }
    }

    private float calculateDistance(Vector3 vector1, Vector3 vector2)
    {
        return Mathf.Sqrt(Mathf.Pow(vector1.x - vector2.x, 2) + Mathf.Pow(vector1.z - vector2.z, 2));
    }

    public static float AngleInDeg(Vector3 vec1, Vector3 vec2)
    {
        return Mathf.Atan2(vec2.x - vec1.x, vec2.z - vec1.z) * 180 / Mathf.PI;
    }
}
