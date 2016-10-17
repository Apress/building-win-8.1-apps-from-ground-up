Imports System.Globalization
Imports Windows.UI.Core

' The Item Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232

''' <summary>
''' A page that displays details for a single item within a group while allowing
''' gestures to flip through other items belonging to the same group.
''' </summary>
Public NotInheritable Class RoamingData
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


#Region "Roaming Data Example"
    Public Property CurrentCulture() As CultureInfo
        Get
            Dim roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings

            If roamingSettings.Containers.ContainsKey("localization") Then
                Dim cultureName =
                    TryCast(roamingSettings.Containers("localization").Values("language"), [String])
                Return If((cultureName IsNot Nothing),
                          New CultureInfo(cultureName), CultureInfo.CurrentCulture)
            End If

            Return New CultureInfo("en-US")
        End Get
        Set(value As CultureInfo)
            Dim roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings

            If Not roamingSettings.Containers.ContainsKey("localization") Then
                'Create a container
                roamingSettings.CreateContainer("localization",
                                                Windows.Storage.ApplicationDataCreateDisposition.Always)
            End If

            roamingSettings.Containers("localization").Values("language") = value.Name
        End Set
    End Property

    Public Property CurrentCultureDetailed() As CultureInfo
        Get
            Dim roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings

            If roamingSettings.Containers.ContainsKey("detailedLocalization") Then
                Dim composite As ApplicationDataCompositeValue =
                    DirectCast(roamingSettings.Containers("detailedLocalization").Values("language"), 
                        ApplicationDataCompositeValue)

                Return If((composite IsNot Nothing),
                          New CultureInfo(composite("selectedLanguage").ToString()), CultureInfo.CurrentCulture)
            Else
                Return New CultureInfo("en-US")
            End If
        End Get
        Set(value As CultureInfo)
            Dim roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings

            If Not roamingSettings.Containers.ContainsKey("detailedLocalization") Then
                'Create a container
                roamingSettings.CreateContainer("detailedLocalization",
                                                Windows.Storage.ApplicationDataCreateDisposition.Always)
            End If

            Dim composite As New ApplicationDataCompositeValue()
            composite("selectedLanguage") = value.DisplayName
            composite("lastChangeTime") = DateTime.Now.ToString()

            roamingSettings.Containers("detailedLocalization").Values("language") = composite
        End Set
    End Property

    Protected Sub GetCompositeData()
        Dim roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings

        If roamingSettings.Containers.ContainsKey("detailedLocalization") Then
            Dim composite As ApplicationDataCompositeValue = DirectCast(roamingSettings.Containers("detailedLocalization").Values("language"), ApplicationDataCompositeValue)

            RoamingSettingsCompositeLanguage.Text = composite("selectedLanguage").ToString()
            RoamingSettingsCompositeLastTimeChanged.Text = composite("lastChangeTime").ToString()
        End If
    End Sub

#End Region

    Public Sub New()
        InitializeComponent()
        Me._navigationHelper = New Common.NavigationHelper(Me)
        AddHandler Me._navigationHelper.LoadState,
            AddressOf NavigationHelper_LoadState

        'Get quota
        Dim quota = ApplicationData.Current.RoamingStorageQuota
        'Usually 100KB
        RoamingDataQuota.Text = [String].Format("{0} KB", quota.ToString())

        'Roaming data - Single value
        RoamingSettingsSingleValue.Text = CurrentCulture.DisplayName

        'Roaming data - Composite data 
        CurrentCultureDetailed = New CultureInfo("en-US")
        GetCompositeData()

        'Roaming Data - DataChanged Event Handler
        AddHandler Windows.Storage.ApplicationData.Current.DataChanged, AddressOf DataChangeHandler

        Dim roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings
        roamingSettings.Values("lastRoamingSettingDate") = DateTime.Now.ToString()
        Windows.Storage.ApplicationData.Current.SignalDataChanged()

        'Roaming Data - Check data version
        VersionHelper.CheckVersion()
        AppVersion.Text = VersionHelper.AppVersion.ToString()
    End Sub

    Private Async Sub DataChangeHandler(ByVal appData As Windows.Storage.ApplicationData, ByVal o As Object)
        ' DataChangeHandler may be invoked on a background thread, 
        ' so use the Dispatcher to invoke the UI-related code on the UI thread.

        Await Me.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                                     Sub()
                                         Dim roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings
                                         Dim lastSuspensionTime As DateTime =
                                             Convert.ToDateTime(roamingSettings.Values("lastRoamingSettingDate").ToString())

                                         DataChangedInvoked.Text = "Yes"
                                     End Sub)
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
