using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    [SerializeField] private Transform roomPos;
    [SerializeField] private GameObject[] rooms;

    [SerializeField] private GameObject[] roomsObj;

    private int _idCurrentRoom;

    private PlayerPhysics _playerPhysics;
    private ShadowController _playerShadow;

    private void Start()
    {
        _idCurrentRoom = 0;

        foreach (GameObject r in rooms)
        {
            r.SetActive(false);
        }

        foreach (GameObject ro in roomsObj)
        {
            ro.SetActive(false);
        }

        rooms[_idCurrentRoom].SetActive(true);
        roomsObj[0].SetActive(true);

        _playerPhysics = FindObjectOfType<PlayerPhysics>();
        _playerShadow = _playerPhysics.GetComponent<ShadowController>();
    }

    public void UpdateMap(int value)
    {

        roomsObj[_idCurrentRoom].SetActive(false);
        roomsObj[value].SetActive(true);
        _idCurrentRoom = value;

        rooms[value].SetActive(true);
        Vector2 newPos = new(rooms[value].transform.localPosition.x, rooms[value].transform.localPosition.y);
        roomPos.localPosition = newPos * -1;
        _playerShadow.UpdateListLights();
    }

}
