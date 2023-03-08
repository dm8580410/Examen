using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Examen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageContac : ContentPage
    {
        public PageContac()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var location = await Geolocation.GetLocationAsync();

                if (location != null)
                {
                    txtlatitud.Text = Convert.ToString(location.Latitude);
                    txtlongitud.Text = Convert.ToString(location.Longitude);

                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (Exception ex)
            {
            }
        }

            protected async void btnsalvar_Clicked(object sender, EventArgs e)

            { 
            if (String.IsNullOrEmpty(txtnombre.Text))
            {
                await DisplayAlert("Alerta", "Debe escrbir un nombre", "OK");
                txtnombre.Focus();
            }

            else 
            if (String.IsNullOrEmpty(txttelefono.Text))

            {
                await DisplayAlert("Alerta", "Debe escrbir un telefono", "OK");
                txttelefono.Focus();

            }
            else
             if (String.IsNullOrEmpty(txtnota.Text))
            {

            }
            await DisplayAlert("Alerta", "Debe escrbir una nota", "OK");
            txttelefono.Focus();

            {

            var con = new Models.Contacto
            {
                nombre = txtnombre.Text,
                telefono = txttelefono.Text,
                edad = int.Parse(edad.Text),
                pais = cbpais.SelectedItem.ToString(),
                nota = txtnota.Text,
               
            };


            if (await App.DBCon.SaveCon(con) > 0)
                await DisplayAlert("Aviso", "Contacto Ingresado", "OK");
            else
                await DisplayAlert("Aviso", "Error", "OK");

            var page = new Views.PageResultado();
            page.BindingContext = con;
            await Navigation.PushAsync(page);

        }
    }

   }
}