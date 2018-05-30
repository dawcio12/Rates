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
using Android.Util;

namespace Rates
{
    public class DataBase
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public bool createDataBase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Waluty.db")))
                {
                    connection.CreateTable<Waluta>();
                    return true;
                }

            }
            catch (SQLiteException e)
            {
                Log.Info("SQLiteEx", e.Message);
                return false;

            }

        }
        public bool InsertIntoWaluta(Waluta waluta)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Waluty.db")))
                {
                    connection.Insert(waluta);
                    return true;
                }
            }
            catch (SQLiteException e)
            {
                Log.Info("SQLiteEX", e.Message);
                return false;
            }
        }
        public List<Waluta> selectWaluta()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Waluty.db")))
                {

                    return connection.Table<Waluta>().ToList();
                }

            }
            catch (SQLiteException e)
            {
                Log.Info("SQLiteEx", e.Message);
                return null;

            }

        }
        public bool queryIntoWaluta(int id)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Waluty.db")))
                {
                    connection.Query<Waluta>("SELECT * FROM Waluta Where Id=?", id);
                    return true;
                }
            }
            catch (SQLiteException e)
            {
                Log.Info("SQLiteEX", e.Message);
                return false;
            }
        }
        public bool updateWaluta(Waluta waluta)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Waluty.db")))
                {

                    connection.Query<Waluta>("UPDATE Waluta set Data=?,Kupno=?,Sprzedaz=? Where Kod=?", waluta.Data, waluta.Kupno, waluta.Sprzedaz, waluta.Kod);
                    return true;
                }
            }
            catch (SQLiteException e)
            {
                Log.Info("SQLiteEX", e.Message);
                return false;
            }
        }
        public bool deleteWaluty()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Waluty.db")))
                {

                    connection.Query<Waluta>("DELETE FROM Waluta");
                    return true;
                }
            }
            catch (SQLiteException e)
            {
                Log.Info("SQLiteEX", e.Message);
                return false;
            }
        }

    }
}