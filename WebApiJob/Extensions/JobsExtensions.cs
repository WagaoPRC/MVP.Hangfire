using Microsoft.VisualBasic;
using MyCronJob;

namespace WebApiJobs.Extensions;
public static class JobsExtensions
{

    public static IServiceCollection addJob(this IServiceCollection services, ILogger logger)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        //Worker worker = new Worker<string>();
        return services;
    }
}
