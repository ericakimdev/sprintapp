﻿#pragma checksum "..\..\..\Pages\UpdateProjectPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "346F4BB5FC809FFBAEC5BEA6C36660802CEEC1013DED8B020F769A72245E0B1B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using SprintApp.Pages;
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


namespace SprintApp.Pages {
    
    
    /// <summary>
    /// UpdateProjectPage
    /// </summary>
    public partial class UpdateProjectPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\Pages\UpdateProjectPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox projectNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Pages\UpdateProjectPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox managerTextBox;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Pages\UpdateProjectPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox clientTextBox;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Pages\UpdateProjectPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox startDateTextBox;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Pages\UpdateProjectPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox dueDateTextBox;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Pages\UpdateProjectPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox devComboBox;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Pages\UpdateProjectPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox membersListBox;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Pages\UpdateProjectPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox projectsComboBox;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Pages\UpdateProjectPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock projectIdTextBox;
        
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
            System.Uri resourceLocater = new System.Uri("/SprintApp;component/pages/updateprojectpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\UpdateProjectPage.xaml"
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
            this.projectNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.managerTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.clientTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.startDateTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.dueDateTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.devComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.membersListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 8:
            
            #line 29 "..\..\..\Pages\UpdateProjectPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddDev_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 30 "..\..\..\Pages\UpdateProjectPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ClearList_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 32 "..\..\..\Pages\UpdateProjectPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.UpdateProject_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.projectsComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 33 "..\..\..\Pages\UpdateProjectPage.xaml"
            this.projectsComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ProjectCB_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 34 "..\..\..\Pages\UpdateProjectPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteProject_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.projectIdTextBox = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

