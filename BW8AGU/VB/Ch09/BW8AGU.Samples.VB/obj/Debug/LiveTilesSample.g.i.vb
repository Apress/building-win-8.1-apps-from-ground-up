﻿

#ExternalChecksum("C:\Projects\W8B\VB\CH09\BW8AGU.Samples.VB\BW8AGU.Samples.VB\LiveTilesSample.xaml", "{406ea660-64cf-4c82-b6f0-42d48172a799}", "4E29DDB6494690E2F486CBC2F99D1ADE")
'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On


Namespace Global.BW8AGU.Samples.VB

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  _
    Partial Class LiveTilesSample
        Inherits Global.Windows.UI.Xaml.Controls.Page

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")>  _
        private WithEvents pageRoot As Global.Windows.UI.Xaml.Controls.Page
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")>  _
        private WithEvents templateNameTextBox As Global.Windows.UI.Xaml.Controls.TextBox
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")>  _
        private WithEvents xmlTemplateNameTextBox As Global.Windows.UI.Xaml.Controls.TextBox
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")>  _
        private WithEvents xmlResultNameTextBox As Global.Windows.UI.Xaml.Controls.TextBox
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")>  _
        private WithEvents backButton As Global.Windows.UI.Xaml.Controls.Button
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")>  _
        private WithEvents pageTitle As Global.Windows.UI.Xaml.Controls.TextBlock

        Private _contentLoaded As Boolean

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")>  _
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub InitializeComponent()
            If _contentLoaded Then
                Return
            End If
            _contentLoaded = true

            Dim uri As New Global.System.Uri("ms-appx:///LiveTilesSample.xaml")
            Global.Windows.UI.Xaml.Application.LoadComponent(Me, uri, Global.Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application)

            pageRoot = CType(Me.FindName("pageRoot"), Global.Windows.UI.Xaml.Controls.Page)
            templateNameTextBox = CType(Me.FindName("templateNameTextBox"), Global.Windows.UI.Xaml.Controls.TextBox)
            xmlTemplateNameTextBox = CType(Me.FindName("xmlTemplateNameTextBox"), Global.Windows.UI.Xaml.Controls.TextBox)
            xmlResultNameTextBox = CType(Me.FindName("xmlResultNameTextBox"), Global.Windows.UI.Xaml.Controls.TextBox)
            backButton = CType(Me.FindName("backButton"), Global.Windows.UI.Xaml.Controls.Button)
            pageTitle = CType(Me.FindName("pageTitle"), Global.Windows.UI.Xaml.Controls.TextBlock)
        End Sub
    End Class

End Namespace


