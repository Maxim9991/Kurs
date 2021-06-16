using App.Lib.Models;
using App.Lib.Services;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AddCarToDatabase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string FilePath { get; set; } = String.Empty;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        { 
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files (*.jpg, *.png)|*.jpg;*.png;";
                dialog.ShowDialog();

                FilePath = dialog.FileName;
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(()=> { 
                var proj = App.Current as IConfigurationService;
                string defaultURL = proj.Configuration.GetSection("ServerRemoteURL").Value;
                CarModel car = new CarModel();
                string converting = ConvertImageService.ConvertToBase64(FilePath);
                car.Image = converting;
                Dispatcher.Invoke(() => {
                    int res = 0;
                    float fres = 0;
                    if (!string.IsNullOrEmpty(this.txtMark.Text))
                        car.Mark = this.txtMark.Text;
                    if (!string.IsNullOrEmpty(this.txtModel.Text))
                        car.Model = this.txtModel.Text;
                    if (!string.IsNullOrEmpty(this.txtFuel.Text))
                        car.Fuel = this.txtFuel.Text;
                    if (!string.IsNullOrEmpty(this.txtYear.Text) && int.TryParse(this.txtYear.Text, out res))
                        car.Year = this.txtYear.Text;
                    else car.Year = String.Empty;
                    if (!string.IsNullOrEmpty(this.txtCapacity.Text) && float.TryParse(this.txtCapacity.Text, out fres))
                        car.Capacity = this.txtCapacity.Text;
                    else car.Capacity = String.Empty;
                });

                WebRequest request = WebRequest.CreateHttp(defaultURL + "api/cars/send");

                request.Method = "POST";
                request.ContentType = "application/json";
                using (StreamWriter sw = new StreamWriter(request.GetRequestStream())) 
                {
                    string json = JsonConvert.SerializeObject(car);
                    sw.Write(json);
                }
                try
            {
                using (StreamReader sr = new StreamReader(((HttpWebResponse)request.GetResponse()).GetResponseStream()))
                {
                    string json = sr.ReadToEnd();
                    string resp = JsonConvert.DeserializeObject<string>(json);
                    //MessageBox.Show(resp);
                    Dispatcher.Invoke(() => {
                        this.txtMark.Text = "";
                        this.txtModel.Text = "";
                        this.txtCapacity.Text = "";
                        this.txtFuel.Text = "";
                        this.txtYear.Text = "";
                    });
                }
            }
            catch (WebException web)
            {
                using (StreamReader sr = new StreamReader(web.Response.GetResponseStream())) 
                {
                    string json = sr.ReadToEnd();
                    var excep = JsonConvert.DeserializeObject<ErrorContent>(json);

                    MessageBox.Show(excep.Errors.GetExceptionInLine());
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            });
        }
    }
}
