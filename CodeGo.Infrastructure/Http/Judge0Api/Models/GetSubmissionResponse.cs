
using System.Text.Json.Serialization;

namespace CodeGo.Infrastructure.Http.Judge0Api.Models;

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);

public record GetSubmissionResponse(
    [property: JsonPropertyName("source_code")] string SourceCode,
    [property: JsonPropertyName("language_id")] int? LanguageId,
    [property: JsonPropertyName("stdin")] string Stdin,
    [property: JsonPropertyName("expected_output")] object ExpectedOutput,
    [property: JsonPropertyName("stdout")] string? Stdout,
    [property: JsonPropertyName("status_id")] int? StatusId,
    [property: JsonPropertyName("created_at")] DateTime? CreatedAt,
    [property: JsonPropertyName("finished_at")] DateTime? FinishedAt,
    [property: JsonPropertyName("time")] string Time,
    [property: JsonPropertyName("memory")] int? Memory,
    [property: JsonPropertyName("stderr")] object Stderr,
    [property: JsonPropertyName("token")] string Token,
    [property: JsonPropertyName("number_of_runs")] int? NumberOfRuns,
    [property: JsonPropertyName("cpu_time_limit")] string CpuTimeLimit,
    [property: JsonPropertyName("cpu_extra_time")] string CpuExtraTime,
    [property: JsonPropertyName("wall_time_limit")] string WallTimeLimit,
    [property: JsonPropertyName("memory_limit")] int? MemoryLimit,
    [property: JsonPropertyName("stack_limit")] int? StackLimit,
    [property: JsonPropertyName("max_processes_and_or_threads")] int? MaxProcessesAndOrThreads,
    [property: JsonPropertyName("enable_per_process_and_thread_time_limit")] bool? EnablePerProcessAndThreadTimeLimit,
    [property: JsonPropertyName("enable_per_process_and_thread_memory_limit")] bool? EnablePerProcessAndThreadMemoryLimit,
    [property: JsonPropertyName("max_file_size")] int? MaxFileSize,
    [property: JsonPropertyName("compile_output")] object CompileOutput,
    [property: JsonPropertyName("exit_code")] int? ExitCode,
    [property: JsonPropertyName("exit_signal")] object ExitSignal,
    [property: JsonPropertyName("message")] object Message,
    [property: JsonPropertyName("wall_time")] string WallTime,
    [property: JsonPropertyName("compiler_options")] object CompilerOptions,
    [property: JsonPropertyName("command_line_arguments")] object CommandLineArguments,
    [property: JsonPropertyName("redirect_stderr_to_stdout")] bool? RedirectStderrToStdout,
    [property: JsonPropertyName("callback_url")] object CallbackUrl,
    [property: JsonPropertyName("additional_files")] object AdditionalFiles,
    [property: JsonPropertyName("enable_network")] bool? EnableNetwork,
    [property: JsonPropertyName("status")] Status Status
);

public record Status(
    [property: JsonPropertyName("id")] int? Id,
    [property: JsonPropertyName("description")] string Description
);

// public record GetSubmissionResponse(
//     string SourceCode,
//     string Stdout,
//     string Stderr,
//     string Created_at,
//     string Finished_at,
//     int number_of_runs
// );


// source_code:"base64 code"
// language_id:93
// stdin:null
// expected_output:null
// stdout:"SGVsbG8gV29ybGQK "
// status_id:3
// created_at:"2023-04-13T00:12:37.048Z"
// finished_at:"2023-04-13T00:12:39.467Z"
// time:"1.638"
// memory:36032
// stderr:null
// token:"abe1d460-2d48-4655-817e-c7bd5f1e3b65"
// number_of_runs:1
// cpu_time_limit:"5.0"
// cpu_extra_time:"1.0"
// wall_time_limit:"10.0"
// memory_limit:128000
// stack_limit:64000
// max_processes_and_or_threads:60
// enable_per_process_and_thread_time_limit:false
// enable_per_process_and_thread_memory_limit:false
// max_file_size:1024
// compile_output:null
// exit_code:0
// exit_signal:null
// message:null
// wall_time:"1.833"
// compiler_options:null
// command_line_arguments:null
// redirect_stderr_to_stdout:false
// callback_url:null
// additional_files:null
// enable_network:false
