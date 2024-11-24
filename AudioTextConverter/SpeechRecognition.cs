using System.IO;
using System.Threading.Tasks;
using Speechmatics.Transcribe.Api;
using Speechmatics.Transcribe.Client;
using Speechmatics.Transcribe.Model;

public class SpeechRecognition
{
    private readonly string speechmaticsApiKey = "VLT0ZIBLRTqfTk9DMqpRoYd4i1cUOxsm"; // Replace with your Speechmatics API key

    public async Task<string> RecognizeSpeechAsync(string audioFilePath, string language)
    {
        var c = new Configuration();
        c.AddDefaultHeader("Authorization", $"Bearer {speechmaticsApiKey}");
        c.BasePath = "https://asr.api.speechmatics.com/v2";

        var api = new DefaultApi(c);
        string jobId;

        using (var s = File.OpenRead(audioFilePath))
        {
            var jobConfig = $"{{\"type\":\"transcription\", \"transcription_config\":{{\"language\":\"{language}\"}}}}";
            var answer = api.JobsPost(jobConfig, s);
            jobId = answer.Id;
        }

        RetrieveJobResponse jobResponse = null;
        while (true)
        {
            await Task.Delay(5000);
            jobResponse = await api.JobsJobidGetAsync(jobId);
            if (jobResponse.Job.Status == JobDetails.StatusEnum.Done)
            {
                break;
            }
        }

        var transcriptResponse = await api.JobsJobidTranscriptGetAsync(jobId);
        string fullTranscript = "";
        foreach (var result in transcriptResponse.Results)
        {
            foreach (var alternative in result.Alternatives)
            {
                fullTranscript += alternative.Content + " ";
            }
        }

        return fullTranscript;
    }
}
