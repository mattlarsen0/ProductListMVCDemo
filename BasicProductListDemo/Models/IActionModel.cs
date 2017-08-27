using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductListMVCDemo.Models
{
    public interface IActionModel
    {
        bool IsValid(out string errorMessage);
    }
}