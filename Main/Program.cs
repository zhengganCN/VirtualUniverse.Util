using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using Util;
using Util.DBContext;
using Util.Math;
using Util.Math.MathException;
using Util.ModelResult;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new MyClass();
            s.dataNo = "123456789809987654321";
            s.tranTime = "20191112111111";
            s.statusInfo = "11";
            s.tranType = "0";
            s.chainStatus = "Y";
            _ = Newtonsoft.Json.JsonConvert.SerializeObject(s);
        }
        public static DataNoModel HandelDataNo(string dataNo)
        {
            var model = new DataNoModel();
            if (!string.IsNullOrEmpty(dataNo))
            {
                model.IdCard = dataNo.Substring(0, 18);
                model.Year = int.Parse(dataNo.Substring(18, 4));
                model.Month = int.Parse(dataNo.Substring(22, 2));
                model.SiteNo = dataNo.Substring(24);
            }
            return model;
        }
    }
    class TEST :BaseCollection
    { 
    }

    class DataNoModel
    {
        /// <summary>
        /// 身份证
        /// </summary>
        public string IdCard { get; set; }
        /// <summary>
        /// 工地编号
        /// </summary>
        public string SiteNo { get; set; }
        /// <summary>
        /// 年
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 月
        /// </summary>
        public int Month { get; set; }
    }
    class MyClass
    {
        public string tranType { get; set; }
        public string tranTime { get; set; }
        public string dataNo { get; set; }
        public string chainStatus { get; set; }
        public string statusInfo { get; set; }

    }
}
