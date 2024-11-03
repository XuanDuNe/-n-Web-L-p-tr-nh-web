using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Weather_Application
{
    public partial class WeatherApp : Form
    {
        public WeatherApp()
        {
            InitializeComponent();

            lb_tempC.Visible = false;
            lb_tempZero.Visible = false;
            lab_temp.Visible = false;
        }

        string APIKey = "29e8e546e5ac4f413e3c9ee688325a59";

        private void btn_Search_Click(object sender, EventArgs e)
        {
            
            getWeather();

            lb_tempC.Visible = true;
            lb_tempZero.Visible = true;
            lab_temp.Visible = true;
        }

        void getWeather()
        {
            using (WebClient wc = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", tB_City.Text, APIKey);
                var json = wc.DownloadString(url);
                WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

                lab_Condition.Text = Info.weather[0].main;
                lab_Details.Text = Info.weather[0].description;
                lab_Sunset.Text = convertDateTime(Info.sys.sunset).ToShortTimeString();
                lab_Sunrise.Text = convertDateTime(Info.sys.sunrise).ToShortTimeString();
                lab_WindSpeed.Text = Info.wind.speed.ToString();
                lab_Pressure.Text = Info.main.pressure.ToString();
                double tempConvert = Info.main.temp - 273.15;
                lab_temp.Text = tempConvert.ToString("F1")+ " °C";

                // Đặt hình nền theo tên thành phố
                SetBackgroundImage(tB_City.Text);
            }
        }

        DateTime convertDateTime(long sec)
        {
            DateTime day = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();
            day = day.AddSeconds(sec).ToLocalTime();

            return day;
        }

        private void SetBackgroundImage(string cityName)
        {
            string imagePath = $"Images\\{cityName}.jpg"; // Đường dẫn tới hình ảnh theo tên thành phố
            if (System.IO.File.Exists(imagePath)) // Kiểm tra xem file có tồn tại không
            {
                this.BackgroundImage = Image.FromFile(imagePath); // Đặt hình nền
                this.BackgroundImageLayout = ImageLayout.Stretch; // Căn chỉnh hình nền
            }
            else
            {
                this.BackgroundImage = null; // Nếu không tìm thấy, đặt lại hình nền về null
            }
        }
      
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void lab_City_Click(object sender, EventArgs e)
        {

        }

        private void lb_tempC_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
