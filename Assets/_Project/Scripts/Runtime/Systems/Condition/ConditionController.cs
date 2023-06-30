using System.Collections.Generic;
using UnityEngine;

public class ConditionController : MonoBehaviour
{
    [SerializeField] private List<GameObject> conditionsObjects;

    [SerializeField] private DoorController doorController;

    public void AddObject(GameObject objectC)
    {
        conditionsObjects.Add(objectC);
    }

    public void RemoveObject(GameObject objectC)
    {
        conditionsObjects.Remove(objectC);

        if (conditionsObjects.Count <= 0)
        {
            doorController.conditionDoor = false;
            doorController.UpddateDoor();    
        }
    }



}
