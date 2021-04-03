using System;
using System.Collections.Generic;
using Yelp.Api.Models;
using Xamarin.Forms;

namespace Seal.Initialpages
{
    public partial class Contact : ContentPage
    {
        BusinessResponse response;
        public Contact(BusinessResponse response)
        {
            InitializeComponent();
        }
    }
}
