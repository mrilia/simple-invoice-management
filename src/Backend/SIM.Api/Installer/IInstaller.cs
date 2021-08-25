using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SIM.Api.Installer
{
    public interface IInstaller
    {
        void InstallServices(IConfiguration configuration, IServiceCollection services);
    }
}