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

namespace MiCareApp.Droid
{
    [Activity(Label = "FinanceMenu", Theme = "@style/MainTheme")]
    public class FinanceMenu : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.FinancialDataList);

            //Back button
            Button BackBtn = FindViewById<Button>(Resource.Id.FinanceBackButton);
            BackBtn.Click += delegate { StartActivity(typeof(IntroPage)); };

            //AgencyBtn
            Button AgencyBtn = FindViewById<Button>(Resource.Id.AgencyBtn);
            AgencyBtn.Click += delegate { StartActivity(typeof(AgencyUsagePage)); };

            //BrokerageBtn
            Button BrokerageBtn = FindViewById<Button>(Resource.Id.BrokerageBtn);
            BrokerageBtn.Click += delegate { StartActivity(typeof(BrokerageHoursPage)); };

            //Home Care Packages button
            Button HomeCareBtn = FindViewById<Button>(Resource.Id.HomeCareBtn);
            HomeCareBtn.Click += delegate { StartActivity(typeof(HomeCarePackagePage)); };

            //SalariesBtn
            Button SalariesBtn = FindViewById<Button>(Resource.Id.SalariesBtn);
            SalariesBtn.Click += delegate { StartActivity(typeof(SalariesWagesPage)); };


        }
    }
}