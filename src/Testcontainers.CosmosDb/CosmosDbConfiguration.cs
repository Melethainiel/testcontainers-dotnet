namespace Testcontainers.CosmosDb;

/// <inheritdoc cref="ContainerConfiguration" />
[PublicAPI]
public sealed class CosmosDbConfiguration : ContainerConfiguration
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CosmosDbConfiguration" /> class.
    /// </summary>
    /// <param name="port">The CosmosDB port.</param>
    /// <param name="database">The database name used in CosmosDB</param>
    /// <param name="partitionCount">The number of partition used in CosmosDB</param>
    public CosmosDbConfiguration(
        int port = 8081,
        string database = null,
        int partitionCount = 2)
    {
        Port = port;
        Database = database;
        PartitionCount = partitionCount;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CosmosDbConfiguration" /> class.
    /// </summary>
    /// <param name="resourceConfiguration">The Docker resource configuration.</param>
    public CosmosDbConfiguration(IResourceConfiguration<CreateContainerParameters> resourceConfiguration)
        : base(resourceConfiguration)
    {
        // Passes the configuration upwards to the base implementations to create an updated immutable copy.
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CosmosDbConfiguration" /> class.
    /// </summary>
    /// <param name="resourceConfiguration">The Docker resource configuration.</param>
    public CosmosDbConfiguration(IContainerConfiguration resourceConfiguration)
        : base(resourceConfiguration)
    {
        // Passes the configuration upwards to the base implementations to create an updated immutable copy.
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CosmosDbConfiguration" /> class.
    /// </summary>
    /// <param name="resourceConfiguration">The Docker resource configuration.</param>
    public CosmosDbConfiguration(CosmosDbConfiguration resourceConfiguration)
        : this(new CosmosDbConfiguration(), resourceConfiguration)
    {
        // Passes the configuration upwards to the base implementations to create an updated immutable copy.
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CosmosDbConfiguration" /> class.
    /// </summary>
    /// <param name="oldValue">The old Docker resource configuration.</param>
    /// <param name="newValue">The new Docker resource configuration.</param>
    public CosmosDbConfiguration(CosmosDbConfiguration oldValue, CosmosDbConfiguration newValue)
        : base(oldValue, newValue)
    {
        Port = BuildConfiguration.Combine(oldValue.Port, newValue.Port);
        Database = BuildConfiguration.Combine(oldValue.Database, newValue.Database);
        PartitionCount = BuildConfiguration.Combine(oldValue.PartitionCount, newValue.PartitionCount);
    }

    /// <summary>
    /// Get the CosmosDB port
    /// </summary>
    public int Port { get;  }
    
    /// <summary>
    /// Get the name of the database 
    /// </summary>
    public string Database { get;  }
    
    /// <summary>
    /// Get the number of partition used in CosmosDB
    /// </summary>
    public int PartitionCount { get;  }

}
