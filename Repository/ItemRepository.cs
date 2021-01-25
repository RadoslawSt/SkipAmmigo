using System;
using Entities.Models;
using Interface;
using Entities;
using System.Collections.Generic;
using System.Linq;
using Entities.Extensions;
namespace Repository
{
    public class ItemRepository : RepositoryBase<Item>, IItemRepository
    {
        public ItemRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
        public IEnumerable<Item> GetAllItems()
        {
            return FindAll()
                .OrderBy(ow => ow.Text);
        }

        public Item GetItemById(string itemId)
        {
            return FindByCondition(owner => owner.Id.Equals(itemId))
                    .DefaultIfEmpty(new Item())
                    .FirstOrDefault();
        }

       

        public void CreateItem(Item item)
        {
            item.Id = Guid.NewGuid().ToString();
            Create(item);
            Save();
        }

        public void UpdateItem(Item dbItem, Item item)
        {
            dbItem.MapItem(item);
            Update(dbItem);
            Save();
        }

        public void DeleteItem(Item item)
        {
            Delete(item);
            Save();
        }
    }
}
