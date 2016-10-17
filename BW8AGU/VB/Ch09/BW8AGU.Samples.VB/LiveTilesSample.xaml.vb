Imports Windows.UI.Notifications
Imports Windows.Data.Xml.Dom

' The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

''' <summary>
''' A basic page that provides characteristics common to most applications.
''' </summary>
Public NotInheritable Class LiveTilesSample
    Inherits Page



    ''' <summary>
    ''' NavigationHelper is used on each page to aid in navigation and 
    ''' process lifetime management
    ''' </summary>
    Public ReadOnly Property NavigationHelper As Common.NavigationHelper
        Get
            Return Me._navigationHelper
        End Get
    End Property
    Private _navigationHelper As Common.NavigationHelper

    ''' <summary>
    ''' This can be changed to a strongly typed view model.
    ''' </summary>
    Public ReadOnly Property DefaultViewModel As Common.ObservableDictionary
        Get
            Return Me._defaultViewModel
        End Get
    End Property
    Private _defaultViewModel As New Common.ObservableDictionary()

    Private templatesName As New ObservableCollection(Of String)()
    Private selectedTemplate As TileTemplateType

    Public Sub New()

        InitializeComponent()
        Me._navigationHelper = New Common.NavigationHelper(Me)
        AddHandler Me._navigationHelper.LoadState, AddressOf NavigationHelper_LoadState
        AddHandler Me._navigationHelper.SaveState, AddressOf NavigationHelper_SaveState
    End Sub

    ''' <summary>
    ''' Populates the page with content passed during navigation.  Any saved state is also
    ''' provided when recreating a page from a prior session.
    ''' </summary>
    ''' <param name="sender">
    ''' The source of the event; typically <see cref="NavigationHelper"/>
    ''' </param>
    ''' <param name="e">Event data that provides both the navigation parameter passed to
    ''' <see cref="Frame.Navigate"/> when this page was initially requested and
    ''' a dictionary of state preserved by this page during an earlier
    ''' session.  The state will be null the first time a page is visited.</param>
    Private Sub NavigationHelper_LoadState(sender As Object, e As Common.LoadStateEventArgs)
        For Each item In [Enum].GetValues(GetType(TileTemplateType))
            templatesName.Add(item.ToString())
        Next
        DefaultViewModel("templates") = templatesName
        DefaultViewModel("UseQueue") = False
    End Sub

    ''' <summary>
    ''' Preserves state associated with this page in case the application is suspended or the
    ''' page is discarded from the navigation cache.  Values must conform to the serialization
    ''' requirements of <see cref="Common.SuspensionManager.SessionState"/>.
    ''' </summary>
    ''' <param name="sender">
    ''' The source of the event; typically <see cref="NavigationHelper"/>
    ''' </param>
    ''' <param name="e">Event data that provides an empty dictionary to be populated with 
    ''' serializable state.</param>
    Private Sub NavigationHelper_SaveState(sender As Object, e As Common.SaveStateEventArgs)

    End Sub

#Region "NavigationHelper registration"

    ''' The methods provided in this section are simply used to allow
    ''' NavigationHelper to respond to the page's navigation methods.
    ''' 
    ''' Page specific logic should be placed in event handlers for the  
    ''' <see cref="Common.NavigationHelper.LoadState"/>
    ''' and <see cref="Common.NavigationHelper.SaveState"/>.
    ''' The navigation parameter is available in the LoadState method 
    ''' in addition to page state preserved during an earlier session.

    Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
        _navigationHelper.OnNavigatedTo(e)
    End Sub

    Protected Overrides Sub OnNavigatedFrom(e As NavigationEventArgs)
        _navigationHelper.OnNavigatedFrom(e)
    End Sub

#End Region


    Private Sub ComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)

        If e.AddedItems.Count > 0 Then
            Dim templateName As String = e.AddedItems(0).ToString()
            selectedTemplate = DirectCast([Enum].Parse(GetType(TileTemplateType), templateName), TileTemplateType)
            Dim template = TileUpdateManager.GetTemplateContent(selectedTemplate)
            templateNameTextBox.Text = templateName
            Dim xmlContent As String = template.GetXml()
            Dim xDocumentForXml = XDocument.Parse(xmlContent)
            xmlTemplateNameTextBox.Text = xDocumentForXml.ToString()


            Dim textNodes = xDocumentForXml.Descendants(XName.[Get]("text"))
            Dim imageNodes = xDocumentForXml.Descendants(XName.[Get]("image"))
            For Each item As XElement In textNodes
                item.Value = String.Format("text {0}", item.Attribute(XName.[Get]("id")))
            Next
            For Each item As XElement In imageNodes
                item.Attribute(XName.[Get]("src")).Value = "Assets\picture.png"
            Next
            xmlResultNameTextBox.Text = xDocumentForXml.ToString()
        End If

    End Sub

    Private Sub ShowTestNotification(sender As Object, e As RoutedEventArgs)

        Dim document As New XmlDocument()

        document.LoadXml(xmlResultNameTextBox.Text)
        Dim updater = TileUpdateManager.CreateTileUpdaterForApplication()
        updater.Clear()
        updater.Update(New TileNotification(document))
    End Sub


End Class
