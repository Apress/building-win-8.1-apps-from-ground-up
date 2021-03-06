﻿Imports Windows.Storage.Pickers

' The Item Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232

''' <summary>
''' A page that displays details for a single item within a group while allowing
''' gestures to flip through other items belonging to the same group.
''' </summary>
Public NotInheritable Class UserData
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


    Public Sub New()
        InitializeComponent()
        Me._navigationHelper = New Common.NavigationHelper(Me)
        AddHandler Me._navigationHelper.LoadState,
            AddressOf NavigationHelper_LoadState
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

#Region "User Data Sample"

    Private Async Sub SaveDataButton_Click(sender As Object, e As RoutedEventArgs)
        Dim userFile As StorageFile =
            Await KnownFolders.PicturesLibrary.CreateFileAsync("userdata.txt",
                                                               CreationCollisionOption.ReplaceExisting)
        Await FileIO.WriteTextAsync(userFile, UserDataTextBox.Text)
    End Sub

    Private Async Sub GetDataButton_Click(sender As Object, e As RoutedEventArgs)
        Try
            Dim userFile As StorageFile = Await KnownFolders.PicturesLibrary.GetFileAsync("userdata.txt")
            StoredData.Text = Await FileIO.ReadTextAsync(userFile)
        Catch
            Throw New Exception("Data not found!")
        End Try
    End Sub

    Public Async Sub GetFile()
        Dim openPicker As New FileOpenPicker()
        openPicker.ViewMode = PickerViewMode.List
        openPicker.SuggestedStartLocation = PickerLocationId.Desktop
        openPicker.FileTypeFilter.Add(".txt")
        Dim file As StorageFile = Await openPicker.PickSingleFileAsync()

        'do something
        If file IsNot Nothing Then
        End If
    End Sub

    Public Async Sub GetFolder()
        Dim openPicker As New FolderPicker()
        openPicker.ViewMode = PickerViewMode.List
        openPicker.SuggestedStartLocation = PickerLocationId.Desktop
        openPicker.FileTypeFilter.Add(".txt")
        Dim folder As StorageFolder = Await openPicker.PickSingleFolderAsync()

        'do something
        If folder IsNot Nothing Then
        End If
    End Sub

    Private Sub OpenFilePickerButton_Click(sender As Object, e As RoutedEventArgs)
        GetFile()
    End Sub

    Private Sub OpenFolderPickerButton_Click(sender As Object, e As RoutedEventArgs)
        GetFolder()
    End Sub

#End Region

End Class
