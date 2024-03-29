﻿using AutoAid.Domain.Common;

namespace AutoAid.WebApi.Configuration
{
    public static class AppSettingsRegister
    {
        public static void SettingsBinding(this IConfiguration configuration)
        {
            do
            {
                AppConfig.ConnectionStrings = new ConnectionStrings();
                AppConfig.FirebaseConfig = new FirebaseConfig();
                AppConfig.JwtSetting = new JwtSetting();
                AppConfig.AwsCredentials = new AwsCredentials();
                AppConfig.VnpayConfig = new VnpayConfig();
            }
            while (AppConfig.ConnectionStrings == null);

            configuration.Bind("ConnectionStrings", AppConfig.ConnectionStrings);
            configuration.Bind("FirebaseConfig", AppConfig.FirebaseConfig);
            configuration.Bind("JwtSetting", AppConfig.JwtSetting);
            configuration.Bind("AwsCredentials", AppConfig.AwsCredentials);
            configuration.Bind("VnpayConfig", AppConfig.VnpayConfig);
        }
    }
}
