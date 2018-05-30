using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Net;
using System;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using Android.Net;
using Android.Content;

namespace Rates
{
    [Activity(Label = "Rates", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private List<TabelaA.RootObject> mItems;
        private List<TabelaC.RootObject> mItems1;
        private List<Waluta> mWaluta;
        private ListView mListView;
        private WebClient mClient;
        private WebClient mClient1;
        private System.Uri mUrl;
        private System.Uri mUrl1;
        private string DbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbWaluty.db3");
        private DateTime Time = DateTime.Now;
        DataBase db;




        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            ConnectivityManager manager = (ConnectivityManager)GetSystemService(ConnectivityService);
            NetworkInfo networkInfo = manager.ActiveNetworkInfo;
            SetContentView(Resource.Layout.Main);
            mListView = FindViewById<ListView>(Resource.Id.myListView);
            CheckFirstRun(networkInfo);
            mListView.ItemClick += MListView_ItemClick;

            
        }

        private void MListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            Dialog_Kalk dialog = new Dialog_Kalk(mWaluta[e.Position].Średni, mWaluta[e.Position].Kod, mWaluta[e.Position].Kupno, mWaluta[e.Position].Sprzedaz );
            
            dialog.Show(transaction, "dialog fragment");
            Console.WriteLine(mWaluta[e.Position].Nazwa);
        }

        void GetData()
        {
            mClient = new WebClient();
            mUrl = new System.Uri("http://api.nbp.pl/api/exchangerates/tables/a/?format=json");
            mClient.DownloadDataAsync(mUrl);
            mClient.DownloadDataCompleted += MClient_DownloadDataCompleted;
           
        }



        void MClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                string json = Encoding.UTF8.GetString(e.Result);
                mItems = JsonConvert.DeserializeObject<List<TabelaA.RootObject>>(json);
            });
            AddData(mItems);
        }


        void MClient_DownloadDataCompleted1(object sender, DownloadDataCompletedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                string json = Encoding.UTF8.GetString(e.Result);
                mItems1 = JsonConvert.DeserializeObject<List<TabelaC.RootObject>>(json);
            });
            UpdateData(mItems1);

        }
        void AddData(List<TabelaA.RootObject> mItems)
        {
           
            for (int i = 0; i < mItems[0].rates.Count; i++)
            {

                Waluta awaluta = new Waluta()
                {
                    Id = i,
                    Data = DateTime.Parse(mItems[0].effectiveDate),
                    Nazwa = mItems[0].rates[i].currency,
                    Kod = mItems[0].rates[i].code,
                    Średni = mItems[0].rates[i].mid
                };

                db.InsertIntoWaluta(awaluta);
                
            }
          
            mClient1 = new WebClient();
            mUrl1 = new System.Uri("http://api.nbp.pl/api/exchangerates/tables/c/?format=json");
            mClient1.DownloadDataAsync(mUrl1);
            mClient1.DownloadDataCompleted += MClient_DownloadDataCompleted1;


        }
        void UpdateData(List<TabelaC.RootObject> mItems)
        {

            
            for (int i = 0; i < mItems[0].rates.Count; i++)
            {

                Waluta awaluta = new Waluta()
                {
                    Id = i,
                    Data = DateTime.Parse(mItems[0].effectiveDate),
                    Nazwa = mItems[0].rates[i].currency,
                    Kod = mItems[0].rates[i].code,
                    Kupno = mItems[0].rates[i].bid,
                    Sprzedaz = mItems[0].rates[i].ask

                };
                db.updateWaluta(awaluta);
            }
            mWaluta = db.selectWaluta();
            ListViewAdapter adapter = new ListViewAdapter(this, mWaluta);
            mListView.Adapter = adapter;
        }

        public void CheckFirstRun(NetworkInfo networkInfo)
        {
            
            const String PREFS_NAME = "MyPrefsFile";
            const String PREF_VERSION_CODE_KEY = "version_code";
            const int DOESNT_EXIST = -1;

            
            int currentVersionCode = Application.Context.ApplicationContext.PackageManager.GetPackageInfo(Application.Context.ApplicationContext.PackageName, 0).VersionCode;
            
            ISharedPreferences prefs = GetSharedPreferences(PREFS_NAME, FileCreationMode.Private);
            int savedVersionCode = prefs.GetInt(PREF_VERSION_CODE_KEY,DOESNT_EXIST);

           
            if (currentVersionCode == savedVersionCode)
            {
                db = new DataBase();
                db.createDataBase();
                mWaluta = db.selectWaluta();
                ListViewAdapter adapter = new ListViewAdapter(this, mWaluta);
                mListView.Adapter = adapter;

                
                return;

            }
            else if (savedVersionCode == DOESNT_EXIST)
            {
                db = new DataBase();
                db.createDataBase();
                 if (networkInfo != null && networkInfo.IsConnectedOrConnecting)
                 {
                 GetData();
                 }
               

            }
            else if (currentVersionCode > savedVersionCode)
            {
                db = new DataBase();
                db.createDataBase();
                if (networkInfo != null && networkInfo.IsConnectedOrConnecting)
                {
                    GetData();
                }
                
            }

            prefs.Edit().PutInt(PREF_VERSION_CODE_KEY,currentVersionCode).Apply();
            
        }
    }

}


