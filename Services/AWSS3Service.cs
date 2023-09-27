using System.Net;
using Amazon.S3;
using Amazon.S3.Transfer;
using BookstoreAPI.CustomExceptions.Exceptions;

namespace BookstoreAPI.Services;

public sealed class AWSS3Service
{
    private readonly IAmazonS3 _s3Client;
    private readonly ILogger<AWSS3Service> _logger;

    public AWSS3Service(IAmazonS3 s3Client,  ILogger<AWSS3Service> logger )
    {
        _s3Client = s3Client;
        _logger = logger;
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string bucketName, string folderPath, string fileName)
    {
        try
        {
            var key = $"{folderPath}/{fileName}";

            var fileTransferUtility = new TransferUtility(_s3Client);
            await fileTransferUtility.UploadAsync(fileStream, bucketName, key);

            return $"https://{bucketName}.s3.amazonaws.com/{key}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new InternalServerException(ex.Message, HttpStatusCode.InternalServerError);
        }
    }
}