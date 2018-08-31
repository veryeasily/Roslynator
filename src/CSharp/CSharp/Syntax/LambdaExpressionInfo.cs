﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Roslynator.CSharp.Syntax.SyntaxInfoHelpers;

namespace Roslynator.CSharp.Syntax
{
    //TODO:  make public
    /// <summary>
    /// Provides information about a lambda expression.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    internal readonly struct LambdaExpressionInfo : IEquatable<LambdaExpressionInfo>
    {
        private LambdaExpressionInfo(
            LambdaExpressionSyntax lambdaExpression,
            ParameterSyntax parameter,
            ParameterListSyntax parameterList,
            CSharpSyntaxNode body)
        {
            LambdaExpression = lambdaExpression;
            Parameter = parameter;
            ParameterList = parameterList;
            Body = body;
        }

        /// <summary>
        /// The lambda expression.
        /// </summary>
        public LambdaExpressionSyntax LambdaExpression { get; }

        /// <summary>
        /// The parameter.
        /// </summary>
        public ParameterSyntax Parameter { get; }

        /// <summary>
        /// The first parameter.
        /// </summary>
        public ParameterSyntax FirstParameter
        {
            get { return Parameter ?? ParameterList?.Parameters.FirstOrDefault(); }
        }

        /// <summary>
        /// The body of the lambda expression.
        /// </summary>
        public CSharpSyntaxNode Body { get; }

        /// <summary>
        /// The parameter list.
        /// </summary>
        public ParameterListSyntax ParameterList { get; }

        /// <summary>
        /// True if this instance is a simple lambda expression.
        /// </summary>
        public bool IsSimpleLambda
        {
            get { return LambdaExpression?.Kind() == SyntaxKind.SimpleLambdaExpression; }
        }

        /// <summary>
        /// True if this instance is a parenthesized lambda expression.
        /// </summary>
        public bool IsParenthesizedLambda
        {
            get { return LambdaExpression?.Kind() == SyntaxKind.ParenthesizedLambdaExpression; }
        }

        /// <summary>
        /// Determines whether this struct was initialized with an actual syntax.
        /// </summary>
        public bool Success
        {
            get { return LambdaExpression != null; }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get { return ToDebugString(Success, this, LambdaExpression); }
        }

        internal static LambdaExpressionInfo Create(
            SyntaxNode node,
            bool walkDownParentheses = true,
            bool allowMissing = false)
        {
            return CreateImpl(Walk(node, walkDownParentheses) as LambdaExpressionSyntax, allowMissing);
        }

        internal static LambdaExpressionInfo Create(
            LambdaExpressionSyntax lambdaExpression,
            bool allowMissing = false)
        {
            return CreateImpl(lambdaExpression, allowMissing);
        }

        private static LambdaExpressionInfo CreateImpl(
            LambdaExpressionSyntax lambdaExpression,
            bool allowMissing = false)
        {
            switch (lambdaExpression?.Kind())
            {
                case SyntaxKind.SimpleLambdaExpression:
                    {
                        var simpleLambda = (SimpleLambdaExpressionSyntax)lambdaExpression;

                        ParameterSyntax parameter = simpleLambda.Parameter;

                        if (!Check(parameter, allowMissing))
                            break;

                        CSharpSyntaxNode body = simpleLambda.Body;

                        if (!Check(body, allowMissing))
                            break;

                        return new LambdaExpressionInfo(simpleLambda, parameter, null, body);
                    }
                case SyntaxKind.ParenthesizedLambdaExpression:
                    {
                        var parenthesizedLambda = (ParenthesizedLambdaExpressionSyntax)lambdaExpression;

                        ParameterListSyntax parameterList = parenthesizedLambda.ParameterList;

                        if (!Check(parameterList, allowMissing))
                            break;

                        CSharpSyntaxNode body = parenthesizedLambda.Body;

                        if (!Check(body, allowMissing))
                            break;

                        return new LambdaExpressionInfo(parenthesizedLambda, null, parameterList, body);
                    }
            }

            return default;
        }

        /// <summary>
        /// Returns the string representation of the underlying syntax, not including its leading and trailing trivia.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return LambdaExpression?.ToString() ?? "";
        }

        /// <summary>
        /// Determines whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance. </param>
        /// <returns>true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false. </returns>
        public override bool Equals(object obj)
        {
            return obj is LambdaExpressionInfo other && Equals(other);
        }

        /// <summary>
        /// Determines whether this instance is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(LambdaExpressionInfo other)
        {
            return EqualityComparer<LambdaExpressionSyntax>.Default.Equals(LambdaExpression, other.LambdaExpression);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return EqualityComparer<LambdaExpressionSyntax>.Default.GetHashCode(LambdaExpression);
        }

        public static bool operator ==(in LambdaExpressionInfo info1, in LambdaExpressionInfo info2)
        {
            return info1.Equals(info2);
        }

        public static bool operator !=(in LambdaExpressionInfo info1, in LambdaExpressionInfo info2)
        {
            return !(info1 == info2);
        }
    }
}