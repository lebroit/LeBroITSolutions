﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConnectorY.WebserviceY {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Customer", Namespace="http://WebServiceY.nl/")]
    [System.SerializableAttribute()]
    public partial class Customer : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CustomerIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BedrijfsnaamField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ContactField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AdresField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WoonplaatsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ProvincieField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PostcodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LandField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TelefoonField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FaxField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string CustomerID {
            get {
                return this.CustomerIDField;
            }
            set {
                if ((object.ReferenceEquals(this.CustomerIDField, value) != true)) {
                    this.CustomerIDField = value;
                    this.RaisePropertyChanged("CustomerID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Bedrijfsnaam {
            get {
                return this.BedrijfsnaamField;
            }
            set {
                if ((object.ReferenceEquals(this.BedrijfsnaamField, value) != true)) {
                    this.BedrijfsnaamField = value;
                    this.RaisePropertyChanged("Bedrijfsnaam");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string Contact {
            get {
                return this.ContactField;
            }
            set {
                if ((object.ReferenceEquals(this.ContactField, value) != true)) {
                    this.ContactField = value;
                    this.RaisePropertyChanged("Contact");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string Adres {
            get {
                return this.AdresField;
            }
            set {
                if ((object.ReferenceEquals(this.AdresField, value) != true)) {
                    this.AdresField = value;
                    this.RaisePropertyChanged("Adres");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string Woonplaats {
            get {
                return this.WoonplaatsField;
            }
            set {
                if ((object.ReferenceEquals(this.WoonplaatsField, value) != true)) {
                    this.WoonplaatsField = value;
                    this.RaisePropertyChanged("Woonplaats");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string Provincie {
            get {
                return this.ProvincieField;
            }
            set {
                if ((object.ReferenceEquals(this.ProvincieField, value) != true)) {
                    this.ProvincieField = value;
                    this.RaisePropertyChanged("Provincie");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string Postcode {
            get {
                return this.PostcodeField;
            }
            set {
                if ((object.ReferenceEquals(this.PostcodeField, value) != true)) {
                    this.PostcodeField = value;
                    this.RaisePropertyChanged("Postcode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public string Land {
            get {
                return this.LandField;
            }
            set {
                if ((object.ReferenceEquals(this.LandField, value) != true)) {
                    this.LandField = value;
                    this.RaisePropertyChanged("Land");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=8)]
        public string Telefoon {
            get {
                return this.TelefoonField;
            }
            set {
                if ((object.ReferenceEquals(this.TelefoonField, value) != true)) {
                    this.TelefoonField = value;
                    this.RaisePropertyChanged("Telefoon");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=9)]
        public string Fax {
            get {
                return this.FaxField;
            }
            set {
                if ((object.ReferenceEquals(this.FaxField, value) != true)) {
                    this.FaxField = value;
                    this.RaisePropertyChanged("Fax");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://WebServiceY.nl/", ConfigurationName="WebserviceY.WebServiceYSoap")]
    public interface WebServiceYSoap {
        
        // CODEGEN: Generating message contract since element name GetCustomersResult from namespace http://WebServiceY.nl/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://WebServiceY.nl/GetCustomers", ReplyAction="*")]
        ConnectorY.WebserviceY.GetCustomersResponse GetCustomers(ConnectorY.WebserviceY.GetCustomersRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetCustomersRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetCustomers", Namespace="http://WebServiceY.nl/", Order=0)]
        public ConnectorY.WebserviceY.GetCustomersRequestBody Body;
        
        public GetCustomersRequest() {
        }
        
        public GetCustomersRequest(ConnectorY.WebserviceY.GetCustomersRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class GetCustomersRequestBody {
        
        public GetCustomersRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetCustomersResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetCustomersResponse", Namespace="http://WebServiceY.nl/", Order=0)]
        public ConnectorY.WebserviceY.GetCustomersResponseBody Body;
        
        public GetCustomersResponse() {
        }
        
        public GetCustomersResponse(ConnectorY.WebserviceY.GetCustomersResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://WebServiceY.nl/")]
    public partial class GetCustomersResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ConnectorY.WebserviceY.Customer[] GetCustomersResult;
        
        public GetCustomersResponseBody() {
        }
        
        public GetCustomersResponseBody(ConnectorY.WebserviceY.Customer[] GetCustomersResult) {
            this.GetCustomersResult = GetCustomersResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WebServiceYSoapChannel : ConnectorY.WebserviceY.WebServiceYSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WebServiceYSoapClient : System.ServiceModel.ClientBase<ConnectorY.WebserviceY.WebServiceYSoap>, ConnectorY.WebserviceY.WebServiceYSoap {
        
        public WebServiceYSoapClient() {
        }
        
        public WebServiceYSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WebServiceYSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebServiceYSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebServiceYSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ConnectorY.WebserviceY.GetCustomersResponse ConnectorY.WebserviceY.WebServiceYSoap.GetCustomers(ConnectorY.WebserviceY.GetCustomersRequest request) {
            return base.Channel.GetCustomers(request);
        }
        
        public ConnectorY.WebserviceY.Customer[] GetCustomers() {
            ConnectorY.WebserviceY.GetCustomersRequest inValue = new ConnectorY.WebserviceY.GetCustomersRequest();
            inValue.Body = new ConnectorY.WebserviceY.GetCustomersRequestBody();
            ConnectorY.WebserviceY.GetCustomersResponse retVal = ((ConnectorY.WebserviceY.WebServiceYSoap)(this)).GetCustomers(inValue);
            return retVal.Body.GetCustomersResult;
        }
    }
}
