﻿using HonorFlightScreening.Helper;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace HonorFlightScreening
{
    public class MyTelemetryInitializer : ITelemetryInitializer
    {

        public void Initialize(ITelemetry telemetry)

        {

            if (string.IsNullOrEmpty(telemetry.Context.Cloud.RoleName))
            {
                //set custom role name here
                telemetry.Context.Cloud.RoleName = Constants.ConfigAppServiceName;
                telemetry.Context.Cloud.RoleInstance = Environment.MachineName;

            }
        }
    }
}
