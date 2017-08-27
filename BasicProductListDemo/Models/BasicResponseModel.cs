using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductListMVCDemo.Models
{
    /// <summary>
    /// Basic model that can be used for various purposes
    /// </summary>
    public class BasicResponseModel
    {
        public bool Success { get; set; }

        /// <summary>
        /// String that represents some content to be returned.
        /// This could be HTML or an error message depending on context.
        /// </summary>
        public string Content { get; set; }
    }
}