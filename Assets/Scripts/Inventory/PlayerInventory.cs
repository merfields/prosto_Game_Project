using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    private List<PickableItem> takenItems = new List<PickableItem>();
    [SerializeField] private int maxNumberOfItemsInInventory = 5;
    [SerializeField] GameObject infoWindow;
    private InventoryItemSlot[] itemSlots;

    private void Awake() {
        itemSlots = GetComponentsInChildren<InventoryItemSlot>();
    }

    public bool AddItemToInventory(PickableItem itemToAdd)
    {
        if (!(takenItems.Count >= maxNumberOfItemsInInventory))
        {
            takenItems.Add(itemToAdd);
            AddItemToUi(itemToAdd);

            Debug.Log("Added " + itemToAdd.ToString() + " to inventory");
            return true;
        }
        else
        {
            infoWindow.GetComponent<InformationWindow>().SetWindowText("Ваш инвентарь заполнен");
            Instantiate(infoWindow, transform.position, Quaternion.identity);
            return false;
        }
    }

    private void AddItemToUi(PickableItem itemToAdd)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].ItemInSlot == null)
            {
                itemSlots[i].ItemInSlot = itemToAdd;
                return;
            }
        }
    }

    public void RemoveItemFromInventory(PickableItem itemToRemove)
    {
        takenItems.Remove(itemToRemove);
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].ItemInSlot == itemToRemove)
            {
                itemSlots[i].ItemInSlot = null;
                return;
            }
        }
    }

    public void CheckIsSelectedItemInInventory()
    {
        foreach (InventoryItemSlot slot in itemSlots)
        {
            if (slot.IsItemSelected == true)
            {
                slot.DeselectItem();
                return;
            }
        }
    }
}

