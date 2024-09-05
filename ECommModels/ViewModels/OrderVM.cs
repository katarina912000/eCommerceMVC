using ECommModels.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommModels.ViewModels
{
    public class OrderVM
    {
        public OrderHeader OrderHeader { get; set; }
        [ValidateNever]

        public IEnumerable<SelectListItem> OrderDetail { get; set; }
    }
}
