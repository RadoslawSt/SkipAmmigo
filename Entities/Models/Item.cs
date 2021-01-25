using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Entities.Extensions;
namespace Entities.Models
{
    public class Item : IEntity
    {
        [Key]
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
    }
}
