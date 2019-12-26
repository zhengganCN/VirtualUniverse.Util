using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Util.Data.Repository.MongoDBRepository;

namespace UtilTest.DataTest.MongoDBTest
{
    class ExtensionTest
    {
        private Repository<Student> repository;
        [SetUp]
        public void SetUp()
        {
            repository = new Repository<Student>(new MongoDBContext());
        }

        
    }
}
