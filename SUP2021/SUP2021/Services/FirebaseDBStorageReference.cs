using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Firebase;
using Firebase.Storage;
using Newtonsoft.Json;

namespace SUP2021.Services
{
    public class FirebaseDBStorageReference
    {
        private const string FirebaseStorageEndpoint = "https://firebasestorage.googleapis.com/v0/b/";

        private readonly FirebaseStorage storage;
        private readonly List<string> children;

        internal FirebaseDBStorageReference(FirebaseStorage storage, string childRoot)
        {
            this.children = new List<string>();

            this.storage = storage;
            this.children.Add(childRoot);
        }

        
        public FirebaseStorageTask PutAsync(Stream stream, CancellationToken cancellationToken)
        {
            return new FirebaseStorageTask(this.storage.Options, this.GetTargetUrl(), this.GetFullDownloadUrl(), stream, cancellationToken);
        }

        
        public FirebaseStorageTask PutAsync(Stream fileStream)
        {
            return this.PutAsync(fileStream, CancellationToken.None);
        }
              
        public async Task<string> GetDownloadUrlAsync()
        {
            var url = this.GetDownloadUrl();
            var resultContent = "N/A";

            try
            {
                using (var http = await this.storage.Options.CreateHttpClientAsync().ConfigureAwait(false))
                {
                    var result = await http.GetAsync(url);
                    resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(resultContent);

                    result.EnsureSuccessStatusCode();

                    return this.GetFullDownloadUrl() + data["downloadTokens"];
                }
            }
            catch (Exception ex)
            {
                throw new FirebaseStorageException(url, resultContent, ex);
            }
        }
                
        public async Task DeleteAsync()
        {
            var url = this.GetDownloadUrl();
            var resultContent = "N/A";

            try
            {
                using (var http = await this.storage.Options.CreateHttpClientAsync().ConfigureAwait(false))
                {
                    var result = await http.DeleteAsync(url).ConfigureAwait(false);

                    resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

                    result.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                throw new FirebaseStorageException(url, resultContent, ex);
            }
        }

    
        public FirebaseDBStorageReference Child(string name)
        {
            this.children.Add(name);
            return this;
        }

        private string GetTargetUrl()
        {
            return $"{FirebaseStorageEndpoint}{this.storage.StorageBucket}/o?name={this.GetEscapedPath()}";
        }

        private string GetDownloadUrl()
        {
            return $"{FirebaseStorageEndpoint}{this.storage.StorageBucket}/o/{this.GetEscapedPath()}";
        }

        private string GetFullDownloadUrl()
        {
            return this.GetDownloadUrl() + "?alt=media&token=";
        }

        private string GetEscapedPath()
        {
            return Uri.EscapeDataString(string.Join("/", this.children));
        }
    }
}
