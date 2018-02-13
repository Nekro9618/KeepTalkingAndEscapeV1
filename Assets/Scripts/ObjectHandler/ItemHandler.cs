﻿using System;
using System.Collections.Generic;
using TrustfallGames.KeepTalkingAndEscape.Datatypes;
using UnityEditor;
using UnityEngine;

namespace TrustfallGames.KeepTalkingAndEscape.Listener {
    public class ItemHandler : MonoBehaviour {
        //All Items which can exist.
        private List<Item> _itemsDatabase = new List<Item>();
        //The current Items in the Inventory
        private List<Item> _inventory = new List<Item>();

        public List<Item> Inventory {
            get {return _inventory;}
            set {_inventory = value;}
        }

        public List<Item> ItemsDatabase {
            get {return _itemsDatabase;}
            set {_itemsDatabase = value;}
        }

        /// <summary>
        /// If items are combineable, it combines them, adds the new item to the inventory, and remove the combined items.
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <returns>Returns true, if the combination was valid.</returns>
        public bool ItemsCombineable(Item item1, Item item2) {
            if(item1.Combineable && item2.Combineable)
                if(item1.CombineWith == CombineWith.Item && item2.CombineWith == CombineWith.Item)
                    if(item1.CombineWithItem == item2.ItemId && item2.CombineWithItem == item1.ItemId) {
                        AddItemToInv(item1.NextItem);
                        RemoveItemFromInventory(item1.ItemId);
                        RemoveItemFromInventory(item2.ItemId);
                        return true;
                    }

            return false;
        }

        /// <summary>
        /// Search in the item database for the item and adds it to the inventory.
        /// </summary>
        /// <param name="Item you want to add"></param>
        public void AddItemToInv(ItemId itemId) {
            foreach(var obj in _itemsDatabase) {
                if(obj.ItemId == itemId) {
                    _inventory.Add(obj);
                    return;
                }

                throw new ArgumentException("Item is not in Databse. Please Check you database file.");
            }
        }

        /// <summary>
        /// Removes the Item from the inventory.
        /// </summary>
        /// <param name="itemId"></param>
        private void RemoveItemFromInventory(ItemId itemId) {
            for(int i = 0; i < _inventory.Count; i++) {
                var obj = _inventory[i];
                if(obj.ItemId == itemId) {
                    _inventory.RemoveAt(i);
                    return;
                }
            }
        }

        /// <summary>
        /// Returns the item object with the itemID
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private Item GetItemFromDatabase(ItemId itemId) {
            foreach(var obj in _itemsDatabase) {
                if(obj.ItemId == itemId) {
                    return obj;
                }
            }
            throw new ArgumentException("Item is not in Database. Please check the database file.");
        } 
    }
}