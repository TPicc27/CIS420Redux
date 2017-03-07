using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS420Redux.Models
{
    public class Document
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int StudentNumber { get; set; }
        public byte[] FileBytes { get; set; }
        public int ContentLength { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public string UploadedBy
        {
            get; set;
        }
    }
}