﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1008
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _ {
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
    public class Auth {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Auth() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Chimera.Authentication.Core.Resources.Auth", typeof(Auth).Assembly);
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
        ///   Looks up a localized string similar to Password.
        /// </summary>
        public static string Password {
            get {
                return ResourceManager.GetString("Password", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to be at least {0} characters long.
        /// </summary>
        public static string PwdLengthMsg_p1 {
            get {
                return ResourceManager.GetString("PwdLengthMsg_p1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to contain at least {0} lower case characters.
        /// </summary>
        public static string PwdLowerCaseMsg_p1 {
            get {
                return ResourceManager.GetString("PwdLowerCaseMsg_p1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} must {1}.
        /// </summary>
        public static string PwdMust_p2 {
            get {
                return ResourceManager.GetString("PwdMust_p2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to contain at least {0} numbers.
        /// </summary>
        public static string PwdNumberMsg_p1 {
            get {
                return ResourceManager.GetString("PwdNumberMsg_p1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to contain at most {0} character repetitions.
        /// </summary>
        public static string PwdRepetitionsMsg_p1 {
            get {
                return ResourceManager.GetString("PwdRepetitionsMsg_p1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to contain at least {0} upper case characters.
        /// </summary>
        public static string PwdUpperCaseMsg_p1 {
            get {
                return ResourceManager.GetString("PwdUpperCaseMsg_p1", resourceCulture);
            }
        }
    }
}
