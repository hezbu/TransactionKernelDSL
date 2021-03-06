﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TransactionKernelDSL.Framework.Language.CustomCode.Validation {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ValidationResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ValidationResources() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TransactionKernelDSL.Framework.Language.CustomCode.Validation.ValidationResources" +
                            "", typeof(ValidationResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
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
        ///   Busca una cadena traducida similar a Output Engine {0} cannot have any Data SOurce Support because of its Output Engine&apos;s Type..
        /// </summary>
        internal static string DataSourceSupportInvalidEngineTypeError {
            get {
                return ResourceManager.GetString("DataSourceSupportInvalidEngineTypeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Engine{0} has an invalid Name.
        /// </summary>
        internal static string EngineInvalidNameError {
            get {
                return ResourceManager.GetString("EngineInvalidNameError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SQL Server Environment Variable {0} has an invalid name.
        /// </summary>
        internal static string EnvironmentSQLServerVariableInvalidNameError {
            get {
                return ResourceManager.GetString("EnvironmentSQLServerVariableInvalidNameError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Handler {0} has at least one forwarding handler pointing to itself, therefore making an infinite forbidden loop.
        /// </summary>
        internal static string HandlerForwardingError {
            get {
                return ResourceManager.GetString("HandlerForwardingError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Handler {0} has an invalid Name.
        /// </summary>
        internal static string HandlerInvalidNameError {
            get {
                return ResourceManager.GetString("HandlerInvalidNameError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Handler {0} has an invalid TransactionId.
        /// </summary>
        internal static string HandlerInvalidTransactionIdError {
            get {
                return ResourceManager.GetString("HandlerInvalidTransactionIdError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Handler {0} is pointing itself as its maintenance handler, therefore making an infinite forbidden loop.
        /// </summary>
        internal static string HandlerMaintenanceError {
            get {
                return ResourceManager.GetString("HandlerMaintenanceError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Handler {0}&apos;s TransactionId cannot be empty.  Must be set by right-clicking on handler, and then in Properties.
        /// </summary>
        internal static string HandlerTransactionIdError {
            get {
                return ResourceManager.GetString("HandlerTransactionIdError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Layer {0} has an invalid Name.
        /// </summary>
        internal static string LayerInvalidNameError {
            get {
                return ResourceManager.GetString("LayerInvalidNameError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Layer {0}&apos;s Level is lower than zero. Must be zero or more..
        /// </summary>
        internal static string LayerLevelError {
            get {
                return ResourceManager.GetString("LayerLevelError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Model connection string is not overridden, and it should be if it is meant to be used. Must be overridden by right-clicking on diagram&apos;s background, and then in Properties.
        /// </summary>
        internal static string ModelConnectionStringWarning {
            get {
                return ResourceManager.GetString("ModelConnectionStringWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Model InstanceId cannot be zero or less. Must be set by right-clicking on diagram&apos;s background, and then in Properties.
        /// </summary>
        internal static string ModelInstanceIdError {
            get {
                return ResourceManager.GetString("ModelInstanceIdError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Model {0} has an invalid Name.
        /// </summary>
        internal static string ModelInvalidNameError {
            get {
                return ResourceManager.GetString("ModelInvalidNameError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Model {0} has an invalid Namespace.
        /// </summary>
        internal static string ModelInvalidNamespaceError {
            get {
                return ResourceManager.GetString("ModelInvalidNamespaceError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Model name cannot be empty. Must be set by right-clicking on diagram&apos;s background, and then in Properties.
        /// </summary>
        internal static string ModelNameError {
            get {
                return ResourceManager.GetString("ModelNameError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Model namespace cannot be empty. Must be set by right-clicking on diagram&apos;s background, and then in Properties.
        /// </summary>
        internal static string ModelNamespaceError {
            get {
                return ResourceManager.GetString("ModelNamespaceError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Is not possible to have more than one FunneledOutputEngine or TcpFunneledOutputEngine with SQLServerDataSourceSupport.
        /// </summary>
        internal static string ModelOnlyOneFunneledEngineError {
            get {
                return ResourceManager.GetString("ModelOnlyOneFunneledEngineError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Satellite instance {0} is not valid!.
        /// </summary>
        internal static string SatelliteInstancesError {
            get {
                return ResourceManager.GetString("SatelliteInstancesError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Forwarding Link between handlers {0} and {1} must have a sequence order equal or more than zero.
        /// </summary>
        internal static string SequenceOrderMoreOrEqualThanZeroError {
            get {
                return ResourceManager.GetString("SequenceOrderMoreOrEqualThanZeroError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Link between {0} and {1} must be placed on elements on the same layer.
        /// </summary>
        internal static string SourceAndTargetWithinSameLayerError {
            get {
                return ResourceManager.GetString("SourceAndTargetWithinSameLayerError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Time Trigger {0}&apos;s InputTransactionEngine&apos;s type is invalid. It must be of type TimeTriggeredInputEngine in order to link this trigger to it..
        /// </summary>
        internal static string TimeTriggerEngineTypeInvalidError {
            get {
                return ResourceManager.GetString("TimeTriggerEngineTypeInvalidError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Time Trigger {0} has not any TimeTriggerInputEngine associated to it..
        /// </summary>
        internal static string TimeTriggerHasNotInputEngineError {
            get {
                return ResourceManager.GetString("TimeTriggerHasNotInputEngineError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Time Trigger {0} has an invalid DueIntervalTime. It must be more than zero seconds.
        /// </summary>
        internal static string TimeTriggerInvalidDueTimeIntervalError {
            get {
                return ResourceManager.GetString("TimeTriggerInvalidDueTimeIntervalError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Time Trigger {0} has an invalid Name.
        /// </summary>
        internal static string TimeTriggerInvalidNameError {
            get {
                return ResourceManager.GetString("TimeTriggerInvalidNameError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Time Trigger {0} has an invalid IntervalTime. It must be more than zero seconds.
        /// </summary>
        internal static string TimeTriggerInvalidTimeIntervalError {
            get {
                return ResourceManager.GetString("TimeTriggerInvalidTimeIntervalError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Web Service {0} has an empty WebServiceClassName, which is not valid.
        /// </summary>
        internal static string WebServiceClassNameEmptynessError {
            get {
                return ResourceManager.GetString("WebServiceClassNameEmptynessError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Web Service {0} has an invalid WebServiceClassName.
        /// </summary>
        internal static string WebServiceClassNameInvalidNameError {
            get {
                return ResourceManager.GetString("WebServiceClassNameInvalidNameError", resourceCulture);
            }
        }
    }
}
