using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace UWPWeather
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /*
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var position = await LocationManager.GetPosition();

            var lat = position.Coordinate.Latitude;
            var lon = position.Coordinate.Longitude;

            RootObject myWeather = 
                await OpenWeatherMapProxy.GetWeather(
                    lat,
                    lon);

            //Schedule update
            var uri = String.Format("http://uwpweatherservice.azurewebsites.net/?lat={0}&lon={1}",lat,lon);

            var tileContent = new Uri(uri);
            var requestedInterval = PeriodicUpdateRecurrence.HalfHour;

            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.StartPeriodicUpdate(tileContent, requestedInterval);

            string icon = String.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.weather[0].icon);
            ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
            ResusltTextBlock.Text = myWeather.name + " - " + ((int)myWeather.main.temp).ToString() + " - " + myWeather.weather[0].description;
        }
        */

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var position = await LocationManager.GetPosition();

                var lat = position.Coordinate.Latitude;
                var lon = position.Coordinate.Longitude;

                RootObject myWeather =
                    await OpenWeatherMapProxy.GetWeather(
                        lat,
                        lon);

                // Schedule update
                var uri = String.Format("http://uwpweatherservice.azurewebsites.net/?lat={0}&lon={1}", lat, lon);

                var tileContent = new Uri(uri);
                var requestedInterval = PeriodicUpdateRecurrence.HalfHour;

                var updater = TileUpdateManager.CreateTileUpdaterForApplication();
                updater.StartPeriodicUpdate(tileContent, requestedInterval);

                string icon = String.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.weather[0].icon);
                ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
                //ResultTextBlock.Text = myWeather.name + " - " + ((int)myWeather.main.temp).ToString() + " - " + myWeather.weather[0].description;
                TempTextBlock.Text = ((int)myWeather.main.temp).ToString();
                DescriptionTextBlock.Text = myWeather.weather[0].description;
                LocationTextBlock.Text = myWeather.name;
            }
            catch (Exception)
            {
                LocationTextBlock.Text = "Unable to get weather at this time.";
            }
        }
    }
}
