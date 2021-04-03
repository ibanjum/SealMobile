using System;
using System.Collections.Generic;

namespace Seal.Models
{
    public class CategoryModel
    {
        public string Name { get; set; }
        public List<ItemModel> CategoryItems { get; set; }

        public CategoryModel(string name)
        {
            Name = name;
        }
    }
}
