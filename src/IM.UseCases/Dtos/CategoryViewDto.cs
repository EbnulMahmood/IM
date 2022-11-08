using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IM.UseCases.Dtos
{
    public class CategoryViewDto
    {
        public string Name { get; set; } = string.Empty;
        public string StatusHtml { get; set; } = string.Empty;
        public string ActionLinkHtml { get; set; } = string.Empty;
    }
}