' The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

Imports System.Text.RegularExpressions
Imports Windows.ApplicationModel.Resources
Imports Windows.Phone.UI.Input
Imports Windows.UI
Imports Windows.UI.Core

''' <summary>
''' An empty page that can be used on its own or navigated to within a Frame.
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page

    Dim AppName As String = Package.Current.DisplayName

    Async Sub BackPressed(sender As Object, e As BackPressedEventArgs)
        'Handles any Back button presses.
        e.Handled = True
        If SlimBookUWPWebView.CanGoBack Then
            SlimBookUWPWebView.GoBack()
        Else
            Await displayMessageAsync(AppName, "Are you sure you want to exit the app?", "")
        End If
    End Sub

    Public Sub CLOSE_SB()
        SIDEBAR.Background = New SolidColorBrush(Color.FromArgb(255, 59, 89, 152))
        SIDEBAR.Width = 50
        IS_SIDEBAR_OPEN = False
    End Sub

    Public Sub DisplayPlatform()

        Dim platformFamily = Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily

        If platformFamily = "Windows.Desktop" Then
            Is_Desktop = True
        Else
            Is_Desktop = False
        End If
    End Sub

    Protected Overrides Sub OnNavigatedTo(ByVal e As NavigationEventArgs)
        If SetFullScreen = "0" Then
            View.ExitFullScreenMode()
        Else
            View.TryEnterFullScreenMode()
        End If
        ChangeSize()
        MyBase.OnNavigatedTo(e)
    End Sub

    Private Sub ABOUT_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles ABOUT.Tapped
        OPTIONS(4)
    End Sub

    Private Sub BACK_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles BACK.Tapped
        OPTIONS(3)
    End Sub

    Private Sub CB_ABOUT_Click(sender As Object, e As RoutedEventArgs) Handles CB_ABOUT.Click
        OPTIONS(4)
    End Sub

    Private Sub CB_BACK_Click(sender As Object, e As RoutedEventArgs) Handles CB_BACK.Click
        OPTIONS(3)
    End Sub

    Private Sub CB_GITHUB_Click(sender As Object, e As RoutedEventArgs) Handles CB_GITHUB.Click
        OPTIONS(5)
    End Sub

    Private Sub CB_HOME_Click(sender As Object, e As RoutedEventArgs) Handles CB_HOME.Click
        OPTIONS(1)
    End Sub

    Private Sub CB_SETTINGS_Click(sender As Object, e As RoutedEventArgs) Handles CB_SETTINGS.Click
        OPTIONS(6)
    End Sub

    Private Sub CB_TOP_Click(sender As Object, e As RoutedEventArgs) Handles CB_TOP.Click
        OPTIONS(2)
    End Sub

    Private Sub ChangeSize()
        Dim bounds = ApplicationView.GetForCurrentView().VisibleBounds
        Dim scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel
        Dim size = New Size(bounds.Width * scaleFactor, bounds.Height * scaleFactor)
        CLOSEALL()
        If Not Is_Desktop Then
            If size.Width > size.Height Then
                PORTRAIT = False
                CommBar.Visibility = Visibility.Collapsed
                SIDEBAR.Visibility = Visibility.Visible
                SlimBookUWPWebView.Margin = New Thickness(SIDEBAR.Width, 0, 0, 0)
            Else
                PORTRAIT = True
                CommBar.Visibility = Visibility.Visible
                SIDEBAR.Visibility = Visibility.Collapsed
                SlimBookUWPWebView.Margin = New Thickness(0, 0, 0, CommBar.ActualHeight)
            End If
        End If
    End Sub

    Private Sub CLOSEALL()
        CLOSE_SB()
        G_SETTINGS.Visibility = Visibility.Collapsed
    End Sub

    Private Sub CS_Click(sender As Object, e As RoutedEventArgs) Handles CS.Click
        G_SETTINGS.Visibility = Visibility.Collapsed
    End Sub

    Private Sub GITHUB_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles GITHUB.Tapped
        OPTIONS(5)
    End Sub

    Private Sub Go_Home()
        Dim mwv As Uri 'Contains the source URL for Facebook Touch
        mwv = New Uri(MyWebViewSource & "?sk=h_chr")
        SlimBookUWPWebView.Navigate(New Uri(MyWebViewSource))
    End Sub

    Private Sub HOME_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles HOME.Tapped
        OPTIONS(1)
    End Sub

    Private Sub MainPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        CLOSEALL()
        Me.InitializeComponent()
        AddHandler HardwareButtons.BackPressed, AddressOf BackPressed
        DisplayPlatform()
        If Is_Desktop Then
            CommBar.Visibility = Visibility.Collapsed
            SIDEBAR.Visibility = Visibility.Visible
            SlimBookUWPWebView.Margin = New Thickness(SIDEBAR.Width, 0, 0, 0)
        Else
            CommBar.Visibility = Visibility.Visible
            SIDEBAR.Visibility = Visibility.Collapsed
            SlimBookUWPWebView.Margin = New Thickness(0, 0, 0, CommBar.ActualHeight)
        End If

        If SetFullScreen Is Nothing Then
            localSettings.Values("FullScreen") = "0"
        Else
            If SetFullScreen = "0" Then
                View.ExitFullScreenMode()
            Else
                View.TryEnterFullScreenMode()
            End If
        End If
        If HideAds Is Nothing Then
            localSettings.Values("Hide_Ads") = "0"
        Else
            If HideAds = "0" Then
            End If
        End If
        If LockTopBar Is Nothing Then
            localSettings.Values("LockTopBar") = "0"
        Else
            If LockTopBar = "0" Then
            End If
        End If
        ChangeSize()
        Go_Home()
        AddHandler SystemNavigationManager.GetForCurrentView().BackRequested, Sub(s, a)

                                                                                  If SlimBookUWPWebView.CanGoBack Then
                                                                                      SlimBookUWPWebView.GoBack()
                                                                                      a.Handled = True
                                                                                  End If
                                                                              End Sub
    End Sub

    Private Sub MainPage_Loading(sender As FrameworkElement, args As Object) Handles Me.Loading
        If SetFullScreen = "0" Then
            View.ExitFullScreenMode()
        Else
            View.TryEnterFullScreenMode()
        End If
        ChangeSize()
    End Sub

    Private Sub MainPage_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles Me.SizeChanged
        ChangeSize()
    End Sub

    Private Sub OPEN_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles OPEN.Tapped
        If IS_SIDEBAR_OPEN Then
            CLOSE_SB()
        Else
            SIDEBAR.Background = New SolidColorBrush(Color.FromArgb(191, 59, 89, 152))
            SIDEBAR.Width = 220
            IS_SIDEBAR_OPEN = True
        End If
    End Sub

    Private Async Sub OPTIONS(ByVal x As Integer)
        Select Case x
            Case 1  'HOME
                SlimBookUWPWebView.Source = New Uri("https://touch.facebook.com/home.php")
            Case 2  'TOP
                Dim ScrollToTopString = "var int = setInterval(function(){window.scrollBy(0, -36); if( window.pageYOffset === 0 ) clearInterval(int); }, 0.1);"
                Await SlimBookUWPWebView.InvokeScriptAsync("eval", New String() {ScrollToTopString})
            Case 3  'BACK
                If SlimBookUWPWebView.CanGoBack Then
                    SlimBookUWPWebView.GoBack()
                Else
                    Await displayMessageAsync("Quit SlimBook UWP", "Are you sure you want to quit the app?", "")
                End If
            Case 4  'ABOUT
                CLOSEALL()
                SettingsSetup(0)
            Case 5
                Dim logoURL = New Uri("https://github.com/CelestialDoom/SlimBook-UWP")
                Await Windows.System.Launcher.LaunchUriAsync(logoURL)
            Case 6
                CLOSEALL()
                SettingsSetup(1)
        End Select
    End Sub

    Private Async Sub QUIT_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles QUIT.Tapped
        Await displayMessageAsync("Quit SlimBook UWP", "Are you sure you want to quit the app?", "")
    End Sub

    Private Sub SETTINGS_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles SETTINGS.Tapped
        OPTIONS(6)
    End Sub

    Private Sub SettingsSetup(ByVal x As Integer)
        Dim number As PackageVersion = Package.Current.Id.Version
        Select Case x
            Case 0
                PivotSettingsAbout.SelectedIndex = 0
            Case 1
                PivotSettingsAbout.SelectedIndex = 3
        End Select
        AN.Text = AppName
        version.Text = String.Format(" {0}.{1}.{2}" & vbCrLf, number.Major, number.Minor, number.Build)
        privacy.Text = PrivacyInfo
        SVC.ChangeView(Nothing, 0, Nothing, True)
        SVP.ChangeView(Nothing, 0, Nothing, True)
        SVS.ChangeView(Nothing, 0, Nothing, True)
        If Is_Desktop Then
            G_SETTINGS.Margin = New Thickness((SIDEBAR.Width + 10), 10, 10, 10)
        Else
            If Not PORTRAIT Then
                G_SETTINGS.Margin = New Thickness((SIDEBAR.Width + 10), 10, 10, 10)
            Else
                G_SETTINGS.Margin = New Thickness(10, 10, 10, (CommBar.ActualHeight + 10))
            End If
        End If
        G_SETTINGS.Visibility = Visibility.Visible
    End Sub

    Private Sub SlimBookUWPWebView_GotFocus(sender As Object, e As RoutedEventArgs) Handles SlimBookUWPWebView.GotFocus
        CLOSEALL()
    End Sub

    Private Async Sub SlimBookUWPWebView_LoadCompleted(sender As Object, e As NavigationEventArgs) Handles SlimBookUWPWebView.LoadCompleted
        Dim cssToApply As String = ""
        If LockTopBar = "1" Then
            cssToApply += "#header {position: fixed; z-index: 12; top: 0px;} #root {padding-top: 44px;} .item.more {position:fixed; bottom: 0px; text-align: center !important;}"
        End If
        Dim h = ApplicationView.GetForCurrentView().VisibleBounds.Height - 44
        Dim density As Single = DisplayInformation.GetForCurrentView().LogicalDpi
        Dim barHeight As Integer = CInt((h / density))
        cssToApply += ".flyout {max-height:" & barHeight & "px; overflow-y:scroll;}"
        If HideAds = "1" Then
            cssToApply += "#m_newsfeed_stream article[data-ft*=""\\""ei\\"":\\""""] {display:none !important;}"
        End If
        Await SlimBookUWPWebView.InvokeScriptAsync("eval", {"javascript:function addStyleString(str) { var node = document.createElement('style'); node.innerHTML = " & "str; document.body.appendChild(node); } addStyleString('" & cssToApply & "');"})
    End Sub

    Private Sub SlimBookUWPWebView_NavigationFailed(sender As Object, e As WebViewNavigationFailedEventArgs) Handles SlimBookUWPWebView.NavigationFailed
        Dim loader = New ResourceLoader()
        Dim noConnection As String = loader.GetString("noConnection")
        SlimBookUWPWebView.NavigateToString(noConnection)
    End Sub

    Private Sub SlimBookUWPWebView_NewWindowRequested(sender As WebView, args As WebViewNewWindowRequestedEventArgs) Handles SlimBookUWPWebView.NewWindowRequested
        If args.Uri.AbsoluteUri.Contains(".gif") OrElse args.Uri.AbsoluteUri.Contains("video") Then
            SlimBookUWPWebView.Navigate(args.Uri)
            args.Handled = True
        End If
    End Sub

    Private Sub togg_Ads_Toggled(sender As Object, e As RoutedEventArgs) Handles togg_Ads.Toggled
        Dim toggleSwitch As ToggleSwitch = TryCast(sender, ToggleSwitch)
        If toggleSwitch IsNot Nothing Then
            If toggleSwitch.IsOn = True Then
                localSettings.Values("Hide_Ads") = "1"
            Else
                localSettings.Values("Hide_Ads") = "0"
            End If
        End If
    End Sub

    Private Sub togg_FS_Toggled(sender As Object, e As RoutedEventArgs) Handles togg_FS.Toggled
        Dim toggleSwitch As ToggleSwitch = TryCast(sender, ToggleSwitch)
        If toggleSwitch IsNot Nothing Then
            If toggleSwitch.IsOn = True Then
                View.TryEnterFullScreenMode()
                localSettings.Values("FullScreen") = "1"
            Else
                View.ExitFullScreenMode()
                localSettings.Values("FullScreen") = "0"
            End If
        End If
    End Sub

    Private Sub togg_TB_Toggled(sender As Object, e As RoutedEventArgs) Handles togg_TB.Toggled
        Dim toggleSwitch As ToggleSwitch = TryCast(sender, ToggleSwitch)
        If toggleSwitch IsNot Nothing Then
            If toggleSwitch.IsOn = True Then
                localSettings.Values("LockTopBar") = "1"
            Else
                localSettings.Values("LockTopBar") = "0"
            End If
        End If
    End Sub

    Private Sub TOP_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles TOP.Tapped
        OPTIONS(2)
    End Sub

End Class