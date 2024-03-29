﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace craftersmine.Valknut.Launcher.Bootstrap.Properties {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("craftersmine.Valknut.Launcher.Bootstrap.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
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
        ///   Ищет локализованную строку, похожую на Something went wrong during launcher bootstrapping!\r\nPlease try again or contact support!\r\n\r\n{0}\r\nStatus code: {1}.
        /// </summary>
        internal static string Error_BootstrapDataError {
            get {
                return ResourceManager.GetString("Error.BootstrapDataError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на An error has occured while bootstrapping launcher! Contact support for more information!\r\n\r\nMessage: {0}\r\nStacktrace:\r\n {1}.
        /// </summary>
        internal static string Error_BootstrapError {
            get {
                return ResourceManager.GetString("Error.BootstrapError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Unable to verify downloaded launcher archive! Try relaunching bootstrapper and download launcher again. If problem occurs anyway, contact support!.
        /// </summary>
        internal static string Error_LauncherVerification {
            get {
                return ResourceManager.GetString("Error.LauncherVerification", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Error!.
        /// </summary>
        internal static string Error_Title {
            get {
                return ResourceManager.GetString("Error.Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Downloading launcher....
        /// </summary>
        internal static string Status_DownloadingLauncher {
            get {
                return ResourceManager.GetString("Status.DownloadingLauncher", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Downloading launcher data....
        /// </summary>
        internal static string Status_DownloadingLauncherData {
            get {
                return ResourceManager.GetString("Status.DownloadingLauncherData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Downloading launcher... {0}% - {1:F2} MB/{2:F2} MB.
        /// </summary>
        internal static string Status_DownloadingLauncherProgress {
            get {
                return ResourceManager.GetString("Status.DownloadingLauncherProgress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Extracting launcher....
        /// </summary>
        internal static string Status_ExtractingLauncher {
            get {
                return ResourceManager.GetString("Status.ExtractingLauncher", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Loading Valknut Bootstrapper....
        /// </summary>
        internal static string Status_LoadingBootstrapper {
            get {
                return ResourceManager.GetString("Status.LoadingBootstrapper", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Starting launcher....
        /// </summary>
        internal static string Status_StartingLauncher {
            get {
                return ResourceManager.GetString("Status.StartingLauncher", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Validating downloaded launcher....
        /// </summary>
        internal static string Status_ValidatingLauncher {
            get {
                return ResourceManager.GetString("Status.ValidatingLauncher", resourceCulture);
            }
        }
    }
}
