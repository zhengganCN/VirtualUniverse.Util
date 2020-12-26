using NUnit.Framework;
using System;
using System.Collections.Generic;
using VirtualUniverse.DirectoryOperation.Operations;

namespace Test.AmazedAlgorithm
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