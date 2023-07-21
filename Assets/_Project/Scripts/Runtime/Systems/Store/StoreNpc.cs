using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreNpc : MonoBehaviour
{
    private Inventory _inventory;

    [SerializeField] private bool canInteract;

    [SerializeField] private GameObject _panelStore;
    [SerializeField] private GameObject _buttonOpen;

    private void Start()
    {
        _panelStore.SetActive(false);
        _buttonOpen.SetActive(false);
        _inventory = FindObjectOfType<Inventory>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            print(name);
            _panelStore.SetActive(true);
            _inventory.OpenOrCloseInventory(true);
            _buttonOpen.SetActive(false);
        }
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
        if(collision.gameObject.TryGetComponent(out PlayerPhysics player))
        {
            canInteract = false;
            _buttonOpen.SetActive(false);
        }
    }




}
