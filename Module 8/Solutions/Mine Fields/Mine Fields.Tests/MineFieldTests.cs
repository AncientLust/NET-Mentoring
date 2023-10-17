namespace Mine_Fields.Tests
{
    public class MineFieldTests
    {
        [Fact]
        public void Should_Correctly_Produce_1x1_Field_With_Mine()
        {
            var mineField = new MineField();
            string[,] input =
            {
                { "*" }
            };
            string[,] expectedResult =
            {
                { "*" }
            };

            var result = mineField.GetMineMap(input);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Should_Correctly_Produce_1x1_Field_Without_Mine()
        {
            var mineField = new MineField();
            string[,] input =
            {
                { "." }
            };
            string[,] expectedResult =
            {
                { "0" }
            };

            var result = mineField.GetMineMap(input);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Should_Correctly_Produce_2x2_Field()
        {
            var mineField = new MineField();
            string[,] input =
            {
                { ".", "*" },
                { ".", "." },
            };
            string[,] expectedResult =
            {
                { "1", "*" },
                { "1", "1" },
            };

            var result = mineField.GetMineMap(input);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Should_Correctly_Produce_2x1_Field()
        {
            var mineField = new MineField();
            string[,] input =
            {
                { "."},
                { "."},
            };
            string[,] expectedResult =
            {
                { "0" },
                { "0" },
            };

            var result = mineField.GetMineMap(input);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Should_Correctly_Produce_3x3_Field()
        {
            var mineField = new MineField();
            string[,] input =
            {
                { ".", "*", "." },
                { ".", ".", "*" },
                { ".", ".", "." },
            };
            string[,] expectedResult =
            {
                { "1", "*", "2" },
                { "1", "2", "*" },
                { "0", "1", "1" },
            };

            var result = mineField.GetMineMap(input);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Should_Correctly_Produce_4x4_Field()
        {
            var mineField = new MineField();
            string[,] input =
            {
                { ".", "*", ".", "." },
                { ".", ".", "*", "." },
                { ".", ".", ".", "." },
                { "*", ".", ".", "." },
            };
            string[,] expectedResult =
            {
                { "1", "*", "2", "1" },
                { "1", "2", "*", "1" },
                { "1", "2", "1", "1" },
                { "*", "1", "0", "0" },
            };

            var result = mineField.GetMineMap(input);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Should_Correctly_Count_Corner_Values()
        {
            var mineField = new MineField();
            string[,] input =
            {
                { "*", ".", ".", "*" },
                { ".", ".", ".", "." },
                { ".", ".", ".", "."  },
                { "*", ".", ".", "*"  },
            };
            string[,] expectedResult =
            {
                { "*", "1", "1", "*" },
                { "1", "1", "1", "1" },
                { "1", "1", "1", "1" },
                { "*", "1", "1", "*" },
            };

            var result = mineField.GetMineMap(input);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Should_Correctly_Produce_1x4_Field()
        {
            var mineField = new MineField();
            string[,] input =
            {
                { "*", ".", ".", "*" }
            };
            string[,] expectedResult =
            {
                { "*", "1", "1", "*" }
            };

            var result = mineField.GetMineMap(input);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Should_Correctly_Count_Empty_Field()
        {
            var mineField = new MineField();
            string[,] input =
            {
                { ".", ".", ".", "." },
                { ".", ".", ".", "." },
                { ".", ".", ".", "."  },
                { ".", ".", ".", "."  },
            };
            string[,] expectedResult =
            {
                { "0", "0", "0", "0" },
                { "0", "0", "0", "0" },
                { "0", "0", "0", "0" },
                { "0", "0", "0", "0" },
            };

            var result = mineField.GetMineMap(input);

            Assert.Equal(expectedResult, result);
        }
    }
}