using System.Configuration;
using CityPowerAndLight.Config;
using Microsoft.Extensions.Configuration;

/// <summary>
/// The <c>AppConfig</c> class is responsible for loading and validating
/// configuration values for the application.
/// </summary>
internal class AppConfig
{
    /// <summary>
    /// Gets the configuration values for demo entities.
    /// </summary>
    public DemoValuesConfig DemoValues { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppConfig"/> class.
    /// Binds the configuration values from the provided 
    /// <see cref="IConfiguration"/> instance.
    /// </summary>
    /// <param name="appConfiguration">The configuration instance to bind values 
    /// from.</param>
    /// <exception cref="ConfigurationErrorsException">
    /// Thrown when the DemoValues section is missing or not properly configured.
    /// </exception>
    public AppConfig(IConfiguration appConfiguration)
    {
        appConfiguration.Bind(this);
        if (DemoValues == null || !DemoValues.Validate())
        {
            throw new ConfigurationErrorsException(
                "DemoValues section is missing or not properly configured in " +
                $"{nameof(appConfiguration)}");
        }
    }
}