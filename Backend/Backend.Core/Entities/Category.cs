﻿using Backend.Core.Enums;

namespace Backend.Core.Entities
{
    public class Category : BaseEntity
    {
        public required string Name {  get; set; }
        public OperationType Type { get; set; }
        public List<Category>? subcategories { get; set;}
    }
}
