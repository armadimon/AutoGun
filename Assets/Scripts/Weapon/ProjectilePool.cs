using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform projectileParent;
    public int poolSize = 50;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(projectilePrefab, projectileParent);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetProjectile(Transform firePoint, Vector3 bulletRotation)
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            obj.transform.SetParent(projectileParent, false);
            obj.transform.position = firePoint.position;
            Quaternion currentRotation = obj.transform.rotation;
            obj.transform.rotation = Quaternion.Euler(currentRotation.eulerAngles.x, bulletRotation.y, currentRotation.eulerAngles.z);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(projectilePrefab, projectileParent);
            obj.transform.localPosition = Vector3.zero;
            return obj;
        }
    }

    public void ReturnProjectile(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.localPosition = Vector3.zero;
        pool.Enqueue(obj);
    }
}
