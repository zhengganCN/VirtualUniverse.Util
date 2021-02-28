using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using VirtualUniverse.Extension.System;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/2/24 23:46:33；更新时间：
************************************************************************************/
namespace Test
{
    /// <summary>
    /// 类说明：
    /// </summary>
    class DirectoryExtensionTest
    {
        [Test]
        public void GetDirectoriesTest()
        {
            DirectoryExtension.GetDirectories(@"G:\Softwares\VirtualUniverse.Util", 0);
        }

        [Test]
        public void GetDirectoriesByRecursiveTest()
        {
           DirectoryExtension.GetDirectoriesByRecursive(@"G:\Softwares\VirtualUniverse.Util\VirtualUniverse.Extension\Test\bin\Debug");
        }
    }
}
