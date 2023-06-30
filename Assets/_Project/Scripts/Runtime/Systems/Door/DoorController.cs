using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject[] doorsWarp;

    [SerializeField] private Sprite[] spritesLocked;
    [SerializeField] private Sprite[] spritesOpened;

    public bool conditionDoor;
    public bool needKey;

    private void Start()
    {
        UpddateDoor();
    }

    public void UpddateDoor()
    {
        for (int i = 0; i < doorsWarp.Length; i++)
        {
            if (needKey || conditionDoor)
            {
                doorsWarp[i].GetComponent<SpriteRenderer>().sprite = spritesLocked[i];
            }
            else
            {
                doorsWarp[i].GetComponent<SpriteRenderer>().sprite = spritesOpened[i];
            }
        }
    }


}
