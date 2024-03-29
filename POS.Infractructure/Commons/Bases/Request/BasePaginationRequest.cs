﻿namespace POS.Infractructure.Commons.Bases.Request
{
    public class BasePaginationRequest
    {
        public int NumPage { get; set; } = 1;
        public int NumRecordsPages { get; set; } = 10;

        private readonly int NumMaxRecordsPage = 50;

        public string Order { get; set; } = "asc";
        public string? Sort { get; set; } = null;

        public int Record
        {
            get => NumRecordsPages;
            set
            {
                NumRecordsPages = value > NumMaxRecordsPage ? NumMaxRecordsPage : value;
            }
        }
    }
}
