﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vasont.Inspire.SDK.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Vasont.Inspire.SDK.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to The specified access method requires an access token be specified in the configuration..
        /// </summary>
        internal static string AccessMethodRequiresTokenErrorText {
            get {
                return ResourceManager.GetString("AccessMethodRequiresTokenErrorText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An introspection endpoint must be specified to verify client access token access. Either enable discovery or specify an introspection endpoint in configuration..
        /// </summary>
        internal static string IntrospectionConfigNeededErrorText {
            get {
                return ResourceManager.GetString("IntrospectionConfigNeededErrorText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A component identity or unique file name must be specified in the editor request..
        /// </summary>
        internal static string InvalidEditorRequestMissingComponentErrorText {
            get {
                return ResourceManager.GetString("InvalidEditorRequestMissingComponentErrorText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Request method must be POST, PUT, or DELETE.
        /// </summary>
        internal static string InvalidRequestTypeErrorText {
            get {
                return ResourceManager.GetString("InvalidRequestTypeErrorText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The access token specified was not valid..
        /// </summary>
        internal static string TokenIntrospectionInvalidErrorText {
            get {
                return ResourceManager.GetString("TokenIntrospectionInvalidErrorText", resourceCulture);
            }
        }
    }
}