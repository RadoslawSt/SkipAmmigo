using System;
using Microsoft.AspNetCore.Mvc;
using Interface;
using Entities.Models;
using Entities.Extensions;

namespace JumpApp.Controllers
{
    [Route("api/[controller]")]
    public class ItemController : Controller
	{

		private readonly IRepositoryWrapper repoWrapper;

		public ItemController(IRepositoryWrapper RepoWrapper)
		{
			repoWrapper = RepoWrapper;
		}

		[HttpGet]
		public IActionResult List()
		{
			return Ok(repoWrapper.Item.GetAllItems());
            //return Ok(repoWrapper.Item.fin)
		}

		[HttpGet("{Id}")]
		public Item GetItem(string Id)
		{
            //Guid result = Guid.Parse(Id);
            var item = repoWrapper.Item.GetItemById(Id);
			return item;
		}

		[HttpPost]
		public IActionResult Create([FromBody]Item item)
		{
			try
			{
				if (item == null || !ModelState.IsValid)
				{
					return BadRequest("Invalid State");
				}

                repoWrapper.Item.CreateItem(item);

			}
			catch (Exception)
			{
				return BadRequest("Error while creating");
			}
			return Ok(item);
		}

        [HttpPut("{Id}")]
        public IActionResult Edit(string Id, [FromBody] Item item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }
                var dbItem = repoWrapper.Item.GetItemById(Id);
                if (dbItem.IsEmptyObject())
                {
                   // _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                repoWrapper.Item.UpdateItem(dbItem, item);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public void Delete(string id)
        {          
            repoWrapper.Item.DeleteItem(GetItem(id));
        }
    }
}
