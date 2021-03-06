﻿using System;
using Promitor.Scraper.Configuration;
using Promitor.Scraper.Scheduling.Cron;
using Promitor.Scraper.Validation.Interfaces;

namespace Promitor.Scraper.Validation.Steps
{
    public class ScrapingScheduleValidationStep : ValidationStep, IValidationStep
    {
        private const string DefaultCronSchedule = "*/5 * * * *";

        public string ComponentName { get; } = "Cron Schedule";

        public ValidationResult Run()
        {
            var scrapingCronSchedule = Environment.GetEnvironmentVariable(EnvironmentVariables.Scraping.CronSchedule);
            if (string.IsNullOrWhiteSpace(scrapingCronSchedule))
            {
                LogMessage(
                    $"No scraping schedule was specified, falling back to default '{DefaultCronSchedule}' cron schedule...");
                scrapingCronSchedule = DefaultCronSchedule;
                Environment.SetEnvironmentVariable(EnvironmentVariables.Scraping.CronSchedule, scrapingCronSchedule);
            }

            try
            {
                CronSchedule.Parse(scrapingCronSchedule);
                return ValidationResult.Successful(ComponentName);
            }
            catch (Exception exception)
            {
                LogMessage(
                    $"No valid scraping schedule was specified - '{scrapingCronSchedule}'. Details: {exception.Message}");
                return ValidationResult.Failure(ComponentName,
                    $"No valid scraping schedule was specified - '{scrapingCronSchedule}'.");
            }
        }
    }
}