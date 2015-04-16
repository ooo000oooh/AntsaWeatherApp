using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace AntsaWeatherApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            Application.Current.Suspending += new SuspendingEventHandler(App_Suspending);

        }


         void App_Suspending(Object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Suspending starts");
             saveDataOnSuspension(e);
        }

        private void saveDataOnSuspension(SuspendingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Saving data on suspension.. nothing interesting to save");
            
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("On navigated to");
           
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void TextBlock_SelectionChanged_1(object sender, RoutedEventArgs e)
        {

        }

   

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string city=CityTextBox.Text.Trim();
            if (city.Length > 0) {
                this.QueryGrid.Visibility = Visibility.Collapsed;
                this.ProcessingGrid.Visibility = Visibility.Visible;
                this.ResultGrid.Visibility = Visibility.Collapsed;
                this.ErrorText.Visibility = Visibility.Collapsed;
                await fetchWeatherForecastData(city,processData);
            }
            System.Diagnostics.Debug.WriteLine("button method done");
         
        }

        public void processData(string jsonData)
        {
                  
            System.Diagnostics.Debug.WriteLine("PROCESS JSON DATA"+jsonData);
            try
            {
                AntsaWeatherApp.WeatherData w = Deserialize<AntsaWeatherApp.WeatherData>(jsonData);
                System.Diagnostics.Debug.WriteLine("Object created from data " + w);
                this.Country.Text = "Country: " + w.query.results.channel.location.country;
                this.City.Text = "City: " + w.query.results.channel.location.city;
                this.Temperature.Text="Temperature: "+w.query.results.channel.item.condition.temp+"c"; //celsius added to query
                this.Sunrise.Text = "Sunrise: " + w.query.results.channel.astronomy.sunrise;
                this.Sunset.Text = "Sunset: " + w.query.results.channel.astronomy.sunset;
                this.CityTextBox.Text = "";
                this.QueryGrid.Visibility = Visibility.Visible;
                this.ProcessingGrid.Visibility = Visibility.Collapsed;
                this.ResultGrid.Visibility = Visibility.Visible;
               
            }
            catch (Exception e)
            {
                this.ErrorText.Text = "Error:" + e.Message;
                this.ErrorText.Visibility = Visibility.Visible;
            }
          
        }

        private async Task<string> fetchWeatherForecastData(string city,Action<string>callback)
         {
            //System.Diagnostics.Debug.WriteLine("Setting delay for gui to test gui with async button");
            //await Task.Delay(9000);           
            try
            {
                //Create HttpClient
                Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();                
                httpClient.DefaultRequestHeaders.Accept.TryParseAdd("application/json");
                //Combined and encoded YQL query -> search WOEID for city and fetch weather data
                String encodedYqlQuery = "http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20u%20%3D%20%27c%27%20and%20woeid%20in%20(select%20woeid%20from%20geo.places(0%2C1)%20where%20text%3D%22"+city+"%22)";
                System.Diagnostics.Debug.WriteLine("Weather webService uri/ YQL " + encodedYqlQuery);
                //Fire the query
                String res=await httpClient.GetStringAsync(new Uri(encodedYqlQuery));
                //And call the callback method after returning
                callback(res);                         
            }

            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR "+ex);                
            }
            return "error fetchingData";  
        }

    
        //From course material
         public static string Serialize(object obj)
        {
            if (obj == null)
            {
                return "null";
            }

            DataContractJsonSerializer ser = new DataContractJsonSerializer(obj.GetType());

            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, obj);

            ms.Position = 0;

            string json = String.Empty;

            using (StreamReader sr = new StreamReader(ms))
            {
                json = sr.ReadToEnd();
            }

            ms.Dispose();

            return json;

        }

      //From course material
        public static T Deserialize<T>(string json)
        {
            DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(T));

            using (MemoryStream mem = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                System.Diagnostics.Debug.WriteLine("Deserialization started");
                return (T)deserializer.ReadObject(mem);
            }

        }

      

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Clicked2 while processing. UI not freezing");
        }
    }

    
}
