﻿using Microsoft.Extensions.DependencyInjection;
using WatchDog;
using WatchDog.src.Enums;

namespace ShoeStore.Application.Extensions.WatchDog
{
    public static class WatchDogExtensions
    {
        public static IServiceCollection AddWatchDog(this IServiceCollection services)
        {
            services.AddWatchDogServices(options =>
            {
                options.IsAutoClear = true;
                options.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Daily;
            });

            return services;
        }
    }
}