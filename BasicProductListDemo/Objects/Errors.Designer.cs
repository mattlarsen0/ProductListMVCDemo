﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProductListMVCDemo.Objects {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Errors {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Errors() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BasicProductListDemo.Objects.Errors", typeof(Errors).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The current product could not be found. Please refresh your page and try again..
        /// </summary>
        public static string CannotFindProduct {
            get {
                return ResourceManager.GetString("CannotFindProduct", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There was an error with your request. Support has been notified..
        /// </summary>
        public static string GenericMVCInternalError {
            get {
                return ResourceManager.GetString("GenericMVCInternalError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There was an error with your request. Please refesh your page and try again. If this message keeps appearing, please contact support..
        /// </summary>
        public static string GenericMVCValidationError {
            get {
                return ResourceManager.GetString("GenericMVCValidationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Reccomended age cannot be negative..
        /// </summary>
        public static string ProductsNegativeAge {
            get {
                return ResourceManager.GetString("ProductsNegativeAge", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Price cannot be negative..
        /// </summary>
        public static string ProductsNegativePrice {
            get {
                return ResourceManager.GetString("ProductsNegativePrice", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Quantity cannot be negative..
        /// </summary>
        public static string ProductsNegativeQuantity {
            get {
                return ResourceManager.GetString("ProductsNegativeQuantity", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Year of release cannot be negative..
        /// </summary>
        public static string ProductsNegativeYear {
            get {
                return ResourceManager.GetString("ProductsNegativeYear", resourceCulture);
            }
        }
    }
}
