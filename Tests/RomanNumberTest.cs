using App;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Reflection;

namespace Tests
{
    [TestClass]
    public class RomanNumberTest
    {
        [TestMethod]
        public void ParseTest() {            
            TestCase[] validCases = [
                new("I",      1    ),
                new("II",     2    ),
                new("III",    3    ),
                new("IV",     4    ),
                new("IIII",   4    ),  
                new("IX",     9    ),
                new("XX",     20   ),
                new("X",      10   ),
                new("N",      0    ),
                new("XV",     15   ),
                new("XIX",    19   ),
                new("XXVI",   26   ),
                new("CIX",    109  ),
                new("CD",     400  ),
                new("MMXXIV", 2024 ),
                new("MMMM",   4000 ),

                new(" XX",    20   ),
                new("CC ",    200  ),
                new("  XX",   20   ),
                new("  CC  ", 200  ),
                new("\tIV",   4    ),
                new("CD\n",   400  ),
                new("XI\t\t", 11   ),
            ];
            foreach (TestCase validCase in validCases)  {
                Assert.AreEqual(
                    validCase.Value, 
                    RomanNumber.Parse(validCase.Source.ToString()!).Value, 
                    $"Valid Parser Test: '{validCase.Source}' => {validCase.Value}"
                );
            }

            /*Dictionary<String, int> testCases = new() {
                { "I", 1 },
                { "II", 2 },
                { "III", 3 },
                { "N", 0},
                { "V", 5},
                { "X", 10},
                { "L", 50},
                { "C", 100},
                { "M", 1000 },
            };
            foreach (var testCase in testCases) {
                Assert.AreEqual(testCase.Value, RomanNumber.Parse(testCase.Key).Value, $"Roman value {testCase.Value} -> {testCase.Key}");
            }*/

            var Assert_ThrowsException_Method =
                typeof(Assert)
                .GetMethods()
                .Where(m =>
                    m.Name == "ThrowsException" &&
                    m.IsGenericMethod &&
                    m.GetParameters().Length == 2 &&
                    m.GetParameters().First().ParameterType.Name == "Func`1")
                .FirstOrDefault();

            Assert.IsNotNull(
                Assert_ThrowsException_Method,
                "Reflection: 'ThrowsException' not found"
            );

            /*String[][] testCases7 = [
                ["IIX",  "X", "2"],
                ["IVIX", "X", "3"],
            ];
            foreach (var testCase in testCases7) {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNumber.Parse(testCase[0]),
                    $"RomanNumber.Parse('{testCase[0]}') must throw FormatException"
                );
                Assert.IsTrue(
                    ex.Message.Contains($"'{testCase[1]}'") &&
                    ex.Message.Contains($"position {testCase[2]}"),
                    $"ex.Message must contain error char '{testCase[1]}' and its position {testCase[2]}: " +
                    $"testCase='{testCase[0]}',ex.Message='{ex.Message}'"
                );
                Assert.IsTrue(
                    ex.Message.Contains("RomanNumber.Parse"),
                    $"ex.Message must contain origin (class and method): " +
                    $"testCase='{testCase[0]}',ex.Message='{ex.Message}'"
                );
                Assert.IsTrue(
                    ex.Message.Contains($"'{testCase[0]}'"),
                    $"ex.Message must contain input value '{testCase[0]}': " +
                    $"testCase='{testCase[0]}',ex.Message='{ex.Message}'"
                );
                Assert.IsTrue(
                    ex.Message.Contains($"more than one less digit before"),
                    $"ex.Message must contain input value 'more than one less digit before': " +
                    $"testCase='{testCase[0]}',ex.Message='{ex.Message}'"
                );
            }*/



            String[][] testCases8 = [
                [ "IXIX",  "I", "0" ],  
                [ "CXCXC", "X", "1" ],   
                [ "IXX",   "I", "0" ],  
                [ "IXXX",  "I", "0" ],
            ];
            foreach (var testCase in testCases8) {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNumber.Parse(testCase[0]),
                    $"RomanNumber.Parse('{testCase[0]}') must throw FormatException"
                );
                Assert.IsTrue(
                    ex.Message.Contains($"'{testCase[1]}'") &&
                    ex.Message.Contains($"position {testCase[2]}"),
                    $"ex.Message must contain error char '{testCase[1]}' and its position {testCase[2]}: " +
                    $"testCase='{testCase[0]}',ex.Message='{ex.Message}'"
                );
                Assert.IsTrue(
                    ex.Message.Contains("RomanNumber.Parse"),
                    $"ex.Message must contain origin (class and method): " +
                    $"testCase='{testCase[0]}',ex.Message='{ex.Message}'"
                );
                Assert.IsTrue(
                    ex.Message.Contains($"'{testCase[0]}'"),
                    $"ex.Message must contain input value '{testCase[0]}': " +
                    $"testCase='{testCase[0]}',ex.Message='{ex.Message}'"
                );
            }
        }

        [TestMethod]
        public void ToStringTest() {
            RomanNumber rn = new(1);
            Assert.IsNotNull ( rn.ToString() );
            Dictionary<int, string> testCases = new() 
            {
                {1, "I" },
                {2, "II" },
                {3, "III" },
                {4, "IV" },
                {5, "V" },
                {6, "VI" },
                {7, "VII" },
                {8, "VIII" },
                {9, "IX" },
                {10, "X" },
                {11, "XI" },
                {12, "XII" },
            };
            foreach(var pair in testCases) {
                Assert.AreEqual(
                    pair.Value, 
                    (rn with { Value = pair.Key }).ToString(), 
                    $"RomanNumber.ToString {pair.Key} --> {pair.Value}"
                );
            }
        }

        [TestMethod]
        public void CrossTest_Parse_ToString() { 
            for(int i = 0; i < 40; i++) {
                Assert.AreEqual(
                    i,
                    RomanNumber.Parse(new RomanNumber(i).ToString()).Value,
                    $"Cross test for {i}"
                );
            }
        }

        /*[TestMethod]
        public void DigitalValueTest() {
            Dictionary<char, int> testCases = new() {
                { 'N', 0},
                { 'I', 1},
                { 'V', 5},
                { 'X', 10},
                { 'L', 50},
                { 'C', 100},
                { 'D', 500},
                { 'M', 1000},
            };
            foreach (var testCase in testCases)
                foreach (var _testCase in testCases) {
                    Assert.AreEqual(testCase.Value, RomanNumber.DigitalValue(testCase.Key), $"DigitalValue('{testCase.Value}')->{testCase.Value}");
                    Assert.AreEqual(_testCase.Value, RomanNumber.DigitalValue(_testCase.Key), $"DigitalValue('{_testCase.Value}')->{_testCase.Value}");
                }
            var ex_source = new List<string>();
            char[] ExcludedSymbols = { 'I', 'N', 'V', 'X', 'L', 'C', 'D', 'M' };
            char[] AllowedSymbols = Enumerable.Range(0, 255)
                                   .Select(c => (char)c)
                                   .Where(c => !ExcludedSymbols.Contains(c))
                                   .ToArray();
            var rand = new Random();
            var ex_cases = new List<char>();
            for (int i = 0; i < 100; i++) {
                ex_cases.Add(AllowedSymbols[rand.Next(AllowedSymbols.Length)]);
            }
            foreach (var testCase in ex_cases) {
                var ex = Assert.ThrowsException<ArgumentException>(() => RomanNumber.DigitalValue(testCase), $"DigitalValue('{testCase}') must throw ArgumentException");
                Assert.IsFalse(String.IsNullOrEmpty(ex.Message), "ex.Message must not be empty");
                Assert.IsTrue(ex.Message.Contains($"invalid value '{testCase}'") && ex.Message.Contains("argument 'digit'"), $"ex.Message must contain param name ('digit') and its value '{testCase}'. ex.Messaage: '{ex.Message}'");
                Assert.IsTrue(ex.Message.Contains("'RomanNumber.DigitalValue'"), $"ex.Message must contain origin (class and method): '{ex.Message}'");
                Assert.IsTrue(ex.Source?.Contains("RomanNumber.DigitalValue"), $"ex.Source must contain origin (class and method): '{ex.Source}'");
            }
            Assert.AreEqual(1, RomanNumber.DigitalValue('I'), "DigitalValue('I')-> 1");
        }*/
    }

    public class TestCase
    {
        public Object Source { get; set; }
        public Int32? Value { get; set; }
        public String? ExceptionMessage { get; set; }
        public Type? ExceptionType { get; set; }

        public TestCase()
        {
            Source = null!;
        }

        public TestCase(Object Source, Int32? Value)
        {
            this.Source = Source;
            this.Value = Value;
        }
        public TestCase(Object Source, String? ExceptionMessage, Type? ExceptionType = null)
        {
            this.Source = Source;
            this.ExceptionMessage = ExceptionMessage;
            this.ExceptionType = ExceptionType;
        }
    }

    public static class StringExtension
    {
        public static String F(this String str, params Object[] pars)
        {
            return String.Format(str, pars);
        }
    }

    public static class AssertExtension
    {
        private static MethodInfo Assert_ThrowsException_Method = 
            typeof(Assert)
                .GetMethods()
                .Where(m =>
                    m.Name == "ThrowsException" &&
                    m.IsGenericMethod &&
                    m.GetParameters().Length == 2 &&
                    m.GetParameters().First().ParameterType.Name == "Func`1")
                .FirstOrDefault()!;

        public static void CheckTestCase(Func<Object?> action, TestCase testCase)
        {
            Type exType = testCase.ExceptionType ?? typeof(Exception);

            var Assert_ThrowsException_Generic =
                Assert_ThrowsException_Method
                .MakeGenericMethod(exType);

            dynamic? ex = Assert_ThrowsException_Generic.Invoke(null, [
                action,
                $"action('{testCase.Source}') must throw {exType.Name}"
            ]);

            Assert.AreEqual(
                testCase.ExceptionMessage,
                ex!.Message,
                $"ex.Message must contain part '{testCase.ExceptionMessage}': " +
                $"testCase='{testCase.Source}',ex.Message='{ex.Message}'"
            );
        }
    }

}

