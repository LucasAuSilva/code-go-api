
using System.Net.Http.Headers;
using System.Text.Json;
using CodeGo.Application.Common.Interfaces.Http;
using CodeGo.Domain.Common.Enums;
using CodeGo.Infrastructure.Http.Judge0Api.Models;

namespace CodeGo.Infrastructure.Http.Judge0Api;

public class CompilerApi : ICompilerApi
{
    private readonly HttpClient _httpClient;

    public CompilerApi(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> SendCodeToCompile(string code, Language language)
    {
        var languageId = GetJudgeLanguageId(language);
        var codeInBytes = System.Text.Encoding.UTF8.GetBytes(code);
        var codeInBase64 = Convert.ToBase64String(codeInBytes);

        var body = new SubmissionModel(languageId, codeInBase64);
        var createSubmissionResponse = await CreateSubmission(body);
        var getSubmissionResponse = await GetSubmission(createSubmissionResponse.Token);

        var resultBase64InBytes = Convert.FromBase64String(getSubmissionResponse.Stdout ?? "");
        return System.Text.Encoding.UTF8.GetString(resultBase64InBytes).Replace("\n", "");
    }

    private static int GetJudgeLanguageId(Language language)
    {
        var languageId = 0;
        language
            .When(Language.Javascript).Then(() =>
            {
                languageId = 93;
            })
            .When(Language.Python).Then(() =>
            {
                languageId = 71;
            })
            .When(Language.Csharp).Then(() => 
            {
                languageId = 51;
            });
        return languageId;
    }

    private async Task<GetSubmissionResponse> GetSubmission(string token)
    {
        //TODO: validation for error on request
        await Task.Delay(TimeSpan.FromSeconds(3));
        var response = await _httpClient.GetAsync($"submissions/{token}?base64_encoded=true&fields=*");
        response.EnsureSuccessStatusCode();
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var submission = JsonSerializer.Deserialize<GetSubmissionResponse>(jsonResponse);
        return submission!;
    }

    private async Task<CreateSubmissionResponse> CreateSubmission(SubmissionModel body)
    {
        //TODO: validation for error on request
        var content = new StringContent(JsonSerializer.Serialize(body), new MediaTypeHeaderValue("application/json"));
        var response = await _httpClient.PostAsync($"/submissions?base64_encoded=true&fields=*", content);
        response.EnsureSuccessStatusCode();
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var createSubmissionResponse = JsonSerializer.Deserialize<CreateSubmissionResponse>(jsonResponse);
        return createSubmissionResponse!;
    }
}
