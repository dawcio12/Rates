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
using Android.Content.Res;

namespace Rates
{
    public class ListViewAdapter : BaseAdapter
    {
        private Activity activity;
        private List<Waluta> waluta;
        public ListViewAdapter(Activity activity, List<Waluta> waluta)
        {
            this.activity = activity;
            this.waluta = waluta;

        }

        public override int Count
        {
            get
            {
                return waluta.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return waluta[position].Id;
        }



        public override Android.Views.View GetView(int position, Android.Views.View convertView, ViewGroup parent)
        {
            Android.Views.View row = convertView;
            if (row == null)
            {
                row = activity.LayoutInflater.Inflate(Resource.Layout.ListView_row, null, false);

            }
            TextView txtName = row.FindViewById<TextView>(Resource.Id.txtName);
            txtName.Text = waluta[position].Nazwa;
            TextView txtDate = row.FindViewById<TextView>(Resource.Id.txtDate);
            txtDate.Text = waluta[position].Data.ToShortDateString();
            TextView txtSell = row.FindViewById<TextView>(Resource.Id.txtSell);
            txtSell.Text = waluta[position].Sprzedaz.ToString();
            TextView txtBuy = row.FindViewById<TextView>(Resource.Id.txtBuy);
            txtBuy.Text = waluta[position].Kupno.ToString();
            TextView txtMid = row.FindViewById<TextView>(Resource.Id.txtMid);
            txtMid.Text = waluta[position].Średni.ToString();
            TextView txtKod = row.FindViewById<TextView>(Resource.Id.txtKod);
            txtKod.Text = waluta[position].Kod.ToString();
            int resourceId = (int)typeof(Resource.Drawable).GetField(waluta[position].Kod.ToString()+"1").GetValue(null);
            ImageView imgFlaga = row.FindViewById<ImageView>(Resource.Id.imgFlaga);
            imgFlaga.SetImageResource(resourceId);
            return row;
        }
    }
}