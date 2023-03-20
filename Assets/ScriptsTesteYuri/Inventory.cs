using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
  //  public List<Slot> slots = new List<Slot>();
  //  public List<Item> items = new List<Item>();
  //
  //  private void Awake()
  //  {
  //      slots.AddRange(GetComponentsInChildren<Slot>());
  //      UpdateInventory();
  //  }
  //
  //  public void AddItem(Item item)
  //  {
  //      foreach (Slot slot in slots)
  //      {
  //          if (!slot.isOccupied)
  //          {
  //              slot.item = item;
  //              slot.isOccupied = true;
  //              slot.background.color = Color.gray;
  //              items.Add(item);
  //              UpdateInventory();
  //              return;
  //          }
  //      }
  //  }
  //
  //  public void RemoveItem(Item item)
  //  {
  //      foreach (Slot slot in slots)
  //      {
  //          if (slot.item == item)
  //          {
  //              slot.item = null;
  //              slot.isOccupied = false;
  //              slot.background.color = Color.white;
  //              items.Remove(item);
  //              UpdateInventory();
  //              return;
  //          }
  //      }
  //  }
  //
  //  public void MoveItem(Slot source, Slot destination)
  //  {
  //      if (!destination.isOccupied)
  //      {
  //          destination.item = source.item;
  //          destination.isOccupied = true;
  //          destination.background.color = Color.gray;
  //          source.item = null;
  //          source.isOccupied = false;
  //          source.background.color = Color.white;
  //          UpdateInventory();
  //      }
  //  }
  //
  //  public void UpdateInventory()
  //  {
  //      foreach (Slot slot in slots)
  //      {
  //          if (slot.isOccupied)
  //          {
  //              slot.background.color = Color.gray;
  //          }
  //          else
  //          {
  //              slot.background.color = Color.white;
  //          }
  //      }
  //  }
}