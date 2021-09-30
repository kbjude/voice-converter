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
                //- Get the absolute path.
                //- verify file checksum
                //- generate a unique identifier
                //- compress it
                //- create its standalon meta file
            }
        }


        static List<Metadata> GetMetada(string metadataFilePath)
        {
            var metadataFileStream = File.Open(metadataFilePath, FileMode.Open);
            var settings = new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new DateTimeFormat("yyy-MM-dd'T'HH:mm:ssZ")
            };
            var serializer = new DataContractJsonSerializer(typeof(List<Metadata>), settings);
            return  (List<Metadata>)serializer.ReadObject(metadataFileStream);
        }
    }
}
