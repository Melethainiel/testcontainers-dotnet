namespace Testcontainers.CosmosDb;

/// <inheritdoc cref="ContainerBuilder{TBuilderEntity, TContainerEntity, TConfigurationEntity}" />
[PublicAPI]
public sealed class CosmosDbBuilder : ContainerBuilder<CosmosDbBuilder, CosmosDbContainer, CosmosDbConfiguration>
{
    public const string CosmosDbImage = "Cosmos:6.0";

    public const string DefaultDatabaseName = "default";
    public const int DefaultDbPort = 8081;


    /// <summary>
    /// Initializes a new instance of the <see cref="CosmosDbBuilder" /> class.
    /// </summary>
    public CosmosDbBuilder() : this(new CosmosDbConfiguration())
    {
        DockerResourceConfiguration = Init()
            .DockerResourceConfiguration;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CosmosDbBuilder" /> class.
    /// </summary>
    /// <param name="resourceConfiguration">The Docker resource configuration.</param>
    private CosmosDbBuilder(CosmosDbConfiguration resourceConfiguration) : base(resourceConfiguration)
    {
        DockerResourceConfiguration = resourceConfiguration;
    }

    /// <inheritdoc />
    protected override CosmosDbConfiguration DockerResourceConfiguration { get; }

    /// <summary>
    /// Sets the CosmosDb database name.
    /// </summary>
    /// <param name="database">The CosmosDb database name.</param>
    /// <returns>A configured instance of <see cref="CosmosDbBuilder" />.</returns>
    public CosmosDbBuilder WithDatabase(string database)
    {
        return Merge(DockerResourceConfiguration, new CosmosDbConfiguration(database: database))
            .WithEnvironment("AZURE_COSMOS_EMULATOR_DATABASE", database);
    }
    
    /// <summary>
    /// Sets the CosmosDb port.
    /// </summary>
    /// <param name="port">The CosmosDb port.</param>
    /// <returns>A configured instance of <see cref="CosmosDbBuilder" />.</returns>
    public CosmosDbBuilder WithPort(int port)
    {
        return Merge(DockerResourceConfiguration, new CosmosDbConfiguration(port: port))
            .WithPortBinding(port, port)
            .WithEnvironment("AZURE_COSMOS_EMULATOR_ARGS", $"/Port={port}");
    }

    /// <inheritdoc />
    public override CosmosDbContainer Build()
    {
        Validate();
        return new CosmosDbContainer(DockerResourceConfiguration, TestcontainersSettings.Logger);
    }

    /// <inheritdoc />
    protected override CosmosDbBuilder Init()
    {
        return base.Init()
            .WithImage(CosmosDbImage)
            .WithPort(DefaultDbPort)
            .WithDatabase(DefaultDatabaseName)
            .WithWaitStrategy(
                Wait.ForUnixContainer()
                    .AddCustomWaitStrategy(new WaitUntil()));
    }

    /// <inheritdoc />
    protected override void Validate()
    {
        base.Validate();

        _ = Guard.Argument(DockerResourceConfiguration.Database, nameof(DockerResourceConfiguration.Database))
            .NotNull()
            .NotEmpty();
    }

    /// <inheritdoc />
    protected override CosmosDbBuilder Clone(IResourceConfiguration<CreateContainerParameters> resourceConfiguration)
    {
        return Merge(DockerResourceConfiguration, new CosmosDbConfiguration(resourceConfiguration));
    }

    /// <inheritdoc />
    protected override CosmosDbBuilder Clone(IContainerConfiguration resourceConfiguration)
    {
        return Merge(DockerResourceConfiguration, new CosmosDbConfiguration(resourceConfiguration));
    }

    /// <inheritdoc />
    protected override CosmosDbBuilder Merge(
        CosmosDbConfiguration oldValue,
        CosmosDbConfiguration newValue)
    {
        return new CosmosDbBuilder(new CosmosDbConfiguration(oldValue, newValue));
    }

    /// <inheritdoc cref="IWaitUntil" />
    private sealed class WaitUntil : IWaitUntil
    {
        private static readonly IEnumerable<string> Pattern = new[]
        {
            "Started"
        };

        /// <inheritdoc />
        public async Task<bool> UntilAsync(IContainer container)
        {
            var (stdout, _) = await container.GetLogs(timestampsEnabled: false)
                .ConfigureAwait(false);
            return Pattern.Contains(stdout);
        }
    }
}