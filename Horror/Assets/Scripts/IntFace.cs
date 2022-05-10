﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class InventorySlot 
{
    public Item item;
    public int amount;

    public InventorySlot (Item item, int amount = 1)
    {
        this.item = item;
        this.amount = amount;
    }
}

public class IntFace : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> items = new List<InventorySlot>();
    [SerializeField] private int size = 15;
    [SerializeField] public UnityEvent OnInventoryChanged;
    public GameObject inventoryObject;
    public GameObject flashlight;
    public Button flashlightSwitch;

    public GameObject PauseMenu;
    public static bool GameIsPaused = false;
    
    void Start()
    {
        inventoryObject.SetActive(false);
    }

    public bool AddItems(Item item, int amount = 1)
    {
        foreach (InventorySlot slot in items)
        {
            if(slot.item.id == item.id)
            {
                slot.amount += amount;
                OnInventoryChanged.Invoke();
                return true;
            }
        }
        
        if (items.Count >= size) return false;

        InventorySlot new_slot = new InventorySlot(item, amount);

        items.Add(new_slot);

        if(item.id == 0)
        {
            flashlight.SetActive(true);
            flashlightSwitch.gameObject.SetActive(true);
        }

        OnInventoryChanged.Invoke();

        return true;
    }

    public Item GetItem(int i)
    {
        return i < items.Count ? items[i].item : null;
    }

    public int GetAmount(int i)
    {
        return i < items.Count ? items[i].amount : 0;
    }

    public int GetSize()
    {
        return items.Count;
    }

    public void OpenInventory()
    {
        if(inventoryObject.activeSelf == false)
        {
            inventoryObject.SetActive(true);
        }
        else
        {
            inventoryObject.SetActive(false);
        }
    }

    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void ExitinMenu()
    {
        Application.LoadLevel("MainMenu");
    }


    
}