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
        string relativeUri = $"/v1/models?access_token={AuthToken}";
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

    private async Task<List<Entity>?> GetEntitiesAsync(string modelId)
    {
        // Get entities
        string relativeUri = $"/v1/models/{modelId}/entities?access_token={AuthToken}";
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

    private async Task<List<Table>?> GetTablesAsync(string modelId)
    {
        // Get entities
        string relativeUri = $"/v1/models/{modelId}/tables?access_token={AuthToken}";
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

    private async Task<List<Foreignkey>?> GetForeignKeysAsync(string modelId)
    {
        // Get entities
        string relativeUri = $"/v1/models/{modelId}/foreignkeys?access_token={AuthToken}";
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

    public async Task GetSchemaAsync(string modelName)
    {

    }
}
