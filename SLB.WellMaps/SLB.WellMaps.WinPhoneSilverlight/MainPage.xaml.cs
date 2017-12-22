﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SLB.WellMaps.WinPhoneSilverlight.Resources;
using Xamarin.Forms;

namespace SLB.WellMaps.WinPhoneSilverlight
{
    public partial class MainPage : Xamarin.Forms.Platform.WinPhone.FormsApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            Forms.Init();

            LoadApplication(new SLB.WellMaps.App());

        }

    }
}