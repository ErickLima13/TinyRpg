using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject[] doorsWarp;

    [SerializeField] private List<SpriteRenderer> spritesDoors;

    [SerializeField] private Sprite[] spritesLocked;
    [SerializeField] private Sprite[] spritesOpened;

    public bool conditionDoor;
    public bool needKey;

    public ItemData item;

    private void Start()
    {
        foreach (var door in doorsWarp)
        {
            spritesDoors.Add(door.GetComponent<SpriteRenderer>());
        }

        UpddateDoor();
    }

    public void UpddateDoor()
    {
        for (int i = 0; i < doorsWarp.Length; i++)
        {
            if (needKey || conditionDoor)
            {
                spritesDoors[i].sprite = spritesLocked[i];
            }
            else
            {
                spritesDoors[i].sprite = spritesOpened[i];
            }
        }
    }


}
