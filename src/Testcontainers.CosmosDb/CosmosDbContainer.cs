namespace Testcontainers.CosmosDb;

/// <inheritdoc cref="DockerContainer" />
[PublicAPI]
public sealed class CosmosDbContainer : DockerContainer
{
    private readonly CosmosDbConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="CosmosDbContainer" /> class.
    /// </summary>
    /// <param name="configuration">The container configuration.</param>
    /// <param name="logger">The logger.</param>
    public CosmosDbContainer(
        CosmosDbConfiguration configuration,
        ILogger logger) : base(configuration, logger)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Gets the CosmosDb connection string.
    /// </summary>
    /// <returns>The CosmosDb connection string.</returns>
    public string GetConnectionString()
    {
        return $"AccountEndpoint=https://{Hostname}:{_configuration.Port};AccountKey={_configuration.Password}";
    }
}