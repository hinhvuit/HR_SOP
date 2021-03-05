﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace HR_SOP.HRData {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="YNSafetySoap", Namespace="http://10.224.52.12:2002/")]
    public partial class YNSafety : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private SoapHead soapHeadValueField;
        
        private System.Threading.SendOrPostCallback UserValidateOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetEmpInfo_DtOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetEmpInfo_JsonOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public YNSafety() {
            this.Url = global::HR_SOP.Properties.Settings.Default.HR_SOP_HRData_YNSafety;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public SoapHead SoapHeadValue {
            get {
                return this.soapHeadValueField;
            }
            set {
                this.soapHeadValueField = value;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event UserValidateCompletedEventHandler UserValidateCompleted;
        
        /// <remarks/>
        public event GetEmpInfo_DtCompletedEventHandler GetEmpInfo_DtCompleted;
        
        /// <remarks/>
        public event GetEmpInfo_JsonCompletedEventHandler GetEmpInfo_JsonCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SoapHeadValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://10.224.52.12:2002/UserValidate", RequestNamespace="http://10.224.52.12:2002/", ResponseNamespace="http://10.224.52.12:2002/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool UserValidate() {
            object[] results = this.Invoke("UserValidate", new object[0]);
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void UserValidateAsync() {
            this.UserValidateAsync(null);
        }
        
        /// <remarks/>
        public void UserValidateAsync(object userState) {
            if ((this.UserValidateOperationCompleted == null)) {
                this.UserValidateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUserValidateOperationCompleted);
            }
            this.InvokeAsync("UserValidate", new object[0], this.UserValidateOperationCompleted, userState);
        }
        
        private void OnUserValidateOperationCompleted(object arg) {
            if ((this.UserValidateCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UserValidateCompleted(this, new UserValidateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SoapHeadValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://10.224.52.12:2002/GetEmpInfo_Dt", RequestNamespace="http://10.224.52.12:2002/", ResponseNamespace="http://10.224.52.12:2002/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable GetEmpInfo_Dt(string empNo) {
            object[] results = this.Invoke("GetEmpInfo_Dt", new object[] {
                        empNo});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void GetEmpInfo_DtAsync(string empNo) {
            this.GetEmpInfo_DtAsync(empNo, null);
        }
        
        /// <remarks/>
        public void GetEmpInfo_DtAsync(string empNo, object userState) {
            if ((this.GetEmpInfo_DtOperationCompleted == null)) {
                this.GetEmpInfo_DtOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetEmpInfo_DtOperationCompleted);
            }
            this.InvokeAsync("GetEmpInfo_Dt", new object[] {
                        empNo}, this.GetEmpInfo_DtOperationCompleted, userState);
        }
        
        private void OnGetEmpInfo_DtOperationCompleted(object arg) {
            if ((this.GetEmpInfo_DtCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetEmpInfo_DtCompleted(this, new GetEmpInfo_DtCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SoapHeadValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://10.224.52.12:2002/GetEmpInfo_Json", RequestNamespace="http://10.224.52.12:2002/", ResponseNamespace="http://10.224.52.12:2002/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetEmpInfo_Json(string empNo) {
            object[] results = this.Invoke("GetEmpInfo_Json", new object[] {
                        empNo});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetEmpInfo_JsonAsync(string empNo) {
            this.GetEmpInfo_JsonAsync(empNo, null);
        }
        
        /// <remarks/>
        public void GetEmpInfo_JsonAsync(string empNo, object userState) {
            if ((this.GetEmpInfo_JsonOperationCompleted == null)) {
                this.GetEmpInfo_JsonOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetEmpInfo_JsonOperationCompleted);
            }
            this.InvokeAsync("GetEmpInfo_Json", new object[] {
                        empNo}, this.GetEmpInfo_JsonOperationCompleted, userState);
        }
        
        private void OnGetEmpInfo_JsonOperationCompleted(object arg) {
            if ((this.GetEmpInfo_JsonCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetEmpInfo_JsonCompleted(this, new GetEmpInfo_JsonCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://10.224.52.12:2002/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://10.224.52.12:2002/", IsNullable=false)]
    public partial class SoapHead : System.Web.Services.Protocols.SoapHeader {
        
        private string userNameField;
        
        private string passwordField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        public string UserName {
            get {
                return this.userNameField;
            }
            set {
                this.userNameField = value;
            }
        }
        
        /// <remarks/>
        public string Password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void UserValidateCompletedEventHandler(object sender, UserValidateCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UserValidateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UserValidateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void GetEmpInfo_DtCompletedEventHandler(object sender, GetEmpInfo_DtCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetEmpInfo_DtCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetEmpInfo_DtCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataTable Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void GetEmpInfo_JsonCompletedEventHandler(object sender, GetEmpInfo_JsonCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetEmpInfo_JsonCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetEmpInfo_JsonCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591