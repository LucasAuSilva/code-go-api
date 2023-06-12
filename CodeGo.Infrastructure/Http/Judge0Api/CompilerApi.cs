
using CodeGo.Application.Common.Interfaces.Http;
using CodeGo.Infrastructure.Http.Judge0Api.Models;
using Microsoft.Extensions.Options;
using RestSharp;

namespace CodeGo.Infrastructure.Http.Judge0Api;

public class CompilerApi : ICompilerApi
{
    private readonly RestClient _httpClient;
    private readonly Judge0Settings _judge0Settings;

    public CompilerApi(IOptions<Judge0Settings> judge0Settings)
    {
        _judge0Settings = judge0Settings.Value;
        var options = new RestClientOptions(_judge0Settings.Host);
        _httpClient = new RestClient(options);
        SetupHttpClient();
    }

    private void SetupHttpClient()
    {
        _httpClient.AddDefaultHeader("content-type", "application/json");
        _httpClient.AddDefaultHeader("Content-Type", "application/json");
        _httpClient.AddDefaultHeader("X-RapidAPI-Key", _judge0Settings.ApiKey);
    }

    public async Task<string> SendCodeToCompile(string code)
    {
        var codeInBytes = System.Text.Encoding.UTF8.GetBytes(code);
        var codeInBase64 = Convert.ToBase64String(codeInBytes);

        var body = new SubmissionModel(93, codeInBase64);
        var createSubmissionResponse = await CreateSubmission(body);
        var getSubmissionResponse = await GetSubmission(createSubmissionResponse.Token);

        var resultBase64InBytes = Convert.FromBase64String(getSubmissionResponse.Stdout);
        return System.Text.Encoding.UTF8.GetString(resultBase64InBytes);
    }

    private async Task<GetSubmissionResponse> GetSubmission(string token)
    {
        //TODO: validation for error on request
        var request = new RestRequest("/submissions/{token}", Method.Get)
            .AddUrlSegment("token", token);
        request.AddQueryParameter("base64_encoded", "true");
        request.AddQueryParameter("fields", "*");
        var response = await _httpClient.GetAsync<GetSubmissionResponse>(request);
        return response!;
    }

    private async Task<CreateSubmissionResponse> CreateSubmission(SubmissionModel body)
    {
        //TODO: validation for error on request
        var request = new RestRequest("/submissions", Method.Post);
        request.AddQueryParameter("base64_encoded", "true");
        request.AddQueryParameter("fields", "*");
        request.AddJsonBody(body);
        var response = await _httpClient.PostAsync<CreateSubmissionResponse>(request);
        return response!;
    }
}
