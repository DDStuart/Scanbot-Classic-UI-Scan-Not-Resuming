﻿using ScanbotSDK.MAUI;
using ScanbotSDK.MAUI.RTU.v1;
using System.Diagnostics;
using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.ClassicComponent;

namespace Scanbot_Classic_UI_Scan_Not_Resuming
{
    public partial class MainPage : ContentPage
    {
        private int _barcodeCount = 0;
        public MainPage()
        {
            InitializeComponent();
            cameraView.BarcodeFormats = BarcodeFormats.All.ToList();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await CheckAndRequestCameraPermission();
            var licenseExpires = ScanbotBarcodeSDK.LicenseInfo.ExpirationDate;
#if ANDROID
            var now = DateTime.UtcNow;
#else
            var now = DateTime.Now;
#endif
            var duration = licenseExpires - now;
            license.Text = $"License status: Expires in {duration?.TotalSeconds ?? 0} seconds.";
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            
            cameraView.Handler?.DisconnectHandler();
        }

        private void CameraView_OnOnBarcodeScanResult(BarcodeResultBundle result)
        {
            Debug.WriteLine("Barcode scanned: " + result.Barcodes[0].Text);
            scanned.Text = $"Scanned barcodes: {++_barcodeCount}";
        }

        public static async Task<PermissionStatus> CheckAndRequestCameraPermission()
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.Camera>();

            if (status == PermissionStatus.Granted)
                return status;

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // Prompt the user to turn on in settings
                // On iOS once a permission has been denied it may not be requested again from the application
                await Application.Current.MainPage.DisplayAlert("Validation Issue: Camera permission", "Permission Denied. Please change permission in the application settings.", "Close");

                return status;
            }

            if (Permissions.ShouldShowRationale<Permissions.Camera>())
            {
                // Prompt the user with additional information as to why the permission is needed
                await Application.Current.MainPage.DisplayAlert("Validation Issue: Camera permission", "Please grant permission to access the camera in order to proceed.", "Close");
            }

            status = await Permissions.RequestAsync<Permissions.Camera>();

            return status;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(NextPage)}");
        }
    }
}
