﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using SIM.Application.Messages;

namespace SIM.Api.Middleware
{
    /// <summary>
    ///  OnExceptionMiddleware , Produce Custom message during Internal Server Exception
    /// </summary>
    public class OnExceptionMiddleware : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="env"></param>
        public OnExceptionMiddleware(IWebHostEnvironment env)
        {
            _env = env;
        }

        /// <inheritdoc />
        public void OnException(ExceptionContext context)
        {
            var error = new ApiMessage();

            if (_env.IsDevelopment())
            {
                error.Message = context.Exception.Message;
                error.Detail = context.Exception.StackTrace;
            }
            else
            {
                error.Message = "A server error occurred";
                error.Detail = context.Exception.Message;
            }

            context.Result = new ObjectResult(error)
            {
                StatusCode = 500
            };
        }
    }
}