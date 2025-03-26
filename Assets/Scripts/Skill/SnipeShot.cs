using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnipeShot : MonoBehaviour, ISkill
{
    public string SkillName { get; set; }

    [SerializeField]
    private Sprite _skillIcon;
    public Sprite SkillIcon { get { return _skillIcon; } set { _skillIcon = value; } }

    public float CoolDown { get; set; }

    [SerializeField] private GameObject snipeMarkerPrefab;
    [SerializeField] private float damage = 100f;
    [SerializeField] private float snipeRange = 10f;
    [SerializeField] private Vector3 boxSize = new Vector3(2f, 2f, 10f);

    private GameObject snipeMarkerInstance;

    public void Start()
    {
        Debug.Log(SkillIcon);
    }

    public void Activate()
    {
        StartCoroutine(StartSnipe());
    }

    private IEnumerator StartSnipe()
    {
        GameObject target = FindClosestTarget();
        if (target == null) yield break;

        ShowSnipeMarker(target.transform.position);

        yield return new WaitForSeconds(1f);

        ApplyDamage(target);
    }

    private GameObject FindClosestTarget()
    {

        Transform playerTransform = CharacterManager.Instance.player.transform;
        Vector3 boxCenter = playerTransform.position + playerTransform.forward * (snipeRange / 2);
        Quaternion boxRotation = Quaternion.LookRotation(playerTransform.forward);

        Collider[] hitColliders = Physics.OverlapBox(boxCenter, boxSize / 2, boxRotation, LayerMask.GetMask("Enemy"));

        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider hit in hitColliders)
        {
            float distance = Vector3.Distance(playerTransform.position, hit.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = hit.gameObject;
            }
        }

        return closestEnemy;
    }

    private void ShowSnipeMarker(Vector3 position)
    {
        if (snipeMarkerPrefab != null)
        {
            snipeMarkerInstance = Instantiate(snipeMarkerPrefab, position, Quaternion.identity);
            Destroy(snipeMarkerInstance, 1.1f); // 1초 후 제거
        }
    }

    private void ApplyDamage(GameObject target)
    {
        var enemy = target.GetComponent<BaseController>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }

    // private void OnDrawGizmos()
    // {
    //     if (CharacterManager.Instance == null || CharacterManager.Instance.player == null) return;
    //
    //     Transform playerTransform = CharacterManager.Instance.player.transform;
    //     Vector3 boxCenter = playerTransform.position + playerTransform.forward * (snipeRange / 2);
    //     Quaternion boxRotation = Quaternion.LookRotation(playerTransform.forward);
    //
    //     Gizmos.color = Color.red;
    //     Matrix4x4 rotationMatrix = Matrix4x4.TRS(boxCenter, boxRotation, Vector3.one);
    //     Gizmos.matrix = rotationMatrix;
    //     Gizmos.DrawWireCube(Vector3.zero, boxSize);
    // }
}
