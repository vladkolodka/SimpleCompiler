﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Compiler.Resources {
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
    internal class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Compiler.Resources.Messages", typeof(Messages).Assembly);
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
        ///   Looks up a localized string similar to | -1 |{0}|File not found..
        /// </summary>
        internal static string FileNotFound {
            get {
                return ResourceManager.GetString("FileNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}|-4|{1}|Identifier not defined. .
        /// </summary>
        internal static string IdentyfierNotDefined {
            get {
                return ResourceManager.GetString("IdentyfierNotDefined", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}|2|{1} {2}|Indentifier founded..
        /// </summary>
        internal static string IndentifierFounded {
            get {
                return ResourceManager.GetString("IndentifierFounded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to | -2 ||You must specify the name of the file..
        /// </summary>
        internal static string InvalidFileName {
            get {
                return ResourceManager.GetString("InvalidFileName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}|-5|{1} {2}|Parsing completed with errors..
        /// </summary>
        internal static string ParsingError {
            get {
                return ResourceManager.GetString("ParsingError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}|3||Parsing completed successfully..
        /// </summary>
        internal static string ParsingSuccessful {
            get {
                return ResourceManager.GetString("ParsingSuccessful", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}|1|{1} {2} {3} {4}|Token found..
        /// </summary>
        internal static string TokenFounded {
            get {
                return ResourceManager.GetString("TokenFounded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}|-3|{1} {2}|Unexpected symbol..
        /// </summary>
        internal static string UnexpectedSymbol {
            get {
                return ResourceManager.GetString("UnexpectedSymbol", resourceCulture);
            }
        }
    }
}
