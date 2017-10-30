﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vocabulary.Server.Resources {
    
    
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
        [global::System.Configuration.DefaultSettingValueAttribute("<нет такой команды>")]
        public string NoSuchCommand {
            get {
                return ((string)(this["NoSuchCommand"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<слово отсутвует в словаре>")]
        public string NoSuchWord {
            get {
                return ((string)(this["NoSuchWord"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<({0}):значения слова успешно добавлены>")]
        public string MeansAdded {
            get {
                return ((string)(this["MeansAdded"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<значения уже добавлены>")]
        public string MeansAreAlreadyAdded {
            get {
                return ((string)(this["MeansAreAlreadyAdded"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<слово не задано>")]
        public string WordIsNotSet {
            get {
                return ((string)(this["WordIsNotSet"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<значения слова успешно удалены>")]
        public string MeansAreDeleted {
            get {
                return ((string)(this["MeansAreDeleted"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<слово/значение отсутвует в словаре>")]
        public string NoSuchWordOrMean {
            get {
                return ((string)(this["NoSuchWordOrMean"]));
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
        [global::System.Configuration.DefaultSettingValueAttribute("<сервер запущен, нажмите любую клавишу...>")]
        public string ServerIsStarted {
            get {
                return ((string)(this["ServerIsStarted"]));
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
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<ошибка остановки сервера>")]
        public string StopServerError {
            get {
                return ((string)(this["StopServerError"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<ошибка обработки запроса>")]
        public string HandleRequestError {
            get {
                return ((string)(this["HandleRequestError"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<ошибка старта сервера>")]
        public string StartServerError {
            get {
                return ((string)(this["StartServerError"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<ошибка получения запроса>")]
        public string RequestError {
            get {
                return ((string)(this["RequestError"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<значения не заданы>")]
        public string MeansAreNotSet {
            get {
                return ((string)(this["MeansAreNotSet"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<дублируется имя команды>")]
        public string CommandNameDuplicated {
            get {
                return ((string)(this["CommandNameDuplicated"]));
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
    }
}
