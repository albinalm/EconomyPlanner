//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EconomyPlanner.Repository.Languages {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class CommonLoc {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CommonLoc() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("EconomyPlanner.Repository.Languages.CommonLoc", typeof(CommonLoc).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Household.
        /// </summary>
        internal static string ExpenseTypeHousehold {
            get {
                return ResourceManager.GetString("ExpenseTypeHousehold", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Shopping.
        /// </summary>
        internal static string ExpenseTypeShopping {
            get {
                return ResourceManager.GetString("ExpenseTypeShopping", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Subscription.
        /// </summary>
        internal static string ExpenseTypeSubscription {
            get {
                return ResourceManager.GetString("ExpenseTypeSubscription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Gift.
        /// </summary>
        internal static string IncomeTypeGift {
            get {
                return ResourceManager.GetString("IncomeTypeGift", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Refund.
        /// </summary>
        internal static string IncomeTypeRefund {
            get {
                return ResourceManager.GetString("IncomeTypeRefund", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Salary.
        /// </summary>
        internal static string IncomeTypeSalary {
            get {
                return ResourceManager.GetString("IncomeTypeSalary", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Other.
        /// </summary>
        internal static string TransactionTypeOther {
            get {
                return ResourceManager.GetString("TransactionTypeOther", resourceCulture);
            }
        }
    }
}
