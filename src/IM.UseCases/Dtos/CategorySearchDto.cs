using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IM.UseCases.Dtos
{
    public class CategorySearchDto
    {
        public long Id { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}