﻿

#ExternalChecksum("C:\Users\AntonioTuribbio\Source\Workspaces\BuildingWindows8AppsFromTheGroundUp\VB\BW8AGU.Samples.VB-Ch10\BW8AGU.Samples.VB\BW8AGU.Samples.VB\SectionPage.xaml", "{406ea660-64cf-4c82-b6f0-42d48172a799}", "1F7A7C1D387232B703482949EF0E84B5")
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
    Partial Class SectionPage
        Inherits Global.Windows.UI.Xaml.Controls.Page

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")>  _
        private WithEvents pageRoot As Global.Windows.UI.Xaml.Controls.Page
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")>  _
        private WithEvents itemsViewSource As Global.Windows.UI.Xaml.Data.CollectionViewSource
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")>  _
        private WithEvents itemGridView As Global.Windows.UI.Xaml.Controls.GridView
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks", "4.0.0.0")>  _
        private WithEvents backButton As Global.Windows.UI.Xaml.Controls.AppBarButton
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

            Dim uri As New Global.System.Uri("ms-appx:///SectionPage.xaml")
            Global.Windows.UI.Xaml.Application.LoadComponent(Me, uri, Global.Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application)

            pageRoot = CType(Me.FindName("pageRoot"), Global.Windows.UI.Xaml.Controls.Page)
            itemsViewSource = CType(Me.FindName("itemsViewSource"), Global.Windows.UI.Xaml.Data.CollectionViewSource)
            itemGridView = CType(Me.FindName("itemGridView"), Global.Windows.UI.Xaml.Controls.GridView)
            backButton = CType(Me.FindName("backButton"), Global.Windows.UI.Xaml.Controls.AppBarButton)
            pageTitle = CType(Me.FindName("pageTitle"), Global.Windows.UI.Xaml.Controls.TextBlock)
        End Sub
    End Class

End Namespace

