' The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' An empty page that can be used on its own or navigated to within a Frame.
''' </summary>
Public NotInheritable Class AboutSettings
    Inherits Page

    Public localSettings As Windows.Storage.ApplicationDataContainer = Windows.Storage.ApplicationData.Current.LocalSettings
    Public SetFullScreen As Object = localSettings.Values("FullScreen")

    Protected Overrides Sub OnNavigatedTo(ByVal e As NavigationEventArgs)
        If TypeOf e.Parameter Is String AndAlso Not String.IsNullOrWhiteSpace(CStr(e.Parameter)) Then
            PivotSettingsAbout.SelectedIndex = 3
        Else
            PivotSettingsAbout.SelectedIndex = 0
        End If

        MyBase.OnNavigatedTo(e)
    End Sub

    Private Sub AboutSettings_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SetFullScreen Is Nothing Then
            localSettings.Values("FullScreen") = "0"
        Else
            If SetFullScreen = "0" Then
                View.ExitFullScreenMode()
                togg_FS.IsOn = False
            Else
                View.TryEnterFullScreenMode()
                togg_FS.IsOn = True
            End If
        End If
        If HideAds Is Nothing Then
            localSettings.Values("Hide_Ads") = "0"
            togg_Ads.IsOn = False
        Else
            If HideAds = "0" Then
                togg_Ads.IsOn = False
            Else
                togg_Ads.IsOn = True
            End If
        End If
        If LockTopBar Is Nothing Then
            localSettings.Values("LockTopBar") = "0"
            togg_TB.IsOn = False
        Else
            If LockTopBar = "0" Then
                togg_TB.IsOn = False
            Else
                togg_TB.IsOn = True
            End If
        End If
        Dim number As PackageVersion = Package.Current.Id.Version
        version.Text = String.Format(" {0}.{1}.{2}" & vbCrLf, number.Major, number.Minor, number.Build)
        privacy.Text = PrivacyInfo
        SVC.ChangeView(Nothing, 0, Nothing, True)
        SVP.ChangeView(Nothing, 0, Nothing, True)
    End Sub

    Private Sub BACK_Click(sender As Object, e As RoutedEventArgs) Handles BACK.Click
        Frame.Navigate(GetType(MainPage))
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

End Class