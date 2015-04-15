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
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace AntsaWeatherApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void TextBlock_SelectionChanged_1(object sender, RoutedEventArgs e)
        {

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            
            System.Diagnostics.Debug.WriteLine("Given city " + CityTextBox.Text.Trim());           
            this.QueryGrid.Visibility = Visibility.Collapsed;
            this.ProcessingGrid.Visibility = Visibility.Visible;
            this.ResultGrid.Visibility = Visibility.Collapsed;
            await fetchWeatherForecastData(CityTextBox.Text.Trim(),processData);
            System.Diagnostics.Debug.WriteLine("button method done");
         
        }

        public void processData(string jsonData)
        {
            System.Diagnostics.Debug.WriteLine("processing data starts");
            this.QueryGrid.Visibility = Visibility.Visible;
            this.ProcessingGrid.Visibility = Visibility.Collapsed;
            this.ResultGrid.Visibility = Visibility.Visible;
            System.Diagnostics.Debug.WriteLine("PROCESS JSON DATA"+jsonData);
           AntsaWeatherApp.WeatherData w= Deserialize<AntsaWeatherApp.WeatherData>(jsonData);
           System.Diagnostics.Debug.WriteLine("Object created from data "+w);
        }

        private async Task<string> fetchWeatherForecastData(string city,Action<string>callback)
        {
            System.Diagnostics.Debug.WriteLine("Setting delay for gui to test gui with async button");
            //await Task.Delay(9000);
            System.Diagnostics.Debug.WriteLine("delay passed, if clicked button check from logs");
            try
            {
                //Create HttpClient
                Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();
                //Define Http Headers  select * from weather.forecast where woeid=helsinki
                httpClient.DefaultRequestHeaders.Accept.TryParseAdd("application/json");
                String weatherQueryUri = "http://query.yahooapis.com/v1/public/yql?q=select * from geo.places where text=\"" + city+"\"";
                System.Diagnostics.Debug.WriteLine("Fetching data starts from URI " + weatherQueryUri);
              String res=await httpClient.GetStringAsync(new Uri(weatherQueryUri));
              callback(res);
                         
            }

            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR "+ex);
                
            }
            return "error fetchingData";  
        }

    

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

        /// <summary>
        /// Parses json string to object instance
        /// </summary>
        /// <typeparam name="T">type of the object</typeparam>
        /// <param name="json">json string representation of the object</param>
        /// <returns>Deserialized object instance</returns>
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
