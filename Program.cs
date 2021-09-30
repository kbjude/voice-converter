using System;
using System.IO;

namespace voiceconverter
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //- Iterate through the subfolders of /mtn/uploads
            foreach(var subfolder in Directory.GetDirectories("/mnt/uploads"))
            {
                //- get the path to metadata file

                var metadataFilePath = Path.Combine(subfolder, "metadata.json");
                Console.WriteLine($"Reading {metadataFilePath}");
                //- extract metadata, including audio file info from metadata file

                //* For each audio
                //- Get the absolute path.
                //- verify file checksum
                //- generate a unique identifier
                //- compress it
                //- create its standalon meta file
            }

            Console.WriteLine("Hello World!");
        }
    }
}
