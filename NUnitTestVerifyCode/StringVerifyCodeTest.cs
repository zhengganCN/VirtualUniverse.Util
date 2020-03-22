using AmazedMath.Math;
using AmazedVerifyCode;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using static AmazedVerifyCode.StringVerifyCode;

namespace NUnitTestVerifyCode.VerifyCodeTest
{
    class StringVerifyCodeTest
    {
        [SetUp]
        public void SetUp() { }
        [Test]
        public void StringVerifyCode()
        {
            RandomNumber random = new RandomNumber();
            var number = random.GenerateRandom(1000, 9999);
            var numberVerifyCode = new StringVerifyCode(100, 50);
            numberVerifyCode.SetBackgroundColor(Color.White);
            var bitmap = numberVerifyCode.Generate(number.ToString());
            var path = Path.Combine(Directory.GetCurrentDirectory(), "StringVerifyCode.jpeg");
            bitmap.Save(path, ImageFormat.Jpeg);
        }
        [Test]
        public void StringVerifyCodeMoreChar()
        {
            RandomNumber random = new RandomNumber();
            var number = random.GenerateRandom(1000000000, 9999999999);
            var numberVerifyCode = new StringVerifyCode(100, 50);
            numberVerifyCode.SetBackgroundColor(Color.White);
            var bitmap = numberVerifyCode.Generate(number.ToString());
            var path = Path.Combine(Directory.GetCurrentDirectory(), "StringVerifyCodeMoreChar.jpeg");
            bitmap.Save(path, ImageFormat.Jpeg);
        }
        [Test]
        public void StringVerifyCodeTextMoreHeight()
        {
            RandomNumber random = new RandomNumber();
            var number = random.GenerateRandom(1000, 9999);
            var numberVerifyCode = new StringVerifyCode(100, 50);
            numberVerifyCode.SetBackgroundColor(Color.White);
            numberVerifyCode.SetTextFont(new Font(FontFamily.GenericMonospace, 80,FontStyle.Bold));
            numberVerifyCode.SetTextPosition(EnumPosition.LeftCenter);
            var bitmap = numberVerifyCode.Generate(number.ToString());
            var path = Path.Combine(Directory.GetCurrentDirectory(), "StringVerifyCodeTextMoreHeight.jpeg");
            bitmap.Save(path, ImageFormat.Jpeg);
        }

        [Test]
        public void StringVerifyCodeInterferenceLine()
        {
            RandomNumber random = new RandomNumber();
            var number = random.GenerateRandom(1000, 9999);
            var numberVerifyCode = new StringVerifyCode(100, 50);
            numberVerifyCode.SetBackgroundColor(Color.White);
            numberVerifyCode.SetInterferenceLine(2, 2);
            var bitmap = numberVerifyCode.Generate(number.ToString());
            var path = Path.Combine(Directory.GetCurrentDirectory(), "StringVerifyCodeInterferenceLine.jpeg");
            bitmap.Save(path, ImageFormat.Jpeg);
        }
        #region 设置文本位置(文本未超过图片宽度)
        [Test]
        public void StringVerifyCodeTextPositionCenter()
        {
            RandomNumber random = new RandomNumber();
            var number = random.GenerateRandom(1000, 9999);
            var numberVerifyCode = new StringVerifyCode(100, 50);
            numberVerifyCode.SetBackgroundColor(Color.White);
            numberVerifyCode.SetTextPosition(EnumPosition.Center);
            var bitmap = numberVerifyCode.Generate(number.ToString());
            var path = Path.Combine(Directory.GetCurrentDirectory(), "StringVerifyCodeTextPositionCenter.jpeg");
            bitmap.Save(path, ImageFormat.Jpeg);
        }
        [Test]
        public void StringVerifyCodeTextPositionBottomCenter()
        {
            RandomNumber random = new RandomNumber();
            var number = random.GenerateRandom(1000, 9999);
            var numberVerifyCode = new StringVerifyCode(100, 50);
            numberVerifyCode.SetBackgroundColor(Color.White);
            numberVerifyCode.SetTextPosition(EnumPosition.BottomCenter);
            var bitmap = numberVerifyCode.Generate(number.ToString());
            var path = Path.Combine(Directory.GetCurrentDirectory(), "StringVerifyCodeTextPositionBottomCenter.jpeg");
            bitmap.Save(path, ImageFormat.Jpeg);
        }
        [Test]
        public void StringVerifyCodeTextPositionLeftBottom()
        {
            RandomNumber random = new RandomNumber();
            var number = random.GenerateRandom(1000, 9999);
            var numberVerifyCode = new StringVerifyCode(100, 50);
            numberVerifyCode.SetBackgroundColor(Color.White);
            numberVerifyCode.SetTextPosition(EnumPosition.LeftBottom);
            var bitmap = numberVerifyCode.Generate(number.ToString());
            var path = Path.Combine(Directory.GetCurrentDirectory(), "StringVerifyCodeTextPositionLeftBottom.jpeg");
            bitmap.Save(path, ImageFormat.Jpeg);
        }
        [Test]
        public void StringVerifyCodeTextPositionLeftCenter()
        {
            RandomNumber random = new RandomNumber();
            var number = random.GenerateRandom(1000, 9999);
            var numberVerifyCode = new StringVerifyCode(100, 50);
            numberVerifyCode.SetBackgroundColor(Color.White);
            numberVerifyCode.SetTextPosition(EnumPosition.LeftCenter);
            var bitmap = numberVerifyCode.Generate(number.ToString());
            var path = Path.Combine(Directory.GetCurrentDirectory(), "StringVerifyCodeTextPositionLeftCenter.jpeg");
            bitmap.Save(path, ImageFormat.Jpeg);
        }
        [Test]
        public void StringVerifyCodeTextPositionLeftTop()
        {
            RandomNumber random = new RandomNumber();
            var number = random.GenerateRandom(1000, 9999);
            var numberVerifyCode = new StringVerifyCode(100, 50);
            numberVerifyCode.SetBackgroundColor(Color.White);
            numberVerifyCode.SetTextPosition(EnumPosition.LeftTop);
            var bitmap = numberVerifyCode.Generate(number.ToString());
            var path = Path.Combine(Directory.GetCurrentDirectory(), "StringVerifyCodeTextPositionLeftTop.jpeg");
            bitmap.Save(path, ImageFormat.Jpeg);
        }
        [Test]
        public void StringVerifyCodeTextPositionRightBottom()
        {
            RandomNumber random = new RandomNumber();
            var number = random.GenerateRandom(1000, 9999);
            var numberVerifyCode = new StringVerifyCode(100, 50);
            numberVerifyCode.SetBackgroundColor(Color.White);
            numberVerifyCode.SetTextPosition(EnumPosition.RightBottom);
            var bitmap = numberVerifyCode.Generate(number.ToString());
            var path = Path.Combine(Directory.GetCurrentDirectory(), "StringVerifyCodeTextPositionRightBottom.jpeg");
            bitmap.Save(path, ImageFormat.Jpeg);
        }
        [Test]
        public void StringVerifyCodeTextPositionRightCenter()
        {
            RandomNumber random = new RandomNumber();
            var number = random.GenerateRandom(1000, 9999);
            var numberVerifyCode = new StringVerifyCode(100, 50);
            numberVerifyCode.SetBackgroundColor(Color.White);
            numberVerifyCode.SetTextPosition(EnumPosition.RightCenter);
            var bitmap = numberVerifyCode.Generate(number.ToString());
            var path = Path.Combine(Directory.GetCurrentDirectory(), "StringVerifyCodeTextPositionRightCenter.jpeg");
            bitmap.Save(path, ImageFormat.Jpeg);
        }
        [Test]
        public void StringVerifyCodeTextPositionRightTop()
        {
            RandomNumber random = new RandomNumber();
            var number = random.GenerateRandom(1000, 9999);
            var numberVerifyCode = new StringVerifyCode(100, 50);
            numberVerifyCode.SetBackgroundColor(Color.White);
            numberVerifyCode.SetTextPosition(EnumPosition.RightTop);
            var bitmap = numberVerifyCode.Generate(number.ToString());
            var path = Path.Combine(Directory.GetCurrentDirectory(), "StringVerifyCodeTextPositionRightTop.jpeg");
            bitmap.Save(path, ImageFormat.Jpeg);
        }
        [Test]
        public void StringVerifyCodeTextPositionTopCenter()
        {
            RandomNumber random = new RandomNumber();
            var number = random.GenerateRandom(1000, 9999);
            var numberVerifyCode = new StringVerifyCode(100, 50);
            numberVerifyCode.SetBackgroundColor(Color.White);
            numberVerifyCode.SetTextPosition(EnumPosition.TopCenter);
            var bitmap = numberVerifyCode.Generate(number.ToString());
            var path = Path.Combine(Directory.GetCurrentDirectory(), "StringVerifyCodeTextPositionTopCenter.jpeg");
            bitmap.Save(path, ImageFormat.Jpeg);
        }
        #endregion
        #region 设置文本位置(文本超过图片宽度)
        [Test]
        public void StringVerifyCodeManyCharTextPositionCenter()
        {
            RandomNumber random = new RandomNumber();
            var number = random.GenerateRandom(1000000000, 9999999999);
            var numberVerifyCode = new StringVerifyCode(100, 50);
            numberVerifyCode.SetBackgroundColor(Color.White);
            numberVerifyCode.SetTextPosition(EnumPosition.Center);
            var bitmap = numberVerifyCode.Generate(number.ToString());
            var path = Path.Combine(Directory.GetCurrentDirectory(), "StringVerifyCodeManyCharTextPositionCenter.jpeg");
            bitmap.Save(path, ImageFormat.Jpeg);
        }
        [Test]
        public void StringVerifyCodeManyCharTextPositionBottomCenter()
        {
            RandomNumber random = new RandomNumber();
            var number = random.GenerateRandom(1000000000, 9999999999);
            var numberVerifyCode = new StringVerifyCode(100, 50);
            numberVerifyCode.SetBackgroundColor(Color.White);
            numberVerifyCode.SetTextPosition(EnumPosition.BottomCenter);
            var bitmap = numberVerifyCode.Generate(number.ToString());
            var path = Path.Combine(Directory.GetCurrentDirectory(), "StringVerifyCodeManyCharTextPositionBottomCenter.jpeg");
            bitmap.Save(path, ImageFormat.Jpeg);
        }
        [Test]
        public void StringVerifyCodeManyCharTextPositionLeftBottom()
        {
            RandomNumber random = new RandomNumber();
            var number = random.GenerateRandom(1000000000, 9999999999);
            var numberVerifyCode = new StringVerifyCode(100, 50);
            numberVerifyCode.SetBackgroundColor(Color.White);
            numberVerifyCode.SetTextPosition(EnumPosition.LeftBottom);
            var bitmap = numberVerifyCode.Generate(number.ToString());
            var path = Path.Combine(Directory.GetCurrentDirectory(), "StringVerifyCodeManyCharTextPositionLeftBottom.jpeg");
            bitmap.Save(path, ImageFormat.Jpeg);
        }
        [Test]
        public void StringVerifyCodeManyCharTextPositionLeftCenter()
        {
            RandomNumber random = new RandomNumber();
            var number = random.GenerateRandom(1000000000, 9999999999);
            var numberVerifyCode = new StringVerifyCode(100, 50);
            numberVerifyCode.SetBackgroundColor(Color.White);
            numberVerifyCode.SetTextPosition(EnumPosition.LeftCenter);
            var bitmap = numberVerifyCode.Generate(number.ToString());
            var path = Path.Combine(Directory.GetCurrentDirectory(), "StringVerifyCodeManyCharTextPositionLeftCenter.jpeg");
            bitmap.Save(path, ImageFormat.Jpeg);
        }
        [Test]
        public void StringVerifyCodeManyCharTextPositionLeftTop()
        {
            RandomNumber random = new RandomNumber();
            var number = random.GenerateRandom(1000000000, 9999999999);
            var numberVerifyCode = new StringVerifyCode(100, 50);
            numberVerifyCode.SetBackgroundColor(Color.White);
            numberVerifyCode.SetTextPosition(EnumPosition.LeftTop);
            var bitmap = numberVerifyCode.Generate(number.ToString());
            var path = Path.Combine(Directory.GetCurrentDirectory(), "StringVerifyCodeManyCharTextPositionLeftTop.jpeg");
            bitmap.Save(path, ImageFormat.Jpeg);
        }
        [Test]
        public void StringVerifyCodeManyCharTextPositionRightBottom()
        {
            RandomNumber random = new RandomNumber();
            var number = random.GenerateRandom(1000000000, 9999999999);
            var numberVerifyCode = new StringVerifyCode(100, 50);
            numberVerifyCode.SetBackgroundColor(Color.White);
            numberVerifyCode.SetTextPosition(EnumPosition.RightBottom);
            var bitmap = numberVerifyCode.Generate(number.ToString());
            var path = Path.Combine(Directory.GetCurrentDirectory(), "StringVerifyCodeManyCharTextPositionRightBottom.jpeg");
            bitmap.Save(path, ImageFormat.Jpeg);
        }
        [Test]
        public void StringVerifyCodeManyCharTextPositionRightCenter()
        {
            RandomNumber random = new RandomNumber();
            var number = random.GenerateRandom(1000000000, 9999999999);
            var numberVerifyCode = new StringVerifyCode(100, 50);
            numberVerifyCode.SetBackgroundColor(Color.White);
            numberVerifyCode.SetTextPosition(EnumPosition.RightCenter);
            var bitmap = numberVerifyCode.Generate(number.ToString());
            var path = Path.Combine(Directory.GetCurrentDirectory(), "StringVerifyCodeManyCharTextPositionRightCenter.jpeg");
            bitmap.Save(path, ImageFormat.Jpeg);
        }
        [Test]
        public void StringVerifyCodeManyCharTextPositionRightTop()
        {
            RandomNumber random = new RandomNumber();
            var number = random.GenerateRandom(1000000000, 9999999999);
            var numberVerifyCode = new StringVerifyCode(100, 50);
            numberVerifyCode.SetBackgroundColor(Color.White);
            numberVerifyCode.SetTextPosition(EnumPosition.RightTop);
            var bitmap = numberVerifyCode.Generate(number.ToString());
            var path = Path.Combine(Directory.GetCurrentDirectory(), "StringVerifyCodeManyCharTextPositionRightTop.jpeg");
            bitmap.Save(path, ImageFormat.Jpeg);
        }
        [Test]
        public void StringVerifyCodeManyCharTextPositionTopCenter()
        {
            RandomNumber random = new RandomNumber();
            var number = random.GenerateRandom(1000000000, 9999999999);
            var numberVerifyCode = new StringVerifyCode(100, 50);
            numberVerifyCode.SetBackgroundColor(Color.White);
            numberVerifyCode.SetTextPosition(EnumPosition.TopCenter);
            var bitmap = numberVerifyCode.Generate(number.ToString());
            var path = Path.Combine(Directory.GetCurrentDirectory(), "StringVerifyCodeManyCharTextPositionTopCenter.jpeg");
            bitmap.Save(path, ImageFormat.Jpeg);
        }
        #endregion
        [Test]
        public void StringVerifyCodeInterferencePointWithSingleColorTest()
        {
            RandomNumber random = new RandomNumber();
            var number = random.GenerateRandom(1000, 9999);
            var numberVerifyCode = new StringVerifyCode(100, 50);
            numberVerifyCode.SetBackgroundColor(Color.White);
            numberVerifyCode.SetInterferencePoint();
            var bitmap = numberVerifyCode.Generate(number.ToString());
            var path = Path.Combine(Directory.GetCurrentDirectory(), "StringVerifyCodeInterferencePointWithSingleColorTest.jpeg");
            bitmap.Save(path, ImageFormat.Jpeg);
        }
        [Test]
        public void StringVerifyCodeInterferencePointWithMultiColorTest()
        {
            RandomNumber random = new RandomNumber();
            var number = random.GenerateRandom(1000, 9999);
            var numberVerifyCode = new StringVerifyCode(100, 50);
            numberVerifyCode.SetBackgroundColor(Color.White);
            Color[] colors = new Color[]
            {
               Color.Black,
               Color.Green,
               Color.Blue,
               Color.Red,
               Color.Yellow
            };
            numberVerifyCode.SetInterferencePoint(20, 5, null, colors);
            var bitmap = numberVerifyCode.Generate(number.ToString());
            var path = Path.Combine(Directory.GetCurrentDirectory(), "StringVerifyCodeInterferencePointWithMultiColorTest.jpeg");
            bitmap.Save(path, ImageFormat.Jpeg);
        }
        [Test]
        public void StringVerifyCodeInterferencePointWithMultiColorExceptionTest()
        {
            Assert.Throws<ArgumentException>(StringVerifyCodeInterferencePointWithMultiColorException);
        }
        private void StringVerifyCodeInterferencePointWithMultiColorException()
        {
            RandomNumber random = new RandomNumber();
            var number = random.GenerateRandom(1000, 9999);
            var numberVerifyCode = new StringVerifyCode(100, 50);
            numberVerifyCode.SetBackgroundColor(Color.White);
            Color[] colors = Array.Empty<Color>();
            numberVerifyCode.SetInterferencePoint(20, 5, null, colors);
            var bitmap = numberVerifyCode.Generate(number.ToString());
            var path = Path.Combine(Directory.GetCurrentDirectory(), "StringVerifyCodeInterferencePointWithMultiColorTest.jpeg");
            bitmap.Save(path, ImageFormat.Jpeg);
        }
}
}
