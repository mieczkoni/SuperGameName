using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumEnemyAI : MonoBehaviour {

    Transform player;
    UnityEngine.AI.NavMeshAgent nav;
    public GameObject bullet;
    private float timer = 0.0f;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(player.position);
        timer += Time.deltaTime;
        if (timer > 3.0f)
        {
            NewShot();
            timer = 0.0f;
        }
    }

    void NewShot()
    {
        Vector3 v3_Dir = player.position - transform.position;
        GameObject newBullet = Instantiate(bullet, new Vector3(transform.position.x, 0.5f, transform.position.z), Quaternion.identity);
        newBullet.name = "KUPECZKA";
        float angle = AngleInDeg(newBullet.transform.forward, v3_Dir);
        newBullet.transform.Rotate(0, angle, 0);
    }

    public static float AngleInDeg(Vector3 vec1, Vector3 vec2)
    {
        return Mathf.Atan2(vec2.x - vec1.x, vec2.z - vec1.z) * 180 / Mathf.PI;
    }
}
