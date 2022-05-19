using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ModelConversionApp.Clients.ErStudio.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ModelConversionApp.Clients.ErStudio;

internal class ErStudioClient
{
    private readonly ILogger _logger;
    private readonly string BaseUri;
    private readonly string AuthToken;
    private readonly HttpClient Client;

    public ErStudioClient(string authToken, IConfiguration configuration, ILogger<ErStudioClient> logger) 
    {
        _logger = logger;
        BaseUri = configuration.GetValue<string>(key: "ErStudioServer:BaseUri", defaultValue: "");
        AuthToken = authToken;
        Client = new HttpClient();
        
        // Setup client
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        Client.DefaultRequestHeaders.Add("User-Agent", "Er Studio Model Converter");
    }

    private async Task<Model?> GetModelAsync(string modelName)
    {
        // Get Models
        string relativeUri = $"/api/v1/models?access_token={AuthToken}";
        var response = await Client.GetAsync(requestUri: new Uri(baseUri: new Uri(uriString: BaseUri), relativeUri: relativeUri));

        // Ensure successful request
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError(message: "Error when trying to load models from model repository", response.StatusCode);
        }

        // Serialize response
        Stream responseStream = await response.Content.ReadAsStreamAsync();
        var modelResponse = await JsonSerializer.DeserializeAsync<ModelResponse>(responseStream);

        // Return model
        if (modelResponse != null)
        {
            foreach (var model in modelResponse.Models)
            {
                if (model.Name.Equals(modelName))
                {
                    return model;
                }
            }
        }
        return null;
    }

    private async Task<List<Entity>?> GetEntitiesAsync(int modelId)
    {
        // Get entities
        string relativeUri = $"/api/v1/models/{modelId}/entities?access_token={AuthToken}";
        var response = await Client.GetAsync(requestUri: new Uri(baseUri: new Uri(uriString: BaseUri), relativeUri: relativeUri));

        // Ensure successful request
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError(message: $"Error when trying to get entities for model {modelId}", response.StatusCode);
        }

        // Serialize response
        Stream responseStream = await response.Content.ReadAsStreamAsync();
        var entityResponse = await JsonSerializer.DeserializeAsync<EntityResponse>(responseStream);

        // Return entities
        if (entityResponse != null)
        {
            return entityResponse.Entities;
        }
        return null;
    }

    private async Task<List<Table>?> GetTablesAsync(int modelId)
    {
        // Get entities
        string relativeUri = $"/api/v1/models/{modelId}/tables?access_token={AuthToken}";
        var response = await Client.GetAsync(requestUri: new Uri(baseUri: new Uri(uriString: BaseUri), relativeUri: relativeUri));

        // Ensure successful request
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError(message: $"Error when trying to get entities for model {modelId}", response.StatusCode);
        }

        // Serialize response
        Stream responseStream = await response.Content.ReadAsStreamAsync();
        var tableResponse = await JsonSerializer.DeserializeAsync<TableResponse>(responseStream);

        // Return entities
        if (tableResponse != null)
        {
            return tableResponse.Tables;
        }
        return null;
    }

    private async Task<List<Foreignkey>?> GetForeignKeysAsync(int modelId)
    {
        // Get entities
        string relativeUri = $"/api/v1/models/{modelId}/foreignkeys?access_token={AuthToken}";
        var response = await Client.GetAsync(requestUri: new Uri(baseUri: new Uri(uriString: BaseUri), relativeUri: relativeUri));

        // Ensure successful request
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError(message: $"Error when trying to get foreign keys for model {modelId}", response.StatusCode);
        }

        // Serialize response
        Stream responseStream = await response.Content.ReadAsStreamAsync();
        var foreignkeyResponse = await JsonSerializer.DeserializeAsync<ForeignkeyResponse>(responseStream);

        // Return entities
        if (foreignkeyResponse != null)
        {
            return foreignkeyResponse.Foreignkeys;
        }
        return null;
    }

    private void CreateLakeDataBase(Model model, List<Table> tables, List<Foreignkey> foreignkeys)
    {
        // Create Lake Database
        var lakeDatabase = new ModelConversionApp.Models.LakeDatabase.LakeDatabase();

        // Update database
        lakeDatabase.Database.Name = model.Name;
        lakeDatabase.Database.Description = $"Project: '{model.Project}', Diagram: '{model.Diagram}', Notation: '{model.Notation}'";

        // Add tables
        foreach (var table in tables)
        {
            var lakeDatabaseTable = new ModelConversionApp.Models.LakeDatabase.Table()
            {
                Name = table.Name,
                Description = table.Description,
                Namespace = new ModelConversionApp.Models.LakeDatabase.Namespace()
                {
                    DatabaseName = model.Name
                }
            };

            foreach (var column in table.Columns)
            {
                // Convert data type
                var dataType = DataTypeMapper.ConvertErStudioToLakeDatabaseDataType(dataType: column.Datatype);

                // Create column
                var lakeDatabaseTableColumn = new ModelConversionApp.Models.LakeDatabase.Column()
                {
                    Name = column.Name,
                    Description = column.Description,
                    OriginDataTypeName = new ModelConversionApp.Models.LakeDatabase.OriginDataType()
                    {
                        TypeName = dataType,
                        IsComplexType = false,
                        IsNullable = column.IsNullable.Equals("NULL") ? true : false,
                        Length = dataType.Equals("string") && int.TryParse(column.Length, out var length) ? length : 0,
                        Precision = dataType.Equals("decimal") && int.TryParse(column.Length, out var length) ? length : 0,
                        Scale = int.TryParse(column.Scale, out var scale) ? scale : 0,
                        Properties = new ModelConversionApp.Models.LakeDatabase.OriginDataTypeProperties()
                        {
                            HiveTypeString = dataType,
                            TimestampFormat = dataType.Equals("timestamp") ? "YYYY-MM-DD HH:MM:SS.fffffffff" : null,
                            DateFormat = dataType.Equals("date") ? "YYYY-MM-DD" : null
                        }
                    }
                };

                // Add column to table
                lakeDatabaseTable.AddColumn(column: lakeDatabaseTableColumn);

                // Add column to primary keys
                if (column.PrimaryKey.Equals("True"))
                {
                    lakeDatabaseTable.Properties.PrimaryKeys += column.Name;
                }
            }

            // Add table to table list
            lakeDatabase.Tables.Add(item: lakeDatabaseTable);
        }

        // Add relationships
        var relationships = new Dictionary<string, DataType>();
        foreach (var foreignKey in foreignkeys)
        {
            // Create relationship
            var lakeDatabaseRelationship = new ModelConversionApp.Models.LakeDatabase.Relationship()
            {
                Name = $"{foreignKey.ParentTableName}-{foreignKey.ChildTableName}",
                Namespace = new ModelConversionApp.Models.LakeDatabase.Namespace()
                {
                    DatabaseName = model.Name
                }
            };
        }
    }

    public async Task GetSchemaAsync(string modelName)
    {
        // Get model
        var model = await GetModelAsync(modelName: modelName);
        if (model == null)
        {
            _logger.LogError(message: $"No model with name {modelName} found");
            throw new Exception($"No model with name {modelName} found");
        }

        // Get tables
        var tables = await GetTablesAsync(modelId: model.Id);

        // Get foreign keys
        var foreignkeys = await GetForeignKeysAsync(modelId: model.Id);

    }
}
