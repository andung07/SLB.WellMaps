using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SLB.WellMaps.Services;
using Android.App;
using Android.Content;
using Android.Util;
using Android.OS;
using Android.Provider;
using Android.Content.PM;
using Xamarin.Forms;
using GalaSoft.MvvmLight.Messaging;

using SLB.WellMaps;
using System.Net.Http;
using ModernHttpClient;

namespace SLB.WellMaps.Droid.DependencyImpl
{
    [Service(Exported = true, Enabled = true)]
    [IntentFilter(new[] { "com.mobileiron.HANDLE_CONFIG" })]
    public class AppConnectConfigService : IntentService, IMobileIron
    {
        const string TAG = "AppConnectConfigService";

        public AppConnectConfigService() : base("AppConnectConfigService")
        {
        }

        public static void requestConfig(Context ctx)
        {
            var intent = new Intent("com.mobileiron.REQUEST_CONFIG");
            intent.SetPackage("com.forgepond.locksmith");
            intent.PutExtra("packageName", ctx.PackageName);
            ctx.StartService(intent);
        }

        protected override async void OnHandleIntent(Android.Content.Intent intent)
        {
            try
            {
                if(!Boolean.Parse(Java.Lang.JavaSystem.GetProperty("com.mobileiron.wrapped", "false")) &&
                    !this.PackageName.Equals(PackageManager.GetPermissionInfo("com.mobileiron.CONFIG_PERMISSION", 0).PackageName))
                {
                    Log.Debug(TAG, "Refusing intent as we don't own our permission?!");
                }
            }

            catch (PackageManager.NameNotFoundException ex)
            {
                Log.Debug(TAG, ex.Message + " " + "Refusing intent as we can't find our permission?!");
                return;
            }

            if("com.mobileiron.HANDLE_CONFIG".Equals (intent.Action))
            {
                Log.Debug(TAG, "Received intent : " + intent + " from package " + intent.GetStringExtra("packageName"));

                Bundle config = intent.GetBundleExtra("config");

                if(config != null)
                {
                    Log.Debug(TAG, "Config Received");
                    foreach(var key in config.KeySet())
                    {
                        if(key == "alias")
                        {
                            Log.Debug(TAG, "Alias = " + config.GetString(key));                            
                        }
                    }
                    MessagingCenter.Send<IMobileIron>(this, "ConfigParsed");

                    HttpClient client = new HttpClient(new NativeMessageHandler { DisableCaching = true });
                    var response = await client.GetAsync("https://teamspace.slb.com/sites/ingwellmaps/_vti_bin/listdata.svc");
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new NotificationMessage(await response.Content.ReadAsStringAsync()));
                }
            }

        }
    }
}