using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    [SerializeField] private Transform roomPos;
    [SerializeField] private GameObject[] rooms;

    [SerializeField] private GameObject[] roomsObj;

    private void Start()
    {
        foreach (GameObject r in rooms)
        {
            r.SetActive(false);
        }

        foreach (GameObject ro in roomsObj)
        {
            ro.SetActive(false);
        }

        rooms[0].SetActive(true);
        roomsObj[0].SetActive(true);
    }

    public void UpdateMap(int value)
    {
        foreach (GameObject ro in roomsObj)
        {
            ro.SetActive(false);
        }

        rooms[value].SetActive(true);
        roomsObj[value].SetActive(true);

        Vector2 newPos = new(rooms[value].transform.localPosition.x, rooms[value].transform.localPosition.y);
        roomPos.localPosition = newPos * -1;
    }

}
