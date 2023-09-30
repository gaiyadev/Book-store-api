using System.Net;
using Amazon.S3;
using Amazon.S3.Transfer;
using BookstoreAPI.CustomExceptions.Exceptions;

namespace BookstoreAPI.Services;

public sealed class Awss3Service
{
    private readonly AmazonS3Client _s3Client;
    private readonly ILogger<Awss3Service> _logger;
    private readonly string  _awsBucketName = Environment.GetEnvironmentVariable("AWS_BUCKET_NAME") ?? throw new InvalidOperationException();
    private readonly string _awsSecretAccessKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY") ?? throw new InvalidOperationException();
    private readonly string _awsAccessKeyId = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID") ?? throw new InvalidOperationException();

    public Awss3Service(ILogger<Awss3Service> logger )
    {
        _logger = logger;
        _s3Client = new AmazonS3Client(_awsAccessKeyId, _awsSecretAccessKey, Amazon.RegionEndpoint.USEast1);

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
        catch (AmazonS3Exception ex)
        {
            _logger.LogError($"S3 Error: {ex.Message}", ex);
            throw new InternalServerException(ex.Message, HttpStatusCode.InternalServerError);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected error: {ex.Message}", ex);
            throw new InternalServerException("An unexpected error occurred while uploading the file.", HttpStatusCode.InternalServerError);
        }
    }

}