﻿#pragma checksum "..\..\ConfigPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "9C9466E0011ADCAAD0448E8B409B97BB9294A797CE479234842203D8C984620E"
//------------------------------------------------------------------------------
// <auto-generated>
//     這段程式碼是由工具產生的。
//     執行階段版本:4.0.30319.42000
//
//     對這個檔案所做的變更可能會造成錯誤的行為，而且如果重新產生程式碼，
//     變更將會遺失。
// </auto-generated>
//------------------------------------------------------------------------------

using HMI;
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


namespace HMI {
    
    
    /// <summary>
    /// ConfigPage
    /// </summary>
    public partial class ConfigPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\ConfigPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSortConfig;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\ConfigPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnChannelConfig;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\ConfigPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMtConfig;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\ConfigPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAlertConfig;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\ConfigPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMeasureConfig;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\ConfigPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnFlowConfig;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\ConfigPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Back;
        
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
            System.Uri resourceLocater = new System.Uri("/HMI;component/configpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ConfigPage.xaml"
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
            this.btnSortConfig = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\ConfigPage.xaml"
            this.btnSortConfig.Click += new System.Windows.RoutedEventHandler(this.btnSortConfig_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnChannelConfig = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\ConfigPage.xaml"
            this.btnChannelConfig.Click += new System.Windows.RoutedEventHandler(this.btnChannelConfig_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnMtConfig = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\ConfigPage.xaml"
            this.btnMtConfig.Click += new System.Windows.RoutedEventHandler(this.btnMtConfig_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnAlertConfig = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\ConfigPage.xaml"
            this.btnAlertConfig.Click += new System.Windows.RoutedEventHandler(this.btnAlertConfig_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnMeasureConfig = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\ConfigPage.xaml"
            this.btnMeasureConfig.Click += new System.Windows.RoutedEventHandler(this.btnMeasureConfig_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnFlowConfig = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\ConfigPage.xaml"
            this.btnFlowConfig.Click += new System.Windows.RoutedEventHandler(this.btnFlowConfig_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Back = ((System.Windows.Controls.Button)(target));
            
            #line 92 "..\..\ConfigPage.xaml"
            this.Back.Click += new System.Windows.RoutedEventHandler(this.Back_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

