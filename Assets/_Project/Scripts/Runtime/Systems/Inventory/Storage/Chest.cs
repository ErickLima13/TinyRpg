using UnityEngine;

public class Chest : MonoBehaviour
{
    private Storage storage;

    [SerializeField] private bool canInteract;
    [SerializeField] private GameObject _buttonOpen;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            storage.OpenCloseStorage(true);
            OpenChest();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            //  _dialogController.NextTalk();
        }
    }

    private void OpenChest()
    {
        GameStateController.ChangeState(GameState.Chest);
        _buttonOpen.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Storage player))
        {
            storage = player;
            canInteract = true;
            _buttonOpen.SetActive(canInteract);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Storage player))
        {
            storage = null;
            canInteract = false;
            _buttonOpen.SetActive(canInteract);
        }
    }


}
