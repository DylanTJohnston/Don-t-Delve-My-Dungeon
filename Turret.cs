using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private Transform target;

    [Header("Attributes")]
    public float range = 15f;
        public float fireRate = 1f;
    private float fireCountdown = 0f;
    
    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public Transform baseToRotate;
    public float turnSpeed = 10f;

    public GameObject orbPrefab;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
       InvokeRepeating ("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget ()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        //Looking For Nearest Enemy
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range) {
            target = nearestEnemy.transform;
        } else {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        //Target Lock On
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        Vector3 baseRotation = Quaternion.Lerp(baseToRotate.rotation, lookRotation, (Time.deltaTime * turnSpeed) * 0.25f).eulerAngles;
        partToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f);
        baseToRotate.rotation = Quaternion.Euler (0f, baseRotation.y, 0f);
       
        if (fireCountdown <=0f)
        {
            Shoot();
            fireCountdown = 1f/fireRate;
        }

        fireCountdown -= Time.deltaTime;

    }

    void Shoot ()
    {
        GameObject orbGO = (GameObject)Instantiate(orbPrefab, firePoint.position, firePoint.rotation);
        Orb orb = orbGO.GetComponent<Orb>();

        if (orb != null)
        {
            orb.Seek(target);
        }
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
