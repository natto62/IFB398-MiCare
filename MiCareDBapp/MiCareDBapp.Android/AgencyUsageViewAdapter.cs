﻿using System;
using System.Collections.Generic;
using Android.Graphics;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android;

namespace MiCareDBapp.Droid
{
    class AgencyUsageViewAdapter : BaseAdapter<AgencyUsageData>
    {
        private List<AgencyUsageData> Items;
        private Context Context;

        public AgencyUsageViewAdapter(Context context, List<AgencyUsageData> items)
        {
            Items = items;
            Context = context;
        }

        //count items, create that number of rows
        public override int Count
        {
            get { return Items.Count; }
        }

        //return item id
        public override long GetItemId(int position)
        {
            return position;
        }

        public override AgencyUsageData this[int position]
        {
            get { return Items[position]; }
        }

        //set the view
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(Context).Inflate(Resource.Layout.AgencyUsageTable, null, false);
            }
            //retrieve the shared preferences to edit the row attributes such as text size, colour or if the the data has to be sorted by date
            ISharedPreferences getidpreferences = Application.Context.GetSharedPreferences("UserInformation", FileCreationMode.Private);
            string UserID = getidpreferences.GetString("LatestUserID", String.Empty);
            ISharedPreferences preferences = Application.Context.GetSharedPreferences("UserInformation" + UserID, FileCreationMode.Private);
            int textSize = preferences.GetInt("TextSize", 1);
            bool NightSwitchMode = preferences.GetBoolean("NightSwitchMode", false);
            bool DateSwitchMode = preferences.GetBoolean("DateSwitchMode", false);

            TextView txtAmountAgency = row.FindViewById<TextView>(Resource.Id.txtAmountAgency);
            TextView txtDateAgency = row.FindViewById<TextView>(Resource.Id.txtDateAgency);


            switch (textSize) {
                case 0:
                    txtDateAgency.TextSize = 10;
                    txtAmountAgency.TextSize = 10;
                    break;
                case 1:
                    txtDateAgency.TextSize = 15;
                    txtAmountAgency.TextSize = 15;
                    break;
                case 2:
                    txtDateAgency.TextSize = 20;
                    txtAmountAgency.TextSize = 20;
                    break;
            }

            if (NightSwitchMode) {
                txtDateAgency.SetTextColor(Color.White);
                txtAmountAgency.SetTextColor(Color.White);
            } else {
                txtDateAgency.SetTextColor(Color.Black);
                txtAmountAgency.SetTextColor(Color.Black);
            }

            if (DateSwitchMode) {
                Items.Sort(delegate (AgencyUsageData one, AgencyUsageData two) {
                    return DateTime.Compare(one.GetDate(), two.GetDate());
                });
            }
            //for every item
            txtAmountAgency.Text = "$ " + Items[position].GetAgencyUsageAmount().ToString();
            txtDateAgency.Text = Items[position].GetDate().ToShortDateString();
            //set indicator colours
            if (Items[position].IsGreen())
            {
                if (NightSwitchMode)
                {
                    row.SetBackgroundColor(Color.DarkGreen);
                }
                else
                {
                    row.SetBackgroundColor(Color.LightGreen);
                }
            }
            else if (Items[position].IsRed())
            {
                if (NightSwitchMode)
                {
                    row.SetBackgroundColor(Color.DarkRed);
                }
                else
                {
                    row.SetBackgroundColor(Color.Argb(80, 255, 128, 128));
                }
            }
            else
            {
                if (NightSwitchMode)
                {
                    row.SetBackgroundColor(Color.DarkOrange);
                }
                else
                {
                    row.SetBackgroundColor(Color.LightYellow);
                }
            }

            return row;
        }

    }
}