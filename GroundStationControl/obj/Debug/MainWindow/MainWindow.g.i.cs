﻿#pragma checksum "..\..\..\MainWindow\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "486819C9FE4637CCB764AF8BB3AF5D6F74C18F65BB37D8CAC159BCD7CCFDD442"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using GroundStationControl;
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


namespace GroundStationControl {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\MainWindow\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ArrowsGrid;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\MainWindow\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RightArrow;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\MainWindow\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LeftArrow;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\MainWindow\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button UpArrow;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\MainWindow\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DownArrow;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\MainWindow\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MainText;
        
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
            System.Uri resourceLocater = new System.Uri("/GroundStationControl;component/mainwindow/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainWindow\MainWindow.xaml"
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
            
            #line 8 "..\..\..\MainWindow\MainWindow.xaml"
            ((GroundStationControl.MainWindow)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.OnKeyDown);
            
            #line default
            #line hidden
            
            #line 8 "..\..\..\MainWindow\MainWindow.xaml"
            ((GroundStationControl.MainWindow)(target)).KeyUp += new System.Windows.Input.KeyEventHandler(this.OnKeyUp);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ArrowsGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.RightArrow = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.LeftArrow = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.UpArrow = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.DownArrow = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.MainText = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

