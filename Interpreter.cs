namespace Interpreter
{
    /// <summary>
    /// Contains procedures for implementing a simple calculator
    /// </summary>
    public static class Interpreter
    {
        /// <summary>
        /// Run a piece of code and return its value.
        /// </summary>
        /// <param name="code">The code to run</param>
        /// <param name="dict">The dictionary holding the values of the variables</param>
        /// <returns>The value returned by the executed code.</returns>
        public static float Run(string code, Dictionary dict)
        {
            return Parser.Parse(code).Run(dict);
        }
    }
}
