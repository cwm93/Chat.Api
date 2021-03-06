﻿using Chat.Model.Enum;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Newtonsoft.Json;
using Chat.Model.Utils;
using Infrastructure.Utility;

namespace Chat.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 处理请求数据流
        /// </summary>
        /// <returns></returns>
        protected string GetInputString()
        {
            Stream req = Request.Body;
            string json = new StreamReader(req).ReadToEnd();

            if (!string.IsNullOrEmpty(json))
            {
                while (json.IndexOf("\\/", StringComparison.Ordinal) != -1) json = json.Replace("\\/", "/");
            }

            return json;
        }

        /// <summary>
        /// 获取错误的返回
        /// </summary>
        protected JsonResult ErrorJsonResult(ErrCodeEnum code)
        {
            var errResponse = new ResponseContext<object>(null)
            {
                Head = new ResponseHead(-1, code, code.ToDescription())
            };
            return new JsonResult(errResponse);
        }

       
    }
}