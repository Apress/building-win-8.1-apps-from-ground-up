Imports System.Globalization

' The Item Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232

''' <summary>
''' A page that displays details for a single item within a group while allowing
''' gestures to flip through other items belonging to the same group.
''' </summary>
Public NotInheritable Class LocalData
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

#Region "Local Data Sample"
    Public Property CurrentCulture() As CultureInfo
        Get
            Dim localSettings = Windows.Storage.ApplicationData.Current.LocalSettings
            Dim culture As CultureInfo

            If localSettings.Containers.ContainsKey("localization") Then
                Dim cultureName = TryCast(localSettings.Containers("localization").Values("language"), [String])
                culture = New CultureInfo(cultureName)
            Else
                culture = CultureInfo.CurrentCulture
            End If

            Return culture
        End Get
        Set(value As CultureInfo)
            Dim localSettings = Windows.Storage.ApplicationData.Current.LocalSettings

            If Not localSettings.Containers.ContainsKey("localization") Then
                'Create a new container
                localSettings.CreateContainer("localization",
                                              Windows.Storage.ApplicationDataCreateDisposition.Always)
            End If

            localSettings.Containers("localization").Values("language") = value.Name
        End Set
    End Property

    Public Property CurrentCultureDetailed() As CultureInfo
        Get
            Dim localSettings = Windows.Storage.ApplicationData.Current.LocalSettings

            If localSettings.Containers.ContainsKey("detailedLocalization") Then
                Dim composite As ApplicationDataCompositeValue =
                    DirectCast(localSettings.Containers("detailedLocalization").Values("language"), 
                        ApplicationDataCompositeValue)

                Return If((composite IsNot Nothing), New CultureInfo(composite("selectedLanguage").ToString()),
                          CultureInfo.CurrentCulture)
            Else
                Return New CultureInfo("en-US")
            End If
        End Get
        Set(value As CultureInfo)
            Dim localSettings = Windows.Storage.ApplicationData.Current.LocalSettings

            If Not localSettings.Containers.ContainsKey("detailedLocalization") Then
                'Create a container
                localSettings.CreateContainer("detailedLocalization",
                                              Windows.Storage.ApplicationDataCreateDisposition.Always)
            End If

            Dim composite As New ApplicationDataCompositeValue()
            composite("selectedLanguage") = value.DisplayName
            composite("lastChangeTime") = DateTime.Now.ToString()

            localSettings.Containers("detailedLocalization").Values("language") = composite
        End Set
    End Property

    Protected Sub GetCompositeData()
        Dim localSettings = Windows.Storage.ApplicationData.Current.LocalSettings

        If localSettings.Containers.ContainsKey("detailedLocalization") Then
            Dim composite As ApplicationDataCompositeValue = DirectCast(localSettings.Containers("detailedLocalization").Values("language"), ApplicationDataCompositeValue)

            LocalSettingsCompositeLanguage.Text = composite("selectedLanguage").ToString()
            LocalSettingsCompositeLastTimeChanged.Text = composite("lastChangeTime").ToString()
        End If
    End Sub

#End Region


    Public Sub New()
        InitializeComponent()
        Me._navigationHelper = New Common.NavigationHelper(Me)
        AddHandler Me._navigationHelper.LoadState,
            AddressOf NavigationHelper_LoadState

        'Local data - Single value
        LocalSettingsSingleValue.Text = CurrentCulture.DisplayName

        'Local data - Composite data 
        CurrentCultureDetailed = New CultureInfo("en-US")
        GetCompositeData()
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
    Private Async Sub NavigationHelper_LoadState(sender As Object, e As Common.LoadStateEventArgs)
        ' TODO: Create an appropriate data model for your problem domain to replace the sample data
        Dim item As Data.SampleDataItem = Await Data.SampleDataSource.GetItemAsync(DirectCast(e.NavigationParameter, String))
        Me.DefaultViewModel("Item") = item
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
End Class
