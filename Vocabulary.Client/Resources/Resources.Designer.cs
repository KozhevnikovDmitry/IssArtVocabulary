﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vocabulary.Client.Resources {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Resources : global::System.Configuration.ApplicationSettingsBase {
        
        private static Resources defaultInstance = ((Resources)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Resources())));
        
        public static Resources Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<url сервера не задан>")]
        public string UrlIsNotSet {
            get {
                return ((string)(this["UrlIsNotSet"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<команда не задана>")]
        public string CommandIsNotSet {
            get {
                return ((string)(this["CommandIsNotSet"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<запрос вернул код ошибки: {0}>")]
        public string ResponseErrorCode {
            get {
                return ((string)(this["ResponseErrorCode"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<url имеет неверный формат>")]
        public string UrlIsInvalid {
            get {
                return ((string)(this["UrlIsInvalid"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<порт не задан>")]
        public string PortIsNotSet {
            get {
                return ((string)(this["PortIsNotSet"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<ошибка запроса>")]
        public string RequestError {
            get {
                return ((string)(this["RequestError"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<ошибка: {0}>")]
        public string UnexpectedError {
            get {
                return ((string)(this["UnexpectedError"]));
            }
        }
    }
}
