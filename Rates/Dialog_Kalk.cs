using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;

namespace Rates
{
    class Dialog_Kalk : DialogFragment
    {
        private double średni;
        private double kupno;
        private double sprzedaz;
        private string nazwa;
        private Button mButtSwich;
        private Boolean btnclick = true;

        public Dialog_Kalk(double średni, string nazwa, double kupno, double sprzedaz)
        {
            this.średni = średni;
            this.nazwa = nazwa;
            this.kupno = kupno;
            this.sprzedaz = sprzedaz;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
           
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.dialog_k, container, false);
            int resourceId = (int)typeof(Resource.Drawable).GetField(nazwa + "1").GetValue(null);
            int resourceId1 = (int)typeof(Resource.Drawable).GetField("PLN" + "1").GetValue(null);
            ImageView imgFlaga = view.FindViewById<ImageView>(Resource.Id.flag_country2);
            ImageView imgFlaga1 = view.FindViewById<ImageView>(Resource.Id.flag_country1);
            imgFlaga.SetImageResource(resourceId);
            imgFlaga1.SetImageResource(resourceId1);
            TextView txtRate = view.FindViewById<TextView>(Resource.Id.txtRate);
            
            txtRate.Text = średni.ToString();
            TextView txtResult = view.FindViewById<TextView>(Resource.Id.txtResult);
            txtRate.Text = średni.ToString();
            mButtSwich = view.FindViewById<Button>(Resource.Id.swich);
            mButtSwich.Click += mButtSwich_Click;
            void mButtSwich_Click(object sender, EventArgs e)
            {
                if (btnclick == true)
                {
                    imgFlaga.SetImageResource(resourceId1);
                    imgFlaga1.SetImageResource(resourceId);
                    if (kupno > 0) {
                        txtRate.Text = kupno.ToString();
                    } else {
                        txtRate.Text = średni.ToString(); 
                           }
                    
                    btnclick = false;
                }
                else
                {
                    imgFlaga.SetImageResource(resourceId);
                    imgFlaga1.SetImageResource(resourceId1);
                    if (sprzedaz > 0)
                    {
                        txtRate.Text = sprzedaz.ToString();
                    }
                    else
                    {
                        txtRate.Text = średni.ToString();
                    }
                    btnclick = true;
                }
                
            }
            TextView txtamount = view.FindViewById<TextView>(Resource.Id.txtAmount);
            txtamount.TextChanged += (object sender, TextChangedEventArgs e) =>
           {
               double amount = 0;
               if (txtamount != null)
               {
                   try { amount = Double.Parse(txtamount.Text.Replace('.', ',')); } catch(FormatException ) { }
               }
               //amount = Double.Parse(txtamount.Text.Replace('.', ','));
                   double rate = Double.Parse(txtRate.Text);
                   double result = amount * rate;
                   txtResult.Text = result.ToString();
                  
               
           };
           /* txtamount.KeyPress += (object sender, View.KeyEventArgs e) => {
                e.Handled = false;
                if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
                {
                   
                    double amount = Double.Parse(txtamount.Text.Replace('.',','));
                    double rate = Double.Parse(txtRate.Text);
                    double result = amount * rate;
                    txtResult.Text = result.ToString();
                    e.Handled = true;
                }
            };*/

            return view;
        }

        
    }
}