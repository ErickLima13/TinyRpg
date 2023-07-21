using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
    public GameObject shadowPrefab;
    public Transform shadowPos;

    public LightSource[] lightSources;
    public List<Transform> lightTargets;
    public List<Transform> shadowTargets;

    public float minScale;
    public float maxScale;

    private void OnEnable()
    {
        UpdateListLights();
    }

    private void LateUpdate()
    {
        UpdateShadowPos();
    }

    public void UpdateListLights()
    {
        if(lightSources.Length > 0)
        {
            foreach(Transform t in shadowTargets)
            {
                Destroy(t.gameObject);
            }

            shadowTargets.Clear();
            lightTargets.Clear();
        }

        lightSources = FindObjectsOfType<LightSource>();

        for (int i = 0; i < lightSources.Length; i++)
        {
            lightTargets.Add(lightSources[i].transform);
            GameObject temp = Instantiate(shadowPrefab, shadowPos.position, transform.localRotation, shadowPos);
            shadowTargets.Add(temp.transform);
        }
    }

    private void UpdateShadowPos()
    {
        for (int i = 0; i < lightSources.Length; i++)
        {
            Vector3 dir = lightSources[i].transform.position - transform.position;
            shadowTargets[i].up = dir * -1;

            float dist = Vector3.Distance(transform.position, lightSources[i].transform.position);

            if (dist < minScale)
            {
                dist = minScale;
            }
            else if (dist > maxScale)
            {
                dist = maxScale;
            }

            shadowTargets[i].localScale = new Vector3(shadowTargets[i].localScale.x, dist, shadowTargets[i].localScale.z);
        }
    }
}
