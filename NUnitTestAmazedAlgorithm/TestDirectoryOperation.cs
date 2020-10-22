using AmazedAlgorithm.AmazedDirectory;
using AmazedAlgorithm.UniqueGenerate;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace NUnitTestAmazedAlgorithm
{
    public class TestDirectoryOperation
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ReadDirectoriesTest()
        {
            DirectoryOperation.ReadDirectories(@"G:\Softwares\AmazedUtil");
        }
    }
}