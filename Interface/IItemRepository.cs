using Entities.Models;
using System;
using System.Collections.Generic;

namespace Interface
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetAllItems();
        Item GetItemById(string itemID);
        //Extended GetOwnerWithDetails(Guid ownerId);
        void CreateItem(Item item);
        void UpdateItem(Item dbItem, Item item);
        void DeleteItem(Item item);
    }
}
