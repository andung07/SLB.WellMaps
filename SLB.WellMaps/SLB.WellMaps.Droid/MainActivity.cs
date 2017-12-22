using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Graphics.Drawables;
using Android.Graphics;
using Xamarin.Forms.Platform.Android;
using SLB.WellMaps.Droid.DependencyImpl;

namespace SLB.WellMaps.Droid
{
    [Activity(Label = "SLB.WellMaps", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.tabs;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            global::Xamarin.FormsMaps.Init(this, bundle);

            LoadApplication(new App());

            //MI Service
            //AppConnectConfigService.requestConfig(this);
        }
    }
}