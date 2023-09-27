using System.Net;
using Amazon.S3;
using Amazon.S3.Transfer;
using BookstoreAPI.CustomExceptions.Exceptions;

namespace BookstoreAPI.Services;

public sealed class AWSS3Service
{
    private readonly IAmazonS3 _s3Client;
    private readonly ILogger<AWSS3Service> _logger;
    private readonly string  _awsBucketName = Environment.GetEnvironmentVariable("AWS_BUCKET_NAME") ?? throw new InvalidOperationException();

    public AWSS3Service(IAmazonS3 s3Client,  ILogger<AWSS3Service> logger )
    {
        _s3Client = s3Client;
        _logger = logger;
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string folderPath, string fileName)
    {
        try
        {
            var key = $"{folderPath}/{fileName}";

            var fileTransferUtility = new TransferUtility(_s3Client);
            await fileTransferUtility.UploadAsync(fileStream, _awsBucketName, key);

            return $"https://{_awsBucketName}.s3.amazonaws.com/{key}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new InternalServerException(ex.Message, HttpStatusCode.InternalServerError);
        }
    }
}