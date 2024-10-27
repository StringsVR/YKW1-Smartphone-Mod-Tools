using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YKW1_Smartphone_Mod_Tools
{
    static class FileUtils
    {
        public static async Task CopyAsync(string? inputPath, string outputPath)
        {
            if (inputPath == null)
            {
                return;
            }

            var openForReading = new FileStreamOptions { Mode = FileMode.Open };
            await using FileStream source = new FileStream(inputPath, openForReading);

            var createForWriting = new FileStreamOptions
            {
                Mode = FileMode.Create,
                Access = FileAccess.Write,
                Options = FileOptions.Asynchronous,
                BufferSize = 0, // disable FileStream buffering
                PreallocationSize = source.Length // specify size up-front
            };
            await using FileStream destination = new FileStream(outputPath, createForWriting);
            await source.CopyToAsync(destination);
        }
    }
}
