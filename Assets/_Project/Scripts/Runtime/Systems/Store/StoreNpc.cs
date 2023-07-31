using UnityEngine;

public class StoreNpc : MonoBehaviour
{
    private Inventory _inventory;
    private DialogController _dialogController;
    private NpcDialog _dialogNpc;
    private bool startDialog;

    [SerializeField] private bool canInteract;

    [SerializeField] private GameObject _panelStore;
    [SerializeField] private GameObject _buttonOpen;

    private void Initialization()
    {
        _panelStore.SetActive(false);
        _buttonOpen.SetActive(false);
        _dialogNpc = GetComponent<NpcDialog>();
        _inventory = FindObjectOfType<Inventory>();
        _dialogController = FindObjectOfType<DialogController>();
        _inventory.OnCloseInventoryEvent += CloseStore;
        _dialogController.OnEndDialog += EndTalkOpenStore;
    }

    private void Start()
    {
        Initialization();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract && !startDialog && !IsStoreOpen())
        {
            _dialogController.StartTalk(_dialogNpc._dialog);
            startDialog = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && startDialog && !IsStoreOpen())
        {
            _dialogController.NextTalk();
        }
    }

    private void EndTalkOpenStore()
    {
        _panelStore.SetActive(true);
        _inventory.OpenOrCloseInventory(true);
        _buttonOpen.SetActive(false);
        startDialog = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerPhysics player))
        {
            canInteract = true;
            _buttonOpen.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerPhysics player))
        {
            canInteract = false;
            _buttonOpen.SetActive(false);
        }
    }

    private void CloseStore()
    {
        _panelStore.SetActive(false);
    }

    private bool IsStoreOpen()
    {
        return _panelStore.activeSelf;
    }

    private void OnDestroy()
    {
        _inventory.OnCloseInventoryEvent -= CloseStore;
        _dialogController.OnEndDialog -= EndTalkOpenStore;
    }

}
