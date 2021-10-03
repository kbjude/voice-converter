using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace voiceconverter
{
    class Program
    {
        static void Main(string[] args)
        {

            //- Iterate through the subfolders of /mtn/uploads
            foreach (var subfolder in Directory.GetDirectories("/Users/apple/www/projects/innovationvillage/learning/dotnet/uploads"))
            {
                //- get the path to metadata file

                var metadataFilePath = Path.Combine(subfolder, "metadata.json");
                Console.WriteLine($"Reading {metadataFilePath}");
                //- extract metadata, including audio file info from metadata file
                var metadataCollection = GetMetada(metadataFilePath);

                //* For each audio

                foreach (var metadata in metadataCollection)
                {
                    //- Get the absolute path.
                    var audioFilePath = Path.Combine(subfolder, metadata.File.FileName);
                    //- verify file checksum

                    var md5Checksum = GetChecksum(audioFilePath);
                    if (md5Checksum.Replace("-", "").ToLower() != metadata.File.Md5CheckSum)
                    {
                        throw new Exception("Checksum not verified! File corrupted");
                    }
                    //- generate a unique identifier we shall use the globally unique Id

                    var uniqurId = Guid.NewGuid();
                    metadata.File.FileName = uniqurId + ".mp3";
                    var newPath = Path.Combine("/Users/apple/www/projects/innovationvillage/learning/dotnet/uploads", uniqurId + ".mp3");
                    //- compress it
                    //- create its standalon meta file
                }
            }
        }

        static object GetChecksum(string audioFilePath)
        {
            var fileStream = File.Open(audioFilePath, FileMode.Open);
            var md5 = System.Cryptography.MD5.Create();
            var md5Bytes = md5.ComputeHash(fileStream);
            fileStream.Dispose();
            return BitConverter.ToString(md5Bytes);

        }

        static List<Metadata> GetMetada(string metadataFilePath)
        {
            var metadataFileStream = File.Open(metadataFilePath, FileMode.Open);
            var settings = new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new DateTimeFormat("yyy-MM-dd'T'HH:mm:ssZ")
            };
            var serializer = new DataContractJsonSerializer(typeof(List<Metadata>), settings);
            return (List<Metadata>)serializer.ReadObject(metadataFileStream);
        }
    }
}
