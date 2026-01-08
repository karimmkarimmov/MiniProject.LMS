using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public List<BookDto>? Books { get; set; }
    }

    public class CategoryAddDto 
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
    }

    public class CategoryUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class CategoryDeleteDto
    {
        public int Id { get; set; }
    }

    public class CategorySearchDto
    {
        public string? Name { get; set; } = "";
    }
}

