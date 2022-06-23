using System;
using System.Text.Json;
using System.Threading.Tasks;
using ImageLabeller.Models;
using ImageLabeller.Services;

namespace Thulir.Core.Services
{
    public class ThulirSecrets
    {
        public async Task<ImageLabellerGlobals> GetThulirGlobals()
        {
            string thulirGlobalsStr = SecretsClient.GetSecret("thulir-globals");
            
            return JsonSerializer.Deserialize<ImageLabellerGlobals>(thulirGlobalsStr);
        }
    }
}