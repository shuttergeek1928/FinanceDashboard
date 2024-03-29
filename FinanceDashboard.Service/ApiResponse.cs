﻿using System.Net;

namespace FinanceDashboard.ServiceX
{
    public class ApiResponseX
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string>? Errors { get; set; }
        public object? Result { get; set; }
    }
}
