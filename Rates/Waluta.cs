using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace Rates
{
    public class Waluta
    {
        [PrimaryKey]
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Nazwa { get; set; }
        public string Kod { get; set; }
        public double Kupno { get; set; }
        public double Średni { get; set; }
        public double Sprzedaz { get; set; }

    }
}