using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Supabase;

namespace Stacker.Supabase
{
    internal class Buckets : ConnectionBase
    {
        public async Task<string> UploadFile(string bucketName, string filePath)
        {
            if (!Connected)
            {
                throw new Exception("Not initialized");
            }

            if (!File.Exists(filePath))
            {
                return string.Empty;
            }

            var storage = Client.Instance.Storage;
            var bucket = storage.From(bucketName);

            if (bucket is null)
            {
                return string.Empty;
            }

            var bucketFileName = $"{Guid.NewGuid()}.{Path.GetExtension(filePath)}";

            await bucket.Upload(filePath, bucketFileName);

            return bucketFileName;
        }
    }
}
