#pragma checksum "C:\Users\ASUS S550C\Desktop\Quran online v 2.2\Quran Online v1.2\mediaplayer\AddBackgroundTransfer.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "417214C194E050EE9B4DAB48E666B98D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace mediaplayer {
    
    
    public partial class AddBackgroundTransfer : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.CheckBox wifiOnlyCheckbox;
        
        internal System.Windows.Controls.TextBlock txtonlywifi;
        
        internal System.Windows.Controls.CheckBox externalPowerOnlyCheckbox;
        
        internal System.Windows.Controls.TextBlock txtExternal;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.ListBox ListAuther65;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/mediaplayer;component/AddBackgroundTransfer.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.wifiOnlyCheckbox = ((System.Windows.Controls.CheckBox)(this.FindName("wifiOnlyCheckbox")));
            this.txtonlywifi = ((System.Windows.Controls.TextBlock)(this.FindName("txtonlywifi")));
            this.externalPowerOnlyCheckbox = ((System.Windows.Controls.CheckBox)(this.FindName("externalPowerOnlyCheckbox")));
            this.txtExternal = ((System.Windows.Controls.TextBlock)(this.FindName("txtExternal")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.ListAuther65 = ((System.Windows.Controls.ListBox)(this.FindName("ListAuther65")));
        }
    }
}

