namespace AASeq.Plugins.Standard;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

/// <summary>
/// Calc plugin command.
/// </summary>
internal sealed class Calc : ICommandPlugin {

    /// <summary>
    /// Returns true, if command was successfully executed.
    /// </summary>
    /// <param name="parameters">Parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public static async Task<AASeqNodes> ExecuteAsync(ILogger logger, AASeqNodes parameters, CancellationToken cancellationToken) {
        await Task.CompletedTask;

        var expression = parameters["Value"].AsString("");
        string result;
        try {
            result = Evaluate(expression);
        } catch (Exception) {
            result = "";
        }

        return new AASeqNodes() {
            ["Value"] = result,
        };
    }


    public static Dictionary<char, int> OperatorPrecedence = new() { ['+'] = 1, ['-'] = 1, ['*'] = 2, ['/'] = 2, ['^'] = 3 };

    internal static string Evaluate(string input) {
        var tokens = new List<Token>();
        int i = 0;
        while (i < input.Length) {
            if (char.IsWhiteSpace(input[i])) { i++; continue; }

            if (char.IsDigit(input[i])) {
                int start = i;
                while (i < input.Length && (char.IsDigit(input[i]) || input[i] == '.')) { i++; }
                tokens.Add(new Token(double.Parse(input.Substring(start, i - start))));
                continue;
            }
            if ("+-*/^()".Contains(input[i])) {
                tokens.Add(new Token(input[i]));
                i++;
                continue;
            }
            throw new Exception($"Invalid character: {input[i]}");
        }

        var inputStack = new Stack<Token>();
        var outputQueue = new Queue<Token>();
        foreach (var token in tokens) {
            if (token.IsNumber) {
                outputQueue.Enqueue(token);
            } else if (token.IsOperator) {
                if (token.Operator == '(') {
                    inputStack.Push(token);
                } else if (token.Operator == ')') {
                    while (inputStack.Count > 0 && inputStack.Peek().Operator != '(')
                        outputQueue.Enqueue(inputStack.Pop());
                    if (inputStack.Count == 0 || inputStack.Peek().Operator != '(')
                        throw new Exception("Mismatched parentheses");
                    inputStack.Pop();
                } else {
                    while (inputStack.Count > 0 && inputStack.Peek().IsOperator &&
                           inputStack.Peek().Operator != '(' &&
                           OperatorPrecedence[inputStack.Peek().Operator!.Value] >= OperatorPrecedence[token.Operator!.Value]) {
                        outputQueue.Enqueue(inputStack.Pop());
                    }
                    inputStack.Push(token);
                }
            }
        }

        while (inputStack.Count > 0) {
            if (inputStack.Peek().Operator == '(' || inputStack.Peek().Operator == ')') {
                throw new Exception("Mismatched parentheses");
            }
            outputQueue.Enqueue(inputStack.Pop());
        }

        var evalStack = new Stack<double>();
        foreach (var token in outputQueue) {
            if (token.IsNumber) {
                evalStack.Push(token.Number!.Value);
            } else if (token.IsOperator) {
                double b = evalStack.Pop();
                double a = evalStack.Pop();
                switch (token.Operator) {
                    case '+': evalStack.Push(a + b); break;
                    case '-': evalStack.Push(a - b); break;
                    case '*': evalStack.Push(a * b); break;
                    case '/': evalStack.Push(a / b); break;
                    case '^': evalStack.Push(Math.Pow(a, b)); break;
                }
            }
        }
        return evalStack.Pop().ToString("0.###############", CultureInfo.InvariantCulture);
    }

}


file class Token {
    public Token(double number) {
        Number = number;
    }

    public Token(char op) {
        Operator = op;
    }

    public double? Number { get; }
    public char? Operator { get; }
    public bool IsNumber => Number.HasValue;
    public bool IsOperator => Operator.HasValue;
}
