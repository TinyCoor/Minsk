using System;
using System.Collections;
using System.Collections.Generic;
using Minsk.CodeAnalysis.Syntax;

namespace Minsk.CodeAnalysis
{
    internal sealed class DiagnosticBag :IEnumerable<Diagnostic>
    {
        private readonly List<Diagnostic> _diagnostics = new List<Diagnostic>();

        public IEnumerator<Diagnostic> GetEnumerator() => _diagnostics.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>GetEnumerator();

        private void Report(TextSpan span,string message)
        {
            var diagnostic = new Diagnostic(span, message);
            _diagnostics.Add(diagnostic);
        }

        public void ReportInvalidNumber(TextSpan textSpan, string text, Type type)
        {
            var message = $"The number {text} isn't valid Int32";
            Report(textSpan, message);
        }

        public void AddRange(DiagnosticBag diagnostics)
        {
            _diagnostics.AddRange(diagnostics._diagnostics);
        }

        public void ReportBadCharacter(int position, char current)
        {
            var message = $"ERROR: bad character input: '{current}'";
            Report(new TextSpan(position, 1), message);
        }

        public void ReportUnexpectedToken(TextSpan span, SyntaxKind actualKind, SyntaxKind expectedKind)
        {
           var message = $"ERROR: Unexpoected Token <{actualKind}>, expected <{expectedKind}>";
            Report(span, message);
        }

        public void ReportUndefinedUnaryOperator(TextSpan span, string operatorText, Type operandType)
        {
            var message = $"Unary operator syntax `{operatorText}`  is not defined for type {operandType}";
            Report(span, message);
        }

        internal void ReportUndefinedBinaryOperator(TextSpan span, string operatorTokenText, Type leftOperandType, Type rightOperandType)
        {
            var message = $"Binary operator syntax `{operatorTokenText}`  is not defined for type {leftOperandType} and {rightOperandType}";
            Report(span, message);
        }
    }
};