using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Yelp.Api.Models;

namespace Seal.Initialpages
{
    public partial class InitialSetup : ContentPage
    {
        BusinessResponse _response;
        public InitialSetup(BusinessResponse response)
        {
            InitializeComponent();
            entry.Text = response.Name;
            _response = response;
        }
        async void Btn_pressed(object sender, EventArgs e)
        {
            _response.Name = entry.Text;
            await Navigation.PushAsync(new Contact(_response));
        }
    }
}
