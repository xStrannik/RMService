﻿using Microsoft.Extensions.Configuration;

namespace RMMService.Models
{
    public class Settings
    {
        private readonly IConfiguration configuration;

        public Settings(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public int WorkersCount => configuration.GetValue<int>("WorkersCount");
        public int RunInterval => configuration.GetValue<int>("RunInterval");
        public string InstanceName => configuration.GetValue<string>("name");
        public string ResultPath => configuration.GetValue<string>("ResultPath");
    }
}
