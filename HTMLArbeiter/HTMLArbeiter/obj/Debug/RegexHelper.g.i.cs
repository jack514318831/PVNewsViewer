﻿#pragma checksum "..\..\RegexHelper.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0543D335ACBBEA2C5D2EFAD159BBF888"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using HTMLArbeiter;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace HTMLArbeiter {
    
    
    /// <summary>
    /// RegexHelper
    /// </summary>
    public partial class RegexHelper : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\RegexHelper.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtInput;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\RegexHelper.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtUrl;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\RegexHelper.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtRegex;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\RegexHelper.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtHtml;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\RegexHelper.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMatch;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\RegexHelper.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGetResponse;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\RegexHelper.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnWebClient;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/HTMLArbeiter;component/regexhelper.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\RegexHelper.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.txtInput = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.txtUrl = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtRegex = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtHtml = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.btnMatch = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\RegexHelper.xaml"
            this.btnMatch.Click += new System.Windows.RoutedEventHandler(this.btnMatch_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnGetResponse = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\RegexHelper.xaml"
            this.btnGetResponse.Click += new System.Windows.RoutedEventHandler(this.btnGetResponse_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnWebClient = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\RegexHelper.xaml"
            this.btnWebClient.Click += new System.Windows.RoutedEventHandler(this.btnWebClient_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
