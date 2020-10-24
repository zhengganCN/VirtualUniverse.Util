using Google.Protobuf;
using Grpc.Net.Client;
using GrpcIdGenerateService;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace NUnitTestAmazedUtil.AmazedIdPool.Services
{
    public class IdGenerateService
    {
        private readonly ILogger<IdGenerateService> _logger;
        public IdGenerateService(ILogger<IdGenerateService> logger)
        {
            _logger = logger;
        }
        public byte[] RequestIds(string ip)
        {
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator//不验证证书的有效性
            };
            var channel = GrpcChannel.ForAddress(ip, new GrpcChannelOptions
            {
                HttpHandler = httpClientHandler
            });
            
            var client = new SnowflakeIdGenerate.SnowflakeIdGenerateClient(channel);
            try
            {
                var result = client.GenerateIds(new GenerateIdsRequest
                {
                    IdNumber = 1000
                });
                if (result.Ids != null)
                {
                    return result.Ids.ToByteArray();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return null;
        }

        public List<long> GenerateIds(string ip)
        {
            var idsByte = RequestIds(ip);
            var length = idsByte.Length / 8;
            var ids = new List<long>(length);
            for (int i = 0; i < length; i++)
            {
                var start = i * 8;
                var idBytes = new byte[8] 
                {
                    idsByte[start],
                    idsByte[start+1],
                    idsByte[start+2],
                    idsByte[start+3],
                    idsByte[start+4],
                    idsByte[start+5],
                    idsByte[start+6],
                    idsByte[start+7]  
                };
                var id = BitConverter.ToInt64(idBytes);
                ids.Add(id);
            }
            return ids;
        }
    }
}
