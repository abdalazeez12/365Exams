using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtihani.Models
{
    public class FileUploadResult
    {
        public bool IsAllSucceeded { get; set; }

        public List<string> UploadedFiles { get; set; }

        public List<string> FailedFiles { get; set; }

        public string Message
        {
            get
            {
                if (this.IsAllSucceeded)
                {
                    return "Uploaded Successfully.";
                }
                else
                {
                    return string.Format("Error in uploading files, {0} out of {1} failed to upload.", this.FailedFiles.Count, (this.UploadedFiles.Count + this.FailedFiles.Count));
                }
            }
        }

        public FileUploadResult()
        {
            this.IsAllSucceeded = true;
            this.UploadedFiles = new List<string>();
            this.FailedFiles = new List<string>();
        }
    }
}
