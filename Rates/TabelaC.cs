﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Rates
{
    class TabelaC
    {
        public class Rate
        {
            public string currency { get; set; }
            public string code { get; set; }
            public double bid { get; set; }
            public double ask { get; set; }
        }

        public class RootObject
        {
            public string table { get; set; }
            public string no { get; set; }
            public string tradingDate { get; set; }
            public string effectiveDate { get; set; }
            public List<Rate> rates { get; set; }
        }
    }
}