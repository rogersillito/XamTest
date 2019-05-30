using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace XamTest.UITest
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android
                    //.PreferIdeSettings()
                    .InstalledApp("com.companyname.App3") // Project Settings > Android Manifest > Package Name
                    //.ApkFile(@"C:\Users\rsillito\AppData\Local\Xamarin\Mono for Android\Archives\2019-04-05\XamTest.Android 4-05-19 4.14 PM.apkarchive\com.companyname.App3.apk")
                    .StartApp();
            }

            return ConfigureApp.iOS
                .InstalledApp("com.companyname.XamTest")
                .DeviceIdentifier("7CAA2ADE-619D-43C9-BF0D-02045EE5F28A") // iPhone Xʀ (12.2)
                .StartApp();
        }
    }
}